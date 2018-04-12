using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    LevelManager levelManager;
    Animator anim;
    SpriteRenderer rend;

    public AudioSource explode;
    public AudioSource jump;

    [HideInInspector]
    public bool facingRight = true;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float movementSpeed = 6;

    public static float maxDeathTime = 1;
    [HideInInspector]
    public static float deathTimer;

    public Vector2 wallJumpUp;
    public Vector2 wallJumpOff;
    public Vector2 wallJumpLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeUnstickDelay;

    public static float gravity;
    float maxJumpSpeed;
    float minJumpSpeed;
    Vector3 velocity;
    float velocitySmoothingX;

    bool isLocked = false;

    bool isDying = false;

    Controller2D controller;

    public void Respawned()
    {
        velocity.y = 0;
        velocity.x = 0;
        SetDying(false);
    }
    public void Hide(bool toHide)
    {
        rend.enabled = !toHide;
    }
    public void Lock()
    {
        Lock(!isLocked);
    }
    public void Lock(bool toLock)
    {
        isLocked = toLock;
    }
    public void SetDying(bool _isDying)
    {
        isDying = _isDying;
    }


    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Start()
    {
        controller = GetComponent<Controller2D>();
        levelManager = FindObjectOfType<LevelManager>();
        rend = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpSpeed = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpSpeed);

        deathTimer = maxDeathTime;
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (!isLocked)
            inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = inputVector.x * movementSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothingX, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if (!isLocked)
        {
            if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0 && !(inputVector.y < 0))
            {
                wallSliding = true;
                if (anim.GetBool("WallSlidingCon") == false)
                    anim.SetTrigger("WallSliding");

                anim.SetBool("WallSlidingCon", true);


                if (velocity.y < -wallSlideSpeedMax)
                    velocity.y = -wallSlideSpeedMax;

                if (timeUnstickDelay > 0)
                {
                    velocitySmoothingX = 0;
                    velocity.x = 0;

                    if (inputVector.x != wallDirX && inputVector.x != 0)
                        timeUnstickDelay -= Time.deltaTime;
                    else
                        timeUnstickDelay = wallStickTime;
                }
                else {
                    timeUnstickDelay = wallStickTime;
                }

            }
            else
                anim.SetBool("WallSlidingCon", false);

            if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (wallSliding)
                {
                    jump.Play();
                    if (wallDirX == inputVector.x)
                    {
                        velocity.x = -wallDirX * wallJumpUp.x;
                        velocity.y = wallJumpUp.y;
                    }
                    else if (inputVector.x == 0)
                    {
                        velocity.x = -wallDirX * wallJumpOff.x;
                        velocity.y = wallJumpOff.y;
                    }
                    else {
                        velocity.x = -wallDirX * wallJumpLeap.x;
                        velocity.y = wallJumpLeap.y;
                    }
                }
                if (controller.collisions.below)
                {
                    jump.Play();
                    velocity.y = maxJumpSpeed;
                }
            }
        }
        
        if (!isLocked)
            if ((Input.GetKeyUp(KeyCode.Space)||Input.GetKeyUp(KeyCode.UpArrow)) && (velocity.y > minJumpSpeed))
            {
                velocity.y = minJumpSpeed;
                //if (!wallSliding && !controller.collisions.below)
                //    jump.Play();
            }

        if (rend.enabled)
        { 
            if (Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.Keypad0))
            {
                SetDying(true);
                Lock(true);
            }
            else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Keypad0))
            {
                SetDying(false);
                Lock(false);
            }


            if (isDying)
            {
                deathTimer -= Time.deltaTime;
            }
            else if (deathTimer != maxDeathTime)
            {
                Lock(false);
                if (deathTimer > maxDeathTime)
                    deathTimer = maxDeathTime;
                else
                    deathTimer += Time.deltaTime / 3;
            }
        }

        if (deathTimer <= 0)
        {
            levelManager.RespawnPlayer();
            explode.Play();
            deathTimer = maxDeathTime;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, inputVector);

        anim.SetBool("Grounded", controller.IsGrounded());

        //Debug.Log(wallSliding);
        anim.SetFloat("vSpeed", velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(velocity.x));

        if (velocity.x > 0 && !facingRight && inputVector.x > 0)
            Flip();
        else if (velocity.x < 0 && facingRight && inputVector.x < 0)
            Flip();

        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }
}
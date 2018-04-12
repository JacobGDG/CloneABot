using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public DoorController door;
    LevelManager levelManager;
    AudioSource audS;

    Animator anim;

    bool deadPlayerDetected = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        audS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "DeadPlayer")
        {
            if (other.tag == "Player")
                levelManager.ActionCamEnable(door.transform.position);

            door.activate = true;

            if (!anim.GetBool("Active"))
            {
                if (other.tag == "Player")
                    audS.Play();
                anim.SetTrigger("Activated");
                anim.SetBool("Active", true);
            }

            if (other.tag == "DeadPlayer")
            {
                deadPlayerDetected = true;
                Debug.Log("deadplayer detected: " + deadPlayerDetected);
            } 
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !levelManager.checkActionCam())
            levelManager.ActionCamEnable(door.transform.position);
    }

    IEnumerator turnOffActionCam()
    {
        yield return new WaitForSeconds(0.75f);
        levelManager.ActionCamDisEnable();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !deadPlayerDetected)
        {
            anim.SetBool("Active", false);
            door.activate = false;
        }
        if (other.tag == "Player")
            StartCoroutine(turnOffActionCam());
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //public Controller2D playerController;
    //[HideInInspector]
    public GameObject activeCheckpoint;
    Camera actionCam;

    public GameObject deadPlayerObject;
    Player player;

    public int maxBodyCount = 5;
    List<GameObject> deadPlayerList;

    UIManager UICanvas;

    private float time = 0;

    

    
    int score = 100;

    // Use this for initialization
    void Start () {
        UICanvas = GameObject.Find("Canvas").GetComponent<UIManager>();
        UICanvas.TogglePauseScreen(false);

        player = FindObjectOfType<Player>();

        deadPlayerList = new List<GameObject>();
        for (int x = 0; x < maxBodyCount; x++)
            deadPlayerList.Add(new GameObject());

        actionCam = GameObject.Find("ActionCamera").GetComponent<Camera>();
        actionCam.enabled = false;
    }

    public int GetScore()
    {
        return score;
    }

    void PushOntoArray(ref GameObject newBody)
    {
        Destroy(deadPlayerList[maxBodyCount-1]);
        deadPlayerList.Insert(0, newBody);
    }

    void TogglePauseScreen()
    {
        player.Lock();
        UICanvas.TogglePauseScreen();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseScreen();
        }

        time += Time.deltaTime;



    }

    public float GetTime()
    {
        return time;
    }

    public void ActionCamEnable(Vector3 position)
    {
        actionCam.transform.position = new Vector3(position.x, position.y, -4);
        actionCam.enabled = true;
    }
    public void ActionCamDisEnable()
    {
        actionCam.enabled = false;
    }
    public bool checkActionCam()
    {
        return actionCam.enabled;
    }

    IEnumerator RespawnWaitUnhide(float waitFor)
    {
        yield return new WaitForSeconds(waitFor);
        player.transform.position = activeCheckpoint.transform.position;
        activeCheckpoint.GetComponent<CheckpointManager>().SpawningPlayer();

        yield return new WaitForSeconds(0.9f);
        player.Hide(false);
        player.Lock(false);
    }

    public void RespawnPlayer()
    {
        RespawnPlayer(false);
    }

    public void moneyFound()
    {
        score += 100;
    }
     public void UpgradeFound()
    {
        maxBodyCount++;
        deadPlayerList.Add(new GameObject());
    }
    public void SetActiveCheckpoint(GameObject newCheckpoint)
    {
        if (newCheckpoint != activeCheckpoint)
        {
            activeCheckpoint.GetComponent<CheckpointManager>().NotActive();
            activeCheckpoint = newCheckpoint;
        }
    }

    public void RespawnPlayer(bool quickRespawn)
    {
        

        Vector3 playerDeathPosition = player.transform.position;
        Debug.Log("Player Respawned");



        player.Hide(true);
        player.Lock(true);
        StartCoroutine(RespawnWaitUnhide(quickRespawn ? 0 : 0.4f));
        player.Respawned();

        if (score > 0)
        {
            score -= 100;
            GameObject newBody = Instantiate(deadPlayerObject, playerDeathPosition, Quaternion.identity) as GameObject;
            PushOntoArray(ref newBody);

            if (!player.facingRight)
            {
                player.Flip();
                newBody.transform.localScale = new Vector3(newBody.transform.localScale.x * -1, newBody.transform.localScale.y, newBody.transform.localScale.z);
            }
        }
    }
}

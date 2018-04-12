using UnityEngine;
using System.Collections;

public class HelperController : MonoBehaviour {
    public Player player;
    UIManager canvas;
    Camera mainCamera;
    public float speed = 6.0f;
    bool gettingHelp = false;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<UIManager>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
    //
    public void HelpPopUp(string text)
    {
        canvas.WriteToHelperText(text);
    }

    public void setGettingHelp(bool _gettingHelp)
    {
        gettingHelp = _gettingHelp;
    }

    void MoveTowards(Vector3 goal)
    {
        if (transform.position.x > goal.x + 10.5)
            transform.position -= new Vector3(speed * 3.0f, 0.0f, 0.0f) * Time.deltaTime;
        else if (transform.position.x < goal.x - 10.5)
            transform.position += new Vector3(speed*3.0f, 0.0f, 0.0f) * Time.deltaTime;
        else if (transform.position.x > goal.x + 0.5)
            transform.position -= new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;
        else if (transform.position.x < goal.x - 0.5)
            transform.position += new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;

        if (transform.position.y > goal.y + 10.5)
            transform.position -= new Vector3(0.0f, speed * 3.0f, 0.0f) * Time.deltaTime;
        else if (transform.position.y < goal.y - 10.5)
            transform.position += new Vector3(0.0f, speed * 3.0f, 0.0f) * Time.deltaTime;
        else if (transform.position.y > goal.y + 0.5)
            transform.position -= new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;
        else if (transform.position.y < goal.y - 0.5)
            transform.position += new Vector3(0.0f, speed, 0.0f) * Time.deltaTime;

        if (transform.position.z > goal.z + 0.5)
            transform.position -= new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
        else if (transform.position.z < goal.z - 0.5)
            transform.position += new Vector3(0.0f, 0.0f, speed) * Time.deltaTime;
    }

	// Update is called once per frame
	void Update () {
        Vector3 goal;
        if (gettingHelp)
        {
            goal = mainCamera.transform.position;
            goal += new Vector3(0, 1.0f, 3);
        }
        else
        {
            goal = player.transform.position;
            if (player.facingRight)
                goal += new Vector3(-1, 1, -0.1f);
            else
                goal += new Vector3(1, 1, -0.1f);
        }
        MoveTowards(goal);
    }
}

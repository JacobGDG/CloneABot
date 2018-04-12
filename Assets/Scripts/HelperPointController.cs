using UnityEngine;
using System.Collections;

public class HelperPointController : MonoBehaviour {
    [Multiline]
    public string text;
    public HelperController helper;
    Player player;

    bool gettingHelp = false, helpSeen = false;

    // Use this for initialization
    void Start()
    {
        helper = GameObject.Find("Helper").GetComponent<HelperController>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !helpSeen)
        {
            helper.setGettingHelp(true);
            player.Lock(true);
            gettingHelp = true;
            helper.HelpPopUp(text);
        }
    }
    
    // Update is called once per frame
    void Update () {
	    if (Input.GetKey(KeyCode.Space) && gettingHelp == true)
        {
            helper.setGettingHelp(false);
            player.Lock(false);
            helper.HelpPopUp("");
            helpSeen = true;
            gettingHelp = false;
        }
	}
}

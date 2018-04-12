using UnityEngine;
using System.Collections;

public class SpikeController : MonoBehaviour {
    Player player;


	// Use this for initialization
	void Start () {
	    player = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.SetDying(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.SetDying(true);
        }
    }


    IEnumerator notDying()
    {
        yield return new WaitForSeconds(0.2f);
        player.SetDying(false);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(notDying());
        }
    }
}

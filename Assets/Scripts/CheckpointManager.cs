using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

    LevelManager levelManager;
    Animator anim, signAnim;
    public GameObject sign;
    

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
        signAnim = sign.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SpawningPlayer()
    {
        anim.SetTrigger("isSpawning");
    }
    public void NotActive()
    {
        signAnim.SetBool("isActive", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.SetActiveCheckpoint(gameObject);
            signAnim.SetBool("isActive", true);
        }
    }
}

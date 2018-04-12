using UnityEngine;
using System.Collections;

public class MoneyController : MonoBehaviour
{
    LevelManager levelManager;
    AudioSource audS;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audS = GetComponent<AudioSource>();
    }

    IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.moneyFound();
            audS.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterSound());            
        }
    }
}

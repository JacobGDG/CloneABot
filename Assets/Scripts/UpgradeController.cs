using UnityEngine;
using System.Collections;

public class UpgradeController : MonoBehaviour {
    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
            levelManager.UpgradeFound();
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(DestroyAfterSound());
        }
    }
}

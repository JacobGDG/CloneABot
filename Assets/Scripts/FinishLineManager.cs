using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishLineManager : MonoBehaviour {
    LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	} 

    IEnumerator ScoreScreen()
    {
        SceneManager.LoadScene("HighScores");
        yield return new WaitForSeconds(0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("newPlayerScore", levelManager.GetScore());
            PlayerPrefs.SetFloat("newPlayerTime", levelManager.GetTime());

            StartCoroutine(ScoreScreen());
        }
    }
}

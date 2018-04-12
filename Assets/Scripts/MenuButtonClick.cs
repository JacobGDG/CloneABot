using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonClick : MonoBehaviour {

	public void QuitGame()
    {
        Debug.Log("quitting");
        Application.Quit();
    }

    public void PlayLevel1()
    {
        Debug.Log("changing level");
        SceneManager.LoadScene("Scene01");
    }
}

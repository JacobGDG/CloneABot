using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    Slider deathTimerSlider;
    LevelManager levelManager;
    Text robotsUsedText, TimerText, helperPopUpText;
    Text previousTips;
    GameObject helperPopUp, pauseScreen;


	// Use this for initialization
	void Start () {
        deathTimerSlider = GameObject.Find("DeathTimer").GetComponent<Slider>();
        robotsUsedText = GameObject.Find("RobotsUsedText").GetComponent<Text>();
        TimerText = GameObject.Find("TimeText").GetComponent<Text>();
        helperPopUp = GameObject.Find("HelperText");
        pauseScreen = GameObject.Find("Pause Screen");
        previousTips = GameObject.Find("Previous Tips").GetComponent<Text>();
        helperPopUpText = helperPopUp.GetComponent<Text>();
        levelManager = FindObjectOfType<LevelManager>();
        deathTimerSlider.maxValue = Player.maxDeathTime;
    }
	
    public void WriteToHelperText(string text)
    {
        helperPopUpText.text = text;
    }

    public void TogglePauseScreen()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }
    public void TogglePauseScreen(bool toShow)
    {
        pauseScreen.SetActive(toShow);
    }

    // Update is called once per frame
    void Update () {
        deathTimerSlider.value = Player.deathTimer;
        robotsUsedText.text = ("Research Funds: £" + levelManager.GetScore());

        float time = levelManager.GetTime();

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        var fraction = (time * 100) % 100;

        //update the label value
        TimerText.text = ("Time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction));

        if (helperPopUpText.text == "")
            helperPopUp.SetActive(false);
        else
            helperPopUp.SetActive(true);

    }
}

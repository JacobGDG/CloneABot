using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScreenManager : MonoBehaviour {
    Text highScores;
    InputField newNameField;
    Button commitName;

    float time, fraction;
    int minutes, seconds;
    string outputString = "";

    bool newHighScore = false;

    int newScore;
    float newTime;

    string[] topNames = new string[5];
    int[]topScores = new int[5];
    float[]topTimes = new float[5];

    void Start ()
    {
        highScores = (GameObject.Find("TopTenScores")).GetComponent<Text>();
        newNameField = (GameObject.Find("NameField")).GetComponent<InputField>();
        commitName = (GameObject.Find("CommitName")).GetComponent<Button>();

        SetNewScoreBoard();

        time = newTime;

        minutes = (int)time / 60;
        seconds = (int)time % 60;
        fraction = (time * 100) % 100;

        outputString += "\n";

        if (newScore > PlayerPrefs.GetInt("topScore5"))
        {
            outputString += "NEW HIGH SCORE!!!";
            newHighScore = true;
            newNameField.interactable = newHighScore;
            commitName.interactable = true;
        }

        outputString += string.Format("\nYOU: Score: {0}  Time: {1}\n", newScore, (string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction)));

        highScores.text = outputString;

        
    }

    void SetNewScoreBoard()
    {
        newScore = PlayerPrefs.GetInt("newPlayerScore");
        newTime = PlayerPrefs.GetFloat("newPlayerTime");

        outputString = "";

        for (int x = 0; x < 5; x++)
            if (PlayerPrefs.HasKey("topName" + (x + 1)))
            {
                topNames[x] = PlayerPrefs.GetString("topName" + (x + 1));
                topScores[x] = PlayerPrefs.GetInt("topScore" + (x + 1));
                topTimes[x] = PlayerPrefs.GetFloat("topTime" + (x + 1));
            }
            else
            {
                topNames[x] = "N/A";
                topScores[x] = 0;
                topTimes[x] = 0.0f;
            }

        for (int x = 0; x < 5; x++)
        {
            time = topTimes[x];

            minutes = (int)time / 60;
            seconds = (int)time % 60;
            fraction = (time * 100) % 100;


            outputString += string.Format("{0}. {1}  Score: {2}  Time: {3}\n", x + 1, topNames[x], topScores[x], (string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction)));
        }
    }

    public void CommitNewScore()
    {
        if (newNameField.text != "")
        {
            string newName = newNameField.text;
            int position = -1;

            for (int x = 4; x > -1; x--)
            {
                if ((newScore > PlayerPrefs.GetInt("topScore" + (x + 1))) || PlayerPrefs.HasKey("topScore" + (x + 1)))
                {
                    position = x;
                }
                else
                    break;
            }

            string tempName = newName;
            int tempScore = newScore;
            float tempTime = newTime;

            for (int x=position; x<5; x++)
            {
                string _tempName = PlayerPrefs.GetString("topName" + (x + 1));
                int _tempScore = PlayerPrefs.GetInt("topScore" + (x + 1));
                float _tempTime = PlayerPrefs.GetFloat("topTime" + (x + 1));

                PlayerPrefs.SetString("topName" + (x + 1), tempName);
                PlayerPrefs.SetInt("topScore" + (x + 1), tempScore);
                PlayerPrefs.SetFloat("topTime" + (x + 1), tempTime);

                tempName = _tempName;
                tempScore = _tempScore;
                tempTime = _tempTime;
            }

            PlayerPrefs.Save();
            SetNewScoreBoard();
            newScore = 0;
            highScores.text = outputString;
            newNameField.text = "";
            newNameField.interactable = false;
            commitName.interactable = false;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("ManiMenu");
    }

    public void ClearScoreBoard()
    {
        PlayerPrefs.DeleteAll();
        SetNewScoreBoard();
        highScores.text = outputString;
    }
	// Update is called once per frame
	void Update ()
    {
        commitName.interactable = (newNameField.text != "");


    }
}

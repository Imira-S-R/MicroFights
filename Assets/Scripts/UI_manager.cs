using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    public GameObject pause_menu;
    public GameObject tutorial_1;
    public Text text;
    bool isFirst = true;
    bool needToBePaused = true;
    string game_mode;

    private void Start()
    {
        game_mode = PlayerPrefs.GetString("GameMode");
        Debug.Log("GAME MODE IS: " + game_mode.ToString());
        text.text = "use w a s d keys to move,\nPRESS AND HOLD IN THE DIRECTION YOU WANT TO SHOOT,\nCOLLECT THE PERKS TO GAIN SPECIAL SKILLS TO HELP YOU WITH THE\nGAME.";
        tutorial_1.SetActive(true);
    }

    private void Update()
    {
        if (needToBePaused && game_mode == "Tutorial")
        {
            Time.timeScale = 0f;
        }
    }



    public void changeText ()
    {
        if (isFirst == true)
        {
            text.text = "Now use what you have learned to do this sample level.\nshoot the enemeis and collect the perks.";
            isFirst = false;
        } else
        {
            needToBePaused = false;
            tutorial_1.SetActive(false);
            Time.timeScale = 1f;
        }
    }



    public void continueGame()
    {
        int build_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(build_index);
    }

    public void shop()
    {
        SceneManager.LoadScene(2);
    }

    public void pause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void unpause()
    {
        Time.timeScale = 1f;
        pause_menu.SetActive(false);
    }

    public void home()
    {
        SceneManager.LoadScene(0);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void returnToLevels()
    {
        SceneManager.LoadScene(5);
    }

    public void perks ()
    {
        SceneManager.LoadScene(8);
    }

}

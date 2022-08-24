using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class choose_game_mode : MonoBehaviour
{
    int level;
    int isFirstTime;

    [HideInInspector]
    public Button[] buttons;


    public Button bu1;
    public Button bu2;
    public Button bu3;
    public Button bu4;
    public Button bu5;
    public Button bu6;
    public Button bu7;
    public Button bu8;

    int highscore;
    public Text highscore_text;
    public GameObject tutorial_panels;
    public GameObject notice_panel;

    // Start is called before the first frame update
    void Start()
    {
        isFirstTime = PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1);


        Debug.Log(isFirstTime.ToString());

        tutorial_panels.SetActive(false);
        notice_panel.SetActive(false);
        highscore = PlayerPrefs.GetInt("HIGHSCORE");

        if (highscore == 1)
        {
            highscore_text.text = highscore.ToString() + " KILL";

        } else
        {
            highscore_text.text = highscore.ToString() + " KILLS";
        }

        buttons = new Button[] { bu1, bu2, bu3, bu4, bu5, bu6, bu7, bu8 };

        level = PlayerPrefs.GetInt("Level");

        Debug.Log("Level is: " + level.ToString());
        Debug.Log(PlayerPrefs.GetInt("HIGHSCORE").ToString() + "HIGHSCORE");

        if (level >= 1)
        {
            bu1.interactable = true;

        }

        if (level >= 2)
        {
            buttons[1].interactable = true;
        }  if (level >= 3)
        {
            buttons[2].interactable = true;
        }  if (level >= 4)
        {
            buttons[3].interactable = true;
        }  if (level >= 5)
        {
            buttons[4].interactable = true;
        }  if (level >= 6)
        {
            buttons[5].interactable = true;
        }  if (level >= 7)
        {
            buttons[6].interactable = true;
        }  if (level >= 8)
        {
            buttons[7].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_quests ()
    {
        Debug.Log("IsFirstTime: " + isFirstTime.ToString());    
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) != 1)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.Save();
            SceneManager.LoadScene(5);
        } else
        {
            tutorial_panels.SetActive(true);
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            PlayerPrefs.Save();
        }
    }

    public void tutorial_ok ()
    {
        PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
        PlayerPrefs.SetString("GameMode", "Tutorial");
        PlayerPrefs.Save();
        SceneManager.LoadScene(7);
    }

    public void tutorial_skip ()
    {
        tutorial_panels.SetActive(false);
        PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
        PlayerPrefs.Save();
        Debug.Log("Is First Time: " + PlayerPrefs.GetInt("IsFirstTime").ToString());
    }

    public void play_survival ()
    {
        if (level != 1)
        {
            PlayerPrefs.SetString("GameMode", "Survival");
            PlayerPrefs.Save();
            SceneManager.LoadScene(4);
        } else
        {
            notice_panel.SetActive(true);
        }
    }

    public void returnHome ()
    {
        SceneManager.LoadScene(0);
    }



    public void level_1 ()
    {
        SceneManager.LoadScene(3);
        PlayerPrefs.SetString("GameMode", "Quests");
        PlayerPrefs.SetInt("selected_level", 1);
        PlayerPrefs.Save();
    }

    public void level_2()
    {
        if (level >= 2)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 2);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_3()
    {
        if (level >= 3)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 3);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_4()
    {
        if (level >= 4)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 4);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_5()
    {
        if (level >= 5)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 5);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_6()
    {
        if (level >= 6)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 6);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_7()
    {
        if (level >= 7)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 7);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void level_8()
    {
        if (level >= 8)
        {
            PlayerPrefs.SetString("GameMode", "Quests");
            PlayerPrefs.SetInt("selected_level", 8);
            PlayerPrefs.Save();
            SceneManager.LoadScene(3);
        }
    }

    public void ok ()
    {
        notice_panel.SetActive(false);
    }

}

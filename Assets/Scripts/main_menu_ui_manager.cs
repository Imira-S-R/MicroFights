using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu_ui_manager : MonoBehaviour
{
    public Text heading;
    public Text paragraph;

    private void Start()
    {
        
        health();
        /*PlayerPrefs.SetInt("IsFirstTime", 1);
        PlayerPrefs.Save();
        int isFirstTime = PlayerPrefs.GetInt("IsFirstTime");*/
        /*PlayerPrefs.SetInt("FIRSTTIMEOPENING", 1);
        PlayerPrefs.Save();*/
        int isFirstTime = PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1);
        Debug.Log("IS FIRST TIME: " + isFirstTime.ToString());
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            PlayerPrefs.SetInt("HIGHSCORE", 0);
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetString("Weapons_Owned", "Pistol,");
            PlayerPrefs.SetInt("Coins", 0);
            PlayerPrefs.SetString("Equipped", WeaponData.weapon1.name);
            PlayerPrefs.Save();
        }
    }

    public void play ()
    {
        SceneManager.LoadScene(1);
    }

    public void shop ()
    {
        SceneManager.LoadScene(2);
    }

    public void returnHome ()
    {
        SceneManager.LoadScene(0);
    }

    public void help ()
    {
        SceneManager.LoadScene(6);
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("IsFirstTime", 1);
        PlayerPrefs.SetInt("HIGHSCORE", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetString("Weapons_Owned", "Pistol,");
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetString("Equipped", WeaponData.weapon1.name);
        PlayerPrefs.Save();
    }

    public void health ()
    {
        heading.text = "Full Health";
        paragraph.text = "Sets your health to 100.";
    }

    public void slowEnemy ()
    {
        heading.text = "Slow enemy";
        paragraph.text = "Reduces enemy speed.";
    }

    public void fasterBullets ()
    {
        heading.text = "Faster bullets";
        paragraph.text = "Increases your bullet speed.";
    }

    public void fasterMovement()
    {
        heading.text = "Faster Movement";
        paragraph.text = "Increases your movement speed.";
    }

    public void perks ()
    {
        SceneManager.LoadScene(8);
    }

}

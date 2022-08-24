using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float moveSpeed = 8f;
    public Rigidbody2D rb;
    public int playerHealth = 100;
    public healthbarScript healthbar;
    public float enemyMoveSpeed;
    public int playerDamage = 10;


    public int selected_level;

    public GameObject healthBar_;
    public GameObject level_number_;
    public GameObject bullet_info_panel;
    public GameObject pause_menu;

    public Text level_number;
    public Text ratio;
    public Text prize_;

    public int enemeis = 0;
    public int enemeis_to_kill;
    public int current_level;

    public int enemeis_killed = 0;
    public int shots_fired = 0;

    public int[] monsters_per_level;
    public float[] bonus_per_level_with_no_damage;
    public float[] bonus_per_level_with_damage;
    public Text title;

    public int total;
    public bool isSaved = false;


    private Vector2 movement;

    public GameObject panel;

    public Text shots_fired_text;
    public Text killed_count;

    public int enemy_count = 0;

    public int enemeis_collided_with_player = 0;
    public int enemeis_shot = 0;

    string game_mode;
    int highscore;

    void Start()
    {
        selected_level = PlayerPrefs.GetInt("selected_level");
        Time.timeScale = 1f;
        pause_menu.SetActive(false);
        panel.SetActive(false);
        healthBar_.SetActive(true);
        level_number_.SetActive(true);
        bullet_info_panel.SetActive(true);
        /*PlayerPrefs.SetInt("HIGHSCORE", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetString("Weapons_Owned", "Pistol,");
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetString("Equipped", WeaponData.weapon1.name);
        PlayerPrefs.Save();*/
        healthbar.setMaxHealth(100);

        highscore = PlayerPrefs.GetInt("HIGHSCORE");
        
        game_mode = PlayerPrefs.GetString("GameMode");
        current_level = PlayerPrefs.GetInt("Level");

        selected_level = PlayerPrefs.GetInt("selected_level");

        Debug.Log("Game mode" + game_mode);
        if (game_mode == "Quests" && selected_level == current_level)
        {
            Debug.Log("I am wrong");
            monsters_per_level = new int[] { 6, 8, 10, 12, 14, 16, 18, 24 };
            bonus_per_level_with_no_damage = new float[] { 50, 100, 150, 200, 250, 300, 350, 400 };
            bonus_per_level_with_damage = new float[] { 25, 50, 75, 100, 125, 150, 175, 200 };

            enemeis_to_kill = monsters_per_level[current_level - 1];
            level_number.text = "Level " + selected_level.ToString();
        } else if (game_mode == "Survival")
        {
            level_number.text = "Survival";

        } else if (game_mode == "Quests" && selected_level != current_level)
        {
            /*Debug.Log("Selected Level: " + selected_level.ToString());*/
            enemeis_to_kill = monsters_per_level[selected_level - 1];
            Debug.Log("Enemeis to kill: " + enemeis_to_kill.ToString());
            level_number.text = "Level " + current_level.ToString();
        }else if (game_mode == "Tutorial")
        {
            level_number.text = "Tutorial";
        }



        /*Debug.Log("Enemeis to kill " + (enemeis_to_kill * 4).ToString());*/

        /*Debug.Log("Enemeis to kill" + enemeis_to_kill.ToString());*/

    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject[] enemeis = GameObject.FindGameObjectsWithTag("Enemy");


        if (enemeis_killed > 0 && enemeis.Length == 0 && game_mode == "Tutorial")
        {
            SceneManager.LoadScene(5);
        } else if (playerHealth <= 0 && game_mode == "Tutorial")
        {
            SceneManager.LoadScene(7);
        }

        if (game_mode == "Survival")
        {
            level_number.text = "Survival";
        } else if (game_mode == "Tutorial")
        {
            level_number.text = "Tutorial";
        }

        if (selected_level != current_level && game_mode != "Survival" && game_mode != "Tutorial")
        {
            monsters_per_level = new int[] { 6, 8, 10, 12, 14, 16, 18, 24 };
            enemeis_to_kill = monsters_per_level[selected_level - 1];
            level_number.text = "Level " + selected_level.ToString();

        }

        /*Debug.Log("Enemeis collided with player: " + enemeis_collided_with_player.ToString());*/

        total = enemeis_killed + enemeis_collided_with_player - 1;


        float ratio_num = (float)enemeis_killed / shots_fired;
        ratio_num = Mathf.Round(ratio_num * 10.0f) * 0.1f;

        float prize = (float)enemeis_killed * ratio_num;
        prize = Mathf.Round(prize * 10.0f) * 0.1f;

        if (float.IsNaN(ratio_num)) {
            ratio_num = 0;
        
        }

        if (float.IsNaN(prize))
        {
            prize = 0;
        }

        

        

        if (game_mode == "Quests")
        {

            if (current_level == selected_level)
            {
                if (enemeis_collided_with_player == 0)
                {
                    prize = Mathf.Round(prize) * bonus_per_level_with_no_damage[current_level - 1];
                }
                else if (playerHealth <= 0)
                {
                    prize = Mathf.Round(prize);
                }
                else
                {
                    prize = Mathf.Round(prize) * bonus_per_level_with_damage[current_level - 1];
                }
            } else if (current_level != selected_level)
            {
                if (enemeis_collided_with_player == 0)
                {
                    prize = Mathf.Round(prize) * 100f;
                }
                else if (playerHealth <= 0)
                {
                    prize = Mathf.Round(prize);
                }
                else
                {
                    prize = Mathf.Round(prize) * 50f;
                }
            }


            if (enemeis_killed > 0 && enemeis.Length == 0 && game_mode == "Quests")
            {
                if (isSaved != true)
                {
                    int coins = PlayerPrefs.GetInt("Coins") + ((int)prize);
                    PlayerPrefs.SetInt("Coins", coins);
                    PlayerPrefs.Save();
                    isSaved = true;
                }
                bullet_info_panel.SetActive(false);
                healthBar_.SetActive(false);
                level_number_.SetActive(false);
                Time.timeScale = 0f;
                prize_.text = prize.ToString();
                ratio.text = ratio_num.ToString();
                if (current_level != 8 && selected_level == current_level)
                {
                    PlayerPrefs.SetInt("Level", current_level + 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.Log("Completed");
                }
                killed_count.text = enemeis_killed.ToString();
                shots_fired_text.text = shots_fired.ToString();
                /*Debug.Log("Level Complete Well Done");*/
                panel.SetActive(true);
                title.text = "You Win, Player";
                total = enemeis_collided_with_player + enemeis_killed;
            }

            


            if (playerHealth <= 0 && game_mode == "Quests")
            {

                

                if (isSaved != true)
                {
                    int coins = PlayerPrefs.GetInt("Coins") + ((int)prize);
                    PlayerPrefs.SetInt("Coins", coins);
                    PlayerPrefs.Save();
                    isSaved = true;
                }
                bullet_info_panel.SetActive(false);
                healthBar_.SetActive(false);
                level_number_.SetActive(false);
                prize_.text = prize.ToString();
                ratio.text = ratio_num.ToString();
                Time.timeScale = 0f;
                killed_count.text = enemeis_killed.ToString();
                shots_fired_text.text = shots_fired.ToString();
                /*Debug.Log(enemeis_killed.ToString());*/
                /*Debug.Log(shots_fired.ToString());*/
                panel.SetActive(true);
                title.text = "You Lose, Player";
                total = enemeis_collided_with_player + enemeis_killed;
                /*Debug.Log(total);*/
            }
            } else if (game_mode == "Survival")
            {
            if (playerHealth <= 0)
            {
                if (enemeis_killed > highscore)
                {
                    PlayerPrefs.SetInt("HIGHSCORE", enemeis_killed);
                    PlayerPrefs.Save();
                }

                if (isSaved != true)
                {
                    int coins = PlayerPrefs.GetInt("Coins") + ((int)prize);
                    PlayerPrefs.SetInt("Coins", coins);
                    PlayerPrefs.Save();
                    isSaved = true;
                }
                bullet_info_panel.SetActive(false);
                healthBar_.SetActive(false);
                level_number_.SetActive(false);
                prize_.text = prize.ToString();
                ratio.text = ratio_num.ToString();
                Time.timeScale = 0f;
                killed_count.text = enemeis_killed.ToString();
                shots_fired_text.text = shots_fired.ToString();
                /*Debug.Log(enemeis_killed.ToString());*/
                /*Debug.Log(shots_fired.ToString());*/
                panel.SetActive(true);
                title.text = "You Lose, Player";
                total = enemeis_collided_with_player + enemeis_killed;
                /*Debug.Log(total);*/
            }
        }

        /*if (total == (enemeis_to_kill * 4))
        {
            
        }*/

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


       /* Debug.Log("GAME MODE IS: " + game_mode);
        if (game_mode == "Tutorial" && enemeis.Length == 0)
        {
            SceneManager.LoadScene(7);
        }*/

        /*Debug.Log(enemeis);*/
    }


    private void FixedUpdate()
    {
        transform.position += new Vector3(movement.x, movement.y, 0f) * Time.deltaTime * moveSpeed;
    }


    IEnumerator fasterMovement ()
    {
        /*Debug.Log("Here");*/
        moveSpeed = 13f;
        yield return new WaitForSeconds(20f);
        moveSpeed = 8f;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with one enemy");
            Debug.Log("Player health BEFORE: " + playerHealth.ToString());
            playerHealth -= playerDamage;
            Debug.Log("Player health AFTER: " + playerHealth.ToString());
            healthbar.setHealth(playerHealth);
            enemeis_collided_with_player += 1;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("LEnemy"))
        {
            playerHealth -= 20;
            healthbar.setHealth(playerHealth);
            enemeis_collided_with_player += 1;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("FastMovement"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(fasterMovement());
        }

        if (collision.gameObject.CompareTag("SlowEnemy"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(slowDownEnemy());
        }
        else if (collision.gameObject.CompareTag("LessDamage"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(lessDamage());
        }
    }

    

    IEnumerator lessDamage ()
    {
        /*Debug.Log("Less damage");*/
        healthbar.setMaxHealth(100);
        playerHealth = 100;
        /*Debug.Log("Player health: " + playerHealth.ToString());*/
        yield return new WaitForSeconds(0f);
    }

    IEnumerator slowDownEnemy ()
    {
        Enemy.moveSpeed = 1f;
        yield return new WaitForSeconds(15f);
        Enemy.moveSpeed = 3f;
    }
}

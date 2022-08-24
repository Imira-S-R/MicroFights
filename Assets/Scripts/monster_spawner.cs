using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_spawner : MonoBehaviour
{
    public GameObject[] enemeis;
    private float spawnTime = 10f;
    public int randomNumber;
    public static int[] monsters_per_level;

    public int level;
    private int counter = 10;
    string game_mode;

    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {

        game_mode = PlayerPrefs.GetString("GameMode");

        if (game_mode == "Quests")
        {
            spawnTime = 2f;
            level = PlayerPrefs.GetInt("selected_level");
            /*Debug.Log("Level is: " + level.ToString());*/
            monsters_per_level = new int[] { 6, 8, 10, 12, 14, 16, 18, 20 };
            counter = monsters_per_level[level - 1];
            /*Debug.Log("Mosters spawned this level" + counter.ToString());*/
            /*Debug.Log(counter);*/
            Debug.Log("Counter is: " + counter.ToString());
            StartCoroutine(spawnMonster());
        } else if (game_mode == "Survival")
        {
            Debug.Log("Survial time");
            counter = 50;
            spawnTime = 5f;
            StartCoroutine(spawnMonster());
        } else if (game_mode == "Tutorial")
        {
            counter = 5;
            spawnTime = 1f;
            StartCoroutine(spawnMonster());
        }



    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("GAME MODE IS: " + game_mode);
        Debug.Log("COUNTER IS: " + spawnTime.ToString());
        Debug.Log("SPAWN TIME IS: " + spawnTime.ToString());*/
    }

    IEnumerator spawnMonster ()
    {

        if (game_mode == "Quests")
        {
            if (level > 1)
            {
                while (counter > 0)
                {
                    randomNumber = Random.Range(0, enemeis.Length);
                    yield return new WaitForSeconds(spawnTime);
                    Instantiate(enemeis[randomNumber], transform.position, transform.rotation);
                    counter -= 1;
                }
            }
            else
            {
                while (counter > 0)
                {
                    yield return new WaitForSeconds(spawnTime);
                    Instantiate(enemeis[0], transform.position, transform.rotation);
                    counter -= 1;
                }
            }
        } else if (game_mode == "Survival")
        {
            while (counter > 0)
            {
                randomNumber = Random.Range(0, enemeis.Length);
                yield return new WaitForSeconds(spawnTime);
                Instantiate(enemeis[randomNumber], transform.position, transform.rotation);
                counter -= 1;
            }
        } else if (game_mode == "Tutorial")
        {
            Debug.Log("CAME HERE MATE");
            while (counter > 0)
            {
                yield return new WaitForSeconds(spawnTime);
                Instantiate(enemeis[0], transform.position, transform.rotation);
                counter -= 1;
            }
        }
    }
    
}

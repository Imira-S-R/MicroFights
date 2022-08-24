using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    public Text bullet_number_text;

    [HideInInspector]
    public float bulletForce = 20f;
    public AudioSource source;
    public AudioSource reloading;

    float reloadingTime = 2f;

    [HideInInspector]
    private int number_of_bullets = 20;

    float waitTime = 0.2f;
    private float timeStamp = Mathf.Infinity;
    private Player player;

    public Weapon[] weapons;

    public int number_of_bullets_;
    bool playerWarned = false;





    // Start is called before the first frame update
    void Start()
    {
        weapons = new Weapon[] { WeaponData.weapon1, WeaponData.weapon2, WeaponData.weapon3, WeaponData.weapon4, WeaponData.weapon5 };
        player = FindObjectOfType(typeof(Player)) as Player;

        string gun_name = PlayerPrefs.GetString("Equipped");
        Debug.Log("Gun_name" + gun_name);
        foreach (var item in weapons)
        {
            if (item.name == gun_name)
            {
                number_of_bullets_ = item.number_of_bullets;
                number_of_bullets = item.number_of_bullets;
                waitTime = item.waiting_time;
                reloadingTime = item.reloading_time;
                bulletForce = item.bulletForce;
            }
        }

        Debug.Log("Bullet speed is: " + bulletForce.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FastBullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(fasterBullets());
        }

    }

    // Update is called once per frame
    void Update()
    {
        bullet_number_text.text = number_of_bullets.ToString();

        /*Debug.Log(number_of_bullets);*/

        if (Input.GetMouseButtonDown(0))
        {
            timeStamp = Time.time + waitTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            timeStamp = Mathf.Infinity;
        }

        if (Time.time >= timeStamp)
        {
            StartCoroutine(Shoot());
            timeStamp = Time.time + waitTime;
        }

        /*Debug.Log("Bullet speed: " + bulletForce.ToString());*/
    }

    IEnumerator Shoot ()
    {
        
        if (number_of_bullets == 0 )
        {
            /*Debug.Log("Waiting");*/
            if (playerWarned == false)
            {
                reloading.Play();
                playerWarned = true;
            }
            yield return new WaitForSeconds(reloadingTime);
            playerWarned = false;
            number_of_bullets = number_of_bullets_;
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        player.shots_fired += 1;
        number_of_bullets -= 1;
        source.Play();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);

    }


    

    IEnumerator fasterBullets ()
    {
        float old_value = bulletForce;
        bulletForce += 10f;
        yield return new WaitForSeconds(20f);
        bulletForce = old_value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    public static float moveSpeed = 3f;
    private Vector3 directionToPlayer;
    public GameObject explosionPrefab;
    public AudioClip audio;

    public int damage = 50;

    public healthbarScript healthbar;

    public GameObject[] perks;

    public bool isShot = false;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(Player)) as Player;

        
    }



    private void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy ()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health -= damage;
            if (health <= 0)
            {
                /*player.enemeis_killed += 1;
                player.enemeis += 1;
                player.enemy_count += 1;*/
                if (isShot == false)
                {
                    player.enemeis_killed += 1;
                    player.enemeis_shot += 1;
                    isShot = true;
                }
                AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
                Destroy(this.gameObject);
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                int randomNumber = Random.Range(0, 8);
                Debug.Log(randomNumber);
                if (randomNumber == 1)
                {
                    int randomPerk = Random.Range(0, perks.Length);
                    Debug.Log(randomPerk);
                    Instantiate(perks[randomPerk], transform.position, transform.rotation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(moveSpeed);*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

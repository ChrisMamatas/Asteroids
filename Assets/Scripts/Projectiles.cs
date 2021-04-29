using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameManager manager;

    private float projectileSpeed;
    private float xRange;
    private float yRange;

    public GameObject smallerAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // Referemced to the Game Manager script
        rb = GetComponent<Rigidbody2D>();
        projectileSpeed = 12f;
        xRange = 11f;
        yRange = 6f;
    }

    // Update is called once per frame
    // Moves projectile foward from player direction, destroying when it goes out of bounds
    void Update()
    {
        
        rb.velocity = transform.up * projectileSpeed;

        if (transform.position.x > xRange || transform.position.x < -xRange || transform.position.y > yRange || transform.position.y < -yRange) {
            Destroy(gameObject);
        }
        
    }

    // Destroy and spawn smaller when projectile collides with asteroid, or destroy small asteroid
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Asteroid1")) {

            FindObjectOfType<AudioManager>().Play("Projectile Hit");

            Destroy(gameObject);
            Destroy(collision.gameObject);

            manager.AddScore(5);

            manager.SpawnSmallerAsteroid(gameObject.transform);
            

        }
        else if (collision.gameObject.CompareTag("Asteroid2")) {

            FindObjectOfType<AudioManager>().Play("Projectile Hit");

            manager.AddScore(2);

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    
}

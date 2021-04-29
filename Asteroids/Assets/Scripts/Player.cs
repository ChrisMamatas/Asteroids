using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Transform playerPos;
    public GameObject projectile;
    public GameObject projectileSpawnPoint;
    private GameManager manager;

    private float speed = 3f;
    private float thrust;
    private float rotate;
    private float rotationSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GetComponent<Transform>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        thrust = Input.GetAxisRaw("Vertical");
        rotate = Input.GetAxisRaw("Horizontal");

        if (thrust > 0)
            rb.AddForce(playerPos.up * speed * thrust);
        else {
            // Slows the velocity
            if (rb.velocity != Vector2.zero) {
                rb.velocity -= rb.velocity * 0.98f * Time.deltaTime;
            }
        }
            
        // Handles rotation
        rb.rotation += -rotate * rotationSpeed;

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }
    }

    public void Shoot() {
        Instantiate(projectile, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
    }

    // If the player is hit by an asteroid
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Asteroid1") || collision.gameObject.CompareTag("Asteroid2")) {

            FindObjectOfType<AudioManager>().Play("Player Hit");
            Destroy(gameObject);
            Destroy(collision.gameObject);

            manager.DeathScreen();
        }
    }
}

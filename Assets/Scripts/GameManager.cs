using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate;

    public GameObject asteroid;
    public GameObject smallerAsteroid;
    public GameObject pauseMenu;
    public GameObject deathScreen;
    public GameObject scoreTextGameObject;
    public GameObject playerPrefab;

    int spawnLocation;
    private int numOfSmallerAsteroids;

    private bool isPaused;
    private bool isDead;

    private int score;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        spawnRate = 1f;
        InvokeRepeating("SpawnAsteroid", 1.0f, spawnRate);
    }

    // Checks for escape key when pausing and unpausing
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused == false) {
                isPaused = true;
                PauseGame();
            }
            else {
                isPaused = false;
                ResumeGame();
            }
        }
    }

    // Spawns asteroid facing towards the play screen at a random location outside of the screen
    private void SpawnAsteroid() {

        if (isDead == false) {
            spawnLocation = Random.Range(1, 4);

            if (spawnLocation == 1) {
                Instantiate(asteroid, new Vector2(-13, Random.Range(-5, 5)), Quaternion.Euler(0, 0, Random.Range(-25, -65)));
            }
            else if (spawnLocation == 2) {
                Instantiate(asteroid, new Vector2(Random.Range(-10, 10), 6), Quaternion.Euler(0, 0, Random.Range(-70, -110)));
            }
            else if (spawnLocation == 3) {
                Instantiate(asteroid, new Vector2(13, Random.Range(-5, 5)), Quaternion.Euler(0, 0, Random.Range(-105, -155)));
            }
            else if (spawnLocation == 4) {
                Instantiate(asteroid, new Vector2(Random.Range(-10, 10), -6), Quaternion.Euler(0, 0, Random.Range(-160, -210)));
            }
        }
    }

    // Spawns 1-3 smaller asteroids where projectiles hit big asteroid, moving foward in random directions
    public void SpawnSmallerAsteroid(Transform asteroidTransform) {

        numOfSmallerAsteroids = Random.Range(1, 3);
        for (int i = 0; i <= numOfSmallerAsteroids; i++) {

            Instantiate(smallerAsteroid, asteroidTransform.position, Quaternion.Euler(0, 0, Random.Range(0, 180)));
        }

    }

    // Finds all instances of asteroids in the scene and destroys them
    public void DestroyAsteroids() {
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid1")) {
            Destroy(asteroid);
        }
        foreach (GameObject asteroid2 in GameObject.FindGameObjectsWithTag("Asteroid2")) {
                Destroy(asteroid2);
        }
    }

    // Adds score
    public void AddScore(int score) {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    // Pauses and resumes game
    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    // Opens death screen
    public void DeathScreen() {
        isDead = true;
        deathScreen.SetActive(true);
        scoreTextGameObject.SetActive(true);
        DestroyAsteroids();
    }

    // Restarts the game, setting score to 0 and respawning player
    public void Replay() {
        isDead = false;
        deathScreen.SetActive(false);
        score = 0;
        scoreText.text = score.ToString();
        Instantiate(playerPrefab,transform.position, transform.rotation);
    }
}

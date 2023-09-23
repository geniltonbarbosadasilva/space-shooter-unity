using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public GameObject projectile;
    public GameObject explosion;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public Transform[] shootPosition;
    public Vector2 screenLimit;
    public Vector2[] shootDirection;
    public Image helthBar;
    public TextMeshProUGUI helthText, scoreText, gameOverScoreText;
    public bool pause = false, isDead = false;
    public float speed = 1;
    public float shootSpeed = 300;
    public float shootDelay = 0.5f;
    public float shootTimer = 0;
    public float gameTimer = 0;
    public int maxHelth = 10;
    public int helth;
    public int score = 0;

    // Start is called before the first frame update
    void Start() {
        score = 0;
        helth = maxHelth;
    }

    // Update is called once per frame
    void Update() {
        gameTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        UpdateUI();
        Move();
        Shoot();
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            if(pause && !isDead) {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            } else if(!isDead) {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
            }
        }
    }

    void UpdateUI() {
        helthBar.fillAmount = (float)helth / maxHelth;
        helthText.text = helth + "/" + maxHelth;
        scoreText.text = "Score: " + ((int)gameTimer + score);
    }
    
    public void AddScore(int value = 20) {
        score += value;
        UpdateUI();
    }

    void Shoot() {
        if (Input.GetAxisRaw("Jump") != 0 && shootTimer >= shootDelay) {
            for (int i = 0; i < shootPosition.Length; i++) {
                GameObject newShoot = Instantiate(projectile);
                newShoot.transform.position = shootPosition[i].position;
                newShoot.transform.up = shootDirection[i].normalized;
                newShoot.GetComponent<Rigidbody2D>().AddForce(shootDirection[i] * shootSpeed);
                shootTimer = 0;
            }
        }
    }

    void Move() {
        float vMove = Input.GetAxisRaw("Vertical");
        float hMove = Input.GetAxisRaw("Horizontal");

        transform.Translate((new Vector3(hMove, vMove)).normalized * speed * Time.deltaTime);

        // if (transform.position.x > screenLimit.x) transform.position = new Vector3(-screenLimit.x + .2f, transform.position.y);
        // if (transform.position.x < -screenLimit.x) transform.position = new Vector3(screenLimit.x - .2f, transform.position.y);

        if (transform.position.x > screenLimit.x) transform.position = new Vector3(screenLimit.x, transform.position.y);
        if (transform.position.x < -screenLimit.x) transform.position = new Vector3(-screenLimit.x, transform.position.y);
        if (transform.position.y > screenLimit.y) transform.position = new Vector3(transform.position.x, -screenLimit.y + .2f);
        if (transform.position.y < -screenLimit.y) transform.position = new Vector3(transform.position.x, screenLimit.y - .2f);
    }

    public void TakeDamage(int damage = 1) {
        if(damage < 0) return;
        if(helth - damage > 0) helth -= damage;
        else {
            helth = 0;
            GameOver();
        }
        UpdateUI();
    }

    void GameOver() {
        isDead = true;
        if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        helth = maxHelth;
        transform.position = Vector3.zero;
        int oldScore = PlayerPrefs.GetInt("score", 0);
        int newScore = (int)gameTimer + score;
        if(newScore > oldScore) PlayerPrefs.SetInt("score", newScore);
        if(gameOverScoreText != null) gameOverScoreText.text = "Score: " + newScore.ToString() + "\nRecord: " + oldScore.ToString();
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}

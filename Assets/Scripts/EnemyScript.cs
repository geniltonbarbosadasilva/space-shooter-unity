using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector2 screenLimit;
    public float speed = 1;
    public int direction = 1;
    public int maxHelth = 5;
    public int helth;

    public GameObject projectile;
    public GameObject explosion;
    public Vector2 shootDirection = Vector2.left;
    public float shootDistance = 1;
    public float shootSpeed = 300;
    public float shootDelay = 0.8f;
    public float shootTimer = 0;
    public int scoreBonus = 20;

    void Start() {
        helth = maxHelth;
    }

    void Update() {
        shootTimer += Time.deltaTime;
        Move();
        Shoot();
    }

    void Shoot() {
        if (shootTimer > shootDelay) {
            GameObject newShoot = Instantiate(projectile);
            newShoot.transform.position = transform.position + Vector3.left * shootDistance;
            newShoot.transform.up = shootDirection.normalized;
            newShoot.GetComponent<Rigidbody2D>().AddForce(shootDirection.normalized * shootSpeed);
            shootTimer = 0;
        }
    }

    void Move() {
        transform.Translate(new Vector2(-speed * Time.deltaTime, direction * speed * 2 * Time.deltaTime));
        if (transform.position.y > screenLimit.y || transform.position.y < -screenLimit.y) {
            direction *= -1;
            transform.position = new Vector2(transform.position.x, Mathf.Sign(transform.position.y) * screenLimit.y);
        }
        if (transform.position.x < -screenLimit.x) transform.position = new Vector2(screenLimit.x + .2f, transform.position.y);
    }
    
    public void TakeDamage(int damage = 1) {
        if(damage < 0) return;
        if(helth - damage > 0) helth -= damage;
        else {
            helth = 0;
            Die();
        }
    }

    void Die() {
        try {
            FindObjectOfType<PlayerScript>().AddScore(scoreBonus);
        } catch {}

        helth = maxHelth;
        transform.position = new Vector2(screenLimit.x + .2f, Random.Range(-screenLimit.y, screenLimit.y));
        if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
    }
}

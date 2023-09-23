using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float time = 1;
    public GameObject explosion;

    void Start() {
        StartCoroutine(DestroyProjectile());
    }

    void Update() { }

    IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && this.tag != "PlayerProjectile") {
            if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            collision.GetComponent<PlayerScript>().TakeDamage();
            Destroy(this.gameObject);
        }
        if (collision.tag == "Enemy" && this.tag != "EnemyProjectile") {
            if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            collision.GetComponent<EnemyScript>().TakeDamage();
            Destroy(this.gameObject);
        }
        if (
            collision.tag == "PlayerProjectile" && this.tag == "EnemyProjectile" ||
            collision.tag == "EnemyProjectile" && this.tag == "PlayerProjectile"
        ) {
            if(explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

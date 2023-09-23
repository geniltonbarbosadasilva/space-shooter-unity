using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float time = .1f;

    void Start() {
        StartCoroutine(DestroyProjectile());
    }

    void Update() { }

    IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] GameObject deathFX;

    private void OnParticleCollision(GameObject other) {
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start(){

        AddNonTriggerBoxCollider();

    }

    private void AddNonTriggerBoxCollider() {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
}

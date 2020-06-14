using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxDetection : MonoBehaviour {

    public bool isCollidingLevel = false;
    public bool isCollidingEnemy = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Level")) {
            isCollidingLevel = true;
        } else if(collision.CompareTag("Enemy")) {
            isCollidingEnemy = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.CompareTag("Level")) {
            isCollidingLevel = false;
        } else if(collision.CompareTag("Enemy")) {
            isCollidingEnemy = false;
        }

    }
}

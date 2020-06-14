using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDetection : MonoBehaviour {

    [Header("State")]
    public bool isColliding = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Level")) {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        isColliding = false;
    }
}

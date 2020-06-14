using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [Header("Hitboxes")]
    public GameObject TopHitboxObject;
    public GameObject LeftHitboxObject;
    public GameObject BottomHitboxObject;
    public GameObject RightHitboxObject;

    [Header("Enemy")]
    public GameObject enemySpawner;

    [Header("Position")]
    public Vector3 previousPosition;

    [Header("States")]
    public bool hasDied = false;
    public bool hasReachedObjective = false;

    private HitboxDetection TopHitbox;
    private HitboxDetection LeftHitbox;
    private HitboxDetection BottomHitbox;
    private HitboxDetection RightHitbox;

    private EnemySpawner enemySpawnerScript;

    private Vector3 newPosition;

    void Awake() {
        newPosition = transform.position;
        TopHitbox = TopHitboxObject.GetComponent<HitboxDetection>();
        LeftHitbox = LeftHitboxObject.GetComponent<HitboxDetection>();
        BottomHitbox = BottomHitboxObject.GetComponent<HitboxDetection>();
        RightHitbox = RightHitboxObject.GetComponent<HitboxDetection>();

        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();

        previousPosition = transform.position;
    }

    void Update() {
        newPosition = transform.position;
        if(Input.GetKeyDown(KeyCode.W) && !TopHitbox.isColliding) {
            newPosition.y += 1f;
        } else if(Input.GetKeyDown(KeyCode.A) && !LeftHitbox.isColliding) {
            newPosition.x -= 1f;
        } else if(Input.GetKeyDown(KeyCode.S) && !BottomHitbox.isColliding) {
            newPosition.y -= 1f;
        } else if(Input.GetKeyDown(KeyCode.D) && !RightHitbox.isColliding) {
            newPosition.x += 1f;
        }

        if(newPosition != transform.position) { // When player has moved
            previousPosition = transform.position;
            enemySpawnerScript.hasMoved = true;
        }

        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Enemy")) {
            hasDied = true;
        } else if(collision.CompareTag("Objective")) {
            hasReachedObjective = true;
        }
    }
}

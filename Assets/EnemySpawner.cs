using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Objects")]
    public GameObject enemy;
    public GameObject player;

    [Header("Game States")]
    public bool hasMoved;

    public float[,] enemyPositions;

    void Start() {
        spawnEnemies();
    }

    void Update() {
        if(hasMoved) {
            hasMoved = false;

            foreach(Transform child in transform) {
                child.gameObject.GetComponent<EnemyBehavior>().Move();
            }
        }        
    }

    private void clearEnemies() {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    public void spawnEnemies() {
        clearEnemies();
        if(enemyPositions != null) {
            for(int i = 0; i < enemyPositions.GetLength(0); i++) {
                var enemyObject = Instantiate(enemy, new Vector3(enemyPositions[i, 0], enemyPositions[i, 1], player.transform.position.z), Quaternion.identity);
                enemyObject.GetComponent<EnemyBehavior>().player = player;
                enemyObject.transform.parent = transform;
            }
        }
    }

}

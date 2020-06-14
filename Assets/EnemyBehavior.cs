using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public GameObject player;

    private GameObject directionIndicator;
    private EnemyHitboxDetection directionIndicatorScript;

    private int directionToGo; // 1, 2, 3, 4 = top, right, bottom, left

    void Awake() {
        directionIndicator = transform.GetChild(0).gameObject;
        directionIndicatorScript = directionIndicator.GetComponent<EnemyHitboxDetection>();
    }

    private float getDistance(float deltaX, float deltaY) {
        Vector3 playerPosition = player.transform.position;
        float newX = transform.position.x + deltaX;
        float newY = transform.position.y + deltaY;
        return Mathf.Sqrt(Mathf.Pow((playerPosition.x - newX), 2) + Mathf.Pow((playerPosition.y - newY), 2));
    }

    private void setDirectionToGo() {
        float[] distances = {getDistance(0f, 1f), getDistance(1f, 0f), getDistance(0f, -1f), getDistance(-1f, 0f)}; // Top, Right, Bottom, Left
        int minimum = 0;
        for(int i = 0; i < distances.Length; i++) {
            if(distances[i] <= distances[minimum]) {
                minimum = i;
            }
        }

        directionToGo = minimum;

        Vector3 directionIndicatorPosition = transform.position;

        switch(directionToGo) {
            case 0:
                directionIndicatorPosition.y += 1f;
                break;
            case 1:
                directionIndicatorPosition.x += 1f;
                break;
            case 2:
                directionIndicatorPosition.y -= 1f;
                break;
            case 3:
                directionIndicatorPosition.x -= 1f;
                break;
        }
        directionIndicator.transform.position = directionIndicatorPosition;
    }

    public void Move() {
        if(!directionIndicatorScript.isCollidingLevel && !directionIndicatorScript.isCollidingEnemy) transform.position = directionIndicator.transform.position;
        setDirectionToGo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [Header("Player Object")]
    public GameObject player;

    private float smoothing = 0.1f;
    private const float defaultLength = .2f, defaultPower = .08f;
    private float shakeTimeRemaining, shakePower, shakeFadeTime;

    private void LateUpdate() {
        if(shakeTimeRemaining > 0) {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }

    void FixedUpdate() {
        if(transform.position != player.transform.position) {
            Vector3 playerPosition = player.transform.position;
            playerPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
        }
    }

    public void CameraShake(float length = defaultLength, float power = defaultPower) {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
    }
}

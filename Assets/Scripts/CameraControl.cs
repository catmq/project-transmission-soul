using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject playerObject;
    public float acceleration = 1.0f;
    public float acceStartDistance = 3.0f;
    public Vector3 playerPositionOffset;

    float maxSpeed;
    float currentSpeed;
	// Use this for initialization
	void Start () {
        maxSpeed = playerObject.GetComponent<playerController>().movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = playerObject.transform.position + playerPositionOffset;
        cameraPosition.y = 0;
        playerPosition.y = 0;
        float playerDistance = Vector3.Distance(cameraPosition, playerPosition);

        if (playerDistance > acceStartDistance)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
        }
        else if (currentSpeed > 0)
        {
            currentSpeed -= acceleration * Time.deltaTime;
            if (currentSpeed <= 0) currentSpeed = 0;
        }

        Vector3 forwardVector = playerPosition - cameraPosition;
        transform.Translate(forwardVector * currentSpeed * Time.deltaTime,Space.World);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float movementSpeed = 6f;
    public float growFactor = 0.05f;

    public string deathParticle;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float movementX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float movementZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(movementX, 0, movementZ);

        if (movement.magnitude > 1) movement.Normalize();
        transform.Translate(new Vector3(movementX, 0, movementZ));
	}

    public void Grow(bool isPositive)
    {
        float growValue;
        if (isPositive)
        {
            growValue = Random.value * growFactor;
        }else
        {
            growValue = (Random.value - 0.5f) * 2.0f * growFactor;
        }
        transform.localScale = transform.localScale + Vector3.one * growValue;
    }

    public void OnDisable()
    {
        GameObject deathEffect = Instantiate(Resources.Load(deathParticle) as GameObject, transform.position, Quaternion.AngleAxis(0, Vector3.up));
        ParticleSystem.ShapeModule deathShape = deathEffect.GetComponent<ParticleSystem>().shape;
        deathShape.radius = transform.localScale.x / 2.0f;
        deathEffect.SetActive(true);
    }
}

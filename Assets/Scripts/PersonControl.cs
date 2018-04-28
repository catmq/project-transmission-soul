using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonControl : MonoBehaviour {

    public float colliderRadius = 0.6f;
    public GameObject personMesh;
    public string lineObject = "Prefab_Line_2";
    public string deathParticle = "Prefab_DeathParticle";

    float enterDistance;
    float personLife;
    float connectionDieTime;
    float currentDieTime;

    GameObject playerObject;

    GameObject connectionObject = null;

    bool noDeathParticle = false;

    bool connectionEnding = false;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (connectionEnding && currentDieTime < connectionDieTime)
        {
            currentDieTime += Time.deltaTime;
        }
        float playerDistance = Vector3.Distance(playerObject.transform.position, transform.position);
        if (connectionObject == null && playerDistance > enterDistance)
        {
            personLife -= Time.deltaTime;
            if (personLife < 0)
            {
                Destroy(gameObject);
            }
        }
        if (connectionObject == null && playerDistance < enterDistance)
        {
            connectionObject = Instantiate(Resources.Load(lineObject) as GameObject, transform);
            connectionObject.SetActive(true);
            playerObject.GetComponent<playerController>().Grow(false);
        }
        if (connectionObject != null && playerDistance < enterDistance)
        {
            float widthFactor = 1 - playerDistance / enterDistance;
            if (connectionEnding)
            {
                Debug.Log(currentDieTime);
                widthFactor *= 1 - currentDieTime / connectionDieTime;
            }
            SetLine(playerObject.transform.position - transform.position, widthFactor);
        }
        if (connectionObject != null && playerDistance > enterDistance)
        {
            Destroy(gameObject);
            playerObject.GetComponent<playerController>().Grow(true);
        }
    }

    void SetLine(Vector3 targetPos, float widthFactor)
    {
        LineRenderer line = connectionObject.GetComponent<LineRenderer>();
        //line.SetPosition(1, Vector3.Lerp(Vector3.zero, targetPos, 0.03f));
        //line.SetPosition(2, Vector3.Lerp(Vector3.zero, targetPos, 0.08f));
        //line.SetPosition(3, Vector3.Lerp(Vector3.zero, targetPos, 0.15f));
        //line.SetPosition(4, Vector3.Lerp(Vector3.zero, targetPos, 0.5f));
        //line.SetPosition(5, Vector3.Lerp(Vector3.zero, targetPos, 0.85f));
        //line.SetPosition(6, Vector3.Lerp(Vector3.zero, targetPos, 0.92f));
        //line.SetPosition(7, Vector3.Lerp(Vector3.zero, targetPos, 0.97f));
        //line.SetPosition(8, targetPos);
        //if (targetPos.magnitude > 2)
        //{
        //    line.SetPosition(1, targetPos.normalized);
        //    line.SetPosition(2, targetPos * 0.5f);
        //    line.SetPosition(3, targetPos - targetPos.normalized);
        //}
        //else
        //{
        //    line.SetPosition(1, targetPos * 0.2f);
        //    line.SetPosition(2, targetPos * 0.5f);
        //    line.SetPosition(3, targetPos * 0.8f);
        //}
        line.SetPosition(4, targetPos);
        line.SetPosition(1, targetPos * 0.2f);
        line.SetPosition(2, targetPos * 0.5f);
        line.SetPosition(3, targetPos * 0.8f);
        line.SetPosition(4, targetPos);

        line.widthMultiplier = widthFactor * Mathf.Min(personMesh.transform.localScale.x, playerObject.transform.localScale.x);
        //Keyframe[] curveKeys;
        //curveKeys = line.widthCurve.keys;
        //curveKeys[1].time = widthFactor * 0.5f;
        //curveKeys[1].value = widthFactor * 0.7f;
        //curveKeys[2].time = 1f - curveKeys[1].time;
        //curveKeys[2].value = widthFactor * 0.7f;
        //line.widthCurve = new AnimationCurve(curveKeys);
    }

    public void OnNewPersonCreated(Vector3 personPosition, float scaleFactor_in, float enterDistance_in, float personLife_in, GameObject playerObject_in)
    {
        transform.position = personPosition;
        enterDistance = enterDistance_in;
        personLife = personLife_in;
        playerObject = playerObject_in;
        Vector3 scaleVector = Vector3.one * scaleFactor_in;
        personMesh.transform.localScale = scaleVector;
        Collider[] hitColliders = Physics.OverlapSphere(personPosition, colliderRadius * scaleFactor_in);
        //if (hitColliders.Length != 0)
        //{
        //    noDeathParticle = true;
        //    Destroy(gameObject);
        //}
        personMesh.transform.Rotate(Vector3.up, Random.Range(0, 360));
        gameObject.SetActive(true);

    }

    private void OnApplicationQuit()
    {
        noDeathParticle = true;
    }

    public void OnDestroy()
    {
        if (!noDeathParticle)
        {
            GameObject deathEffect = Instantiate(Resources.Load(deathParticle) as GameObject, transform.position, Quaternion.AngleAxis(0, Vector3.up));
            ParticleSystem.ShapeModule deathShape = deathEffect.GetComponent<ParticleSystem>().shape;
            deathShape.radius = transform.localScale.x / 2.0f;
            deathEffect.SetActive(true);
        }

    }

    public void OnPlayerGoingToDie(float dieTime)
    {
        connectionDieTime = dieTime;
        currentDieTime = 0;
        connectionEnding = true;

    }
}

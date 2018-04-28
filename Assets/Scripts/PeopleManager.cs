using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour {
    
    public int initialSpawn = 8;
    public int peopleSpawnRate = 10;
    public float PeopleRangeMin = 50.0f;
    public float PeopleRangeMax = 70.0f;

    public float distanceRangeMin = 10;
    public float distanceRangeMax = 50;

    public float ScaleMax = 1.5f;
    public float ScaleMin = 0.8f;

    public float lifeRangeMin = 2.0f;
    public float lifeRangeMax = 5.0f;

    public string PersonPrefab = "Prefab_PersonObject";
    public GameObject PeopleNode;
    public GameObject PlayerObject;

    float currentCumulativeTime;


	// Use this for initialization
	void Start () {
        currentCumulativeTime = 0;
        //generate people randomly
        for (int i=0;i<initialSpawn;i++)
        {
            SpawnNewPerson();
        }
	}
	
	// Update is called once per frame
	void Update () {
        currentCumulativeTime += Time.deltaTime;
        int spawnPeopleCount = (int)Mathf.Floor(peopleSpawnRate * currentCumulativeTime);
        if (spawnPeopleCount > 0)
        {
            currentCumulativeTime = 0;
            for (int i=0;i<spawnPeopleCount;i++)
            {
                SpawnNewPerson();
            }
        }
	}

    public void OnPlayerDeath()
    {
        for (int i=0; i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void OnPlayerGoingToDie(float dieTime)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<PersonControl>().OnPlayerGoingToDie(dieTime);
        }
    }

    void SpawnNewPerson()
    {
        float distance = Random.Range(0, PeopleRangeMin);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        float x = distance * Mathf.Cos(angle);
        float z = distance * Mathf.Sin(angle);
        Vector3 newPersonPosition = new Vector3(x, 0, z);
        GameObject newPerson = Instantiate(Resources.Load(PersonPrefab) as GameObject, PeopleNode.transform);
        float scale = Random.Range(ScaleMin, ScaleMax);
        float enterDistance = Random.Range(distanceRangeMin, distanceRangeMax);
        float life = Random.Range(lifeRangeMin, lifeRangeMax);
        newPerson.GetComponent<PersonControl>().OnNewPersonCreated(newPersonPosition,scale, enterDistance, life, PlayerObject);
    }
}

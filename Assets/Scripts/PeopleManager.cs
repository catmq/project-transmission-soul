using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour {
    
    public int initialSpawn = 8;
    public int peopleSpawnRate = 10;
    public float PeopleRangeMin = 50.0f;
    public float PeopleRangeMax = 70.0f;

    public GameObject PeopleNode;
    public GameObject PersonPrefab;
    public GameObject PlayerObject;

    float currentCumulativeTime;


	// Use this for initialization
	void Start () {
        currentCumulativeTime = 0;
        //generate people randomly
        for (int i=0;i<initialSpawn;i++)
        {
            float distance = Random.Range(0, PeopleRangeMin);
            float angle = Random.Range(0f, 2 * Mathf.PI);
            float x = distance * Mathf.Cos(angle);
            float z = distance * Mathf.Sin(angle);
            Vector3 newPersonPosition = new Vector3(x, 0, z);
            GameObject newPerson = Instantiate(PersonPrefab, PeopleNode.transform);
            newPerson.GetComponent<PersonControl>().OnNewPersonCreated(newPersonPosition);
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
                float distance = Random.Range(PeopleRangeMin, PeopleRangeMax);
                float angle = Random.Range(0f, 2 * Mathf.PI);
                float x = distance * Mathf.Cos(angle);
                float z = distance * Mathf.Sin(angle);
                Vector3 newPersonPosition = PlayerObject.transform.position + new Vector3(x, 0, z);
                GameObject newPerson = Instantiate(PersonPrefab, PeopleNode.transform);
                newPerson.GetComponent<PersonControl>().OnNewPersonCreated(newPersonPosition);
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
}

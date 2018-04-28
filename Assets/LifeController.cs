using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

    public float totalLife;
    public float deathTime;

    public Slider progressBarUI;
    public Text currentTimeTextUI;
    public Text TotalTimeTextUI;

    public GameObject playerObject;
    public PeopleManager peopleManager;

    float currentLife;
    bool deathSignalFired = false;
	// Use this for initialization
	void Start () {
        currentLife = 0;
        TotalTimeTextUI.text = Mathf.Floor(totalLife / 60).ToString("00") + ":" + Mathf.Floor(totalLife % 60).ToString("00");
    }
	
	// Update is called once per frame
	void Update () {
        currentLife += Time.deltaTime;
        if (currentLife >= totalLife)
        {
            playerObject.SetActive(false);
            peopleManager.OnPlayerDeath();
        }
        else if (!deathSignalFired && currentLife > totalLife - deathTime)
        {
            peopleManager.OnPlayerGoingToDie(deathTime);
            deathSignalFired = true;
        }
        currentTimeTextUI.text = Mathf.Floor(currentLife / 60).ToString("00") + ":" + Mathf.Floor(currentLife % 60).ToString("00");
        progressBarUI.value = currentLife / totalLife;
    }

    public void Restart()
    {

    }
}

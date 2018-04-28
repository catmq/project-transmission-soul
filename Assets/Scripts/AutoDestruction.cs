using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour {

    ParticleSystem p;
	// Use this for initialization
	void Start () {
        p = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!p.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}

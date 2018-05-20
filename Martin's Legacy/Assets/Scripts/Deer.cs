using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour {

    public GameObject andelie;

    Animator an;
	// Use this for initialization
	void Start () {
        an = GetComponent<Animator>();		
	}
	
	// Update is called once per frame
	void Update () {
		if( transform.position.x < andelie.transform.position.x)
        {
            an.SetInteger("turn",1);
        }else
        {
            an.SetInteger("turn", -1);
        }
    }
}

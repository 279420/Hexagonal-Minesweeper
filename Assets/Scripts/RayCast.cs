using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown (0)){ 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if ( Physics.Raycast (ray,out hit,100.0f)) {
				//StartCoroutine((hit.transform));
				//Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
				hit.transform.GetComponent<Hexagon>().show();
			}
		}
	}
}
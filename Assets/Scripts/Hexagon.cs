using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour {

	public GameObject[] neighboors = new GameObject[6];
	private int currentNeighboor = 0; //we dont need to have order preserved at all.
	public bool mine=false;
	public int explosiveNeighboors=0;
	public bool hidden = true;
	void Start () {
		
	}
	public GameObject getNeighboor(int index){
		return neighboors [index];
	}
	public void addNeighboor(GameObject obj){
		if (currentNeighboor < neighboors.Length) {
			bool found = false;
			for (int i = 0; i < currentNeighboor; i++) {
				if (neighboors [i]!=null && neighboors [i].Equals (obj)) {
					found = true;
				}
			}
			if (!found) {
				neighboors [currentNeighboor] = obj;
				currentNeighboor++;
			}
		}
	}
	public void addNeighboors(GameObject[] obj){
		for (int i = 0; i < obj.Length; i++) {
			if (obj [i] != null) {
				neighboors [currentNeighboor] = obj [i];
				currentNeighboor++;
			}
		}
	}
	public void setNeighboors(GameObject[] obj){
		neighboors = obj;
		currentNeighboor = 0;
		for (int i = 0; i < neighboors.Length; i++) {
			if (neighboors [i] != null) {
				currentNeighboor++;
			}
		}
	}
	public void setMine(bool val){
		mine = val;
	}
	public bool isMine(){
		return mine;
	}
	public int getExplosiveNeighboors(){
		return explosiveNeighboors;
	}
	public void updateExplosiveNeighboors(){
		explosiveNeighboors = 0;
		for (int i = 0; i < currentNeighboor; i++) {
			if (neighboors [i] != null) {
				if (neighboors [i].GetComponent<Hexagon> ().isMine ()) {
					explosiveNeighboors++;
				}
			}
		}
		if (!hidden) {
			if (mine) {
				this.GetComponentInChildren<TextMesh> ().text = "M";
			} else {
				this.GetComponentInChildren<TextMesh> ().text = "" + explosiveNeighboors;
			}
		}
	}
	public void show(){
		hidden = false;
		revelNeighboors ();
		updateExplosiveNeighboors ();
	}
	public bool isHidden(){
		return hidden;
	}
	public void revelNeighboors(){
		for (int i = 0; i < currentNeighboor; i++) {
			if (neighboors [i] != null) {
				if (neighboors [i].GetComponent<Hexagon> ().getExplosiveNeighboors () == 0 && !neighboors [i].GetComponent<Hexagon> ().isMine () && neighboors [i].GetComponent<Hexagon> ().isHidden ()) {
					neighboors [i].GetComponent<Hexagon> ().show ();
				}
			}
		}
	}
}
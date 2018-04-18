using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject hexagonObj;
	public GameObject[] hexagons;
	public GameObject[] faces;
	public int[] facesSides;
	void Start () {
		if (faces != null) {
			for (int i = 0; i < faces.Length; i++) {
				hexagons = DrawHexagonGrid.drawHexagons (faces [i].transform, hexagonObj, 0f, 0f, 4, 6,facesSides[i],i);
				setMines (20);
				updateCounters ();
			}
		} else {
			Debug.Log ("set faces pls");
		}
	}
	private void setMines(int count){
		GameObject[] mines = new GameObject[count];
		int index = Random.Range (0, hexagons.Length - 1);
		for (int i = 0; i < mines.Length; i++) {
			while (hexagons [index].GetComponent<Hexagon> ().isMine ()) {
				index = Random.Range (0, hexagons.Length - 1);
			}
			hexagons [index].GetComponent<Hexagon> ().setMine (true);
		}
	}
	private void updateCounters(){
		for (int i = 0; i < hexagons.Length; i++) {
			if (hexagons [i] != null) {
				hexagons [i].GetComponent<Hexagon> ().updateExplosiveNeighboors ();
			}
		}
	}
}
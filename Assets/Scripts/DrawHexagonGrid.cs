using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHexagonGrid : MonoBehaviour {

	public static GameObject hexagon;
	public static GameObject drawHexagon(Transform parent,double centerX,double centerZ,double scale,double rotate,string name,GameObject[] neighboors){
		hexagon.transform.localScale.Set((float)scale,(float)scale,(float)scale);
		hexagon.GetComponent<MeshRenderer> ().enabled = true;
		GameObject newObj = Instantiate(hexagon,new Vector3((float)centerX, (float)0, (float)centerZ),hexagon.transform.rotation);
		hexagon.GetComponent<MeshRenderer> ().enabled = false;
		newObj.name = name;
		newObj.transform.SetParent (parent);
		newObj.GetComponent<Hexagon> ().setNeighboors (neighboors);
		for (int i = 0; i < neighboors.Length; i++) {
			if (neighboors [i] != null) {
				neighboors [i].GetComponent<Hexagon> ().addNeighboor (newObj);
			}
		}
		return newObj;
		/*scale = 1;
		for (int ii = 0; ii < sideCount; ii++) {
			Vector3 vectStart= new Vector3();
			Vector3 vectLast= new Vector3();
			Vector3 vectThis;
			double radians;
			for (int i = 0; i < sideCount; i++) {
				radians = 2 * Mathf.PI * i * 1/sideCount;
				if (sideCount % 2 == 1) {
					radians -=Mathf.PI;
				}
				vectThis=new Vector3 (centerX+Mathf.Cos (radians) * scale, 0,centerZ+ Mathf.Sin (radians) * scale);
				if (i > 0) {
					Debug.DrawLine (vectThis, vectLast);
				} else {
					vectStart = vectThis;0
				}
				vectLast = vectThis;
			}
			Debug.DrawLine (vectLast, vectStart);
		}*/
	}
	public static GameObject[] drawHexagons(Transform parent,double centerX,double centerZ,double scale,int layers,GameObject[] hexagons,int sideCount,int faceID){
		int additionalHex = 1;
		for (int i = 0; i < layers; i++) {
			additionalHex += i * sideCount;
		}
		GameObject[] output=new GameObject[additionalHex+hexagons.Length+1000];
		for (int i = 0; i < hexagons.Length; i++) {
			output [i] = hexagons [i];
		}
		GameObject[] neighboors = new GameObject[sideCount];
		int temp;
		int k = hexagons.Length;
		for(int i=0;i<(2*layers-1);i++){
			for(int j=0;j<(2*layers-1);j++){
				//TODO: add an if statement to stop the hexagons that are above the diagnals from the top and bottom hexagons.
				//j is x startubg from left
				//i is y starting from top
				//if(j>=Mathf.Abs((float)i-(2*layers-1)/2)-1){
				//	if (j<=(2*layers-1)-Mathf.Abs((float)i-(2*layers-1)/2)+1) {
				if(layers-1){
						temp = 0;
						centerX = i * scale;
						centerZ = j * scale - (i % 2 == 0 ? scale / 2 : 0);
						neighboors = new GameObject[sideCount];
						neighboors [temp] = GameObject.Find ("hexagon_"+ faceID+"_"+ i + "_" + (j-1));
						temp++;
						//neighboors [temp] = GameObject.Find ("hexagon_"+ faceID+"_"+ i + "_" + (j+1));
						//temp++;
						neighboors [temp] = GameObject.Find ("hexagon_"+ faceID+"_"+ (i-1) + "_" + (i%2==0?j-1:j+1));
						temp++;
						neighboors [temp] = GameObject.Find ("hexagon_"+ faceID+"_"+ (i-1) + "_" + j);
						temp++;
						output[k]=drawHexagon (parent, centerX, centerZ, scale, 0, "hexagon_" + faceID+"_"+i + "_" + j, neighboors);
						k++;
				//	}
				}
			}
		}
		return output;
	}
	public static GameObject[] drawHexagons(Transform parent,GameObject gameObj,double centerX,double centerZ,double scale,int layers,int sideCount,int faceID){
		hexagon = gameObj;
		return drawHexagons (parent,centerX, centerZ, scale, layers,new GameObject[0],sideCount,faceID);
	}
}
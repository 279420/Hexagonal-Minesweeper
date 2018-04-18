using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHexagon : MonoBehaviour {

	public static GameObject hexagon;
	public static GameObject drawHexagon(Transform parent,double centerX,double centerZ,double scale,double rotate,string name,GameObject[] neighboors){
		hexagon.transform.localScale.Set((float)scale,(float)scale,(float)scale);
		hexagon.GetComponent<MeshRenderer> ().enabled = true;
		GameObject newObj = Instantiate(hexagon,new Vector3((float)centerX, 0, (float)centerZ),hexagon.transform.rotation);
		hexagon.GetComponent<MeshRenderer> ().enabled = false;
		newObj.name = name;
		newObj.transform.SetParent (parent);
		newObj.GetComponent<Hexagon> ().addNeighboors (neighboors);
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
	public static GameObject[] drawHexagons(Transform parent,double centerX,double centerZ,double scale,int layers,int currentLayer,GameObject[] hexagons,int sideCount,int faceID){
		GameObject[] output=new GameObject[sideCount*currentLayer+hexagons.Length];
		for (int i = 0; i < hexagons.Length; i++) {
			output [i] = hexagons [i];
		}
		double rotate=0;
		GameObject[] neighboors = new GameObject[sideCount];
		if (currentLayer == 0) {
			output[0]=drawHexagon (parent,centerX, centerZ, scale, rotate,"hexagon_"+faceID+"_"+currentLayer+"_"+0,neighboors);
		}
		//rotate = 0;
		rotate -= 2f * Mathf.PI * 1 / (sideCount * currentLayer);
		double offNextRadii = 0;
		bool goingUp = true;
		bool next = true;
		double radii = 0;
		int j=0;
		for (int i = 1; i <= sideCount * currentLayer; i++) {
			rotate = i* 2f * Mathf.PI * 1 / (sideCount * currentLayer);
			//centerX = 2f*(Mathf.Sqrt ((hexagonX)*(hexagonX) + (hexagonY*hexagonY))*Mathf.Cos(Mathf.PI * 1 / (sideCount))) * Mathf.Cos (1 / (sideCount) / 2+rotate+Mathf.PI * 1 / (sideCount));
			//centerZ = 2f*(Mathf.Sqrt ((hexagonX)*(hexagonX) + (hexagonY*hexagonY))*Mathf.Cos(Mathf.PI * 1 / (sideCount))) * Mathf.Sin (1 / (sideCount) / 2+rotate+Mathf.PI * 1 / (sideCount));
			radii=(double)Mathf.Sqrt ((float)((scale * currentLayer)*(scale * currentLayer)));
			if(offNextRadii!=0){
				radii -= .2f;
				if (currentLayer > 2) {
					radii -= .1f;
				}
			}
			radii -= .1f * offNextRadii;
			radii -= currentLayer * .175f;
			if (currentLayer > 1) {
				if (goingUp) {
					if (Mathf.Ceil ((currentLayer - 1) / 2f) == offNextRadii) {
						//rotate-=(goingUp?1:-1)*.05f;
					}
				}
			}
			centerX = 2f*(radii*Mathf.Cos((float)(Mathf.PI * 1 / (sideCount)))) * Mathf.Cos ((float)(1 / (sideCount) / 2+rotate+Mathf.PI * 1 / (sideCount)));
			centerZ = 2f*(radii*Mathf.Cos((float)(Mathf.PI * 1 / (sideCount)))) * Mathf.Sin ((float)(1 / (sideCount) / 2+rotate+Mathf.PI * 1 / (sideCount)));
			if (currentLayer > 1) {
				if (goingUp) {
					if (Mathf.Ceil ((currentLayer - 1) / 2f) == offNextRadii) {
						//rotate+=(goingUp?1:-1)*.05f;
					}
				}
			}
				
			neighboors = new GameObject[sideCount];
			if (currentLayer != 0) {
				j = 0;
				if ((i - 1) != 0) {
					neighboors[j]=GameObject.Find ("hexagon_"+faceID+"_"+currentLayer+"_"+(i-2));
					j++;
				}
				if (i == sideCount * currentLayer) {
					neighboors[j]=GameObject.Find ("hexagon_"+faceID+"_"+currentLayer+"_"+0);
					j++;
				}
				if (currentLayer > 1) {
					neighboors [j] = GameObject.Find ("hexagon_"+ faceID+"_"+ (currentLayer - 1) + "_" + (Mathf.Floor ((float)((rotate / (2f * Mathf.PI * 1 / (sideCount * (currentLayer - 1)))))) - 1));
					j++;
					neighboors [j] = GameObject.Find ("hexagon_"+ faceID+"_"+ (currentLayer - 1) + "_" + (Mathf.Ceil ((float)((rotate / (2f * Mathf.PI * 1 / (sideCount * (currentLayer - 1)))))) - 1));
					j++;
				} else {
					neighboors [j] = GameObject.Find ("hexagon_"+ faceID+"_"+ (currentLayer - 1) + "_" + 0);
					j++;
				}
			}
			output[hexagons.Length-1+i]=drawHexagon (parent,centerX, centerZ, scale,rotate,"hexagon_"+faceID+"_"+currentLayer+"_"+(i-1),neighboors);
			if (currentLayer > 1) {
				if (goingUp) {
					if (next) {
						offNextRadii++;
					}
					if (Mathf.Ceil((currentLayer - 1)/2f) == offNextRadii) {
						if (next && ((currentLayer - 1) % 2 == 0)) {
							next = false;
						} else {
							goingUp = false;
							next = true;
						}
					}
				} else {
					offNextRadii--;
					if (0 == offNextRadii) {
						goingUp = true;
					}
				}
			}
		}
		if (currentLayer != layers) {
			return drawHexagons (parent,centerX, centerZ, scale, layers, currentLayer + 1, output,sideCount,faceID);
		}
		return output;
	}
	public static GameObject[] drawHexagons(Transform parent,GameObject gameObj,double centerX,double centerZ,double scale,int layers,int sideCount,int faceID){
		hexagon = gameObj;
		return drawHexagons (parent,centerX, centerZ, scale, layers, 0,new GameObject[1],sideCount,faceID);
	}
}
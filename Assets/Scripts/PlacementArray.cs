using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArray : MonoBehaviour {

	public GameObject source;
	public bool expandX, expandY, expandZ;
	public int countX, countY, countZ;
	public float offsetX, offsetY, offsetZ;

	// Use this for initialization
	void Start () {
		countX = expandX ? countX : 1; 
		countY = expandY ? countY : 1; 
		countZ = expandZ ? countZ : 1; 

		for (int x = 0; x < countX; x++) {
			Vector3 strideX = x*offsetX*Vector3.right;
			for (int y = 0; y < countY; y++) {
				Vector3 strideY = y*offsetY*Vector3.up;
				for (int z = 0; z < countZ; z++) {
					Vector3 strideZ = z*offsetZ*Vector3.forward;

					if (!(x == 0 && y == 0 && z == 0)) {
						Instantiate(source,
							source.transform.position + source.transform.rotation*(strideX + strideY + strideZ),
							source.transform.rotation);
					}
				}
			}
		}
	}
}

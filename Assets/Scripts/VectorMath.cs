using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath {
	public static Vector3 Flatten(Vector3 v) {
		Vector3 res = new Vector3();
		res.x = v.x; res.z = v.z;
		return res;
	}
}

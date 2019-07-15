using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour {

	private HashSet<GameObject> collisions;
	private HashSet<GameObject> colliders;
	private LinkedList<GameObject> collected, irregular;
	private Rigidbody rigidbody;
	private float startVolume;
	private float currentVolume = 0f;
	public float growthFactor = 1f; // Increases volume linearly to the grow factor
	// Can be used to make the katamari grow faster (beware the holes in it)
	public float irregularBound; // Cutoff objects are no longer irregular,
	// too low values make the katamari too cumbersome to roll
	public float volumeFraction; // Avoids picking up too large objects

	void Start () {
		collisions = new HashSet<GameObject>();
		colliders = new HashSet<GameObject>();
		collected = new LinkedList<GameObject>();
		irregular = new LinkedList<GameObject>();
		startVolume = 4f/3f*Mathf.PI*Mathf.Pow(gameObject.GetComponent<Stats>().length/2,3);

		irregularBound = 0.04f;
		volumeFraction = 0.15f;
	}

	void FixedUpdate() {
		HashSet<GameObject> intersection = new HashSet<GameObject>(collisions);
		intersection.IntersectWith(colliders); // Object collides and has correct orientation
		bool change = false;
		foreach (GameObject g in intersection) {
			bool volumeCond = g.GetComponent<Stats>().GetVolume() <= (startVolume + currentVolume)*volumeFraction;
			if (volumeCond) {
				change = true;
				Attach(g);
			}
		}
		if (change) {
			//Recalculate radius and check on irregulars
			float radius = Mathf.Pow((3f*(startVolume + currentVolume*growthFactor))/(4f*Mathf.PI), 1f/3f);
			transform.GetComponent<SphereCollider>().radius = radius;
			transform.GetComponent<Stats>().length = 2*radius;
			FixIrregular();
		}
	}

	private void FixIrregular() {
		LinkedListNode<GameObject> cur = irregular.Last;
		while (cur != null) {
			GameObject g = cur.Value;
			cur = cur.Previous;
			if (g.GetComponent<Stats>().GetVolume() <= (startVolume + currentVolume)*irregularBound) {
				Destroy(g.GetComponent<MeshCollider>());
				irregular.Remove(g);
			}
		}
	}

	private void Attach(GameObject g) {
		currentVolume += g.GetComponent<Stats>().GetVolume();
		bool lengthCond = g.GetComponent<Stats>().GetVolume() > (startVolume + currentVolume)*irregularBound;
		if (lengthCond) {
			irregular.AddFirst(g);
		} else {
			g.GetComponent<MeshCollider>().enabled = false;
		}
		rigidbody = g.GetComponent<Rigidbody>();
		Destroy(g.GetComponent<Rigidbody>());
		g.GetComponent<SphereCollider>().enabled = false;
		collected.AddLast(g);
		colliders.Remove(g);
		collisions.Remove(g);
		g.transform.parent = gameObject.transform;
		g.layer = 10;
	}

	private void Detach(GameObject g) {
		currentVolume -= collected.Last.Value.GetComponent<Stats>().GetVolume()*growthFactor;

		g.GetComponent<SphereCollider>().enabled = true;
		g.GetComponent<MeshCollider>().enabled = true;
		g.GetComponent<Rigidbody>().isKinematic = false;
		collected.Remove(g);
		colliders.Remove(g);
		collisions.Remove(g);
		g.transform.parent = null;
		g.GetComponent<Rigidbody>().velocity +=
			VectorMath.Flatten(g.transform.position - gameObject.transform.position) +
			3*Vector3.up;
	}

	IEnumerator Toggle(SphereCollider col) {
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds (2);
        gameObject.GetComponent<Collider>().enabled = true;
    }

	void OnTriggerEnter(Collider collider)
    {
		if (gameObject.CompareTag("Katamari")) {
			colliders.Add(collider.gameObject);
		}
    }

	void OnTriggerExit(Collider collider)
    {
        colliders.Remove(collider.gameObject);
    }

	void OnCollisionEnter(Collision collision) {
		collisions.Add(collision.gameObject);
	}

	void OnCollisionExit(Collision collision) {
		collisions.Remove(collision.gameObject);
	}

	void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gameObject.GetComponent<Stats>().length/2);
    }
}

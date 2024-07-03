using UnityEngine;
using System.Collections;

public class Prikol : MonoBehaviour {
	Transform trans;
	public float speed;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		trans.Rotate (new Vector3 (1, 0, 0) * speed * Time.deltaTime);
	}
}

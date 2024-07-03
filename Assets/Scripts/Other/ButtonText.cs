using UnityEngine;
using System.Collections;

public class ButtonText : MonoBehaviour {
	public bool hideOnOut = false;
	Camera cam;
	MeshRenderer mesh;
	Collider col;
	public bool pressed;
	public bool cursorIn;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		col = GetComponentInChildren<Collider>();
		mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hideOnOut) {
			RaycastHit hit;
			mesh.enabled = Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit) && hit.collider == col;
		} else {
			mesh.enabled = true;
		}
		RaycastHit hit_2;
		cursorIn = Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit_2) && hit_2.collider == col;
		pressed = cursorIn && Input.GetMouseButton (0);
	}
}

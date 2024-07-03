using UnityEngine;
using System.Collections;

public class CameraAxes : MonoBehaviour {
	public float maxX = 80;
	public float minX = 5;
	public float maxZoom = 20;
	public float minZoom = 2;
	public float zoomSens = 1;
	public Vector3 euler;
	public float scroll;
	public Transform trans;
	public Transform child;
	public Camera cam;

	// Use this for initialization
	void Start () {
		trans = transform;
		child = trans.GetChild (0);
		cam = child.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		euler.z = 0;
		float x = -Input.GetAxis ("Mouse Y");
		float y = Input.GetAxis ("Mouse X");
		euler += new Vector3 (x, y, 0) * Time.deltaTime * 50;
		euler.x = Mathf.Clamp (euler.x,minX, maxX);
		trans.eulerAngles = euler;
		float scrollD = Input.mouseScrollDelta.y;
		scroll -= scrollD * zoomSens;
		scroll = Mathf.Clamp (scroll, minZoom, maxZoom);
		if (!cam.orthographic) {
			child.localPosition = -Vector3.forward * scroll;
		} else {
			cam.orthographicSize = scroll;
			child.localPosition = -Vector3.forward * 200;
		}
	}
}













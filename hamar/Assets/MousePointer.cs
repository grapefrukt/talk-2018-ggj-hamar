using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

	static Plane plane = new Plane(Vector3.forward, Vector3.zero);

	// Update is called once per frame
	void Update () {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float dist;
		if (!plane.Raycast(ray, out dist)) throw new Exception("aim raycast hit nothing. that's bad.");
		transform.position = ray.GetPoint(dist);
	}
}

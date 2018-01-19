using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierController : MonoBehaviour {

	static Plane plane = new Plane(Vector3.forward, Vector3.zero);
	public Vector2[] handles = new Vector2[4];

	int grabbedHandleIndex = -1;
	Vector2 grabbedHandleOffset = Vector2.zero;
	const float maxGrabDistance = 0.3f;

	public bool animateProgress = true;
	[Range(0, 1)]
	public float progress = 0;

	void Update () {

		if (animateProgress) progress = Mathf.PingPong(Time.time * .25f, 1);

		GeometryDraw.Clear(gameObject);
		GeometryDraw.DrawLine(gameObject, new []{a, d}, .04f, Color.white);

		GeometryDraw.DrawCircle(gameObject, b.x, b.y, .1f, Color.gray);
		GeometryDraw.DrawCircle(gameObject, c.x, c.y, .1f, Color.gray);

		var ad = Vector2.Lerp(a, d, progress);
		GeometryDraw.DrawCircle(gameObject, ad.x, ad.y, .1f, Color.magenta);

		var ab = Vector2.Lerp(a, b, progress);
		var bc = Vector2.Lerp(b, c, progress);
		var cd = Vector2.Lerp(c, d, progress);

		var abbc = Vector2.Lerp(ab, bc, progress);
		var bccd = Vector2.Lerp(bc, cd, progress);
		var abbcbccd = Vector2.Lerp(abbc, bccd, progress);

		GeometryDraw.DrawCircle(gameObject, abbcbccd.x, abbcbccd.y, .1f, Color.green);

		GeometryDraw.Finalize(gameObject);

		if (Input.GetMouseButtonDown(0)) GrabHandle();
		else if (grabbedHandleIndex >= 0 && Input.GetMouseButton(0)) MoveHandle();
		else ReleaseHandle();
	}

	void MoveHandle() {
		var mouse = ScreenToWorld(Input.mousePosition);
		handles[grabbedHandleIndex] = mouse + grabbedHandleOffset;
	}

	void GrabHandle() {
		var nearestDist = maxGrabDistance;
		grabbedHandleIndex = -1;

		var mouse = ScreenToWorld(Input.mousePosition);

		for (var i = 0; i < handles.Length; i++) {
			var d = Vector2.Distance(handles[i], mouse);
			Debug.Log(i + ": " + d);
			if (d > nearestDist) continue;
			nearestDist = d;
			grabbedHandleIndex = i;
		}

		if (grabbedHandleIndex == -1) return;

		grabbedHandleOffset = handles[grabbedHandleIndex] - mouse;
	}

	void ReleaseHandle() {
		grabbedHandleIndex = -1;
	}
	
	static Vector2 ScreenToWorld(Vector2 screenPos) {
		var ray = Camera.main.ScreenPointToRay(screenPos);
		float dist;
		if (!plane.Raycast(ray, out dist)) throw new Exception("aim raycast hit nothing. that's bad.");
		return ray.GetPoint(dist);
	}

	public Vector2 a { get { return handles[0]; } }
	public Vector2 b { get { return handles[1]; } }
	public Vector2 c { get { return handles[2]; } }
	public Vector2 d { get { return handles[3]; } }
}

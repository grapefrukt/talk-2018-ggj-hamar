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
	public float animationSpeed = 1f;
	[Range(0, 1)]
	public float progress = 0;

	float time = 0;

	public bool drawSimpleLine = true;
	public bool drawControlHandles = true;
	public bool drawBezierOutline = true;
	public bool drawSecondOrder = true;
	public bool drawThirdOrder = true;
	public bool drawCurveDot = true;
	public bool drawCurve = true;

	void Update () {

		if (animateProgress) {
			time += Time.deltaTime * animationSpeed * animationSpeed;
			progress = Mathf.PingPong(time, 1);
		}

		var ad = Vector2.Lerp(a, d, progress);
		var ab = Vector2.Lerp(a, b, progress);
		var bc = Vector2.Lerp(b, c, progress);
		var cd = Vector2.Lerp(c, d, progress);
		var abbc = Vector2.Lerp(ab, bc, progress);
		var bccd = Vector2.Lerp(bc, cd, progress);
		var abbcbccd = Vector2.Lerp(abbc, bccd, progress);

		GeometryDraw.Clear(gameObject);

		GeometryDraw.DrawCircle(gameObject, a.x, a.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierPrimaryLine, 0, 1f);
		GeometryDraw.DrawCircle(gameObject, d.x, d.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierPrimaryLine, 0, 1f);

		// draw simple lerp line
		if (drawSimpleLine) { 
			GeometryDraw.DrawLine(gameObject, new []{a, d}, ColorSettings.instance.bezierSimpleLerpThickness, ColorSettings.instance.bezierPrimaryLine);
			// draw simple lerp dot
			GeometryDraw.DrawCircle(gameObject, ad.x, ad.y, ColorSettings.instance.bezierSimpleLerpDotSize, ColorSettings.instance.bezierHandle, 0, 1f);
		}

		// draw handles
		if (drawControlHandles) {
			GeometryDraw.DrawCircle(gameObject, b.x, b.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierSecondaryHandle, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, c.x, c.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierSecondaryHandle, 0, 1f);
		}

		// draw bezier lines
		if (drawBezierOutline) {
			GeometryDraw.DrawLine(gameObject, new[] { a, b }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
			GeometryDraw.DrawLine(gameObject, new[] { b, c }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
			GeometryDraw.DrawLine(gameObject, new[] { c, d }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
		}

		// draw bezier interpolation lines
		if (drawSecondOrder) GeometryDraw.DrawLine(gameObject, new[] { ab, bc }, ColorSettings.instance.bezierTertiaryLineThickness, ColorSettings.instance.bezierTertiaryLine);
		if (drawSecondOrder) GeometryDraw.DrawLine(gameObject, new[] { bc, cd }, ColorSettings.instance.bezierTertiaryLineThickness, ColorSettings.instance.bezierTertiaryLine);
		if (drawThirdOrder) GeometryDraw.DrawLine(gameObject, new[] { abbc, bccd }, ColorSettings.instance.bezierQuarternaryLineThickness, ColorSettings.instance.bezierQuaternaryLine);

		// draw bezier dot
		if (drawCurveDot) GeometryDraw.DrawCircle(gameObject, abbcbccd.x, abbcbccd.y, .1f, ColorSettings.instance.bezierCurve, 0, 1f);

		// draw bezier curve
		if (drawCurve) DrawBezier(64, ColorSettings.instance.bezierCurveThickness, ColorSettings.instance.bezierCurveHandle);
		
		GeometryDraw.Finalize(gameObject);

		if (Input.GetMouseButtonDown(0)) GrabHandle();
		else if (grabbedHandleIndex >= 0 && Input.GetMouseButton(0)) MoveHandle();
		else ReleaseHandle();
	}

	void DrawBezier(int numSamples, float width, Color color) {
		var prev = a;
		for (var i = 0; i < numSamples; i++) {
			var progress = (float)i / (numSamples - 1);
			var ab = Vector2.Lerp(a, b, progress);
			var bc = Vector2.Lerp(b, c, progress);
			var cd = Vector2.Lerp(c, d, progress);

			var abbc = Vector2.Lerp(ab, bc, progress);
			var bccd = Vector2.Lerp(bc, cd, progress);
			var abbcbccd = Vector2.Lerp(abbc, bccd, progress);

			GeometryDraw.DrawLine(gameObject, new []{prev, abbcbccd}, width, color);
			GeometryDraw.DrawCircle(gameObject, abbcbccd.x, abbcbccd.y, width / 2, color, 0, .2f);

			prev = abbcbccd;
		}
		
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

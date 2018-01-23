using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierController : MonoBehaviour {

	static Plane plane = new Plane(Vector3.forward, Vector3.zero);
	public Vector2[] handles = new Vector2[4];

	int grabbedHandleIndex = -1;
	Vector2 grabbedHandleOffset = Vector2.zero;
	const float MaxGrabDistance = 2.0f;

	public bool animateProgress = true;
	[Range(0, 1)]
	public float animationSpeed = 1f;
	[Range(0, 1)]
	public float progress = 0;

	float time = 0;

	public bool drawSimpleLine = true;
	public bool drawSimpleLerp = true;

	public bool drawCubicHandle = true;
	public bool drawCubicOutline = true;
	public bool drawCubicLerps = true;
	public bool drawCubicLines = true;
	public bool drawCubicCurveDot = true;
	public bool drawCubicCurve = true;

	public bool drawQuadraticHandles = true;
	public bool drawQuadraticOutline = true;
	public bool drawQuadraticLerps = true;
	public bool drawQuadraticSecondOrderLines = true;
	public bool drawQuadraticThirdOrderHandles = true;
	public bool drawQuadraticThirdOrderLines = true;
	public bool drawQuadraticCurveDot = true;
	public bool drawQuadraticCurve = true;

	void Update () {

		if (animateProgress) {
			time += Time.deltaTime * animationSpeed * animationSpeed;
			progress = Mathf.PingPong(time, 1);
		}

		var ad = Vector2.Lerp(a, d, progress);
		var ab = Vector2.Lerp(a, b, progress);
		var bc = Vector2.Lerp(b, c, progress);
		var cd = Vector2.Lerp(c, d, progress);
		var bd = Vector2.Lerp(b, d, progress);
		var abbc = Vector2.Lerp(ab, bc, progress);
		var bccd = Vector2.Lerp(bc, cd, progress);
		var abbd = Vector2.Lerp(ab, bd, progress);
		var abbcbccd = Vector2.Lerp(abbc, bccd, progress);

		GeometryDraw.Clear(gameObject);

		GeometryDraw.DrawCircle(gameObject, a.x, a.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierPrimaryLine, 0, 1f);
		GeometryDraw.DrawCircle(gameObject, d.x, d.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierPrimaryLine, 0, 1f);

		// draw simple lerp line
		if (drawSimpleLine) { 
			GeometryDraw.DrawLine(gameObject, new []{a, d}, ColorSettings.instance.bezierSimpleLerpThickness, ColorSettings.instance.bezierPrimaryLine);
		}

		// draw simple lerp dot
		if (drawSimpleLerp) {
			GeometryDraw.DrawCircle(gameObject, ad.x, ad.y, ColorSettings.instance.bezierSimpleLerpDotSize, ColorSettings.instance.bezierHandle, 0, 1f);
		}


		if (drawCubicHandle) {
			GeometryDraw.DrawCircle(gameObject, b.x, b.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierSecondaryHandle, 0, 1f);
		}

		if (drawCubicOutline) {
			GeometryDraw.DrawLine(gameObject, new[] { a, b }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
			GeometryDraw.DrawLine(gameObject, new[] { b, d }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
		}

		if (drawCubicLerps) {
			GeometryDraw.DrawCircle(gameObject, ab.x, ab.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierTertiaryLine, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, bd.x, bd.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierTertiaryLine, 0, 1f);
		}

		if (drawCubicLines) {
			GeometryDraw.DrawLine(gameObject, new[] { ab, bd }, ColorSettings.instance.bezierTertiaryLineThickness, ColorSettings.instance.bezierTertiaryLine);
		}

		if (drawCubicCurveDot) {
			GeometryDraw.DrawCircle(gameObject, abbd.x, abbd.y, .1f, ColorSettings.instance.bezierCurve, -1f, 1f);
		}

		if (drawCubicCurve) {
			DrawCubicBezier(64, ColorSettings.instance.bezierCurveThickness, ColorSettings.instance.bezierCurveHandle);
		}
		
		// draw handles
		if (drawQuadraticHandles) {
			GeometryDraw.DrawCircle(gameObject, b.x, b.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierSecondaryHandle, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, c.x, c.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierSecondaryHandle, 0, 1f);
		}

		// draw bezier lines
		if (drawQuadraticOutline) {
			GeometryDraw.DrawLine(gameObject, new[] { a, b }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
			GeometryDraw.DrawLine(gameObject, new[] { b, c }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
			GeometryDraw.DrawLine(gameObject, new[] { c, d }, ColorSettings.instance.bezierSecondaryLineThickness, ColorSettings.instance.bezierSecondaryLine, 1);
		}

		if (drawQuadraticLerps) {
			GeometryDraw.DrawCircle(gameObject, ab.x, ab.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierTertiaryLine, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, bc.x, bc.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierTertiaryLine, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, cd.x, cd.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierTertiaryLine, 0, 1f);
		}

		// draw bezier interpolation lines
		if (drawQuadraticSecondOrderLines) {
			GeometryDraw.DrawLine(gameObject, new[] { ab, bc }, ColorSettings.instance.bezierTertiaryLineThickness, ColorSettings.instance.bezierTertiaryLine);
			GeometryDraw.DrawLine(gameObject, new[] { bc, cd }, ColorSettings.instance.bezierTertiaryLineThickness, ColorSettings.instance.bezierTertiaryLine);
		}

		if (drawQuadraticThirdOrderHandles) {
			GeometryDraw.DrawCircle(gameObject, abbc.x, abbc.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierQuaternaryLine, 0, 1f);
			GeometryDraw.DrawCircle(gameObject, bccd.x, bccd.y, ColorSettings.instance.bezierHandleDotSize, ColorSettings.instance.bezierQuaternaryLine, 0, 1f);
		}

		if (drawQuadraticThirdOrderLines) {
			GeometryDraw.DrawLine(gameObject, new[] { abbc, bccd }, ColorSettings.instance.bezierQuarternaryLineThickness, ColorSettings.instance.bezierQuaternaryLine);
		}

		// draw bezier dot
		if (drawQuadraticCurveDot) {
			GeometryDraw.DrawCircle(gameObject, abbcbccd.x, abbcbccd.y, .1f, ColorSettings.instance.bezierCurve, -1, 1f);
		}

		// draw bezier curve
		if (drawQuadraticCurve) {
			DrawQuadraticBezier(64, ColorSettings.instance.bezierCurveThickness, ColorSettings.instance.bezierCurveHandle);
		}
		
		GeometryDraw.Finalize(gameObject);

		if (Input.GetMouseButtonDown(0)) GrabHandle();
		else if (grabbedHandleIndex >= 0 && Input.GetMouseButton(0)) MoveHandle();
		else ReleaseHandle();
	}

	void DrawQuadraticBezier(int numSamples, float width, Color color) {
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

	void DrawCubicBezier(int numSamples, float width, Color color) {
		var prev = a;
		for (var i = 0; i < numSamples; i++) {
			var progress = (float)i / (numSamples - 1);
			var ab = Vector2.Lerp(a, b, progress);
			var bd = Vector2.Lerp(b, d, progress);
			var abbd = Vector2.Lerp(ab, bd, progress);

			GeometryDraw.DrawLine(gameObject, new[] { prev, abbd }, width, color);
			GeometryDraw.DrawCircle(gameObject, abbd.x, abbd.y, width / 2, color, 0, .2f);

			prev = abbd;
		}
	}

	void MoveHandle() {
		var mouse = ScreenToWorld(Input.mousePosition);
		handles[grabbedHandleIndex] = mouse + grabbedHandleOffset;
	}

	void GrabHandle() {
		var nearestDist = MaxGrabDistance;
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

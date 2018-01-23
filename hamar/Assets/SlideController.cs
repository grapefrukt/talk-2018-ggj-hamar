using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SlideController : MonoBehaviour {

	public int index = 0;

	public BezierController bezier;
	public PoissonController poisson;
	public UniformPoissonDiskSampler poissonSampler;
	public SpriteRenderer pythagoras;
	public SpriteRenderer twofold;
	public SpriteRenderer rymdkapsel;

	// Update is called once per frame
	void Update () {
		var delta = 0;
		if (Input.GetKeyDown(KeyCode.RightArrow)) delta++;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) delta--;
		if (delta == 0) return;

		if (index + delta < 0) return;

		SetSlide(index + delta);
	}

	void SetSlide(int newIndex) {
		Debug.Log(newIndex);
		index = newIndex;
		DisableEverything();

		var i = 0;
		if (i++ == index) {
			// empty
		} else if (i++ == index) {
			pythagoras.gameObject.SetActive(true);
		} else if (i++ == index) {
			twofold.gameObject.SetActive(true);
		} else if (i++ == index) {
			rymdkapsel.gameObject.SetActive(true);
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawSimpleLine = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.time = 0;
			bezier.drawSimpleLine = true;
			bezier.drawSimpleLerp = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicOutline = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicOutline = true;
			bezier.drawCubicLerps = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicOutline = true;
			bezier.drawCubicLerps = true;
			bezier.drawCubicLines = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicOutline = true;
			bezier.drawCubicLerps = true;
			bezier.drawCubicLines = true;
			bezier.drawCubicCurveDot = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicOutline = true;
			bezier.drawCubicLerps = true;
			bezier.drawCubicLines = true;
			bezier.drawCubicCurveDot = true;
			bezier.drawCubicCurve = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawCubicHandle = true;
			bezier.drawCubicCurve = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
			bezier.drawQuadraticThirdOrderLines = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
			bezier.drawQuadraticThirdOrderLines = true;
			bezier.drawQuadraticCurveDot = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
			bezier.drawQuadraticThirdOrderLines = true;
			bezier.drawQuadraticCurveDot = true;
			bezier.drawQuadraticCurve = true;
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticCurve = true;
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		//} else if (i++ == index) {
		} else {
			index = 0;
		}
	}

	void DisableEverything() {
		bezier.gameObject.SetActive(false);
		poisson.gameObject.SetActive(false);
		pythagoras.gameObject.SetActive(false);
		twofold.gameObject.SetActive(false);
		rymdkapsel.gameObject.SetActive(false);

		DisableAllBools(bezier);
		DisableAllBools(bezier);
		DisableAllBools(poissonSampler);

		bezier.animateProgress = true;
	}

	static void DisableAllBools(object target) {
		var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		var boolType = typeof(bool);
		foreach (var field in fields) {
			if (field.FieldType != boolType) continue;
			field.SetValue(target, false);
		}
	}
}

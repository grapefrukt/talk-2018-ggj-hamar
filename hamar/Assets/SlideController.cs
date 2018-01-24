using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using TMPro;

public class SlideController : MonoBehaviour {

	public int index = 0;
	float timeOffset = -1;

	public BezierController bezier;
	public PoissonController poisson;
	public UniformPoissonDiskSampler poissonSampler;
	public SpriteRenderer pythagoras;
	public SpriteRenderer twofold;
	public SpriteRenderer rymdkapsel; 
	public TextMeshPro notes;
	public TextMeshPro timer;

	void Start(){
		SetSlide(0);
	}

	// Update is called once per frame
	void Update () {

		var t = Time.time - timeOffset;
		var minutes = Mathf.Floor(t / 60);
		var seconds = Mathf.FloorToInt(t % 60);
		timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

		var delta = 0;
		if (Input.GetKeyDown(KeyCode.RightArrow)) delta++;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) delta--;
		if (delta == 0) return;

		if (index + delta < 0) return;

		SetSlide(index + delta);
	}

	void StartTimer(){
		timeOffset = Time.time;
	}

	void SetSlide(int newIndex) {
		Debug.Log(newIndex);
		index = newIndex;
		DisableEverything();

		var note = "";

		var i = 0;
		if (i++ == index) {
			// empty
		} else if (i++ == index) {
			StartTimer();
			note = "Today I’m going to talk about some fancy math tricks you can use in your games. Don’t worry if you’re not a math person, I hope it’s going to make sense regardless.";
		} else if (i++ == index) {
			note = "Game making in general and programming specifically involves a lot of abstraction, someone else has done a bunch of work you can make use of to make even cooler things.";
		} else if (i++ == index) {
			note = "History is full of clever people working out theorems and clever ways of solving problems that, as luck would have it, are really useful when making games thousands of years later.";
		} else if (i++ == index) {
			pythagoras.gameObject.SetActive(true);
			note = "That brings us to this dude.\nPythagoras.";
		} else if (i++ == index) {
			pythagoras.gameObject.SetActive(true);
			note = "He not only came up with what we today call the pythagorean theorem, which involves the length of the sides in a right angle triangle. Incredibly useful stuff for games. He did this 2.5 thousand years ago.";
		} else if (i++ == index) {
			pythagoras.gameObject.SetActive(true);
			note = "He also didn’t like beans. Yes. Really. \nYou see him pictured here in a French manuscript from the 1500s going all “nope” to some fava-beans.";
		} else if (i++ == index) {
			note = "My name is Martin. I’m a solo indie developer and have been so for the past six years. I do a lot of things with my time, but most of it is spent programming. ";
		} else if (i++ == index) {
			note = "I have no proper education for this, I’m making things up as I go along. It’s been working out okay so far. ";
		} else if (i++ == index) {
			twofold.gameObject.SetActive(true);
			note = "Most recently, I released a game called twofold inc. but that’s almost exactly two years ago today. Since then, I’ve been on parental leave, started three new game projects and cancelled two of them.";
		} else if (i++ == index) {
			rymdkapsel.gameObject.SetActive(true);
			note = "Before that I made a game called rymdkapsel that people liked. That was my first commercial game and I was lucky enough to have it do really well.";
		} else if (i++ == index) {
			note = "The first trick we’re going to learn today is Bezier curves.";
		} else if (i++ == index) {
			note = "I’m sure you’re familiar with Bezier curves even if you don’t know them by name. They’re the smooth curves you can draw in almost any drawing program that have a couple of little handles that let you tweak the bend of the line.";
		} else if (i++ == index) {
			note = "You can make these yourself with a little bit of code and they’re really nice for many things. I like to use them when I need something to fly along a path that looks a bit like “it just flew out” but being able to control exactly where it goes. \n";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawSimpleLine = true;
			note = "These little dudes all start with the basic concept of linear interpolation. Which sounds a little bit complicated, but it isn’t too bad. ";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.time = 0;
			bezier.drawSimpleLine = true;
			bezier.drawSimpleLerp = true;
			note = "Essentially, you have a start value and an end value. You feed these two into a little function along with a value that represents how far along this distance (as a percentage) we want to go.";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawSimpleLine = true;
			bezier.drawSimpleLerp = true;
			note = "This is what’s usually called a lerp, short for linear interpolation. \nYou do this for both x and y values separately to get a two dimensional lerp. No biggie.";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.time = 0;
			bezier.drawSimpleLine = true;
			bezier.drawSimpleLerp = true;
			note = "Now, beziers are made by lerping your lerps.\nThere are a couple of variants when it comes to beziers, we’re going to look at cubic beziers.";
		/*} else if (i++ == index) {
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
			bezier.drawCubicCurve = true;*/
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			note = "To make them, we’re going to need two more points. These will be our control handles.";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			note = "Now we imagine three lines, from A to B to C to D.";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			note = "Then we lerp along these lines. That gives us three new points.";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			note = "Which in turn gets us two new lines. ";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
			note = "Which we can lerp along. ";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticOutline = true;
			bezier.drawQuadraticLerps = true;
			bezier.drawQuadraticSecondOrderLines = true;
			bezier.drawQuadraticThirdOrderHandles = true;
			bezier.drawQuadraticThirdOrderLines = true;
			bezier.drawQuadraticCurveDot = true;
			note = "That gives a final line, which we can lerp along too. Maybe you can see it already?";
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
			note = "The path that final lerp takes is our nice smooth curve. ";
		} else if (i++ == index) {
			bezier.gameObject.SetActive(true);
			bezier.drawQuadraticHandles = true;
			bezier.drawQuadraticCurve = true;
			note = "That’s a cubic bezier! A lerp of a lerp of a lerp.";
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

		notes.text = note;
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

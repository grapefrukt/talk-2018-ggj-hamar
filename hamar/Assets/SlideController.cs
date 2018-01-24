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
		SetSlide(26);
	}

	// Update is called once per frame
	void Update () {

		var t = Time.time - timeOffset;
		var minutes = Mathf.Floor(t / 60);
		var seconds = Mathf.FloorToInt(t % 60);
		timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			poisson.sampler.Step();
		}

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
			note = "He also didn’t like beans. \nYes. Really. \nYou see him pictured here in a French manuscript from the 1500s going all “nope” to some fava-beans.";
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
			bezier.drawSimpleLine = true;
			bezier.drawSimpleLerp = true;
			note = "Now, beziers are made by lerping your lerps.\nThere are a couple of variants when it comes to beziers, we’re going to look at cubic beziers.";
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
			bezier.time = 0;
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
			note = "Which in turn gets us two new imaginary lines. ";
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
			note = "That gives a final imaginary line, which we can lerp along too. Maybe you can see it already?";
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
		} else if (i++ == index) {
			note = "Now that your curves are in order, maybe you need to spawn enemies across a level, or flowers on a field or spots on a leopard. That’s where this next trick comes in.";
		} else if (i++ == index) {
			poisson.gameObject.SetActive(true);
			poisson.StopAndClear();
			note = "It’s a thing called Blue Noise. It’s very useful when you need to place things across a level or playfield in a random but uniform fashion. \n";
		} else if (i++ == index) {
			poisson.gameObject.SetActive(true);
			poisson.StopAndClear();
			poisson.GoRandom();
			note = "Pure random won’t do because you’ll get lumps of things in some places and large empty areas in others.";
		} else if (i++ == index) {
			poisson.gameObject.SetActive(true);
			poisson.StopAndClear();
			note = "Blue noise is a computer graphics term (not to be confused with the audio engineering term) that refers to a noise that is uniform but still random. ";
		} else if (i++ == index) {
			note = "One way to make this “blue noise” is Poisson Disk Sampling. Much like bezier curves this is a fairly simple concept to grasp but can be slightly trickier to implement well. ";
		} else if (i++ == index) {
			poisson.gameObject.SetActive(true);
			poisson.StopAndClear();
			poisson.seed = 17;
			poisson.renderRadius = true;
			poisson.renderBounds = true;
			poisson.renderSamples = true;
			poisson.renderColorized = true;
			poisson.sampler.waitOnPoint = true;
			poisson.sampler.waitOnSample = true;
			poisson.Restart();

			note = "The way it works is that we start with a random point. ";
		} else if (i++ == index) {
			poisson.gameObject.SetActive(true);
			poisson.renderRadius = true;
			poisson.renderBounds = true;
			poisson.renderSamples = true;
			poisson.renderColorized = true;
			poisson.sampler.waitOnPoint = true;
			poisson.sampler.waitOnSample = true;

			note = "Then we “sample” around this point a couple of times, adding any points that fall in a valid spot. ";
		} else if (i++ == index) {
			note = "A valid spot is any place that is far enough away from all other spots, yet within the bounds of the area we are filling. ";
		} else if (i++ == index) {
			note = "Once a point has done all its samples, we mark it as done and move on to the next point. ";
		} else if (i++ == index) {
			note = "Then we keep doing this until we no longer have any open points. ";
		} else if (i++ == index) {
			note = "With some luck, this means our target area is filled with points. ";
		} else if (i++ == index) {
			note = "There’s a lot of room to tweak this algorithm, you can adjust how far apart the samples are.";
		} else if (i++ == index) {
			note = "You can adjust how many samples are made. For a more computationally expensive but denser spacing. ";
		} else if (i++ == index) {
			note = "If you’re feeling frisky you can use a variable distance to make interesting patterns. ";
		} else if (i++ == index) {
			note = "The code for this entire presentation along with the Unity project, notes and everything is available at github.com/grapefrukt/talk-2018-ggj-hamar\n";
		} else if (i++ == index) {
			note = "ADVANCE AGAIN TO RESTART PRESENTATION!";
		} else {
			index = 0;
		}

		notes.text = note;
	}

	void DisableEverything() {
		bezier.gameObject.SetActive(false);
		//poisson.gameObject.SetActive(false);
		pythagoras.gameObject.SetActive(false);
		twofold.gameObject.SetActive(false);
		rymdkapsel.gameObject.SetActive(false);

		DisableAllBools(bezier);
		DisableAllBools(poisson);

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

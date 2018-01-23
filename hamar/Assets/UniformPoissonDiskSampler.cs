using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stolen from 
// http://theinstructionlimit.com/fast-uniform-poisson-disk-sampling-in-c

// Adapated from java source by Herman Tulleken
// http://www.luma.co.za/labs/2008/02/27/poisson-disk-sampling/

// The algorithm is from the "Fast Poisson Disk Sampling in Arbitrary Dimensions" paper by Robert Bridson
// http://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf

public enum SampleStrategy {
	oldest,
	newest,
	random
}

public class UniformPoissonDiskSampler : MonoBehaviour {

	const float SquareRootTwo = 1.41421356237f;
	const float TwoPi = Mathf.PI * 2;

	[HideInInspector]
	public Vector2 topLeft, bottomRight;
	Vector2 dimensions;

	[Range(.01f, 2)]
	public float minimumDistance;
	[Range(1, 100)]
	public int pointsPerIteration;

	float cellSize;
	int gridWidth, gridHeight;
	System.Random random;

	Vector2?[,] grid;

	[HideInInspector]
	public List<Vector2> activePoints;
	[HideInInspector]
	public List<Vector2> points;

	public Vector2 head { get; private set; }
	public List<Vector2> samples { get; private set; }
	[HideInInspector]
	public bool lastSampleSuccessful = false;

	public SampleStrategy sampleStrategy = SampleStrategy.oldest;
	public bool waitOnSample = true;
	public bool waitOnPoint = true;

	int sleepStep = 0;
	[HideInInspector]
	public float step = 0;
	public bool pause = true;
	[Range(0, 50)]
	public float speed = 0;

	public void Initialize(Vector2 topLeft, Vector2 lowerRight, int seed) {
		points = new List<Vector2>();

		this.topLeft = topLeft;
		bottomRight = lowerRight;

		dimensions = lowerRight - topLeft;
		cellSize = minimumDistance / SquareRootTwo;
		random = new System.Random(seed);
		gridWidth = (int)(dimensions.x / cellSize) + 1;
		gridHeight = (int)(dimensions.y / cellSize) + 1;

		grid = new Vector2?[gridWidth, gridHeight];
		activePoints = new List<Vector2>();

		samples = new List<Vector2>();

		step = 0;
		sleepStep = 0;
	}

	void Update() {
		if (pause) return;
		step += Time.deltaTime * speed;
	}

	bool Sleep(bool flag) {
		if (!flag) return false;
		if (sleepStep >= step) return true;
		sleepStep++;
		return false;
	}

	public IEnumerator Sample() {
		AddFirstPoint();
		while (Sleep(waitOnPoint)) yield return null;

		while (activePoints.Count != 0) {
			var index = 0;
			switch (sampleStrategy) {
				case SampleStrategy.oldest:
					index = 0;
					break;
				case SampleStrategy.newest:
					index = activePoints.Count - 1;
					break;
				case SampleStrategy.random:
					index = random.Next(activePoints.Count);
					break;
			}
			
			head = activePoints[index];
			samples.Clear();

			while (Sleep(waitOnPoint)) yield return null;

			for (var k = 0; k < pointsPerIteration; k++) {
				var success = AddNextPoint(head);
				lastSampleSuccessful = success;
				while (Sleep(waitOnSample)) yield return null;
			}

			//while (Sleep(waitOnPoint)) yield return null;
			activePoints.RemoveAt(index);
			samples.Clear();
		}
	}

	void AddFirstPoint(float forceX = -1, float forceY = -1) {
		var d = forceX >= 0 ? forceX : random.NextDouble();
		var xr = topLeft.x + dimensions.x * d;

		d = forceY >= 0 ? forceY : random.NextDouble();
		var yr = topLeft.y + dimensions.y * d;

		var p = new Vector2((float)xr, (float)yr);
			
		var index = Denormalize(p, topLeft, cellSize);

		grid[(int)index.x, (int)index.y] = p;

		activePoints.Add(p);
		points.Add(p);
	}

	bool AddNextPoint(Vector2 point) {
		var q = GenerateRandomAround(point, minimumDistance);

		samples.Add(q);

		if (!(q.x >= topLeft.x) || !(q.x < bottomRight.x) || !(q.y > topLeft.y) ||
		    !(q.y < bottomRight.y))
			return false;

		var tooClose = false;

		var qIndex = Denormalize(q, topLeft, cellSize);
			
		for (var i = (int)Math.Max(0, qIndex.x - 2); i < Math.Min(gridWidth, qIndex.x + 3) && !tooClose; i++)
		for (var j = (int)Math.Max(0, qIndex.y - 2); j < Math.Min(gridHeight, qIndex.y + 3) && !tooClose; j++)
			if (grid[i, j].HasValue && Vector2.Distance(grid[i, j].Value, q) < minimumDistance)
				tooClose = true;

		if (tooClose) return false;

		activePoints.Add(q);
		points.Add(q);
		grid[(int)qIndex.x, (int)qIndex.y] = q;
		return true;
	}

	Vector2 GenerateRandomAround(Vector2 point, float distance) {
		var d = random.NextDouble();
		var radius = distance + distance * d;

		d = random.NextDouble();
		var angle = TwoPi * d;

		var newX = radius * Math.Sin(angle);
		var newY = radius * Math.Cos(angle);

		return new Vector2((float)(point.x + newX), (float)(point.y + newY));
	}

	static Vector2 Denormalize(Vector2 point, Vector2 origin, double cellSize) {
		return new Vector2((int)((point.x - origin.x) / cellSize), (int)((point.y - origin.y) / cellSize));
	}
}
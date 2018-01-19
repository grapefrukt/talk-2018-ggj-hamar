﻿using System;
using System.Collections.Generic;
using UnityEngine;

// stolen from 
// http://theinstructionlimit.com/fast-uniform-poisson-disk-sampling-in-c

// Adapated from java source by Herman Tulleken
// http://www.luma.co.za/labs/2008/02/27/poisson-disk-sampling/

// The algorithm is from the "Fast Poisson Disk Sampling in Arbitrary Dimensions" paper by Robert Bridson
// http://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf

public class UniformPoissonDiskSampler {

	const int DefaultPointsPerIteration = 30;
	const int DefaultSeed = 0xfc495b;
	const float SquareRootTwo = 1.41421356237f;
	const float TwoPi = Mathf.PI * 2;

	Vector2 topLeft, lowerRight;
	readonly Vector2 dimensions;
	readonly float minimumDistance;
	readonly int pointsPerIteration;
	readonly float cellSize;
	readonly int gridWidth, gridHeight;
	readonly System.Random random;

	readonly Vector2?[,] grid;
	readonly List<Vector2> activePoints;

	public readonly List<Vector2> points;

	public UniformPoissonDiskSampler(Vector2 topLeft, Vector2 lowerRight, float minimumDistance, int pointsPerIteration = DefaultPointsPerIteration, int seed = DefaultSeed) {
		points = new List<Vector2>();

		this.topLeft = topLeft;
		this.lowerRight = lowerRight;
		this.minimumDistance = minimumDistance;
		this.pointsPerIteration = pointsPerIteration;

		dimensions = lowerRight - topLeft;
		cellSize = minimumDistance / SquareRootTwo;
		random = new System.Random(seed);
		gridWidth = (int)(dimensions.x / cellSize) + 1;
		gridHeight = (int)(dimensions.y / cellSize) + 1;

		grid = new Vector2?[gridWidth, gridHeight];
		activePoints = new List<Vector2>();

		Sample();
	}

	void Sample() {
		AddFirstPoint();

		while (activePoints.Count != 0) {
			var listIndex = random.Next(activePoints.Count);

			var point = activePoints[listIndex];
			var found = false;

			for (var k = 0; k < pointsPerIteration; k++)
				found |= AddNextPoint(point);

			if (!found) activePoints.RemoveAt(listIndex);
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

		if (!(q.x >= topLeft.x) || !(q.x < lowerRight.x) || !(q.y > topLeft.y) ||
		    !(q.y < lowerRight.y))
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
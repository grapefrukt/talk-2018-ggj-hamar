using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonExample : MonoBehaviour {
	int index = 0;
	UniformPoissonDiskSampler sampler;

	// Use this for initialization
	void Start () {
		var tl = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
		var br = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
		sampler = new UniformPoissonDiskSampler(tl, br, .5f, 50, 0xff9394);
	}
	
	// Update is called once per frame
	void Update () {
		if (index > sampler.points.Count - 1) return;
		//GeometryDraw.DrawRectCentered(gameObject, positions[index].x, positions[index].y, .1f, .1f, Color.HSVToRGB(Mathf.Sin(index / 60f) / 2f + .5f, 1, 1));
		GeometryDraw.DrawCircle(gameObject, sampler.points[index].x, sampler.points[index].y, .1f,Color.HSVToRGB(Mathf.Sin(index / 60f) / 2f + .5f, 1, 1), 0, .6f);
		index++;
		GeometryDraw.Finalize(gameObject);
	}

	void OnDrawGizmos() {
	


		
	}
}

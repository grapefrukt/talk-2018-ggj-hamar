using System.Collections;
using UnityEngine;

public class PoissonController : MonoBehaviour {

	[HideInInspector] public UniformPoissonDiskSampler sampler;

	int index = -1;
	public PoissonPoint pointPrefab;

	public bool renderRadius = true;
	public bool renderSamples = true;
	public bool renderColorized = true;
	public bool renderBounds = true;

	bool restartQueued = false;

	public int seed = 0xff3423;
	Coroutine coroutine;
	Vector3 topLeft;
	Vector3 lowerRight;

	void Start () {
		topLeft = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, Camera.main.nearClipPlane));
		lowerRight = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .9f, Camera.main.nearClipPlane));
		StopAndClear();
	}

	void Go() {
		index = -1;
		sampler = GetComponent<UniformPoissonDiskSampler>();
		sampler.Initialize(topLeft, lowerRight, seed);
		coroutine = StartCoroutine(sampler.Sample());
	}

	public void GoRandom() {
		coroutine = StartCoroutine(PlaceRandom(300));
	}

	IEnumerator PlaceRandom(int count) {
		while (count-- > 0) {
			var p = new Vector2(
				Mathf.Lerp(topLeft.x, lowerRight.x, Random.value),
				Mathf.Lerp(topLeft.y, lowerRight.y, Random.value)
			);
			MakePoint(p);
			yield return null;
		}
	}

	void Restart() {
		restartQueued = false;
		StopAndClear();
		Go();
	}

	public void StopAndClear() {
		foreach (Transform child in transform) Destroy(child.gameObject);
		sampler = null;
		if (coroutine != null) StopCoroutine(coroutine);
	}

	// Update is called once per frame
	void Update () {
		if (restartQueued) Restart();

		if (sampler == null) return;

		if (renderSamples) {

			for (var i = 0; i < 16; i++) {
				var r0 = (float) i / (16 - 1) * Mathf.PI * 2;
				var r1 = (float) (i + 1) / (16 - 1) * Mathf.PI * 2;
				Debug.DrawLine(
					sampler.head + new Vector2(Mathf.Cos(r0), Mathf.Sin(r0)) * .1f,
					sampler.head + new Vector2(Mathf.Cos(r1), Mathf.Sin(r1)) * .1f
				);
			}

			for (var i = 0; i < sampler.samples.Count; i++) {
				var color = Color.red;
				if (i == sampler.samples.Count - 1) color = sampler.lastSampleSuccessful ? Color.green : Color.white;

				Debug.DrawLine(sampler.head, sampler.samples[i], color);
			}
		}

		while (sampler.points.Count - 1 > index) {
			index++;
			MakePoint(sampler.points[index]);
		}
	}

	void MakePoint(Vector2 point) {
		var poissonPoint = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity, transform);
		poissonPoint.controller = this;
		poissonPoint.index = index;
		poissonPoint.point = point;
		poissonPoint.Render();
	}

	void OnDrawGizmos() {
		if (!sampler) return;

		if (!renderBounds) return;
		var tl = sampler.topLeft;
		var br = sampler.bottomRight;
		var tr = new Vector2(br.x, tl.y);
		var bl = new Vector2(tl.x, br.y);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(tl, tr);
		Gizmos.DrawLine(tr, br);
		Gizmos.DrawLine(br, bl);
		Gizmos.DrawLine(bl, tl);
	}

	void OnValidate() {
		var points = GetComponentsInChildren<PoissonPoint>();
		foreach (var point in points) point.Render();
	}

	public void QueueRestart() {
		restartQueued = true;
	}
}

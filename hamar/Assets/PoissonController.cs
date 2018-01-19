using UnityEngine;

public class PoissonController : MonoBehaviour {

	[HideInInspector] public UniformPoissonDiskSampler sampler;

	int index = -1;
	public PoissonPoint pointPrefab;

	public bool renderRadius = true;
	public bool renderSamples = true;
	public bool renderColorized = true;
	public bool renderBounds = true;

	public int seed = 0xff3423;
	Coroutine coroutine;

	void Start () {
		Go();
	}

	void Go() {
		index = -1;
		var tl = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, Camera.main.nearClipPlane));
		var br = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .9f, Camera.main.nearClipPlane));

		sampler = GetComponent<UniformPoissonDiskSampler>();
		sampler.Initialize(tl, br, seed);
		coroutine = StartCoroutine(sampler.Sample());
	}

	public void Restart() {
		Stop();
		foreach (Transform child in transform) Destroy(child.gameObject);

		Go();
	}

	public void Stop() {
		if (coroutine != null) StopCoroutine(coroutine);
	}

	// Update is called once per frame
	void Update () {
		if (sampler == null) return;

		if (renderSamples) {
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
	
}

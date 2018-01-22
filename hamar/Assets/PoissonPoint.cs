using UnityEngine;

public class PoissonPoint : MonoBehaviour {
	public Vector2 point;
	public int index;
	public PoissonController controller;
	public bool lastOpenState = true;

	void Update() {
		if (!lastOpenState) return;
		if (controller.sampler.activePoints.Contains(point) != lastOpenState) Render();
	}
	
	// Update is called once per frame
	public void Render () {
		var isOpen = controller.sampler.activePoints.Contains(point);
		var radius = (isOpen ? .10f : .05f) * controller.sampler.minimumDistance;
		var hue = Mathf.PingPong(index * .01f, 1);
		var saturation = controller.renderColorized ? 1 : 0;
		var color = Color.HSVToRGB(hue, saturation, isOpen ? .5f : 1);
		var outlineColor = Color.HSVToRGB(0, 0, .8f);

		GeometryDraw.Clear(gameObject);
		GeometryDraw.DrawCircle(gameObject, point.x, point.y, radius, color, 0, .6f);
		if (controller.renderRadius) {
			GeometryDraw.DrawCircle(gameObject, point.x, point.y, controller.sampler.minimumDistance, outlineColor, 8, 0.2f);
			GeometryDraw.DrawCircle(gameObject, point.x, point.y, controller.sampler.minimumDistance - .02f, ColorSettings.instance.background, 1.5f, 0.2f);
		}
		GeometryDraw.Finalize(gameObject);

		lastOpenState = isOpen;
	}
}

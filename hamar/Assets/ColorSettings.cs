using System;
using UnityEngine;

public class ColorSettings : ScriptableObject {

	public Color bezierHandle;
	public Color bezierEndpoint;
	public Color bezierPrimaryLine;
	public Color bezierSecondaryLine;
	public Color bezierTertiaryLine;
	public Color bezierQuaternaryLine;
	public Color bezierSecondaryHandle;
	public Color bezierCurve;
	public Color bezierCurveHandle;

	[Range(0, .3f)]  public float bezierSimpleLerpThickness = .04f;
	[Range(0, .15f)] public float bezierSimpleLerpDotSize = .1f;
	[Range(0, .15f)] public float bezierHandleDotSize = .1f;
	[Range(0, .3f)] public float bezierSecondaryLineThickness = .1f;
	[Range(0, .3f)] public float bezierTertiaryLineThickness = .1f;
	[Range(0, .3f)] public float bezierQuarternaryLineThickness = .1f;
	[Range(0, .3f)] public float bezierCurveThickness = .1f;

	static ColorSettings i;
	public static ColorSettings instance {
		get {  return i == null ? Initialize() : i; }
	}

	static ColorSettings Initialize() {
		if (i != null) return i;
		return i = Resources.Load<ColorSettings>("ColorSettings");
	}
		
}
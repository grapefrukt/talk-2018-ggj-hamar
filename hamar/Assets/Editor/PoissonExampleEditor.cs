using UnityEditor;
using UnityEngine;

namespace Editor {

	[CustomEditor(typeof(PoissonExample))]
	public class PoissonExampleEditor : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			var poisson = (PoissonExample) target;

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Stop")) poisson.Stop();
			if (GUILayout.Button("Restart")) poisson.Restart();
			GUILayout.EndHorizontal();
		}

	}
}

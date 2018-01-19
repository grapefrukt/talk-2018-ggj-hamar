using UnityEditor;
using UnityEngine;

namespace Editor {

	[CustomEditor(typeof(PoissonController))]
	public class PoissonExampleEditor : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			var poisson = (PoissonController) target;

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Stop")) poisson.Stop();
			if (GUILayout.Button("Restart")) poisson.Restart();
			GUILayout.EndHorizontal();
		}

	}
}

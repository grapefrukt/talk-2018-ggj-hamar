using UnityEditor;
using UnityEngine;

namespace Editor {

	[CustomEditor(typeof(PoissonController))]
	public class PoissonControllerEditor : UnityEditor.Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			var poisson = (PoissonController) target;

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Restart")) poisson.QueueRestart();
			if (GUILayout.Button("Stop & Clear")) poisson.StopAndClear();
			GUILayout.EndHorizontal();

			if (GUILayout.Button("Place Random")) poisson.GoRandom();
		}

	}
}

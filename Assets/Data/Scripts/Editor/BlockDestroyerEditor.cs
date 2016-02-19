using UnityEditor;
using UnityEngine;
using System.Collections;

// Custom Editor using SerializedProperties.
// Automatic handling of multi-object editing, undo, and prefab overrides.
[CustomEditor(typeof(BlockDestroyer))]
[CanEditMultipleObjects]
public class BlockDestroyerEditor : Editor {
	SerializedProperty damageThresholdP;
	SerializedProperty HealthPointsP;
	SerializedProperty scoreValueP;
	
	void OnEnable () {
		// Setup the SerializedProperties.
		damageThresholdP = serializedObject.FindProperty ("DamageThreshold");
		HealthPointsP = serializedObject.FindProperty ("HealthPoints");
		scoreValueP = serializedObject.FindProperty ("ScoreValue");
	}
	
	public override void OnInspectorGUI() {
		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();
		
		// Show the custom GUI controls.
		EditorGUILayout.Slider (damageThresholdP, 0, 10, new GUIContent ("DamageThreshold"));
		
		// Only show the damage progress bar if all the objects have the same damage value:
		if (!damageThresholdP.hasMultipleDifferentValues)
			ProgressBar (damageThresholdP.floatValue / 10.0f, "DamageThreshold");
		
		EditorGUILayout.IntSlider (HealthPointsP, 0, 4, new GUIContent ("HealthPoints"));
		
		// Only show the armor progress bar if all the objects have the same armor value:
		if (!HealthPointsP.hasMultipleDifferentValues)
			ProgressBar (HealthPointsP.intValue / 4.0f, "HealthPoints");
		
		EditorGUILayout.IntSlider (scoreValueP, 0, 10000, new GUIContent ("ScorePoints"));


		
		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
	}
	
	// Custom GUILayout progress bar.
	void ProgressBar (float value, string label) {
		// Get a rect for the progress bar using the same margins as a textfield:
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, value, label);
		EditorGUILayout.Space ();
	}
}
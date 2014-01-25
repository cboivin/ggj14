using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CustomRelations))]
public class CustomRelationsEditor : Editor {

	public CustomRelations customRelations;
	public int behaviorsCount;

	public void OnEnable() {
		this.customRelations = (CustomRelations)this.target;
		behaviorsCount = Enum.GetNames(typeof(BehaviorType)).Length;

		if ( this.customRelations.customRelations == null ) {
			this.customRelations.customRelations = new CustomRelations.Relations[behaviorsCount];
		}
		for ( int i = 0; i < this.behaviorsCount; i++ ) {
			if ( this.customRelations.customRelations[i] == null )  {
			    this.customRelations.customRelations[i] = new CustomRelations.Relations();
			}
			if ( this.customRelations.customRelations[i].relations == null ) {
				this.customRelations.customRelations[i].relations = new CustomRelations.Relation[behaviorsCount];
			}
			for ( int j = 0; j < this.behaviorsCount; j++ ) {
				if ( this.customRelations.customRelations[i].relations[j] == null ) {
					this.customRelations.customRelations[i].relations[j] = new CustomRelations.Relation();
				}
			}
		}
		if ( this.foldouts == null || this.foldouts.Length < behaviorsCount) {
			this.foldouts = new bool[behaviorsCount];
		}

	}

	public bool[] foldouts;

	public override void OnInspectorGUI() {
		for ( int i = 0; i < this.behaviorsCount; i++ ){
			EditorGUILayout.BeginVertical("Box"); 
			{
				this.foldouts[i] = EditorGUILayout.Foldout(this.foldouts[i], "Relations of " + Enum.GetNames(typeof(BehaviorType))[i]);
				if ( this.foldouts [i] ) {
					for( int j = 0; j < this.behaviorsCount; j ++ ) {
						CustomRelations.Relation relation = this.customRelations.customRelations[i].relations[j];
						EditorGUILayout.LabelField(Enum.GetNames(typeof(BehaviorType))[i] + "--->" + Enum.GetNames(typeof(BehaviorType))[j] );
						EditorGUILayout.BeginVertical ("Box");
						{
							relation.AttractionDistance = EditorGUILayout.Slider("Attraction distance", relation.AttractionDistance, 0f, 20f);
							relation.AttractionIntensity = EditorGUILayout.Slider("Attraction intensity", relation.AttractionIntensity, 0f, 20f);
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginVertical ("Box");
						{
							relation.RepulsionDistance = EditorGUILayout.Slider("Repulsion distance", relation.RepulsionDistance, 0f, 20f);
							relation.RepulsionIntensity = EditorGUILayout.Slider("Repulsion intensity", relation.RepulsionIntensity, 0f, 20f);
						}
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.BeginVertical ("Box");
						{
							relation.AlignmentDistance = EditorGUILayout.Slider("Alignment distance", relation.AlignmentDistance, 0f, 20f);
							relation.AlignmentIntensity = EditorGUILayout.Slider("Alignment intensity", relation.AlignmentIntensity, 0f, 10f);
						}
						EditorGUILayout.EndHorizontal();
						this.customRelations.customRelations[i].relations[j] = relation;
					}
				}
			}
			EditorGUILayout.EndVertical();
		}
	}

}

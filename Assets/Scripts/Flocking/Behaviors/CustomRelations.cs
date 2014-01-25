using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CustomRelations : MonoBehaviour {

	[System.Serializable]
	public class Relations {
		public Relation[] relations = new Relation[Enum.GetNames(typeof(BehaviorType)).Length];
	}

	[System.Serializable]
	public class Relation {
		public float AttractionIntensity;
		public float AttractionDistance;

		public float RepulsionIntensity;
		public float RepulsionDistance;

		public float AlignmentIntensity;
		public float AlignmentDistance;
	};

	[SerializeField]
	public Relations[] customRelations = new Relations[Enum.GetNames(typeof(BehaviorType)).Length];

	public Relations getRelations(BehaviorType type) {
		return this.customRelations[(int)type];
	}

	public Relation getRelation(BehaviorType from, BehaviorType to) {
		return this.customRelations[(int)from].relations[(int)to];
	}

	public Relation getRelation(int from, int to) {
		return this.customRelations[from].relations[to];
	}

}

using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class World : MonoBehaviour {

		[Range(0f, 1f)]
		public float Friction;
		[Range(0f, 10f)]
		public float maxVel;
		[Range(0f, 2f)]
		public float maxSteeringVel;


		[Range(0f, 20f)]
		public float AlignmentDistance;
		[Range(0f, 10f)]
		public float AlignmentIntensity;

		[Range(0f, 20f)]
		public float RepulsionDistance;
		[Range(0f, 10f)]
		public float RepulsionIntensity;

		[Range(0f, 20f)]
		public float AttractionDistance;
		[Range(0f, 10f)]
		public float AttractionIntensity;

	} 

}

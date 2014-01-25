using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public abstract class CircularEffector : Effector {

		public float effectDistance;
		public float intensity;

		public override Vector3 ComputeDistance( Boid other ) {
			if ( other.myTransform == null ) {
				return  Vector3.zero;
			}
			return other.myTransform.position - this.myTransform.position;
		}

	}

}
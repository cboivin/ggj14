using UnityEngine;
using System.Collections;

namespace GameJam.Boids {
		
	public abstract class RectangularEffector : MonoBehaviour {

		public abstract void ApplyEffect(Boid other, float distanceToOther, Vector3 directionToOther );

	}

}
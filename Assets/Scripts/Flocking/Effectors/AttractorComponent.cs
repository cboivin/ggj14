using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AttractorComponent : CircularEffector {

		protected float sqrEffectDistance;
		protected float intensity;

		public void Update() {
			this.effectDistance = this.world.AttractionDistance;
			this.sqrEffectDistance = this.effectDistance * this.effectDistance;
			this.intensity = this.world.AttractionIntensity;
		}

		public override void ApplyEffect (Boid other) {
			Vector3 distance = this.ComputeDistance(other);
			if ( distance.sqrMagnitude > this.sqrEffectDistance ) {
				return;
			}
			other.attractionVel += this.intensity * -distance.normalized;
			other.attractionEffectors++;
		}
		
		public void OnDrawGizmos() {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

}

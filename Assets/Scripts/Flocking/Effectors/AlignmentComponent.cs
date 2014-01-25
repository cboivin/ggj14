using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AlignmentComponent : CircularEffector {

		public Vector3 alignmentSpeed;
		public float intensity;
		private float sqrEffectDistance;

		public void Update() {
			this.sqrEffectDistance = this.world.AlignmentDistance*this.world.AlignmentDistance;
			this.effectDistance = this.world.AlignmentDistance;
			this.intensity = this.world.AlignmentIntensity;
		}

		public override void ApplyEffect(Boid other) {
			Vector3 distance = this.ComputeDistance(other);
			if (  distance.sqrMagnitude > this.sqrEffectDistance ) {
				return;
			}
			other.alignmentVel +=  this.intensity * alignmentSpeed;
			other.alignmentEffectors++;
		}

		public void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

}

using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	[ExecuteInEditMode]
	public class LinearRepellerComponent : LinearEffector {
	
		public float intensity;
		private float sqrEffectDistance;

		protected override void OnUpdate() {
			this.sqrEffectDistance = this.effectDistance*this.effectDistance;	
		}

		public override void ApplyEffect (Boid other) {
			Vector3 direction = this.ComputeDistance(other);
			if ( direction.sqrMagnitude > this.sqrEffectDistance ) {
				return;
			}
			other.repulsionVel += direction.normalized * this.intensity / direction.sqrMagnitude;
			other.repulsionEffectors++;
		}

		public void OnDrawGizmos() {
			Gizmos.color = Color.magenta;
			Gizmos.DrawRay(this.myTransform.position, this.effectDirection * 10f);

			Gizmos.color = Color.red;
			Ray middle = this.linePosition;
			Gizmos.DrawRay(middle.origin, middle.direction * 50f);
			middle.direction *= -1f;
			Gizmos.DrawRay(middle.origin, middle.direction * 50f);

			Gizmos.color = Color.cyan;
			Ray max = this.linePosition;
			max.origin = this.myTransform.rotation * Vector3.right * this.effectDistance + this.myTransform.position;
			Gizmos.DrawRay(max.origin, max.direction * 50f);
			max.direction *= -1f;
			Gizmos.DrawRay(max.origin, max.direction * 50f);

			Ray min = this.linePosition;
			min.origin = this.myTransform.rotation * Vector3.left * this.effectDistance + this.myTransform.position;
			Gizmos.DrawRay(min.origin,min.direction * 50f);
			min.direction *= -1f;
			Gizmos.DrawRay(min.origin, min.direction * 50f);

		}

	}

}
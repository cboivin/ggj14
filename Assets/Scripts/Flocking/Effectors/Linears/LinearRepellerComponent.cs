using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	[ExecuteInEditMode]
	public class LinearRepellerComponent : LinearEffector {
	
		public float intensity;
//		private float sqrEffectDistance;

		protected override void OnUpdate() {
//			this.sqrEffectDistance = this.effectDistance*this.effectDistance;	
		}

		public override void ApplyEffect (Boid other) {
			Vector3 direction = this.ComputeDistance(other);
			if ( !this.ComputeSide(other) ) {
				return;
			};
			other.repulsionVel += direction.normalized * this.intensity * direction.sqrMagnitude;
			other.repulsionEffectors++;
		}

		public void OnDrawGizmos() {
			if ( this.myTransform == null ) {
				return;
			}
			Gizmos.color = Color.magenta;
			Gizmos.DrawRay(this.myTransform.position, this.effectDirection * 10f);

			Gizmos.color = Color.red;
			Ray middle = this.linePosition;
			Gizmos.DrawRay(middle.origin, middle.direction * 50f);
			middle.direction *= -1f;
			Gizmos.DrawRay(middle.origin, middle.direction * 50f);
		}

	}

}
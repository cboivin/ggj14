using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AlignmentComponent : Effector {

		public Vector3 alignmentSpeed;
		public float effectDistance;
		public float intensity;

		public void Update() {
			this.effectDistance = this.world.AlignmentDistance;
			this.intensity = this.world.AlignmentIntensity;
		}

		public override void ApplyEffect(Boid other,Vector3 vectorToOther, float distanceToOther) {
			if (  distanceToOther > this.effectDistance ) {
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

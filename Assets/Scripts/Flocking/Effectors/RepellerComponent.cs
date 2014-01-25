using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class RepellerComponent : CircularEffector {

		public float intensity;
		private float sqrEffectDistance;

		public void Update() {
			this.effectDistance = this.world.RepulsionDistance;
			this.sqrEffectDistance = this.effectDistance * this.effectDistance;
			this.intensity = this.world.RepulsionIntensity;
		}

		public override void ApplyEffect (Boid other) {
			Vector3 direction = this.ComputeDistance(other);
			if (  direction.sqrMagnitude > this.sqrEffectDistance ) {
				return;
			}
			other.repulsionVel += this.intensity * direction.normalized;
			other.repulsionEffectors++;
		}
		
		public void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

 }
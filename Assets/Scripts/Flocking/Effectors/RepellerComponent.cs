using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class RepellerComponent : Effector {

		public float effectDistance;
		public float intensity;

		public void Update() {
			this.effectDistance = this.world.RepulsionDistance;
			this.intensity = this.world.RepulsionIntensity;
		}

		public override void ApplyEffect (Boid other,Vector3 directionToOther, float distanceToOther) {
			if (  distanceToOther > this.effectDistance ) {
				return;
			}
			other.repulsionVel += this.intensity * directionToOther;
			other.repulsionEffectors++;
		}
		
		public void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

 }
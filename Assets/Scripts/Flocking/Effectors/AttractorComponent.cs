using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AttractorComponent : Effector {

		public float effectDistance;
		public float intensity;

		public void Update() {
			this.effectDistance = this.world.AttractionDistance;
			this.intensity = this.world.AttractionIntensity;
		}

		public override void ApplyEffect (Boid other,Vector3 directionToOther, float distanceToOther) {
			if (  distanceToOther > this.effectDistance ) {
				return;
			}
			other.attractionVel += this.intensity * -directionToOther;
			other.attractionEffectors++;
		}
		
		public void OnDrawGizmos() {
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

}

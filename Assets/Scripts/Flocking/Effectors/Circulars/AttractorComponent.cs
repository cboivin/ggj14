using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AttractorComponent : CircularEffector {

		private bool isBoid;
		private Boid boid;
		
		public override void OnStart ()
		{
			base.OnStart ();
			this.boid = this.GetComponent<Boid>();
			this.isBoid = this.boid != null;
		}
		
		public override void ApplyEffect(Boid other) {
			if ( this.isBoid ) {
				CustomRelations.Relation relation = this.boid.customRelations.getRelation(this.boid.layer, other.layer);
				this.effectDistance = relation.AttractionDistance;
				this.intensity = relation.AttractionIntensity;
				if ( this.intensity == 0 || this.effectDistance == 0 ) {
					return;
				}
			}
			Vector3 distance = this.ComputeDistance(other);
			if (  distance.sqrMagnitude > this.effectDistance*this.effectDistance ) {
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

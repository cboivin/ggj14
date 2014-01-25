using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class AlignmentComponent : CircularEffector {

		public Vector3 alignmentSpeed;
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
				this.effectDistance = relation.AlignmentDistance;
				this.intensity = relation.AlignmentIntensity;
				if ( this.intensity == 0 || this.effectDistance == 0 ) {
					return;
				}
			}
			Vector3 distance = this.ComputeDistance(other);
			if (  distance.sqrMagnitude > this.effectDistance*this.effectDistance ) {
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

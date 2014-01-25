﻿using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public class RepellerComponent : CircularEffector {

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
				this.effectDistance = relation.RepulsionDistance;
				this.intensity = relation.RepulsionIntensity;
				if ( this.intensity == 0 || this.effectDistance == 0 ) {
					return;
				}
			}
			Vector3 direction = this.ComputeDistance(other);
			if (  direction.sqrMagnitude > this.effectDistance*this.effectDistance ) {
				return;
			}
			Debug.DrawLine(this.myTransform.position, other.transform.position, Color.white);
			other.repulsionVel += this.intensity * direction.normalized;
			other.repulsionEffectors++;
		}
		
		public void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
		}

	}

 }
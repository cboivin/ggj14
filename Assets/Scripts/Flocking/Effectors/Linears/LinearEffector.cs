using UnityEngine;
using System.Collections;

namespace GameJam.Boids {
		
	public abstract class LinearEffector : Effector {

		public float effectDistance;
		protected Ray linePosition;
		protected Vector3 effectDirection;

		public void Update() {
			this.effectDirection = this.myTransform.rotation * Vector3.left;
			this.linePosition.direction = this.myTransform.rotation * Vector3.up;
			this.linePosition.origin = this.myTransform.position;
			this.OnUpdate();
		}

		protected virtual void OnUpdate() {

		}

		public override Vector3 ComputeDistance (Boid other) {
			if ( other.myTransform == null ) {
				return  Vector3.zero;
			}
			return Vector3.Cross(this.linePosition.direction, other.myTransform.position - this.linePosition.origin).magnitude * this.effectDirection;
		}

		public bool ComputeSide(Boid other) {
			Vector3 cross =  Vector3.Cross(this.linePosition.direction, other.myTransform.position - this.linePosition.origin);
			return cross.z < 0;
		}

	}

}
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GameJam.Boids {

	public class Boid : MonoBehaviour {

		[HideInInspector]
		public Effector[] effectors;

		#region Velocity
		public Vector3 velocity;
		private Vector3 lastVel;

		[HideInInspector]
		public Transform myTransform;
		[HideInInspector]
		public Vector3 	attractionVel;
		[HideInInspector]
		public int 	 	attractionEffectors = 1;
		[HideInInspector]
		public Vector3 	repulsionVel;
		[HideInInspector]
		public int 		repulsionEffectors = 1;
		[HideInInspector]
		public Vector3  alignmentVel;
		[HideInInspector]
		public int 		alignmentEffectors = 1;
		#endregion

		public World worldInfos;

		private AlignmentComponent alignmentComponent;

		void Start () {
			this.effectors = this.GetComponents<Effector>();
			this.lastVel = Vector3.zero;
			this.alignmentComponent = this.GetComponent<AlignmentComponent>();
			this.myTransform = this.transform;

			Vector2 popPos = Random.insideUnitCircle;
			this.myTransform.localPosition = Vector3.left * popPos.x + Vector3.up * popPos.y;
		}

		private void Update() {

			this.setAlignmentDirection();

			this.applyVelocities();

			// apply friction
			this.velocity *= worldInfos.Friction;

			// maximize speed
			if ( this.velocity.magnitude >= worldInfos.maxVel ) {
				this.velocity = this.velocity.normalized * worldInfos.maxVel;
			}

			// compute & maximize steering
			Vector3 steeringVel = this.velocity - this.lastVel;
			if ( steeringVel.magnitude > worldInfos.maxSteeringVel) {
				steeringVel = steeringVel.normalized * worldInfos.maxSteeringVel;
			}
			this.velocity = this.lastVel = this.lastVel+steeringVel;

			if ( float.IsNaN(this.velocity.x) || float.IsNaN( this.velocity.y ) || float.IsNaN( this.velocity.z ) ) {
				this.velocity = Vector3.zero;
				this.lastVel = Vector3.zero;
				return;
			}

			if ( this.velocity == Vector3.zero ) {
				return;
			}

			//rotate
			this.myTransform.rotation = Quaternion.LookRotation(this.velocity);

			// apply force
			this.velocity.z = 0f;
			this.myTransform.localPosition += this.velocity * Time.deltaTime;

		}

		private void setAlignmentDirection() {
			if ( this.alignmentComponent != null ) {
				this.alignmentComponent.alignmentSpeed = this.velocity;
			}
		}

		private void applyVelocities() {
			if ( this.alignmentEffectors != 0 ) {
				this.velocity += this.alignmentVel/this.alignmentEffectors;	
			}
			if ( this.repulsionEffectors != 0 ) {
				this.velocity += this.repulsionVel;
			}
			if ( this.attractionEffectors != 0 ) {
				this.velocity += this.attractionVel/this.attractionEffectors;
			}
			this.alignmentEffectors = this.attractionEffectors = this.repulsionEffectors = 0;
			this.alignmentVel = this.repulsionVel = this.attractionVel = Vector3.zero;
		}

		public void Kill() {
			GameObject.Destroy(this.gameObject);
		}

		public void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(this.myTransform.position, this.myTransform.position + this.velocity * 2f);
		}

	} 

}

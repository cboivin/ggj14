using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GameJam.Boids {

	public class Boid : MonoBehaviour {

		[HideInInspector]
		public Effector[] effectors;

		public int layer;
		
		[HideInInspector]
		public AttractorComponent attactor;
		[HideInInspector]
		public RepellerComponent repeller;
		[HideInInspector]
		public AlignmentComponent aligner;

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
		public CustomRelations customRelations;

		void Start () {
			this.effectors = this.GetComponents<Effector>();
			this.aligner = this.GetComponent<AlignmentComponent>();
			this.repeller = this.GetComponent<RepellerComponent>();
			this.attactor = this.GetComponent<AttractorComponent>();

			this.lastVel = Vector3.zero;
			this.myTransform = this.transform;

			this.customRelations = GameObject.FindObjectOfType<CustomRelations>();
			//Vector2 popPos = Random.insideUnitCircle;
			//this.myTransform.localPosition = Vector3.left * popPos.x + Vector3.up * popPos.y;
		}

		private void Update() {

			this.updateEffectors();

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
			
			if ( this.alignmentEffectors + this.repulsionEffectors + this.attractionEffectors < 1  ) {
				this.velocity += this.wander() ;
			}
			this.alignmentEffectors = this.attractionEffectors = this.repulsionEffectors = 0;

			//rotate
			//this.myTransform.rotation = Quaternion.LookRotation(this.velocity);

			// apply force
			this.velocity.z = 0f;
			this.myTransform.localPosition += this.velocity * Time.deltaTime;

		}

		private Vector3 wander() {
			Vector3 circleCenter = velocity.normalized;
			Vector3 wanderForce = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f),Vector3.forward) * Vector3.left * 3f;
			return (wanderForce + circleCenter) * 0.1f;
		}

		private void updateEffectors() {
			if (aligner != null)
			{
				this.aligner.alignmentSpeed = this.velocity;
			}
//			this.aligner.intensity = this.worldInfos.AlignmentIntensity;
//			this.aligner.effectDistance = this.worldInfos.AlignmentDistance;
//			this.repeller.intensity = this.worldInfos.RepulsionIntensity;
//			this.repeller.effectDistance = this.worldInfos.RepulsionDistance;
//			this.aligner.intensity = this.worldInfos.AlignmentIntensity;
//			this.aligner.effectDistance = this.worldInfos.AlignmentDistance;
		}

		private void applyVelocities() {
			if ( this.alignmentEffectors != 0 ) {
				this.velocity += this.alignmentVel * 0.1f/this.alignmentEffectors;
			}
			if ( this.repulsionEffectors != 0 ) {
				this.velocity += this.repulsionVel * 0.1f;
			}
			if ( this.attractionEffectors != 0 ) {
				this.velocity += this.attractionVel * 0.1f /this.attractionEffectors;
			}
			this.alignmentVel = this.repulsionVel = this.attractionVel = Vector3.zero;
		}

		public void Kill() {
			GameObject.Destroy(this.gameObject);
		}

		public void OnDrawGizmos() {
			if ( this.myTransform == null) {
				return;
			}
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(this.transform.position, this.transform.position + this.velocity * 2f);
		}

	} 

}

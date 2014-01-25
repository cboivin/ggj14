using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public abstract class Effector : MonoBehaviour {

		protected World world;
		protected Transform myTransform;
		
		public void Start() {
			this.world = GameObject.FindObjectOfType<World>();
			this.myTransform = this.transform;
		}	

		public virtual void OnStart() {
		}

		public abstract Vector3 ComputeDistance( Boid other );
		public abstract void ApplyEffect( Boid other );
	
	}

}
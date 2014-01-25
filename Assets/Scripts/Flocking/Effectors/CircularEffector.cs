using UnityEngine;
using System.Collections;

namespace GameJam.Boids {

	public abstract class Effector : MonoBehaviour {

		public World world;

		public void Start() {
			this.world = GameObject.FindObjectOfType<World>();
		}

		public abstract void ApplyEffect(Boid other, Vector3 directionToOther, float distanceToOther);

	}

}
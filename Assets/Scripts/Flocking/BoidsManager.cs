using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameJam.Boids {

	public class BoidsManager : MonoBehaviour {

		public GameObject BoidTemplate;
		public int boidsNumber;

		//[HideInInspector]
		public World world;
		[HideInInspector]
		public List<Boid> boids;
		[HideInInspector]
		public LinearEffector[] obstacles;

		#region Update

		void Start() {
			this.world = GameObject.FindObjectOfType<World>();
			this.obstacles = GameObject.FindObjectsOfType<LinearEffector>();
		}

		void Update () {
			this.computeRelations();
		}

		void LateUpdate() {
			this.keepBoidsNumber();
		}

		#endregion

		#region Boids Managment

		private void keepBoidsNumber() {
			this.boidsNumber  = this.boidsNumber < 0 ? 0 : this.boidsNumber;
			while ( this.boids.Count < this.boidsNumber ) {
				Boid boid = (GameObject.Instantiate(this.BoidTemplate) as GameObject).GetComponent<Boid>();
				boid.transform.parent = this.transform;
				boid.worldInfos = this.world;
				this.boids.Add(boid.GetComponent<Boid>());
			}
			while ( this.boids.Count > this.boidsNumber ) {
				this.boids[0].Kill();
				this.boids.RemoveAt(0);
			}
		}

		private void computeRelations() {

			int boidsCount = this.boids.Count;
			Boid[] boidsArray = this.boids.ToArray();
			for ( int i = 0; i < boidsCount-1; i ++ ) {
				for ( int j = i + 1; j < boidsCount; j++ )  {
					int effectorsCount = boidsArray[i].effectors.Length;
					for ( int k = 0; k < effectorsCount; k++ ) {
						boidsArray[i].effectors[k].ApplyEffect(boidsArray[j]);
					}
					effectorsCount = boidsArray[i].effectors.Length;
					for ( int k = 0; k < effectorsCount; k++ ) {
						boidsArray[j].effectors[k].ApplyEffect(boidsArray[i]);
					}
				}
				for ( int j = 0; j < this.obstacles.Length; j++ ) {
					this.obstacles[j].ApplyEffect(boidsArray[i]);
				}
			}
			//last boid wasnt affected by obstacles
			for ( int j = 0; j < this.obstacles.Length; j++ ) {
				this.obstacles[j].ApplyEffect(boidsArray[boidsCount-1]);
			}
		}

		#endregion

	}


}
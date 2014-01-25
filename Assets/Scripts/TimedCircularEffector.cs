using UnityEngine;
using System.Collections;
using GameJam.Boids;

[RequireComponent(typeof(SphereCollider))]
public class TimedCircularEffector : MonoBehaviour {

	public float timeOut;

	private CircularEffector effector;
	private float initialEffectDistance;
	private float initialTimeOut;
	private SphereCollider effectZone;

	public void Start() {
		this.effector = this.GetComponent<CircularEffector>();
		this.initialEffectDistance = this.effector.effectDistance;
		this.initialTimeOut = this.timeOut;
		this.effectZone = this.GetComponent<SphereCollider>();
		this.effectZone.radius = this.initialEffectDistance;
	}

	public void Update() {
		this.timeOut -= Time.deltaTime;
		if ( timeOut <= 0 ) {
			GameObject.Destroy(this.gameObject);
			return;
		}
		this.effector.effectDistance = this.initialEffectDistance * this.timeOut / this.initialTimeOut;
		this.effectZone.radius = this.effector.effectDistance;
	}

	public void OnTriggerEnter(Collider other) {
//		Debug.Log("enter");
		Boid b = other.GetComponent<Boid>();
		if ( b != null ) {
			this.effector.ApplyEffect(b);
		}
	}

	public void OnTriggerStay(Collider other) {
		Boid b = other.GetComponent<Boid>();
		if ( b != null ) {
			this.effector.ApplyEffect(b);
		}
	}

}

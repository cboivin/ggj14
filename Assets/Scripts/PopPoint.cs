using UnityEngine;
using System;
using System.Collections;

public class PopPoint : MonoBehaviour {

	public float coolDown;
	public event Action<PopPoint> FreeHandler;
	private float coolDownTime;
	private bool used;

	void Start () {
			
	}

	public void Use() {
		this.used = true;
		this.coolDownTime = this.coolDown;
	}

	void Update () {
		if ( this.used ) {
			this.coolDownTime -= Time.deltaTime;
			if ( this.coolDownTime < 0f ) {
				this.used = false;
				if ( this.FreeHandler != null ) {
					this.FreeHandler(this);
				}
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawSphere(this.transform.position, 1f);
	}
}

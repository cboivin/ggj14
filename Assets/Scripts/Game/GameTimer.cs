using UnityEngine;
using System;
using System.Collections;

public class GameTimer : MonoBehaviour {

	public float maxTime;
	private float remainingTime;
	public event Action timerEndHandler;
	private bool running;

	void Start () {
		this.ResetTimer();
	}

	public void StartTimer() {
		this.running = true;
	}

	public void ResetTimer() {
		this.remainingTime = this.maxTime;
	}

	void Update () {
		if ( this.running) {
			this.remainingTime -= Time.deltaTime;
			if ( this.remainingTime <= 0f ) {
				if ( this.timerEndHandler != null ) {
					this.running = false;
					Debug.Log("timerEnd");
					this.timerEndHandler();
				}
			}
		}
	}
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PopPoints : MonoBehaviour {

	private List<PopPoint> points;
	private List<PopPoint> available;

	public event Action<PopPoint> PopPointFreeHandler;

	void Start() {
		this.points = new List<PopPoint>(this.GetComponentsInChildren<PopPoint>());	
		this.available = new List<PopPoint>(points);
	}

	void OnPopPointFree(PopPoint popPoint) {
//		Debug.Log("free");
		this.available.Add(popPoint);
		popPoint.FreeHandler -= this.OnPopPointFree;
		if ( this.PopPointFreeHandler != null ) {
			this.PopPointFreeHandler(popPoint);
		}
	}

	public PopPoint GetPopPoint() {
		int index = UnityEngine.Random.Range(0, this.available.Count);
		try {
			PopPoint popPoint = this.available[index];
			this.available.Remove(popPoint);
			popPoint.FreeHandler += this.OnPopPointFree;
			popPoint.Use();
			return popPoint;
		} catch ( Exception ){
			return null;
		}
	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	public SpriteRenderer inGame; 
	public SpriteRenderer startGame; 
	public SpriteRenderer endGame; 
	public GUIText score;
	private float startScreenTime;

	public void startScreen(float timeOut) {
		this.startScreenTime = timeOut;
		this.startGame.enabled = true;
		this.StartCoroutine(this.hideStartScreen());
	}

	private IEnumerator hideStartScreen() {
		yield return new WaitForSeconds(startScreenTime);
		this.startGame.enabled = false;
	}

	public void EndScreen(bool show) {
		this.endGame.enabled = show;
		this.score.enabled = show;
		this.score.text = ScoreManager.getScore().ToString();
	}
}

using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	public void OnGUI() {
		if ( Input.GetAxis("Fire1") > 0 ) {
			this.StartGame();
		}
	}

	private void StartGame() {
		Debug.Log("start game");
		Application.LoadLevel(1);
	}

}

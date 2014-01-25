using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

	public void OnGUI() {
		GUILayout.BeginVertical("Box",GUILayout.Width(Screen.width),GUILayout.Height(Screen.height));
		{
			if ( GUILayout.Button("Start",GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true)) ) {
				this.StartGame();	
			}

			if ( GUILayout.Button("Quit",GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true)) ) {
				this.QuitGame();
			}
		}
		GUILayout.EndHorizontal();
	}

	private void StartGame() {
		Debug.Log("start game");
		Application.LoadLevel(1);
	}

	private void QuitGame() {
		Debug.Log("Quit game");
	}

}

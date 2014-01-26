using UnityEngine;
using System.Collections;

public class Game : GameFSM {

	[HideInInspector]
	public GameTimer readyTimer;
	[HideInInspector]
	public CritController critController;
	[HideInInspector]
	public GUIManager gui;
	[HideInInspector]
	public PlayerController playerController;

	#region ready
	
	protected override void Ready_EnterState() {
		Debug.Log("ready_enter");
		this.playerController = this.GetComponentInChildren<PlayerController>();
		this.gui = GameObject.FindObjectOfType<GUIManager>();
		this.critController = GameObject.FindObjectOfType<CritController>();
		this.readyTimer = this.GetComponent<GameTimer>();
		this.gui.startScreen(this.readyTimer.maxTime);
		this.readyTimer.timerEndHandler += this.OnTimerEnd;	
		this.readyTimer.StartTimer();
	}
	
	protected override void Ready_Update() {
		
	}
	
	protected override void Ready_ExitState() {
		
	}
	
	private void OnTimerEnd() {
		this.state = GameState.Run;
	}
	
	#endregion
	
	#region Run
	
	protected override void Run_EnterState() {
		Debug.Log("Run!");
		this.critController.PopHunter();
		this.playerController.enabled = true;
	}
	
	protected override void Run_Update() {
	}
	
	protected override void Run_ExitState() {
		
	}

	
	#endregion
	
	#region End
	
	protected override void End_EnterState() {
		Application.LoadLevel(0);	
	}
	
	protected override void End_Update() {
		
	}
	
	protected override void End_ExitState() {
		
	}
	
	#endregion

}

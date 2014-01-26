using UnityEngine;
using System.Collections;

public class Game : GameFSM {

	public GameTimer readyTimer;
	public CritController critController;

	#region ready
	
	protected override void Ready_EnterState() {
		Debug.Log("ready_enter");
		this.critController = GameObject.FindObjectOfType<CritController>();
		this.readyTimer = this.GetComponent<GameTimer>();
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
		this.critController.CreateNewSteak();
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

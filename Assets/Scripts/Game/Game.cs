using UnityEngine;
using System.Collections;

public class Game : GameFSM {

	public GameTimer readyTimer;

	#region ready
	
	protected override void Ready_EnterState() {
		this.readyTimer = this.GetComponent<GameTimer>();
		this.readyTimer.timerEndHandler += this.OnTimerEnd;	
		this.readyTimer.StartTimer();
		this.state = GameState.Run;
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

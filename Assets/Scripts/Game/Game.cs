using UnityEngine;
using System.Collections;

public class Game : GameFSM {

	public GameTimer timer;

	#region ready
	
	protected override void Ready_EnterState() {
		this.timer = this.GetComponent<GameTimer>();
		this.timer.timerEndHandler += this.OnTimerEnd;	
		this.timer.StartTimer();
		this.state = GameState.Run;
	}
	
	protected override void Ready_Update() {
		
	}
	
	protected override void Ready_ExitState() {
		
	}
	
	#endregion
	
	#region Run
	
	protected override void Run_EnterState() {
		
	}
	
	protected override void Run_Update() {
	}
	
	protected override void Run_ExitState() {
		
	}

	private void OnTimerEnd() {
		this.state = GameState.End;
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

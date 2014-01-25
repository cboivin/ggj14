using UnityEngine;
using System;
using System.Collections;

public class GameFSM : MonoBehaviour {

	public enum GameState {
		Ready,
		Run,
		End
	};

	private Action updateFunc;
	private Action exitFunc;

	private bool stopUpdate;

	private GameState currentState;
	public GameState state {
		set {
			this.stopUpdate = true;
			Action nextExit = null;
			Action enterFunc = null;
			this.currentState = value;
			switch( this.currentState ) {
			case GameState.Ready :
				enterFunc = this.Ready_EnterState;
				updateFunc = this.Ready_Update;
				nextExit = this.Ready_ExitState;
				break;
			case GameState.Run:
				enterFunc = this.Ready_EnterState;
				updateFunc = this.Ready_Update;
				nextExit = this.Ready_ExitState;
				break;
			case GameState.End:
				enterFunc = this.Ready_EnterState;
				updateFunc = this.Ready_Update;
				nextExit = this.Ready_ExitState;
				break;
			}

			if ( this.exitFunc != null ) {
				this.exitFunc.Invoke();
			}
			this.exitFunc = nextExit;
			enterFunc.Invoke();
			this.stopUpdate = false;
		}
		get {
			return this.currentState;
		}
	}

	public void Start() {
		this.currentState = GameState.Ready;
	}

	public void Update() {
		if ( !stopUpdate ) {
			if ( this.updateFunc != null ) {
				this.updateFunc.Invoke();
			}	
		}
	}

	#region ready

	protected virtual void Ready_EnterState() {

	}

	protected virtual void Ready_Update() {

	}

	protected virtual void Ready_ExitState() {

	}

	#endregion

	#region Run

	protected virtual void Run_EnterState() {

	}

	protected virtual void Run_Update() {

	}

	protected virtual void Run_ExitState() {

	}

	#endregion

	#region End

	protected virtual void End_EnterState() {

	}

	protected virtual void End_Update() {

	}

	protected virtual void End_ExitState() {

	}

	#endregion

}


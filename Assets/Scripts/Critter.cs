using UnityEngine;
using System.Collections;

public enum Behavior
{
	Normal,
	Fear,
	Angry
}

public class Critter : MonoBehaviour {

	public float m_BehaviorRange = 1f;
	public Behavior m_Behavior = Behavior.Normal;
	public Rigidbody m_RigidBody;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

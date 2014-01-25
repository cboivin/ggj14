using UnityEngine;
using System.Collections;
using GameJam.Boids;

public abstract class Behavior : MonoBehaviour
{
	public abstract void ApplyBehavior(Boid boid);
}

using UnityEngine;
using System.Collections.Generic;
using GameJam.Boids;

[RequireComponent(typeof(Boid))]
public class HunterBehavior : Behavior
{
	public float effectDistance;
	public float intensity;

	private Transform m_Transform;

	public void Start()
	{
		m_Transform = transform;
	}

	public override void ApplyBehavior(Boid boid)
	{
		List<Critter> critters = CritController.Instance.m_Crits;
		Vector3 targetPosition = Vector3.zero;

		int attraction = 0;
		foreach(Critter crit in critters)
		{
			if (crit.m_Behavior != BehaviorType.Hunter && crit.gameObject != gameObject)
			{
				Vector3 distance = m_Transform.position - crit.m_Transform.position;
				float magnitude = distance.magnitude;
				if (magnitude < effectDistance)
				{
					targetPosition += crit.m_Transform.position * 3.0f/magnitude;
					attraction++;
				}
			}
		}
		targetPosition.z = 0;
		if (attraction > 0)
		{
			targetPosition /= attraction;
			boid.attractionVel += (targetPosition - m_Transform.position).normalized*this.intensity;
			boid.attractionEffectors++;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
	}
}

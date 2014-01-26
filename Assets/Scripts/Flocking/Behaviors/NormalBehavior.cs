using UnityEngine;
using System.Collections.Generic;
using GameJam.Boids;

public class NormalBehavior : Behavior
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
		if ( m_Transform == null ) {
			return;
		}
		List<Critter> critters = CritController.Instance.m_Crits;
		Vector3 targetPosition = Vector3.zero;
		Vector3 fleePosition = Vector3.zero;

		int repulsion = 0;
		int attraction = 0;
		foreach(Critter crit in critters)
		{
			if  (crit.m_Transform == null ) {
				continue;
			}
			Vector3 distance = m_Transform.position - crit.m_Transform.position;
			float magnitude = distance.magnitude;

			switch(crit.m_Behavior)
			{
				case BehaviorType.Hunter:
				if (magnitude < effectDistance)
				{
					fleePosition += crit.m_Transform.position * 3.0f/magnitude;
					repulsion++;
				}
				break;

				case BehaviorType.Steak:
//				if (magnitude < effectDistance)
//				{
					targetPosition += crit.m_Transform.position * 4.0f/magnitude;
					attraction++;
//				}
				break;
			}
		}
		targetPosition.z = 0;
		if (attraction > 0)
		{
			targetPosition /= attraction;
			boid.attractionVel += (targetPosition - m_Transform.position).normalized*this.intensity;
			boid.attractionEffectors++;
		}
		if (repulsion > 0)
		{
			fleePosition /= repulsion;
			boid.attractionVel += (m_Transform.position - fleePosition).normalized*this.intensity;
			boid.attractionEffectors++;
		}
	}
}

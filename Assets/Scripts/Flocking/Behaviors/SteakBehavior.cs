using UnityEngine;
using System.Collections.Generic;
using GameJam.Boids;

public class SteakBehavior : Behavior
{
	public float effectDistance;
	public float intensity;

	private Transform m_Transform;

	public void Start()
	{
		m_Transform = transform;
	}
	
	public override void ApplyBehavior (Boid boid)
	{
		List<Critter> critters = CritController.Instance.m_Crits;
		Vector3 fleePosition = Vector3.zero;

		int repulsion = 0;
		foreach(Critter crit in critters)
		{
			if (crit.gameObject != gameObject)
			{
				Vector3 distance = m_Transform.position - crit.m_Transform.position;
				float magnitude = distance.magnitude;
				if (magnitude < effectDistance)
				{
					fleePosition += crit.m_Transform.position;
					repulsion++;
				}
			}
		}

		if (repulsion > 0)
		{
			fleePosition /= repulsion;
			boid.attractionVel += (m_Transform.position - fleePosition).normalized*this.intensity;
			boid.attractionEffectors++;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(this.transform.position, this.effectDistance);
	}
}

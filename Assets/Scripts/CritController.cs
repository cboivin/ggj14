using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameJam.Boids;

public class CritController : BoidsManager
{
	public static CritController Instance;

	public Critter m_Player;

	public Vector2 m_Range = new Vector2(5f, 5f);

	public List<Critter> m_Hunters = new List<Critter>();
	public List<Critter> m_Lures = new List<Critter>();

	public List<Critter> m_Crits = new List<Critter>();

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (BoidTemplate != null)
		{
			Transform transform = this.transform;
			for (int i = 0; i< boidsNumber; i++)
			{
				GameObject gameObject = (GameObject)GameObject.Instantiate(BoidTemplate, new Vector3(Random.Range(-m_Range.x, m_Range.x), 0, Random.Range(-m_Range.y, m_Range.y)), Quaternion.identity);

				Boid boid = gameObject.GetComponent<Boid>();
				if (boid)
				{
					boid.transform.parent = transform;
					boid.worldInfos = world;
					boids.Add(boid);
				}

				Critter crit = gameObject.GetComponent<Critter>();
				if (crit != null)
				{
					crit.m_Behavior = (BehaviorType)Random.Range(0, 3);
					m_Crits.Add(crit);
				}
			}

			if (m_Player != null)
			{
				m_Crits.Add(m_Player);
			}
		}
	}
}

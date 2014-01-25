using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameJam.Boids;

public class CritController : BoidsManager
{
	public static CritController Instance;

	public Critter m_Player;

	public Vector2 m_Range = new Vector2(5f, 5f);

	public List<Critter> m_Normals = new List<Critter>();
	public List<Critter> m_Hunters = new List<Critter>();
	public List<Critter> m_Steaks = new List<Critter>();

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
	protected override void Start ()
	{
		base.Start();
		if (BoidTemplate != null)
		{
			Transform transform = this.transform;

			int critTypeNum = boidsNumber/3;
			BehaviorType behaviorType = BehaviorType.Normal; 
			for (int i = 0; i< boidsNumber; i++)
			{
				GameObject gameObject = (GameObject)GameObject.Instantiate(BoidTemplate);

				Boid boid = gameObject.GetComponent<Boid>();
				Critter crit = gameObject.GetComponent<Critter>();

				if (boid && crit)
				{
					if (i!=0 && (int)behaviorType < (int)BehaviorType.Hunter && i%critTypeNum == 0)
					{
						behaviorType++;
					}
					crit.m_Behavior = behaviorType;//(BehaviorType)Random.Range(0, 3);
					m_Crits.Add(crit);

					boid.transform.parent = transform;
					boid.transform.position = new Vector3(Random.Range(-m_Range.x, m_Range.x), Random.Range(-m_Range.y, m_Range.y), 0);
					boid.worldInfos = world;
					boid.layer = (int)crit.m_Behavior;
					boids.Add(boid);
				}
			}

			if (m_Player != null)
			{
				m_Crits.Add(m_Player);
			}
		}
	}
}

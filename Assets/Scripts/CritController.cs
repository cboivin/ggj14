using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CritController : MonoBehaviour {

	public float m_CritNumber = 50;
	public float m_Range = 5f;
	public Critter m_CritPrefab;
	public List<Transform> m_Away;
	public List<Transform> m_Lure;

	private List<Critter> m_Crits = new List<Critter>();

	// Use this for initialization
	void Start ()
	{
		if (m_CritPrefab != null)
		{
			Transform transform = this.transform;
			for (int i = 0; i< m_CritNumber; i++)
			{
				Critter crit = (Critter)GameObject.Instantiate(m_CritPrefab, new Vector3(Random.Range(-m_Range, m_Range), 0, Random.Range(-m_Range, m_Range)), Quaternion.identity);
				crit.transform.parent = transform;
				crit.m_Behavior = (Behavior)Random.Range(0, 3);
				m_Crits.Add(crit);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (Critter crit in m_Crits)
		{
			if (crit.m_Behavior == Behavior.Fear)
			{
				foreach(Transform t in m_Away)
				{
					if ((t.position - crit.transform.position).magnitude < crit.m_BehaviorRange)
					{
						crit.m_RigidBody.AddForce(crit.transform.position - t.position); 
					}
				}
			}
			else if (crit.m_Behavior == Behavior.Angry)
			{
				foreach (Transform t in m_Lure)
				{
					if ((t.position - crit.transform.position).magnitude < crit.m_BehaviorRange)
					{
						crit.m_RigidBody.AddForce(t.position - crit.transform.position); 
					}
				}
			}
		}
	}
}

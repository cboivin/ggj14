using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameJam.Boids;

public class CritController : BoidsManager
{
	public static CritController Instance;

	public int m_HuntersNumber = 1;
	public int m_SteakNumber = 1;

	public Critter m_Player;

	public Vector2 m_Range = new Vector2(5f, 5f);

	public List<Critter> m_Normals = new List<Critter>();
	public List<Critter> m_Hunters = new List<Critter>();
	public List<Critter> m_Steaks = new List<Critter>();

	public List<Critter> m_Crits = new List<Critter>();

	private PopPoints popPoints;

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
		this.popPoints = GameObject.FindObjectOfType<PopPoints>();

		// Init Player

		if (BoidTemplate != null)
		{
			if (m_Player != null)
			{
				m_Crits.Add(m_Player);

				Boid boid = m_Player.GetComponent<Boid>();
				if (boid)
				{
					boid.worldInfos = world;
					boid.layer = (int)m_Player.m_Behavior;
					boids.Add(boid);
				}
			}
		}

		this.InitialPop();
		this.StartContinuousPop();
	}

	#region Instanciate Boids

	public void StartContinuousPop() {
		this.StartCoroutine(this.ContinuousPopCoroutine());
	}

	public void InitialPop() {
		for( int i = 0; i < this.boidsNumber - 1; i ++ ) {
			Vector3 popPos = UnityEngine.Random.insideUnitCircle;
			popPos.Scale(Vector2.right * 2 + Vector2.up * 1);
			popPos *= 15;
			this.InstanciateBoid(popPos, BehaviorType.Normal);
		}
	}

	public void PopHunter() {
		PopPoint p = null;
		do {
			p = this.popPoints.GetPopPoint();
			Debug.Log("search");
		} while ( p == null );
		
		this.InstanciateBoid(p.transform.position, BehaviorType.Hunter);
	}

	protected IEnumerator ContinuousPopCoroutine() {
		int huntersNumber = m_HuntersNumber;
		int steaksNumber = m_SteakNumber;

		while( true )
		{
			while( this.boids.Count >= this.boidsNumber ) {
				yield return new WaitForEndOfFrame();
			}

			PopPoint p = null;
			do {
				p = this.popPoints.GetPopPoint();
				yield return new WaitForEndOfFrame();
			} while ( p == null );

			this.InstanciateBoid(p.transform.position, BehaviorType.Normal);
		}
	}

	public void InstanciateBoid(Vector3 PopPos, BehaviorType type) {
		GameObject gameObject = (GameObject)GameObject.Instantiate(BoidTemplate, PopPos, Quaternion.identity);
		
		Boid boid = gameObject.GetComponent<Boid>();
		Critter crit = gameObject.GetComponent<Critter>();
		
		if (boid && crit)
		{
			crit.m_Behavior = type;
			if (m_Crits.Count == 2)
			{
				Debug.Log("CREATE STEAK");
				crit.m_WillBecomeSteak = true;
			}

			switch ( this.m_Player.m_Behavior )
			{
				case BehaviorType.Hunter:
					crit.m_Display = BehaviorType.Steak;
					break;
				case BehaviorType.Normal:
					switch (crit.m_Behavior)
					{
					case BehaviorType.Normal:
						crit.m_Display = BehaviorType.Normal;
						break;
							
					case BehaviorType.Hunter:
						crit.m_Display = BehaviorType.Hunter;
						break;
							
					case BehaviorType.Steak:
						crit.m_Display = BehaviorType.Steak;
						break;
					}
					break;
				case BehaviorType.Steak:
					crit.m_Display = BehaviorType.Hunter;
					break;
			}

			switch (crit.m_Behavior)
			{
				case BehaviorType.Normal:
					m_Normals.Add(crit);
					break;
					
				case BehaviorType.Hunter:
					m_Hunters.Add(crit);
					break;
					
				case BehaviorType.Steak:
					m_Steaks.Add(crit);
					break;
			}

			m_Crits.Add(crit);
			
			boid.worldInfos = world;
			boid.layer = (int)crit.m_Behavior;
			boids.Add(boid);
		}

	}

	#endregion

	override protected void keepBoidsNumber()
	{
	}

	public void CreateNewSteak()
	{
		Debug.Log("Create new steak");
		int index;
		int tryCount = 0;
		do
		{
			index = Random.Range(0, m_Normals.Count);
			++tryCount;
		} while(m_Normals[index].m_CritterType == CritterType.Player && tryCount < 100);
		//Debug.Log("tryCount = " + tryCount);
		if (m_Normals[index].m_CritterType == CritterType.Player)
		{
			//Debug.Log("no new steak...");
			return;
		}

		Critter newSteak = m_Normals[index];
		newSteak.m_WillBecomeSteak = true;
		/*newSteak.m_Behavior = BehaviorType.Steak;
		newSteak.m_Display = BehaviorType.Steak;

		m_Normals.Remove(newSteak);
		m_Steaks.Add(newSteak);*/
	}

	public void AddSteak(Critter newSteak)
	{
		newSteak.m_Behavior = BehaviorType.Steak;
		newSteak.m_Display = BehaviorType.Steak;

		m_Normals.Remove(newSteak);
		m_Steaks.Add(newSteak);
	}
	
	public void CritterCollision(Critter critter1, Critter critter2)
	{
		if (critter1.mId == critter2.mId)
		{
			return;
		}
		if (critter1.mId == -1 || critter2.mId == -1)
		{
			return;
		}

		BehaviorType critter1Behavior = critter1.m_Behavior;
		BehaviorType critter2Behavior = critter2.m_Behavior;
		//Debug.Log("Collision " + critter1Behavior + "/" + critter2Behavior + "(" + critter1.mId + "/" + critter2.mId + ")");
		Critter eater = null;
		Critter victim = null;
		BehaviorType nextState = BehaviorType.Hunter;
		BehaviorType nextDisplay = BehaviorType.Hunter;

		if (critter1Behavior == BehaviorType.Hunter)
		{
			eater = critter1;
			victim = critter2;
		}
		else if (critter1Behavior == BehaviorType.Normal && critter2Behavior == BehaviorType.Steak)
		{
			eater = critter1;
			victim = critter2;
		}

		//Debug.Log(eater.m_Behavior + " ate " + victim.m_Behavior + "! it becomes a " + nextState);

		victim.mId = -1;

		m_Crits.Remove(victim);
		//m_Hunters.Remove(victim);
		m_Normals.Remove(victim);
		m_Steaks.Remove(victim);

		// Replace a steak eaten by a new one
		if (victim.m_Behavior == BehaviorType.Steak && m_Steaks.Count == 0 && m_Normals.Count > 0 && (m_Hunters.Count == 0 || nextState == eater.m_Behavior))
		{
			CreateNewSteak();
		}

		if (nextState != eater.m_Behavior)
		{
			foreach(Critter crit in m_Hunters)
			{
				if (crit.m_CritterType == CritterType.Player)
				{
					foreach(Critter crit2 in m_Normals)
					{
						if (crit2 != eater)
						{
							crit2.m_Display = BehaviorType.Hunter;
						}
					}
				}
				//Debug.Log("Hunter become Steak");
				//crit.m_Behavior = BehaviorType.Steak;
				//crit.m_Display = BehaviorType.Steak;
				crit.m_Behavior = BehaviorType.Normal;
				crit.m_Display = BehaviorType.Normal;
			}
			//m_Steaks.AddRange(m_Hunters);
			m_Normals.AddRange(m_Hunters);
			m_Hunters.Clear();

			m_Normals.Remove(eater);
			m_Hunters.Add(eater);
		}

		if (eater.m_CritterType == CritterType.Player)
		{
			ScoreManager.AddScore();
			if (nextState == BehaviorType.Hunter)
			{
				foreach (Critter crit in m_Normals)
				{
					if (crit != eater)
					{
						crit.m_Display = BehaviorType.Steak;
					}
				}
			}
		}

		Boid boid = victim.GetComponent<Boid>();
		if (boid != null)
		{
			boids.Remove(boid);
		}

		Destroy(victim.gameObject);
		eater.m_Behavior = nextState;
		eater.m_Display = nextDisplay;
	}
}

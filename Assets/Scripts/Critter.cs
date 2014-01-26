using UnityEngine;
using System.Collections.Generic;
using GameJam.Boids;

public enum BehaviorType
{
	Normal,
	Steak,
	Hunter
}

public enum CritterType
{
	Normal,
	Player,
}

public class Critter : MonoBehaviour
{
	public int mId = -1;
	public CritterType m_CritterType = CritterType.Normal;
	public BehaviorType m_Behavior = BehaviorType.Normal;
	public BehaviorType m_Display = BehaviorType.Normal;
	public Boid m_Boid;
	public SpriteRenderer m_Sprite;

	[HideInInspector]
	public Transform m_Transform;

	public Behavior m_NormalBehavior;
	public Behavior m_SteakBehavior;
	public Behavior m_HunterBehavior;

	public Sprite m_NormalTex;
	public Sprite m_SteakTex;
	public Sprite m_HunterTex;
	public Sprite m_HungryTex;

	public GameObject m_RepulsorPrefab;

	private BehaviorType m_SavedBehavior;
	private BehaviorType m_SavedDisplay;
	private float m_PreviousX;
	private float m_CurrentSteakTime;

	private const float HUNTER_SCALE = 1.5f;
	private const float NORMAL_SCALE = 1.0f;
	private const float STEAK_SCALE = 1.0f;
	private const float PLAYER_STEAK_SCALE = 1.0f;

	private const float PLAYER_STEAK_TIME = 5.0f;

	public static int static_id = 0;

	void Start()
	{
		mId = static_id++;
		m_Transform = transform;
		m_PreviousX = m_Transform.position.x;
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_CurrentSteakTime > 0)
		{
			m_CurrentSteakTime = Mathf.Max(0, m_CurrentSteakTime - Time.deltaTime);
			//Debug.Log("STEAK TIME");
			if (m_CurrentSteakTime == 0)
			{
				m_Behavior = BehaviorType.Normal;
				m_Display = BehaviorType.Normal;

				foreach (Critter crit in CritController.Instance.m_Normals)
				{
					crit.m_Display = BehaviorType.Normal;
				}

				CritController.Instance.m_Steaks.Remove(this);
				CritController.Instance.m_Normals.Add(this);
				CritController.Instance.CreateNewSteak();
			}
		}

		if (m_SavedBehavior != m_Behavior)
		{
			// update boid layer to match critter behavior
			if (m_Boid != null)
			{
				m_Boid.layer = (int)m_Behavior;
			}
			if (m_Behavior == BehaviorType.Steak && m_CritterType == CritterType.Player)
			{
				//Debug.Log("STEAK TIME");
				m_CurrentSteakTime = PLAYER_STEAK_TIME;
			}

			m_SavedBehavior = m_Behavior;
		}

		if (m_SavedDisplay != m_Display)
		{
			UpdateDisplay();
			if (m_RepulsorPrefab)
			{
				GameObject repulsor = (GameObject)Instantiate(m_RepulsorPrefab);
				repulsor.transform.parent = m_Transform;
			}	
			m_SavedDisplay = m_Display;
		}

		UpdateScale();

		Vector3 localScale = m_Sprite.transform.localScale;
		if (m_PreviousX >= m_Transform.position.x)
		{
			localScale.x = -Mathf.Abs(localScale.x);
		}
		else
		{
			localScale.x = Mathf.Abs(localScale.x);
		}
		m_Sprite.transform.localScale = localScale;
		m_PreviousX = m_Transform.position.x;

		m_Sprite.sortingOrder = (int)(m_Transform.transform.position.y * -100);

		if (m_Boid != null)
		{
			Behavior behavior = null;
			switch(m_Behavior)
			{
				case BehaviorType.Normal:
					behavior = m_NormalBehavior;
					break;

				case BehaviorType.Steak:
					behavior = m_SteakBehavior;
					break;

				case BehaviorType.Hunter:
					behavior = m_HunterBehavior;
					break;
			}

			if(behavior!=null)
			{
				behavior.ApplyBehavior(m_Boid);
			}
		}
	}

	void CheckCollider(Collider collider)
	{
		if (m_Behavior != BehaviorType.Steak && mId != -1)
		{
			Critter critter = collider.GetComponent<Critter>();
			if (critter != null && critter.m_Behavior != m_Behavior && critter.m_Behavior != BehaviorType.Hunter)
			{
				CritController.Instance.CritterCollision(this, critter);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		CheckCollider(collider);
	}

	void OnTriggerStay(Collider collider)
	{
		CheckCollider(collider);
	}

	void UpdateDisplay()
	{
		switch(m_Display)
		{
			case BehaviorType.Hunter:
			m_Sprite.sprite = m_CritterType == CritterType.Normal ? m_HunterTex : m_HungryTex;
			break;

			case BehaviorType.Normal:
			m_Sprite.sprite = m_NormalTex;
			break;

			case BehaviorType.Steak:
			m_Sprite.sprite = m_SteakTex;
			break;
		}
	}

	void UpdateScale()
	{
		switch(m_Display)
		{
			case BehaviorType.Hunter:
				m_Transform.localScale = new Vector3(Mathf.Min(HUNTER_SCALE, m_Transform.localScale.x + 0.1f * Time.timeScale), Mathf.Min(HUNTER_SCALE, m_Transform.localScale.y + 0.1f * Time.timeScale), 1);
				break;

			case BehaviorType.Normal:
				if (m_Transform.localScale.x < NORMAL_SCALE)
				{
					m_Transform.localScale = new Vector3(Mathf.Min(NORMAL_SCALE, m_Transform.localScale.x + 0.1f * Time.timeScale), Mathf.Min(NORMAL_SCALE, m_Transform.localScale.y + 0.1f * Time.timeScale), 1);
				}
				if (m_Transform.localScale.x > NORMAL_SCALE)
				{
					m_Transform.localScale = new Vector3(Mathf.Max(NORMAL_SCALE, m_Transform.localScale.x - 0.1f * Time.timeScale), Mathf.Max(NORMAL_SCALE, m_Transform.localScale.y - 0.1f * Time.timeScale), 1);
				}
				break;

			case BehaviorType.Steak:
				m_Transform.localScale = new Vector3(Mathf.Max(m_CritterType==CritterType.Normal ? STEAK_SCALE : PLAYER_STEAK_SCALE, m_Transform.localScale.x - 0.1f * Time.timeScale), Mathf.Max(m_CritterType==CritterType.Normal ? STEAK_SCALE : PLAYER_STEAK_SCALE, m_Transform.localScale.y - 0.1f * Time.timeScale), 1);
				break;
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		if (m_Behavior == BehaviorType.Normal)
		{
			Gizmos.color = Color.blue;
		}
		else if (m_Behavior == BehaviorType.Steak)
		{
			Gizmos.color = Color.red;  
		}
		Gizmos.DrawWireSphere(this.transform.position, 2);
	}
}

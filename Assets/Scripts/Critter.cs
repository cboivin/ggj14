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
	public CritterType m_CritterType = CritterType.Normal;
	public BehaviorType m_Behavior = BehaviorType.Normal;
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

	private BehaviorType m_SavedBehavior;
	private float m_PreviousX;

	private const float HUNTER_SCALE = 1.5f;
	private const float NORMAL_SCALE = 1.0f;
	private const float STEAK_SCALE = 0.5f;

	void Start()
	{
		m_Transform = transform;
		m_PreviousX = m_Transform.position.x;
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_SavedBehavior != m_Behavior)
		{
			UpdateDisplay();
			m_SavedBehavior = m_Behavior;
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

	void UpdateDisplay()
	{
		switch(m_Behavior)
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
		switch(m_Behavior)
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
				m_Transform.localScale = new Vector3(Mathf.Max(STEAK_SCALE, m_Transform.localScale.x - 0.1f * Time.timeScale), Mathf.Max(STEAK_SCALE, m_Transform.localScale.y - 0.1f * Time.timeScale), 1);
				break;
		}
	}
}

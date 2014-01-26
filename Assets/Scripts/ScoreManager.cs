using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;
	public int m_PointPerKill = 1;

	public GUIText m_ScoreLabel;
	public GameObject m_ScoreContainer; // grow anim ?
		
	[HideInInspector]
	public int m_Score = 0;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public static void AddScore()
	{
		Instance.AddScorePrivate();
	}

	public static int getScore() {
		return Instance.m_Score;
	}

	private void AddScorePrivate()
	{
		m_Score += m_PointPerKill;

		m_ScoreLabel.text = m_Score.ToString();
	}
}

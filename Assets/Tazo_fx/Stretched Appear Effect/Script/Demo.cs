using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour
{
	public Material m_Mat = null;
	public bool m_top2bottom = true;
	[Range(0f, 1f)] public float m_Rate = 0f;
	[Range(0.001f, 0.1f)] public float m_AutoRunSpeed = 0.003f;
	private bool m_AutoRun = false;
	
	private void Start ()
	{
		if (!SystemInfo.supportsImageEffects)
			enabled = false;
	}
	private void OnRenderImage (RenderTexture src, RenderTexture dst)
	{
		if (m_top2bottom)
		{
			m_Mat.EnableKeyword("AE_TOP_BOTTOM");
			m_Mat.DisableKeyword("AE_BOTTOM_TOP");
		}
		else
		{
			m_Mat.EnableKeyword("AE_BOTTOM_TOP");
			m_Mat.DisableKeyword("AE_TOP_BOTTOM");
		}
		m_Mat.SetFloat ("_Rate", m_Rate * 9.999f);
		Graphics.Blit (src, dst, m_Mat);
	}
	private void Update ()
	{
		if (m_AutoRun)
		{
			m_Rate += m_AutoRunSpeed;
			if (m_Rate >= 1f)
			{
				m_Rate = 1f;
				m_AutoRun = false;
			}
		}
	}
	private void OnGUI ()
	{
		GUI.Box (new Rect (10, 10, 260, 25), "Stretched Appear Effect Demo");
		if (GUI.Button (new Rect (10, 40, 100, 30), "Let's GO !"))
		{
			m_AutoRun = true;
			m_Rate = 0f;
		}
	}
}
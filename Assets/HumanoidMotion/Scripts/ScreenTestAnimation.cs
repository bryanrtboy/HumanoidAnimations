using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTestAnimation : MonoBehaviour
{

	public Canvas m_canvas;
	public GameObject m_buttonPrefab;

	public Animator m_animator;


	void Awake ()
	{
		if (m_animator == null)
			m_animator = this.GetComponent<Animator> () as Animator;

		AnimatorControllerParameter[] parameters = m_animator.parameters;
		MakeButtons (parameters);
	}

	public void TriggerAnimation (string t)
	{
		m_animator.SetTrigger (t);
	}

	void MakeButtons (AnimatorControllerParameter[] p)
	{
		foreach (AnimatorControllerParameter ap in p) {

			if (ap.type == AnimatorControllerParameterType.Trigger) {
				GameObject g = Instantiate (m_buttonPrefab, Vector3.zero, Quaternion.identity);
				if (m_canvas != null)
					g.transform.SetParent (m_canvas.transform, false);

				Button b = g.GetComponentInChildren<Button> () as Button;
				Text t = g.GetComponentInChildren<Text> () as Text;

				b.name = ap.name;
				t.text = ap.name;
				b.onClick.AddListener (delegate {
					TriggerAnimation (ap.name);
				});
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTestAnimation : MonoBehaviour
{

	public Canvas m_canvas;
	public GameObject m_buttonPrefab;
	public Animator m_animator;
	public GameObject m_lookAtObj;
	public bool m_lookAt = false;

	int m_layer = 1;
	float m_lookSmoother = 1f;
	float lookWeight = 0;

	void Awake ()
	{
		if (m_animator == null)
			m_animator = this.GetComponent<Animator> () as Animator;

		AnimatorControllerParameter[] parameters = m_animator.parameters;
		MakeButtons (parameters);
	}

	void OnAnimatorIK (int layerIndex)
	{
		m_animator.SetLookAtWeight (lookWeight, .5f, 1f, 1f);

		if (m_lookAt) {
			m_animator.SetLookAtPosition (m_lookAtObj.transform.position);
			lookWeight = Mathf.Lerp (lookWeight, 1f, Time.deltaTime * m_lookSmoother);
		} else {
			lookWeight = Mathf.Lerp (lookWeight, 0f, Time.deltaTime * m_lookSmoother);
		}

	}

	public void TriggerAnimation (string t)
	{
		m_animator.SetTrigger (t);
	}

	public void WeightAmount (float amount)
	{
		m_animator.SetLayerWeight (m_layer, amount);
	}

	public void WeightLayer (int layer)
	{
		m_layer = layer;
	}

	public void LookAt (bool b)
	{
		m_lookAt = b;
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

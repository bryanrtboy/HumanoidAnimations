using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

	Text m_text;

	public void UpdateSliderText (float f)
	{
		if (m_text == null)
			m_text = this.GetComponent<Text> () as Text;

		if (m_text != null)
			m_text.text = f.ToString ("##");
	}
}

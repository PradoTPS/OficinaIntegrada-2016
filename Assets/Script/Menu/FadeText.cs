using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
	}
		
	public IEnumerator FadeTextToFullAlpha(float time, Text txt)
	{
		txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 0);
		while (txt.color.a < 1.0f)
		{
			txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + (Time.deltaTime / time));
			yield return null;
		}
		StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
	}

	public IEnumerator FadeTextToZeroAlpha(float time, Text txt)
	{
		txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
		while (txt.color.a > 0.0f)
		{
			txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a - (Time.deltaTime / time));
			yield return null;
		}
		StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayManager : MonoBehaviour {

	public Text displayText;
	public float displayTime;
	public float fadeTime;

	private IEnumerator fadeAlpha;

	public DisplayManager displayManager;

	public void DisplayMessage (string message) {
		displayText.text = message;
		SetAlpha ();
	}

	void SetAlpha () {
		if (fadeAlpha != null) {
			StopCoroutine (fadeAlpha);
		}
		fadeAlpha = FadeAlpha ();
		StartCoroutine (fadeAlpha);
	}

	IEnumerator FadeAlpha () {
		Color resetColor = displayText.color;
		resetColor.a = 1;
		displayText.color = resetColor;

		yield return new WaitForSeconds (displayTime);

		while (displayText.color.a > 0) {
			Color displayColor = displayText.color;
			displayColor.a -= Time.deltaTime / fadeTime;
			displayText.color = displayColor;
			yield return null;
		}
		yield return null;
	}
}
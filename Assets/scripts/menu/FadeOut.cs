using UnityEngine;
using System.Collections;

public class FadeOut : BarnBehaviour 
{
	public Texture2D _texture;
	public Color     _color;
	public float     _fadeDurationInSeconds = 1.0f;


	private float _startTime;

    //##################################################################################################
    // METHODS

	public void StartFade()
	{
		this.enabled = true;

		_color.a = 1.0f;
		_startTime = Time.time;
		Destroy(this, _fadeDurationInSeconds+1 ); // +1 for safety TODO betterify
	}
	
	void OnGUI()
	{
		_color.a = Mathf.Lerp(0.0f, 1.0f, (Time.time-_startTime)/_fadeDurationInSeconds);
		GUI.color = _color;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _texture);
	}


	public float FadeDurationInSeconds
	{
		get { return _fadeDurationInSeconds; }
		set { _fadeDurationInSeconds = value; }
	}
}

using UnityEngine;
using System.Collections;

public class FadeIn : BarnBehaviour 
{

    public Texture2D _texture;
	public Color     _color;
	public float     _fadeDurationInSeconds = 1.0f;


	private float    _startTime;


	void OnLevelWasLoaded ( int level)
	{
		_color.a = 1.0f;
		_startTime = Time.time;
		Destroy(this, _fadeDurationInSeconds);
	}
	

	void OnGUI()
	{
		_color.a = Mathf.Lerp(1.0f, 0.0f, (Time.time-_startTime)/_fadeDurationInSeconds);
		GUI.color = _color;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), _texture);
	}


	public float FadeDurationInSeconds
	{
		get { return _fadeDurationInSeconds; }
		set { _fadeDurationInSeconds = value; }
	}
}

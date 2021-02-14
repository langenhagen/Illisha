using UnityEngine;
using System.Collections;

public class OnCreationGrow : BarnBehaviour
{

	private Vector3 _targetScale;
    
	// The amount of addet scale on each frame
	public Vector3 _scaler;

	private bool _firstrun = true;

	// Use this for initialization
	void Start ()
	{
		Vector3 localScale = gameObject.transform.localScale;
		_targetScale = new Vector3( localScale.x,
									localScale.y,
									localScale.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 localScale = gameObject.transform.localScale;
		Vector3 newScale = localScale;

		if( _firstrun)
		{
			gameObject.transform.localScale = _targetScale/3;
			_firstrun = false;
			return;
		}


        if( _targetScale.x >= localScale.x)
			newScale.x += _scaler.x * Time.deltaTime;
		if( _targetScale.y >= localScale.y)
			newScale.y += _scaler.y * Time.deltaTime;
		if( _targetScale.z >= localScale.z)
			newScale.z += _scaler.z * Time.deltaTime;

		if( _targetScale.x <= localScale.x &&
		    _targetScale.y <= localScale.y &&
		    _targetScale.z <= localScale.z)
		{
			newScale = _targetScale;
			enabled = false;
		}
				
		gameObject.transform.localScale = newScale;
	}
}

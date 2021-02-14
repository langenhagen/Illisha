using UnityEngine;

public class OnDeathRotation : OnDeath {

	public float _time = 2.0f;

    private float _downScaleFactor = 0.9f;

    void Start()
    {
        if (Random.value < 0.08f)
        {
            _downScaleFactor = 0.98f;
        }
    }

    void Update()
	{

        transform.Rotate(_time * 5, _time * 6, _time * 7);

		Vector3 newScale = transform.localScale;
        newScale.x = newScale.x * _downScaleFactor;
        newScale.y = newScale.y * _downScaleFactor;
        newScale.z = newScale.z * _downScaleFactor;

		transform.localScale = newScale;

		_time -= Time.deltaTime;

		if( newScale.x <= 0.1f)
		{
			Destroy(gameObject);
		}
	}


	public override void Die()
	{
		enabled = true;
		Invoke ("Destroy",_time);
	}


}
using UnityEngine;

public class OnDeathExplosion : OnDeath {

	// the explosion object
	public GameObject _explosion;

	
	public override void Die()
	{
		Instantiate (_explosion, this.transform.position, new Quaternion());

		Destroy();
	}
}
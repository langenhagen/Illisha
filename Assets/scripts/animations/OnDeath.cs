using UnityEngine;

public class OnDeath : BarnBehaviour
{
	public virtual void Die()
	{
		// Standard implementation - overrride this for more sophisticated ways of Death :)
		Destroy();
	}

	protected void Destroy()
	{
		Destroy (gameObject);
	}
}
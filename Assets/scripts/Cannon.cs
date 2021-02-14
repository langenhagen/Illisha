using UnityEngine;


public class Cannon : BarnBehaviour
{
    public Rigidbody _projectile;

    // The text mesh on the _projectile
    private TextMesh _projectileTextMesh;


	public float _initialProjectileSpeed = 100.0f;


    private CannonMovement  _cannonMovement;
    private CannonTargeting _cannonTargeting;


	//##################################################################################################
	// METHODS
	

	void Start () 
	{
		_projectileTextMesh = _projectile.GetComponent<TextMesh>();

        _cannonMovement  = GetComponent<CannonMovement>();
        _cannonTargeting = GetComponent<CannonTargeting>();
	}
	

	void Update () 
	{
		string inputString = Input.inputString;
		if( 0 < inputString.Length)
		{
			char c = inputString[0];
			
			if (c != '\b' && c != '\n' && c != '\r')
			{
				Fire( c);
			}
		}
	}


	private void Fire ( char key)
	{
        Session session = GameManager.Session;

		_projectileTextMesh.text = new string( key, 1);

        Rigidbody newProjectile = Instantiate(_projectile, transform.position, transform.rotation) as Rigidbody;
		newProjectile.velocity  = transform.TransformDirection(0, _initialProjectileSpeed, 0);	

        _cannonMovement.Resume();

        // remove glyph from glyph list if it's clear that it will be hit
        GameObject glyph = _cannonTargeting.AimsAt();
        if (glyph != null && glyph.GetComponent<Glyph>().Letter == key && session.Word.Glyphs.Count > 1)
        {
            session.Word.Glyphs.Remove(glyph);
        }


        GameManager.Game.NumShots++;
        session.ShotTimestamps.Push(Time.time);
	}
	
}


using UnityEngine;

public class Bullet : BarnBehaviour
{

    public float _speed   = 100.0f; 

	public float _timeOut = 0.6f;

    public OnDeath _onDeathCorrect;
    public OnDeath _onDeathWrong;

    public char Letter { get; set; }

	//##################################################################################################
	// METHODS

	// Kill the bullet after a while automatically
	void Start ()
	{
        Letter = this.GetComponent<TextMesh>().text[0];

        Invoke("TimeoutDie", _timeOut);
	}

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += _speed * Time.deltaTime;
        transform.position = newPosition;
    }


	public void OnCollisionEnter ( Collision collision)
	{
        Glyph glyph = collision.collider.gameObject.GetComponent<Glyph>();

        if (glyph != null)
		{
            if ( glyph.Letter == Letter)
			{
                _onDeathCorrect.Die ();
			}
            else
            {
                _onDeathWrong.Die();
            }
		}
	}

    public void TimeoutDie()
    {        
        _onDeathWrong.Die();
    }


    //##################################################################################################
    // GETTERS & SETTERS

}



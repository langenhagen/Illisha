using UnityEngine;
using System.Collections.Generic;



// TODO cleanup, privatize, re-work
public class CannonMovement : BarnBehaviour 
{
	public enum State
	{
		Move,
		MoveToLeft,
		Pause
	}

	public enum MovementDirection
	{
		Left,
		Right
	}

	//##################################################################################################
	// VARS
	 
	public Game.Mode   _mode;                   // describes the type of cursor movement

	public State _state = State.Move;	        // stores the state of the CannonMovement

	public Vector3 _target = Vector3.zero;

    public float _speed = 1.0f;                 // the speed var to be adjusted via unity editor
    public float _fastspeed = 10.0f;            // speed for moving to nearest bound or for jumping to special position
	public float _minspeed = 0.3f;


	public float _gearUpNumber   = 0.3f;
	public float _gearDownNumber = 0.2f;

	private CannonTargeting _cannonTargeting;
	
	public MovementDirection _movementDirection = MovementDirection.Right;

	public float _pauseOnNewWordTime = 0.2f;    // in seconds
    public float _pauseForNSeconds;             // in seconds

	private Vector3 _dummyvec;


    //##################################################################################################
    // METHODS

    
    void Start()
	{
        _mode = GameManager.Game.GameMode;

		_cannonTargeting = GetComponent<CannonTargeting>();
		_dummyvec = new Vector3(13.7f, 42.12f, 69.88f);
		_target = _dummyvec;
	}


    void Update () 
    {
		// init Target here bc in start it may be bullshit 
        //XXX solution error prone and dirty
		if(_target == _dummyvec)
        {
            List<GameObject> glyphs = GameManager.Session.Word.Glyphs;

			Target = glyphs[ glyphs.Count-1].transform.position;
            FastMoveToBeginning();
        }

		if(_state == State.Pause)
        {
            _pauseForNSeconds = Pause (_pauseForNSeconds);
        }
		else if(_state == State.MoveToLeft)
		{
			if( FastMoveToBeginning())
                _pauseForNSeconds = Pause(_pauseOnNewWordTime);
		}
		else if( _state == State.Move)
		{
			switch(_mode)
            {
                case Game.Mode.TypeWriter:
                    ModeTypewriter();
                    break;
                case Game.Mode.PingPong:
                    ModePingPong();
                    break;
                default:
                    Log("CannonMovement.Update(): Mode " + _mode + " not handleable!", Logger.Error);
                    break;
            }
			
		}
    }


    /// <summary>
    /// Translates the cannon fast into direction of the leftmost letter.
    /// Returns true, if the leftmost letter x position is reached.
    /// </summary>
    /// <returns><c>true</c>, if movement is completed, <c>false</c> otherwise.</returns>
	public bool FastMoveToBeginning()
	{
        bool ret = false;

		if( _state != State.MoveToLeft)
		{
			Target = LeftRim();
			_state = State.MoveToLeft;
		}

		float xDist = Mathf.Max ( 1, Mathf.Abs( transform.position.x - LeftRim().x));
		transform.position = Vector3.MoveTowards ( transform.position, Target, _fastspeed * xDist * Time.deltaTime);
		if( transform.position == Target)
			ret = true;

        return ret;
	}


    public float Pause( float seconds)
    {
        if( _state != State.Pause && seconds > 0)
        {
            _state = State.Pause;
        }
        else if( seconds <= 0)
        {
            Resume ();
        }

        return Mathf.Max ( 0, seconds - Time.deltaTime);
    }

    /// <summary>
    /// Resumes the movement.
    /// </summary>
    public void Resume()
    {
        if( _state == State.Pause)
            _state = State.Move;
    }


	// TODO fastify
	public void ModePingPong()
	{
		List<GameObject> glyphs = GameManager.Session.Word.Glyphs;
		bool isAiming = _cannonTargeting.AimsAt();
		float halfGlyphSizeX = glyphs[0].collider.bounds.size.x/2;

		Vector3 leftRim  = LeftRim();
		Vector3 rightRim = RightRim();


		// update Target if Target is out of sync
		if(Target != leftRim && Target != rightRim)
		{
			if( _movementDirection == MovementDirection.Left)
				Target = leftRim;
			else
				Target = rightRim;
		}


		// set speed
		float speed;
		if( isAiming)
			speed = _speed * Time.deltaTime;
		else
		{
			// different in every step! ergo ==> different multiplier every time!
			float xDist = Mathf.Abs( transform.position.x - NearestGlyph(_movementDirection).transform.position.x);

			speed = _fastspeed * xDist * Time.deltaTime; 
		}
		// actual movement & targeting update
		transform.position = Vector3.MoveTowards ( transform.position, Target, speed);
		isAiming = _cannonTargeting.AimsAt();


		// Change Left/Right Targets if necessary 
		if( transform.position == leftRim ||
		   (!isAiming && Vector3.Distance ( transform.position, leftRim) < halfGlyphSizeX))
		{
			_movementDirection = MovementDirection.Right;
			Target = rightRim;
		}
		else if( transform.position == rightRim ||
		        (!isAiming && Vector3.Distance( transform.position, rightRim) < halfGlyphSizeX))
		{
			_movementDirection = MovementDirection.Left;
			Target = leftRim;
		}
	}


    public void ModeTypewriter()
    {
        Target = NearestGlyph(MovementDirection.Right).transform.position;

        if (Target.x != transform.position.x)
        {
            // different in every step! ergo ==> different multiplier every time!
            float xDist = Mathf.Abs(transform.position.x - Target.x);

            transform.position = Vector3.MoveTowards(
                transform.position, 
                Target, 
                _fastspeed * xDist * Time.deltaTime);    
        }        
    }

    // TODO use it
	public void AdjustSpeed()
	{
        // pro ausgelassenenen buchstaben langsamer werden,
        // sonst schneller.

        
		/*//XXX clean up
        var session = GameManager.Session;
        var pps     = GameManager.PPScript;
        
		
        float smallestRememberableTime = Mathf.Max (0, Time.time - pps._secondsToRememberShots);
		
		int numPossibleHitsWithinLastNSeconds = 0;
		int numHitsWithinLastNSeconds = 0;


        foreach (float timestamp in session.PossibleHitsTimestamps)
		{
			// get num of possible shots within last N seconds
			
			if( timestamp < smallestRememberableTime)
			{
				break;
			}
			
			numPossibleHitsWithinLastNSeconds++;
		}


        foreach (float timestamp in session.HitTimestamps)
		{
			// get num of hits within last N seconds
			
			if( timestamp < smallestRememberableTime)
			{
				break;
			}
			numHitsWithinLastNSeconds++;
		}
		
		//float quasiHitRate = (float)numHitsWithinLastNSeconds / numPossibleHitsWithinLastNSeconds;
		
		
		
        if( numHitsWithinLastNSeconds > pps._numHitsToSpeedUp)
		{
			SpeedUp();
		}
        else if( numHitsWithinLastNSeconds < pps._numFailsToSpeedDown)
		{
			SpeedDown();
		}*/
	}


	public void SpeedUp()
    {
		Speed += _gearUpNumber;
	}
	

	public void SpeedDown()
    {
		Speed -= _gearDownNumber;
	}



    //##################################################################################################
    // HELPERS


	/// <summary>
	/// Returns a Vector with the x component of the first element of Session.Glyphs
	/// and y and z components of the Cannon. In other words, the left rim position
	/// of the Cannon's movement path. 
	/// </summary>
	/// <returns>The left rim.</returns>
	private Vector3 LeftRim()
	{
		Vector3 ret = GameManager.Session.Word.Glyphs[0].transform.position;
		ret.y = transform.position.y;
		ret.z = transform.position.z;
		
		return ret;
	}


	/// <summary>
	/// Returns a Vector with the x component of the last element of Session.Glyphs
	/// and y and z components of the Cannon. In other words, the right rim position
	/// of the Cannon's movement path. 
	/// </summary>
	/// <returns>The right rim.</returns>
	private Vector3 RightRim()
	{
        Word w = GameManager.Session.Word;
		
		Vector3 ret = w.Glyphs[ w.Glyphs.Count-1].transform.position;
		ret.y = transform.position.y;
		ret.z = transform.position.z;
		
		return ret;
	}


    /// <summary>
    /// Finds the nearest glyoh in movement direction.
    /// </summary>
    /// <param name="direction">The direction: Left or Right.</param>
    /// <returns>Returns the Game object of the nearest glyph.</returns>
	private GameObject NearestGlyph( MovementDirection direction)
	{
        GameObject ret;

        Word w = GameManager.Session.Word;

		// std init ret
		if( direction == MovementDirection.Left)
			ret = w.Glyphs[0];
		else
			ret = w.Glyphs[ w.Glyphs.Count -1 ];


		// iterate to find nearest glyph
		foreach (GameObject glyph in w.Glyphs)
		{
			Vector3 glyphPos = glyph.transform.position;


			if(direction == MovementDirection.Left &&
			   glyphPos.x <= transform.position.x &&
			   glyphPos.x > ret.transform.position.x)
			{
				ret = glyph;
			}
			else if(direction == MovementDirection.Right &&
			   		glyphPos.x >= transform.position.x &&
			   		glyphPos.x < ret.transform.position.x)
			{
				ret = glyph;
			}
		}

		return ret;
	}

	

    //##################################################################################################
    // GETTERS & SETTERS
    
    public float Speed    { get { return _speed;                         }
                            set { _speed = Mathf.Max (value, _minspeed); }}


	public Vector3 Target { get { return _target;                        }
		                    set 
                            {
                                _target = new Vector3(value.x, transform.position.y, transform.position.z); 
                            }
    }

}

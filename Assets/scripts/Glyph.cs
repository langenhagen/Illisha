using UnityEngine;
using System.Collections;

public class Glyph : BarnBehaviour
{

    private bool _isHidden = false;

    /// <summary>
    /// The representing letter.
    /// </summary>
    private char _letter;


    public OnDeath _onDeath;
    // TODO kein on death aber ne anim .... public OnDeath _onDeathWrong;


    //##################################################################################################
    // METHODS

    void Start()
    { 
    }

	void OnCollisionEnter(Collision collision)
	{
        GameObject bullet = collision.collider.gameObject;

		// TODO beautify
		if(bullet.tag == "Bullet")
		{
			char bulletLetter = bullet.GetComponent<Bullet>().Letter;


            if (bulletLetter == _letter)
			{
                OnCorrectLetter();
			}
            else
            {
                OnWrongLetter();
            }
		}

	}

    private void OnCorrectLetter()
    {
        GameManager.Session.Word.RemoveGlyph ( gameObject);

        // in order not to distract other bullets, disable the collider 
        // of this letter immediately and independently from the onDeath animation
        gameObject.collider.enabled = false; 
        
        _onDeath.Die ();
    }

    private void OnWrongLetter()
    {

    }


    //##################################################################################################
    // GETTER & SETTER


    public char Letter      {   get { return _letter;       }
                                set
                                { 
                                    _letter = value;
                                    if( !IsHidden)
                                        GetComponentInChildren<TextMesh>().text = new string(value, 1); 
                                }
                            }


    public bool IsHidden    {   get { return _isHidden;     }
                                set
                                {
                                    _isHidden = value;
                                    if (value)
                                    {
                                        // TODO
                                        GetComponentInChildren<TextMesh>().text = "*";
                                    }
                                }
                            }

}

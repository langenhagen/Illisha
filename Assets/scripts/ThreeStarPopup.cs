using UnityEngine;
using System.Collections;

public class ThreeStarPopup : BarnBehaviour 
{
    public UISprite _star1Full;
    public UISprite _star2Full;
    public UISprite _star3Full;

    public UISprite _star1Empty;
    public UISprite _star2Empty;
    public UISprite _star3Empty;

    public UILabel _lblPoints;
    public UILabel _lblSeconds;

    public float _starActivationTimeOffset = 0.05f;

    public float _startDuration   = 0.2f;
    public float _sustainDuration = 0.3f;
    public float _endDuration     = 0.1f;

    public TweenAlpha    _tweenStartAlpha;
    public TweenScale    _tweenStartScale;
    public TweenAlpha    _tweenEndAlpha;
    public TweenPosition _tweenEndPosition;
    public TweenRotation _tweenEndRotation;
    public TweenScale    _tweenEndScale;


    private int _numStars;

    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start ()
    {
        transform.position = GameManager.UI.IngameUI.ThreeStarPopupContainer.transform.position;


        // set all stars inactive at beginning
        _star1Full.gameObject.SetActive(false);
        _star2Full.gameObject.SetActive(false);
        _star3Full.gameObject.SetActive(false);
        _star1Empty.gameObject.SetActive(false);
        _star2Empty.gameObject.SetActive(false);
        _star3Empty.gameObject.SetActive(false);

        _tweenStartAlpha.duration  = _startDuration;
        _tweenStartScale.duration  = _startDuration;

        _tweenEndAlpha.duration    = _endDuration;
        _tweenEndPosition.duration = _endDuration;
        _tweenEndRotation.duration = _endDuration;
        _tweenEndScale.duration    = _endDuration;


        _tweenEndPosition.SetStartToCurrentValue();

        _tweenEndPosition.to = new Vector3(
            _tweenEndPosition.from.x, 
            _tweenEndPosition.from.y + 100, 
            0);

        Invoke("FadeOut", _startDuration + _sustainDuration);
        Destroy(gameObject, _startDuration + _sustainDuration + _endDuration);
        

        PopUp();
	}
	

    public void PopUp()
    {
        _tweenStartAlpha.enabled = true;
        _tweenStartScale.enabled = true;

        // TODO play sounds
        switch (_numStars)
        {
            case 0:
                Invoke("ActivateStar1Empty", _starActivationTimeOffset);
                Invoke("ActivateStar2Empty", _starActivationTimeOffset);
                Invoke("ActivateStar3Empty", _starActivationTimeOffset);
                break;
            case 1:
                Invoke("ActivateStar1Full",  _starActivationTimeOffset);
                Invoke("ActivateStar2Empty", 2*_starActivationTimeOffset);
                Invoke("ActivateStar3Empty", 2*_starActivationTimeOffset);
                break;
            case 2:
                Invoke("ActivateStar1Full",  _starActivationTimeOffset);
                Invoke("ActivateStar2Full",  2*_starActivationTimeOffset);
                Invoke("ActivateStar3Empty", 3*_starActivationTimeOffset);
                break;
            case 3:
                Invoke("ActivateStar1Full",  _starActivationTimeOffset);
                Invoke("ActivateStar2Full", 2*_starActivationTimeOffset);
                Invoke("ActivateStar3Full", 3*_starActivationTimeOffset);
                break;
            default:
                Log("Number of stars =" + _numStars + " not tolerated by the script!", Logger.Error);
                break;
        }

    }

    public void FadeOut()
    {
        _tweenEndAlpha.enabled    = true;
        _tweenEndPosition.enabled = true;
        _tweenEndRotation.enabled = true;
    }

    public void SetValues(int numStars, int points, int seconds)
    {
        _numStars = numStars;

        _lblPoints.text = "+ " + points;
        _lblSeconds.text = seconds + " sec";
    }

    //##################################################################################################
    // HELPERS

    // these are used to activate the stars by Invoke() within PopUp()
    private void ActivateStar1Full()  { _star1Full.gameObject.SetActive(true);  }
    private void ActivateStar2Full()  { _star2Full.gameObject.SetActive(true);  }
    private void ActivateStar3Full()  { _star3Full.gameObject.SetActive(true);  }

    private void ActivateStar1Empty() { _star1Empty.gameObject.SetActive(true); }
    private void ActivateStar2Empty() { _star2Empty.gameObject.SetActive(true); }
    private void ActivateStar3Empty() { _star3Empty.gameObject.SetActive(true); }

}

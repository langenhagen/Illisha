using UnityEngine;
using System.Collections;


public class AchievementPopup : BarnBehaviour
{
    public float _startDuration = 0.2f;
    public float _sustainDuration = 0.3f;
    public float _endDuration = 0.1f;


    public UILabel    _lblAchievementName;
    public UISprite   _texAchievementImage;


    public TweenAlpha    _tweenStartAlpha;
    public TweenPosition _tweenStartPosition;
    public TweenScale    _tweenStartScale;
    public TweenAlpha    _tweenEndAlpha;
    public TweenPosition _tweenEndPosition;
    public TweenScale    _tweenEndScale;

    //##################################################################################################
    // METHODS


    void Start()
    {
        Vector3 pos = GameManager.UI.IngameUI.AchievementPopupContainer.transform.position;
        transform.position = pos;

        _tweenStartAlpha.duration    = _startDuration;
        _tweenStartPosition.duration = _startDuration;
        _tweenStartScale.duration    = _startDuration;
        _tweenEndAlpha.duration      = _endDuration;
        _tweenEndPosition.duration   = _endDuration;
        _tweenEndScale.duration      = _endDuration;

        // XXX change


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
        _tweenStartAlpha.enabled    = true;
        //_tweenStartPosition.enabled = true;
        _tweenStartScale.enabled    = true;
    }

    public void FadeOut()
    {
        _tweenEndAlpha.enabled    = true;
        _tweenEndPosition.enabled = true;
        _tweenEndScale.enabled    = true;
    }

    public void SetSpriteName(string spriteName)
    {
        _texAchievementImage.spriteName = spriteName;
    }


    //##################################################################################################
    // GETTERS & SETTERS

    public float StartDuration          {   get { return _startDuration;    }
                                            set { _startDuration = value;   }}

    public float SustainDuration        {   get { return _sustainDuration;  }
                                            set { _sustainDuration = value; }}

    public float EndDuration            {   get { return _endDuration;      }
                                            set { _endDuration = value;     }}

    public float OverallLifeDuration
    {
        get { return _startDuration + _sustainDuration + _endDuration; }
    }



    public string AchievementName       {   get { return _lblAchievementName.text;  }
                                            set { _lblAchievementName.text = value; }}
}

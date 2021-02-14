using UnityEngine;
using System.Collections.Generic;

// TODO constantize the values, privatise them de-Monobehaviour the script
public class PointPowerScript : BarnBehaviour
{
    // The time-range in seconds the controller will make use of info abt shots, hits and possible and hits 
    public int  _secondsToRememberShots = 15;
    
    // TODO rename... Values should be whithin [0,1]
    public float _quasiHitRateUp = 0.9f;
    public float _quasiHitRateDown = 0.4f;
    
    public int _numHitsToSpeedUp = 10;
    public int _numFailsToSpeedDown = 15;

    public float PermanentLetterPointMultiplier { get; set; }
    public float PermanentLetterPointAdder      { get; set; }
    public float TempLetterPointMultiplier      { get; set; }
    public float TempLetterPointAdder           { get; set; }

    public float PermanentWordPointMultiplier   { get; set; }
    public float PermanentWordPointAdder        { get; set; }
    public float TempWordPointMultiplier        { get; set; }
    public float TempWordPointAdder             { get; set; }

    

    public float _lettersPerSecFor1Star = 1.0f;
    public float _lettersPerSecFor2Star = 2.0f;
    public float _lettersPerSecFor3Star = 3.0f;

    
    //##################################################################################################
    // METHODS

    public void Start()
    {
        PermanentLetterPointMultiplier = 1.0f;
        PermanentLetterPointAdder      = 0.0f;
        TempLetterPointMultiplier      = 1.0f;
        TempLetterPointAdder           = 0.0f;
                                       
        PermanentWordPointMultiplier   = 1.0f;
        PermanentWordPointAdder        = 0.0f;
        TempWordPointMultiplier        = 1.0f;
        TempWordPointAdder             = 0.0f;
    }

    public void UpdateScore(float secondsSpent, Vocab vocab)
    {

        float lettersPerSec = vocab.Phrase.Length / secondsSpent;

        int points   = PlusWord(lettersPerSec);
        int numStars = NumStars(lettersPerSec);

        GameManager.UI.IngameUI.Show3StarPopup(
            numStars,
            points,
            secondsSpent);

        if (numStars >= 3)
        {
            // TODO change bout that
            FindGameObjectWithTagSafe("Cannon").GetComponent<CannonMovement>().SpeedUp();
            FindGameObjectWithTagSafe("Cannon").GetComponent<CannonMovement>().SpeedUp();
        }
        else if (numStars == 2)
        {
            FindGameObjectWithTagSafe("Cannon").GetComponent<CannonMovement>().SpeedUp();
        }
        else if (numStars <= 1)
        {
            FindGameObjectWithTagSafe("Cannon").GetComponent<CannonMovement>().SpeedDown();
        }
    }



    public int PlusLetter()
    {
        Game g = GameManager.Game;
        int points = (int)(PermanentLetterPointMultiplier * TempLetterPointMultiplier + PermanentLetterPointAdder + TempLetterPointAdder);

        g.Score += points;
        g.NumHits++;
        GameManager.Session.HitTimestamps.Push(Time.time);


        TempLetterPointMultiplier = 1;
        TempLetterPointAdder = 0;
        

        return points;
    }

    /// <summary>
    /// Adds points to the score
    /// </summary>
    public int PlusWord( float lettersPerSec)
    {
        Game g = GameManager.Game;


        TempWordPointMultiplier = lettersPerSec;

        int points = (int)(PermanentWordPointMultiplier * TempWordPointMultiplier + PermanentWordPointAdder + TempWordPointMultiplier);

        g.Score    += points;
        g.NumWords++;


        TempWordPointMultiplier = 1;
        TempWordPointAdder = 0;

        return points;
    }

    public int NumStars(float lettersPerSec)
    {
        int ret;

        if (lettersPerSec > _lettersPerSecFor3Star)
            ret = 3;
        else if (lettersPerSec > _lettersPerSecFor2Star)
            ret = 2;
        else if (lettersPerSec > _lettersPerSecFor1Star)
            ret = 1;
        else
            ret = 0;

        return ret;
    }

    //##################################################################################################
    // GETTERS && SETTERS


    public float LettersPerSecFor1Star
    {
        get { return _lettersPerSecFor1Star; }
        set { _lettersPerSecFor1Star = value; }
    }

    public float LettersPerSecFor2Star
    {
        get { return _lettersPerSecFor2Star; }
        set { _lettersPerSecFor2Star = value; }
    }


    public float LettersPerSecFor3Star
    {
        get { return _lettersPerSecFor3Star; }
        set { _lettersPerSecFor3Star = value; }
    }
}

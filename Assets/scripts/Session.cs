using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Stores game-relevant data that is not to be stored.
/// </summary>
public class Session : BarnBehaviour
{

    /// The current word.
    public  Word        Word                    { get; set; }
    public float        TimeSessionStarted      { get; private set; }   // in Unity Time.time

    public Stack<float> PossibleHitsTimestamps  { get; set; }           // Timestamps of possible shots
    public Stack<float> ShotTimestamps          { get; set; }           // UNITY-timestamps of actual shots, when shots are fired
    public Stack<float> HitTimestamps           { get; set; }           // UNITY-timestamps of hits, when hits landed!

    //##################################################################################################
    // METHODS


    public void Init()
    {
        PossibleHitsTimestamps = new Stack<float>();
        ShotTimestamps         = new Stack<float>();
        HitTimestamps          = new Stack<float>();

        TimeSessionStarted = Time.time;
        
        Word = GetComponent<Word>();
        Word.NextWord();
    }

    //##################################################################################################
    // HELPERS

    

    //##################################################################################################
    // GETTERS & SETTERS

}

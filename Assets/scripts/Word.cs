using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO
// TODO make variables fixed, in another branch!s
public class Word : BarnBehaviour 
{
    // The glyph prefab
    public Transform _glyphPrefab;

    // the number of completed words necessary to hide any letters in the presented vocab.
    public int _numWordsToHideLetters = 1000;

    // i hope you understand what is meant.
    public int _numVocabOccuredToHideLetters = 3;

    public float _wordXVariance = 1.0f;
    public float _wordYVariance = 1.0f;

    public float _letterCreateTimeOffset = 0.4f;

    // Variance in the random z-shifting of the glyphs in Z direction.
    // Something between [0,1] or TODO [0,1[
    // TODO doesnt work idiot safe
    public float _glyphYTranslationVariance = 3.0f;


    // keeps track of the current letters.
    public List<GameObject> Glyphs { get; set; }  

    // the cannon movement script
    private CannonMovement _cannonMovement;  

    private float _timeCurrentWordStarted;

    private int   _currenVocabIndex = 0;


	//##################################################################################################
	// METHODS
    
    void Awake()
    {
        Glyphs = new List<GameObject>();

        _cannonMovement = GetComponentOnObjectWithTagSafe<CannonMovement>("Cannon");
    }


    public void RemoveGlyph( GameObject glyph)
    {
        // TODO maybe fancify
        Glyphs.Remove ( glyph);

        GameManager.PPScript.PlusLetter();

        if( 0 == Glyphs.Count)
        {
            OnCorrectWord();
        }

        GameManager.UI.IngameUI.UpdateUI();
    }

    // TODO RE-WORK word finding algorithm
    public void NextWord()
    {
        Dictionary<int,Vocab> vocabs = GameManager.Game.Vocabs;

        Vocab vocab;
        do
        {
            _currenVocabIndex = (_currenVocabIndex + 1) % (vocabs.Count + 1);
        }
        while (!vocabs.TryGetValue(_currenVocabIndex, out vocab));
        

        StartCoroutine("DoGenerateWord", vocab);

        SetTranslatedPhrase( vocab.Phrase);
        
        _cannonMovement.FastMoveToBeginning();
    }

    //##################################################################################################
    // HELPERS

    private IEnumerator DoGenerateWord(Vocab vocab)
    {
        string word = vocab.Translation;
        bool[] hideCharacter = new bool[word.Length];
        
        for( int i = 0; i< hideCharacter.Length; ++i)
            hideCharacter[i] = false;


        Vector3 wordPosition = NewWordPosition( 1 /* TODO about that*/);
        

        // predetermine hidden letters
        int numWords = GameManager.Game.NumWords;
        if (numWords > _numWordsToHideLetters && vocab.Count > _numVocabOccuredToHideLetters)
        {
            int numLettersToHide = Mathf.Min(numWords / _numWordsToHideLetters, vocab.Phrase.Length);

            // - u see the flaw? it's on purpose
            for( int i = 0; i < numLettersToHide; ++i)
            {
                hideCharacter[(int)(Random.value * vocab.Phrase.Length)] = true;
            }
        }


        for(int i = 0; i< word.Length; ++i)
        {
            // Create individual letters, tag and position them
            
            GameObject newGlyphObject = (Instantiate(_glyphPrefab) as Transform).gameObject;
            Glyph glyph = newGlyphObject.GetComponentInChildren<Glyph>();


            newGlyphObject.transform.position = new Vector3( 
                wordPosition.x + newGlyphObject.collider.bounds.size.x * i,
                wordPosition.y + Random.Range(0, _glyphYTranslationVariance), 
                wordPosition.z - glyph.collider.bounds.size.y / glyph.transform.localScale.y / 2);

            glyph.Letter   = word[i];
            glyph.IsHidden = hideCharacter[i];

            Glyphs.Add (newGlyphObject);
            
            yield return new WaitForSeconds(_letterCreateTimeOffset);
        }

        _timeCurrentWordStarted = Time.time;
    }

    private void OnCorrectWord()
    {

        float secondsSpent  = Time.time - _timeCurrentWordStarted;


        GameManager.Game.Vocabs[_currenVocabIndex].PlusOne(secondsSpent);
        GameManager.PPScript.UpdateScore(secondsSpent, GameManager.Game.Vocabs[_currenVocabIndex]);
        GameManager.Game.AchievementController.HandleNewAchievements();

        GameManager.Character.EyeMovement.Excite();


        var cameraFlight = Camera.main.GetComponent<CameraFlight>();
        if (Random.value > 0.8f && cameraFlight.IsRolling() == false)
            cameraFlight.Roll();


        NextWord();
    }


    
    private void SetTranslatedPhrase( string translation)
    {
        GameManager.UI.IngameUI.LblTranslation.text = translation;
    }

    private Vector3 NewWordPosition( int wordNum)
    {
        Vector3 ret = new Vector3();

        ret.x = (Random.value-0.5f) * _wordXVariance;
        ret.y = Random.value * _wordYVariance ;
        ret.z = 0;
        

        return ret;
    }

    //##################################################################################################
    // GETTERS & SETTERS
}

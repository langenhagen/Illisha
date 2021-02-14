
/// <summary>
/// Model class that stores a vocab and additional info within the game's context.
/// </summary>
public class Vocab
{
    public string   Phrase          { get; private set; }
    public string   Translation     { get; private set; }
    public float    Commonness      { get; private set; }
    public float    TimeForPhrase   { get; set; }           // time spent on sovling this vocab in seconds
    public int      Count           { get; set; }           // number of times this phrase was solved

    //##################################################################################################
    // CONSTRUCTORS

    public Vocab(string phrase, string translation, float commonness, float timeForPhrase, int count)
    {
        Phrase = phrase;
        Translation = translation;
        Commonness = commonness;
        TimeForPhrase = timeForPhrase;
        Count = count;
    }


    //##################################################################################################
    // METHODS

    /// <summary>
    /// Increments the number of times the phrase was solved by 1 and 
    /// adds the given time to the total time spent on this phrase.
    /// </summary>
    /// <param name="time">The time spent on this phrase in this particular move.</param>
    public void PlusOne(float time)
    {
        Count++;
        TimeForPhrase += time;
    }

    public override string ToString()
    {
        return "Vocab[phrase: " + Phrase + "; translation: " + Translation + "; commonness: " + Commonness + "; " + " time_for_phrase: " + TimeForPhrase + "; count: " + Count + "]";
    }

}
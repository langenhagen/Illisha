using UnityEngine;
using System.Linq;
using System.Collections;

// Does the wiring and provision of the ingame UI elements.
public class Ingame2DUI : BarnBehaviour
{
	// 2D UI Elements
    public UILabel _lblWords;
    public UILabel _lblScore;
    public UILabel _lblTranslation;

    public UIWidget _3StarPopupContainer;
    public UIWidget _achievementPopupContainer;

    public Transform _3starPopupPrefab;
    public Transform _achievementPopupPrefab;


	//##################################################################################################
	// METHODS

	void Start () 
	{	
        UpdateUI();
	}
	


	public void UpdateUI()
	{
        Game g = GameManager.Game;

		_lblScore.text = "Score " + g.Score.ToString();
        _lblWords.text = "Words " + g.NumWords.ToString();
	}


    public void Show3StarPopup( int numStars, int points, float secondsSpent)
    {
        Transform popup = Instantiate(_3starPopupPrefab) as Transform;
        popup.GetComponent<ThreeStarPopup>().SetValues(numStars, points, (int)secondsSpent);
    }


    public void ShowAchievmentPopups()
    {
        StartCoroutine("DoAchievementApplyAndPopups"); 
    }


    //##################################################################################################
    // HELPERS

    private IEnumerator DoAchievementApplyAndPopups()
    {
        var newAchievements = GameManager.Game.AchievementController.NewAchievements;

        while (newAchievements.Count > 0)
        {
            Achievement achievement = newAchievements.Dequeue();

            Transform transform = Instantiate(GameManager.UI.IngameUI.AchievementPopupPrefab) as Transform;
            AchievementPopup newPopup = transform.GetComponent<AchievementPopup>();

            newPopup.AchievementName = achievement.Name;
            newPopup.SetSpriteName(achievement.ImageName);

            yield return new WaitForSeconds(newPopup.OverallLifeDuration);
        }
    }



    //##################################################################################################
    // GETTERS & SETTERS

    public UIWidget     ThreeStarPopupContainer     { get { return _3StarPopupContainer;        }}
    public UIWidget     AchievementPopupContainer   { get { return _achievementPopupContainer;  }}

    public UILabel      LblTranslation              { get { return _lblTranslation;             }}

    public Transform    ThreeStarPopupPrefab        { get { return _3starPopupPrefab;           }}

    public Transform    AchievementPopupPrefab      { get { return _achievementPopupPrefab;     }}
}
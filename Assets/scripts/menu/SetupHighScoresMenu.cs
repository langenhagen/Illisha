using UnityEngine;
using System.Collections.Generic;

public class SetupHighScoresMenu : BarnBehaviour
{
        public UIGrid _gridHighScore;

        public UILabel _lblNamePrefab;
        public UILabel _lblPointsPrefab;

    //##################################################################################################
    // METHODS


    // TODO re-work
    void OnEnable()
    {
        foreach (Transform label in _gridHighScore.transform)
            Destroy(label.gameObject);

        List<Pair<string, int>> highscore = GameManager.Instance.Highscore();
        int i = 0;

        foreach (Pair<string, int> playerScorePair in highscore)
        {
            UILabel lblName   = Instantiate(_lblNamePrefab)   as UILabel;
            UILabel lblPoints = Instantiate(_lblPointsPrefab) as UILabel;
            
            lblName.name   = "" + i + playerScorePair.Item1;
            lblPoints.name = "" + i + playerScorePair.Item1 + "Points";

            lblName.text   = playerScorePair.Item1;
            lblPoints.text = playerScorePair.Item2.ToString();

            lblName.transform.parent   = _gridHighScore.transform;
            lblPoints.transform.parent = _gridHighScore.transform;
            
            lblName.transform.localScale   = Vector3.one;
            lblPoints.transform.localScale = Vector3.one;
            
            ++i;
        }
        _gridHighScore.repositionNow = true;
    }
}

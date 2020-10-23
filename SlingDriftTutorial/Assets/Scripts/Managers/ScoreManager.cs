using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    #region Singleton
    private static ScoreManager _instance;
    private void Awake()
    {
        if (_instance ==null)
        {
            _instance = this;
        }
    }
    #endregion

    public static ScoreManager Instance()
    {
        return _instance;
    }

    private int _score = 0;

    public void SetScore(int score)
    {
        _score = score;
        UIManagerSystem.Instance().ChangeScoreTextValue(_score);
    }

    public int GetScore()
    {
        return _score;
    }
}

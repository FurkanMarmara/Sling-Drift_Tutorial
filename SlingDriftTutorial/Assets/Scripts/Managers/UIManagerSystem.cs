using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerSystem : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private GameObject _levelUpText;
    [SerializeField]
    private GameObject _resetButton;
    [SerializeField]
    private GameObject _tapToStartText;


    #region Singleton
    private static UIManagerSystem _instance;
    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }
    #endregion

    public static UIManagerSystem Instance()
    {
        return _instance;
    }

    public void ChangeScoreTextValue(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void ShowLevelUpText()
    {
        _levelUpText.SetActive(true);
    }

    public void CloseLevelUpText()
    {
        _levelUpText.SetActive(false);
    }

    public void ShowResetButton()
    {
        _resetButton.SetActive(true);
        CloseLevelUpText();
    }

    public void CloseResetButton()
    {
        _resetButton.SetActive(false);
    }

    public void ResetRoads()
    {
        GameManager.Instance().ResetRoads();
        CloseResetButton();
    }

    public void CloseTabToStartText()
    {
        _tapToStartText.SetActive(false);
    }
}

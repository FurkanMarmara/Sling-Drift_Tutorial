using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private RoadBuilder _roadBuilder;

    #region Singleton
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
    }
    #endregion

    public static GameManager Instance()
    {
        return _instance;
    }

    public void ResetRoads()
    {
        _roadBuilder.ResetRoads();
    }

    public void CreateRoadContinue()
    {
        _roadBuilder.ContinueRoadCreate();
    }

}

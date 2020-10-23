using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    #region Singleton 
    private static ObjectPooler _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        SpawnPoolableObjects();
    }
    #endregion 


    public static ObjectPooler Instance()
    {
        return _instance;
    }

    [SerializeField]
    private List<PoolItem> _poolableList = new List<PoolItem>();

    Dictionary<PoolItemType, Queue<GameObject>> _objectPool = new Dictionary<PoolItemType, Queue<GameObject>>();

    private List<Road> _createdRoads = new List<Road>();

    private void SpawnPoolableObjects()
    {
        for (int i = 0; i < _poolableList.Count; i++)
        {
            Queue<GameObject> poolObjects = new Queue<GameObject>();
            for (int j = 0; j < _poolableList[i].count; j++)
            {
                GameObject gO = Instantiate(_poolableList[i].gO, new Vector3(0, 0, 0), Quaternion.identity);
                gO.SetActive(false);
                poolObjects.Enqueue(gO);
                _createdRoads.Add(gO.GetComponent<Road>());
            }
            _objectPool.Add(_poolableList[i].poolItemType, poolObjects);
        }
    }


    public void SpawnFromPool(PoolItemType objectType, Vector3 spawnPos, Quaternion rotation)
    {
        GameObject gO = _objectPool[objectType].Dequeue();
        gO.transform.position = spawnPos;
        gO.transform.rotation = rotation;
        gO.SetActive(true);


    }

    public void DestroyPool(PoolItemType objectType, GameObject gO)
    {
        gO.SetActive(false);
        _objectPool[objectType].Enqueue(gO);
        gO.transform.position = Vector3.zero;
        gO.transform.rotation = Quaternion.identity;
        
    }

    public void DestroySomePools()
    {
        for (int i = 0; i < _createdRoads.Count; i++)
        {

            if (_createdRoads[i].gameObject.activeInHierarchy)
            {
                _createdRoads[i].Reset();
            }

        }
    }

}

[System.Serializable]
public class PoolItem
{
    public PoolItemType poolItemType;
    public GameObject gO;
    public int count;
}


public enum PoolItemType
{
    RoadLeft,
    RoadRight,
    RoadLeftToRight,
    RoadRightToLeft,
    RoadLevelUp
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool isRight;
    public bool doubleCorner;

    [SerializeField]
    private PoolItemType _roadType;



    public void Reset()
    {
        ObjectPooler.Instance().DestroyPool(_roadType,gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            StartCoroutine("ResetTime", other.gameObject);
        }
    }

    private IEnumerator ResetTime(GameObject gO)//gO should have to PlayerController Component
    {
        PlayerController player = gO.GetComponent<PlayerController>();
        WaitForSeconds time = new WaitForSeconds(5f);
        yield return time;
        if (player.isLive)
        {
            Reset();
        }
    }

    private void OnDisable()
    {
        StopCoroutine("ResetTime");
    }

}

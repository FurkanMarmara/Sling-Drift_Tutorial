using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRoad : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManagerSystem.Instance().ShowLevelUpText();
            PlayerController player = other.GetComponent<PlayerController>();
            player._levelUpController = true;
            player._speed = 2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManagerSystem.Instance().CloseLevelUpText();
            PlayerController player = other.GetComponent<PlayerController>();
            player._levelUpController = false;
            player._speed = 1f;
            GameManager.Instance().CreateRoadContinue();
        }
        
    }

}

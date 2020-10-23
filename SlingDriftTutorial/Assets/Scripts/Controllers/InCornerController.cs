using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCornerController : MonoBehaviour
{
    [SerializeField]
    private bool _isRight;
    [SerializeField]
    private bool _isUCorner;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag=="Player")
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if (player.isLive)
            {
                ScoreManager.Instance().SetScore(ScoreManager.Instance().GetScore() + 1);
                player._canRotate = true;
                if (_isRight)
                {
                    player._canRotateRight = true;
                    if (!_isUCorner)
                    {
                        player.direction += 1;
                    }
                    else
                    {
                        player.direction += 2;
                    }
                    
                }
                else
                {
                    player._canRotateRight = false;
                    if (!_isUCorner)
                    {
                        player.direction -= 1;
                    }
                    else
                    {
                        player.direction -= 2;
                    }
                }
            }
           
        }
    }
}

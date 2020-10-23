using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float _speed = 1f;
    [SerializeField]
    private float _rotationSpeed = 2f;

    public bool isLive = false;
    public bool _canRotate;
    public bool _canRotateRight;
    public int direction = 0; //-1 left,0 front,1 right

    private bool _firstStart = true;


    private bool _canStillRotate = false;
    private bool _directionIsEnd = true;

    private bool _testBool1;
    private bool _testBool2;

    public bool _levelUpController;

    private Rope _rope;
    private bool _canThrowRope;
    private GameObject _ropeHolder;

    private void Start()
    {
        _rope = GetComponent<Rope>();
    }

    void Update()
    {
        if (_firstStart)
        {
            FirstStart();
        }

        if (isLive)
        {
            transform.Translate(Vector3.forward * _speed);
            RotateControl();
        }
        if (!_directionIsEnd && isLive)
        {
            DirectionControl();
        }
    }

    public void RotateControl()
    {
        float rotation = 1;
        rotation *= _rotationSpeed;

        if (_canRotate || _canStillRotate)//Dönüş yapabiliyorsak
        {
            _directionIsEnd = true;
            _testBool1 = false;
            _testBool2 = false;
#if UNITY_EDITOR
            MouseControl(rotation);
#else
            TouchControl(rotation);
#endif
        }
        else // Dönüş yapamıyorsak
        {
            if (!_levelUpController)
            {
                _speed = 1f;
            }
            _directionIsEnd = false;
        } 
    }

    public void DirectionControl()
    {
        Quaternion testA = transform.rotation;
        float test = transform.rotation.y;
        if (direction == -1)//SOLA GİDİYORSAK
        {
            if (_testBool1 || test < -0.9f)
            {
                _testBool1 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, -50, 0), 0.4f);
                if (test < -0.4f && test > -0.43f)
                {
                    _testBool1 = false;
                }
            }
            else if (_testBool2 || test > 0)
            {
                _testBool2 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, -120, 0), 0.4f);

                if (test < -0.85f && test > -0.89f)
                {
                    _testBool2 = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, -90, 0), 0.2f);
            } 
           
        }
        else if (direction == 0)//İLERİYE GİDİYORSAK
        {
            if (_testBool1 || test >0.35f)
            {
                _testBool1 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, -30, 0), 0.4f);
                if (test > -0.27f && test < -0.25f)
                {
                    _testBool1 = false;
                }
            }
            else if (_testBool2 || test <-0.35f)
            {
                _testBool2 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, 30, 0), 0.4f);
                if (test > 0.25f && test < 0.27f)
                {
                    _testBool2 = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, 0, 0), 0.2f);
            }
            
        }
        else if (direction == 1)//SAĞA GİDİYORSAK
        {
            if (_testBool1 || test > 0.9f)
            {
                _testBool1 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, 50, 0), 0.4f);
                if (test > 0.4f && test < 0.44f)
                {
                    _testBool1 = false;
                }
            }
            else if (_testBool2 || test < 0)
            {
                _testBool2 = true;
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, 120, 0), 0.4f);
                if (test > 0.85f && test < 0.89f)
                {
                    _testBool2 = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(testA, Quaternion.Euler(0, 90, 0), 0.2f);
            }
            
        }
    }

    public void ResetPlayerPositionAndRotation()
    {
        if (!isLive)
        {
            gameObject.transform.position = new Vector3(0, 0, 0);
            gameObject.transform.rotation = Quaternion.identity;
            isLive = true;
        }
    }

    public void Die()
    {
        direction = 0;
        _speed = 1f;
        isLive = false;
        _canRotate = false;
        _canStillRotate = false;
        _testBool1 = false;
        _testBool2 = false;
        ScoreManager.Instance().SetScore(0);
        UIManagerSystem.Instance().ShowResetButton();
        _rope._lineRenderer.positionCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Rope"))
        {
            _canThrowRope = true;
            _ropeHolder = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Rope"))
        {
            _canThrowRope = false;
        }
    }

    private void MouseControl(float rotation)
    {
        if (Input.GetMouseButton(0))
        {
            if (_canThrowRope)
            {
                _rope._lineRenderer.positionCount = 2;
                _rope._lineRenderer.SetPosition(0, transform.position);
                _rope._lineRenderer.SetPosition(1, _ropeHolder.transform.position);
            }

            if (!_levelUpController)
            {
                _speed = 1.1f;
            }
            _canStillRotate = true;
            if (_canRotateRight)
            {
                transform.Rotate(0, rotation, 0);
                transform.Translate(Vector3.left * _speed / 4);
            }
            else
            {
                transform.Rotate(0, -rotation, 0);
                transform.Translate(Vector3.right * _speed / 4);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!_levelUpController)
            {
                _speed = 1f;
            }
            _rope._lineRenderer.positionCount = 0;
            if (!_canThrowRope)
            {
                _ropeHolder = null;
            }
            _canStillRotate = false;
        }
    }

    private void TouchControl(float rotation)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (_canThrowRope)
            {
                _rope._lineRenderer.positionCount = 2;
                _rope._lineRenderer.SetPosition(0, transform.position);
                _rope._lineRenderer.SetPosition(1, _ropeHolder.transform.position);
            }

            if (!_levelUpController)
            {
                _speed = 1.1f;
            }
            _canStillRotate = true;
            if (_canRotateRight)
            {
                transform.Rotate(0, rotation, 0);
                transform.Translate(Vector3.left * _speed / 4);
            }
            else
            {
                transform.Rotate(0, -rotation, 0);
                transform.Translate(Vector3.right * _speed / 4);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (!_levelUpController)
                {
                    _speed = 1f;
                }
                _rope._lineRenderer.positionCount = 0;
                if (!_canThrowRope)
                {
                    _ropeHolder = null;
                }
                _canStillRotate = false;
            }
        }
    }

    private void FirstStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstStart = false;
            isLive = true;
            UIManagerSystem.Instance().CloseTabToStartText();
        }
        else if (Input.touchCount>0)
        {
            _firstStart = false;
            isLive = true;
            UIManagerSystem.Instance().CloseTabToStartText();
        }
    }
}

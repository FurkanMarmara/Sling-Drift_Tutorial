using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    private int xPos = 25;
    private int zPos = 0;
    private int yRotation = 0;

    [SerializeField]
    private List<Road> _roads = new List<Road>();

    [SerializeField]
    private Road _levelUpRoad;

    private bool _isFirstBuild=true;

    [SerializeField]
    private PlayerController _player;

    private Road previousRoad;

    private void Start()
    {
        CreateRoads(2);
    }

    private void CreateRoads(int count)
    {
        int counter = 0;
        for (int j = 0; j < count; j++) //Kaç adet 15 lik yol üreteceğiz
        {
            for (int i = 0; i < 15; i++)
            {
                counter++;
                Road currentRoad;
                if (i == 0)//Sadece Left Yada Right Gelebilir.
                {
                    int random = Random.Range(0, 2);
                    if (yRotation==0)
                    {
                        xPos += 25;
                        zPos += 120;
                    }
                    else if(yRotation>=90)
                    {
                        if (previousRoad.isRight)
                        {
                            xPos += 120;
                            zPos -= 25;
                            random = 0;
                        }
                        else
                        {
                            xPos -= 20;
                            yRotation -= 180;
                            zPos += 25;
                            random = 1;
                        }
                    }
                    else
                    {
                        xPos -= 120;
                        zPos += 25;
                        random = 1;
                    }
                    if (_isFirstBuild)
                    {
                        _isFirstBuild = false;
                        xPos = 25;
                        zPos = 40;
                        yRotation = 0;
                    }

                    if (random==0)
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadLeft, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[0];
                    }
                    else
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadRight, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[1];
                    }
                    previousRoad = currentRoad;
                }
                else
                {
                    int random = 0;
                    if (previousRoad.isRight)
                    {
                        if (previousRoad.doubleCorner)//Left gelecek
                        {
                            xPos += 10;
                            yRotation += 180;
                            zPos += 55;
                            random = 0;
                        }
                        else
                        {
                            if (yRotation == 0)
                            {
                                random = Random.Range(0, 2);//Test Corner gelme şansı
                                if (random == 0)//Right To Left Gelecek
                                {
                                    xPos -= 5;
                                    yRotation -= 90;
                                    zPos += 10;
                                    random = 2;
                                }
                                else
                                {
                                    xPos += 5;
                                    yRotation += 90;
                                    zPos += 25;
                                    random = 0;
                                }
                            }
                            else//Left Yada Right Gelebilir.
                            {
                                random = Random.Range(0, 2);
                                xPos -= 25;
                                yRotation += 90;
                                zPos += 5;

                            }
                        }
                    }
                    else
                    {
                        if (previousRoad.doubleCorner)//Right Gelecek
                        {
                            xPos += 30;
                            zPos += 105;
                            random = 1;
                        }
                        else
                        {
                            if (yRotation == 0)
                            {
                                random = Random.Range(0, 2);//Test Corner gelme şansı
                                if (random == 1)//Left To Right Gelecek
                                {
                                    xPos -= 85;
                                    yRotation -= 90;
                                    zPos += 10;
                                    random = 3;
                                }
                                else //Right Gelecek
                                {
                                    random = 1;
                                    xPos -= 55;
                                    yRotation -= 90;
                                    zPos += 75;
                                }
                            }
                            else//Right Yada Left Gelebilir
                            {
                                random = Random.Range(0, 2);
                                xPos += 75;
                                yRotation -= 90;
                                zPos += 55;

                            }
                        }
                    }

                    if (random == 0)
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadLeft, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[0];
                    }
                    else if(random == 1)
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadRight, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[1];
                    }
                    else if (random==2)
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadRightToLeft, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[2];
                    }
                    else
                    {
                        ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadLeftToRight, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
                        currentRoad = _roads[3];
                    }
                    previousRoad = currentRoad;
                }

            }
            if (previousRoad.isRight)//15.YOL SAGA DÖNÜS İLE BİTİYORSA
            {
                if (previousRoad.doubleCorner)
                {
                    xPos += 10;
                    yRotation += 180;
                    zPos += 80;
                }
                else
                {
                    if (yRotation == 0)//15. YOLUN ROTATE'İ İLERİYE DOĞRUYSA
                    {
                        xPos += 5;
                        yRotation += 90;
                        zPos += 50;
                    }
                    else
                    {
                        xPos -= 50;
                        yRotation += 90;
                        zPos += 5;
                    }
                }
            }
            else
            {
                if (previousRoad.doubleCorner)
                {
                    xPos += 30;
                    zPos += 80;
                }
                else
                {
                    if (yRotation == 0)
                    {
                        xPos -= 155;
                        yRotation += 90;
                        zPos += 50;
                    }
                    else
                    {
                        xPos += 50;
                        yRotation -= 90;
                        zPos += 55;
                    }
                }
            }
            ObjectPooler.Instance().SpawnFromPool(PoolItemType.RoadLevelUp, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, yRotation, 0));
        }

    }

    public void ResetRoads()
    {
        ObjectPooler.Instance().DestroySomePools();
        xPos = 25;
        yRotation = 0;
        zPos = 40;
        _isFirstBuild = true;
        _player.ResetPlayerPositionAndRotation();
        CreateRoads(2);
    }

    public void ContinueRoadCreate()
    {
        CreateRoads(1);
    }


}

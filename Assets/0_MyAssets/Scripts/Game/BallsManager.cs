using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    [SerializeField] BallController ballPrefab;
    BallController[] ballControllers;
    float ballsDistance = 0.4f;

    void Awake()
    {
        ballControllers = new BallController[30];
        for (int i = 0; i < ballControllers.Length; i++)
        {
            ballControllers[i] = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity, transform);
        }
    }

    void Start()
    {
        Vector3 startPos = new Vector3(-3.8f, 9.5f, 0);
        Vector3 pos = startPos;

        for (int i = 0; i < ballControllers.Length; i++)
        {
            ballControllers[i].transform.position = pos;

            bool isNextOdd = (i + 1) % 2 == 0;
            if (isNextOdd)
            {
                if ((i + 1) % 4 == 0)
                {
                    pos.x = startPos.x;
                }
                else
                {
                    pos.x = startPos.x + ballsDistance;
                }

                pos.y += ballsDistance;
            }
            else
            {
                pos.x += ballsDistance * 2;
            }
        }
    }


}

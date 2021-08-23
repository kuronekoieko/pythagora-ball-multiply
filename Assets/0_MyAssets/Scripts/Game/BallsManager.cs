using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallsManager : MonoBehaviour
{
    [SerializeField] BallController ballPrefab;
    [SerializeField] int initialCount;
    List<BallController> ballControllers = new List<BallController>();
    float ballsDistance = 0.4f;
    public static BallsManager i;
    public int ActiveCount => ballControllers.Count(_ => _.gameObject.activeSelf);

    void Awake()
    {
        i = this;
        for (int i = 0; i < 100; i++)
        {
            ballControllers.Add(Instantiate(ballPrefab, Vector3.zero, Quaternion.identity, transform));
            ballControllers[i].gameObject.SetActive(i < initialCount);
        }
    }

    void Start()
    {
        Vector3 startPos = new Vector3(-3.8f, 9.5f, 0);
        Vector3 pos = startPos;

        for (int i = 0; i < ballControllers.Count; i++)
        {

            if (i > initialCount - 1)
            {
                continue;
            }

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

    public BallController GetNewBall()
    {
        var ball = ballControllers.Where(_ => !_.gameObject.activeSelf).FirstOrDefault();
        if (ball == null)
        {
            ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity, transform);
            ballControllers.Add(ball);
        }
        return ball;
    }

}

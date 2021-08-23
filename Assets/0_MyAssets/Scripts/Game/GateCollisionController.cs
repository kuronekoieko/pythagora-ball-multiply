using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollisionController : MonoBehaviour
{
    [SerializeField] GateController gateController;

    void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<BallController>();
        if (ball == null) return;
        gateController.OnTriggerEnterBall(ball);
    }
}

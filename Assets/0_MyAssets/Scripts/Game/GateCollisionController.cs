using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GateType
{
    Multiple,
    Decrease,
}

public class GateCollisionController : MonoBehaviour
{
    GateType gateType;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] int count;


    void Start()
    {

        gateType = (count > 0) ? GateType.Multiple : GateType.Decrease;

        switch (gateType)
        {
            case GateType.Multiple:
                break;
            case GateType.Decrease:
                textMeshPro.text = count.ToString();
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<BallController>();
        if (ball == null) return;


        switch (gateType)
        {
            case GateType.Multiple:
                Multiple(ball);
                break;
            case GateType.Decrease:
                Decrease(ball);
                break;
            default:
                break;
        }
    }

    void Multiple(BallController ball)
    {
        ball.Multiple();
    }

    void Decrease(BallController ball)
    {
        count += 1;
        textMeshPro.text = count.ToString();
        ball.Decrease();
        if (count == 0) transform.parent.gameObject.SetActive(false);
    }
}

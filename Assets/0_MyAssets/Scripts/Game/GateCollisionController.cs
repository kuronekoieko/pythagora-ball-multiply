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
                textMeshPro.text = "x" + count.ToString();
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
        if (ball.IsDuplicated == true) return;
        ball.Multiple();
        ball.IsDuplicated = true;

        for (int i = 0; i < count - 1; i++)
        {
            var angle = Random.Range(0, 360);
            var radius = 0.3f;
            var rad = angle * Mathf.Deg2Rad;
            var px = Mathf.Cos(rad);
            var py = Mathf.Sin(rad);
            var offset = new Vector3(px, py, 0) * radius;

            Debug.Log(offset);
            var newBall = BallsManager.i.GetNewBall();
            newBall.transform.position = ball.transform.position + offset;
        }
    }

    void Decrease(BallController ball)
    {
        count += 1;
        textMeshPro.text = count.ToString();
        ball.Decrease();
        if (count == 0) transform.parent.gameObject.SetActive(false);
    }
}

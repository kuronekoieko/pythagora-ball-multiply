using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using System.Linq;
using DG.Tweening;

public enum GateType
{
    Multiple,
    Decrease,
}

public class GateController : MonoBehaviour
{
    [SerializeField] [OnValueChanged(nameof(OnChangedGateType))] GateType gateType;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] SpriteRenderer bgSr;
    [SerializeField] GateColor[] gateColors;
    [SerializeField] [OnValueChanged(nameof(OnChangedCount))] int count;
    Vector2 upperRightLimit;
    Vector2 bottomLeftLimit;
    float ballRadius = 0.2f;
    float padding = 0.2f;
    Tween multipleTween;

    void OnChangedGateType()
    {
        var gateColor = gateColors.Where(_ => _.gateType == gateType).FirstOrDefault();
        if (gateColor == null) return;
        textMeshPro.color = gateColor.textColor;
        bgSr.color = gateColor.bgColor;
    }

    void OnChangedCount()
    {
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

        Vector2 size;
        size.x = bgSr.size.x * bgSr.transform.localScale.x;
        size.y = bgSr.size.y * bgSr.transform.localScale.y;
        upperRightLimit = transform.position + (Vector3)size / 2f;
        bottomLeftLimit = transform.position - (Vector3)size / 2f;
    }


    public void OnTriggerEnterBall(BallController ball)
    {
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

    public void Multiple(BallController ball)
    {
        if (ball.IsPassed(this)) return;
        PlayMultipleTween();

        for (int i = 0; i < count - 1; i++)
        {
            var pos = Vector3.zero;
            pos.x = Random.Range(bottomLeftLimit.x, upperRightLimit.x);
            pos.y = Random.Range(bottomLeftLimit.y, upperRightLimit.y);
            //pos.y = transform.position.y + size.y / 2f;
            var newBall = BallsManager.i.GetNewBall();
            newBall.transform.position = pos;
            newBall.Multiple(this);
        }
    }


    void PlayMultipleTween()
    {
        if (multipleTween == null)
        {
            multipleTween = textMeshPro.transform.DOScale(Vector3.one * 1.3f, 0.2f).SetEase(Ease.Flash, 2);
            return;
        }

        if (multipleTween.IsPlaying()) return;
        multipleTween = textMeshPro.transform.DOScale(Vector3.one * 1.3f, 0.2f).SetEase(Ease.Flash, 2);
    }



    Vector3 GetRandomDegreeOffset()
    {
        var radius = 0.2f;
        var angle = Random.Range(0, 360);
        var rad = angle * Mathf.Deg2Rad;
        var vec = Vector2.zero;
        vec.x = Mathf.Cos(rad);
        vec.y = Mathf.Sin(rad);
        return new Vector3(vec.x, vec.y, 0) * radius; ;
    }

    public void Decrease(BallController ball)
    {
        PlayMultipleTween();
        count += 1;
        textMeshPro.text = count.ToString();
        ball.Decrease();
        if (count == 0) gameObject.SetActive(false);
    }

}

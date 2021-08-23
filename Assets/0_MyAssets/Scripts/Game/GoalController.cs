using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class GoalController : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Transform goalModelTf;
    int goalCount;
    Tween scaleTween;
    Tween bounceTween;
    Tween downTween;

    void Awake()
    {
        this.ObserveEveryValueChanged(_ => goalCount)
            .Subscribe(_ => OnChangedGoalCount())
            .AddTo(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<BallController>();
        if (ball == null) return;
        if (ball.IsGoaled) return;
        ball.IsGoaled = true;
        goalCount++;
    }

    void OnChangedGoalCount()
    {
        text.text = goalCount + "";
        if (scaleTween != null) scaleTween.Kill();
        text.transform.localScale = Vector3.one;
        scaleTween = text.transform.DOScale(Vector3.one * 1.2f, 0.3f).SetEase(Ease.Flash, 2);

        if (goalCount < 100)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.green;
        }


        if (goalCount < 30f)
        {
            if (bounceTween == null)
            {
                bounceTween = goalModelTf.DOMoveY(-7.9f, 0.2f).SetEase(Ease.Flash, 2);
            }
            else
            {
                if (!bounceTween.IsPlaying()) bounceTween = goalModelTf.DOMoveY(-7.9f, 0.2f).SetEase(Ease.Flash, 2);
            }
            return;
        }
        if (goalCount == 30) CameraController.i.ChangePos();
        if (goalModelTf.transform.position.y < -11.7f) return;


        // if (downTween != null) downTween.Kill();
        downTween = goalModelTf.DOMoveY(-0.2f, 0.3f).SetRelative();
    }
}

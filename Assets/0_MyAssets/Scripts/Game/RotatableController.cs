using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatableController : MonoBehaviour
{
    [SerializeField] SpriteRenderer arrowSR;
    Tween tween;
    float startAngle;
    int counter = -1;
    void Start()
    {
        startAngle = transform.eulerAngles.z;
        DOTween.ToAlpha(() => arrowSR.color, (x) => arrowSR.color = x, 0.1f, 1f).SetEase(Ease.Flash, 2).SetLoops(-1);
    }

    public void OnPointerDown()
    {
        if (tween != null) tween.Kill();
        Vector3 angle = Vector3.zero;
        angle.z = counter * 45f;
        tween = transform.DORotate(angle, 0.4f);
        counter *= -1;
    }
}

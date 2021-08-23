using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatableController : MonoBehaviour
{
    [SerializeField] SpriteRenderer arrowSR;
    float startAngle;

    void Start()
    {
        startAngle = transform.eulerAngles.z;
        DOTween.ToAlpha(() => arrowSR.color, (x) => arrowSR.color = x, 0.1f, 1f).SetEase(Ease.Flash, 2).SetLoops(-1);
    }

    public void OnDrag()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.up = worldPos - transform.position;
        SoundManager.i?.Play(4);
    }
}

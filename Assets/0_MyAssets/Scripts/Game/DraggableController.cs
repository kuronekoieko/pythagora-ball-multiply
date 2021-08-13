using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DraggableController : MonoBehaviour
{
    [SerializeField] Transform axisTf;
    [SerializeField] SpriteRenderer arrowSR;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        DOTween.ToAlpha(() => arrowSR.color, (x) => arrowSR.color = x, 0.1f, 1f).SetEase(Ease.Flash, 2).SetLoops(-1);

    }

    public void OnDrag()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.x = Mathf.Clamp(worldPos.x, -3.4f, 3.4f);
        worldPos.y = startPos.y;
        transform.position = worldPos;
        axisTf.position = worldPos;
    }

}

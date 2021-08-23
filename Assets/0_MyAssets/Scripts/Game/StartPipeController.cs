using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx;

public class StartPipeController : MonoBehaviour
{
    [SerializeField] Transform axisLeftTf;
    [SerializeField] Transform axisRightTf;
    [SerializeField] Text countText;
    [SerializeField] Text startCountText;
    int count;
    int startCount;

    void Awake()
    {
        this.ObserveEveryValueChanged(_ => count)
            .Subscribe(_ => OnChangedGoalCount())
            .AddTo(this.gameObject);
    }

    void Start()
    {
        startCount = BallsManager.i.ActiveCount;
        count = startCount;
        startCountText.text = "/" + startCount;
    }

    public void OnPointerDown()
    {
        axisLeftTf.transform.DORotate(Vector3.forward * -90f, 0.5f);
        axisRightTf.transform.DORotate(Vector3.forward * 90f, 0.5f);
    }

    void OnChangedGoalCount()
    {
        countText.text = count.ToString();
    }

    void OnTriggerExit(Collider other)
    {
        var ball = other.gameObject.GetComponent<BallController>();
        if (ball == null) return;
        count--;
    }
}

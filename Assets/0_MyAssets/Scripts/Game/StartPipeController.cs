using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartPipeController : MonoBehaviour
{
    [SerializeField] Transform axisLeftTf;
    [SerializeField] Transform axisRightTf;


    public void OnPointerDown()
    {
        axisLeftTf.transform.DORotate(Vector3.forward * -90f, 0.5f);
        axisRightTf.transform.DORotate(Vector3.forward * 90f, 0.5f);
    }
}

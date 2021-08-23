using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] ParticleSystem brokenPs;
    List<GateController> passedGates { set; get; } = new List<GateController>();

    public void Multiple(GateController gate)
    {
        passedGates.Add(gate);
        gameObject.SetActive(true);
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(scale, 0.2f);
    }

    public bool IsPassed(GateController gate)
    {
        if (passedGates.Contains(gate))
        {
            return true;
        }
        else
        {
            passedGates.Add(gate);
            return false;
        }
    }

    public void Decrease()
    {
        gameObject.SetActive(false);
        brokenPs.transform.parent = null;
        brokenPs.Play();
    }
}

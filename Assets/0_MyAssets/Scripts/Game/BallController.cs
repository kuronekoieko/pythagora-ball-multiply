using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] ParticleSystem brokenPs;
    List<GateController> passedGates { set; get; } = new List<GateController>();
    public bool IsGoaled { set; get; }

    public void Multiple(GateController gate)
    {
        if (IsGoaled) return;
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
        if (IsGoaled) return;
        gameObject.SetActive(false);
        brokenPs.transform.parent = null;
        brokenPs.Play();
    }
}

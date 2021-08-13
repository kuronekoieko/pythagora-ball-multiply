using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool IsDuplicated { set; get; }
    public void Multiple()
    {

    }

    public void Decrease()
    {
        gameObject.SetActive(false);
    }
}

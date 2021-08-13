using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableController : MonoBehaviour
{
    [SerializeField] Transform axisTf;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Variables.screenState != ScreenState.Game) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Variables.screenState = ScreenState.Failed;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Variables.screenState = ScreenState.Clear;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    public UnityEvent<string> e_startDialog;
    public UnityEvent e_onDialogEnd;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (e_startDialog == null)
        {
            e_startDialog = new UnityEvent<string>();
        }

        if (e_onDialogEnd == null)
        {
            e_onDialogEnd = new UnityEvent();
        }
    }
}

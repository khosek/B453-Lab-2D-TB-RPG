using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    // Create a singleton instance of this class.
    public static GameEvents instance;

    public UnityEvent<string> e_startDialog; // The event to start a new dialog.
    public UnityEvent e_onDialogEnd; // The event for when a dialog ends.

    private void Awake()
    {
        // Check to see if the singleton instance is not empty, and it's not this.
        if (instance != null && instance != this)
        {
            // If it wasn't empty and isn't this, destroy this instance. This ensures there's only ever one instance.
            Destroy(gameObject);
        }
        else
        {
            // Otherwise, make the static instance this instance.
            instance = this;
        }

        // Check to see if the events have been initialized yet, if not, initialize them.
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

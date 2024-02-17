using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Interface to the Input methods and properties used by the application.
 * Allows values to be mocked for testing.
 */
public interface IInputInterface 
{
    float GetAxis(string axisName);
    bool GetKey(KeyCode key);
    bool GetKeyDown(KeyCode key);

    Vector3 mousePosition
    {
        get;
    }

    public bool GetMouseButton(int button);
}


using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

class RobotInput : IInputInterface
{
    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }

    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public Vector3 mousePosition
    {
        get { return Input.mousePosition; }
    }

    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool GetMouseButton(int button)
    {
        return Input.GetMouseButton(button);
    }
}


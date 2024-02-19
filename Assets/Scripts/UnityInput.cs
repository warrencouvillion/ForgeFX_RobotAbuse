using UnityEngine;

class UnityInput : IInputInterface
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnClick : MonoBehaviour
{
    public float m_speed = 0.01f;
    Transform rootXform;

    // Start is called before the first frame update
    void Start()
    {
        rootXform = transform;
        while(rootXform.parent != null && rootXform.parent.gameObject.tag == gameObject.tag)
        {
            rootXform = rootXform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool m_isMoving = false;
    Vector3 m_lastMousePos;

    private void OnMouseDrag()
    {
        if(!m_isMoving)
        {
            m_lastMousePos = Input.mousePosition;
            m_isMoving=true;
        }
        var mousePos = Input.mousePosition;
        var deltaMouse = (mousePos - m_lastMousePos) * m_speed;
        m_lastMousePos = mousePos;

        Debug.Log(deltaMouse);

        if (rootXform != null) 
        {
            rootXform.position += deltaMouse;
        }
    }

    private void OnMouseUp()
    {
        m_isMoving = false;
    }
}

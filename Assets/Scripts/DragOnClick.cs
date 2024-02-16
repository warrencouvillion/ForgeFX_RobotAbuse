using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnClick : MonoBehaviour
{
    public float m_mouseMovementScale = 0.001f;
    private Transform m_rootXform;

    // Start is called before the first frame update
    void Start()
    {
        m_rootXform = transform;
        while(m_rootXform.parent != null && m_rootXform.parent.gameObject.tag == gameObject.tag)
        {
            m_rootXform = m_rootXform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool m_movingState = false;
    bool m_isMoving
    {
        get { return m_movingState; }
        set
        {
            m_movingState = value;
            //Hide the cursor during motion so user can't tell when it's not
            //over the part. The part becomes the cursor.
            Cursor.visible = !value;
        }
    }

    Vector3 m_lastMousePos;

    private void OnMouseDrag()
    {
        if(!m_isMoving)
        {
            m_lastMousePos = Input.mousePosition;
            m_isMoving=true;
        }
        var mousePos = Input.mousePosition;
        var deltaMouse = (mousePos - m_lastMousePos) * m_mouseMovementScale;
        m_lastMousePos = mousePos;

        Debug.Log(deltaMouse);

        if (m_rootXform != null) 
        {
            m_rootXform.position += Camera.main.transform.TransformDirection(deltaMouse);
        }
    }

    private void OnMouseUp()
    {
        m_isMoving = false;
    }
}

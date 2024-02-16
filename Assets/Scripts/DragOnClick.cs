using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnClick : MonoBehaviour
{
    public float m_attachDistance = 0.1f;
    public float m_mouseMovementScale = 0.001f;
    private Transform m_groupRootXform;
    private Transform m_parentXform;
    private Vector3 m_anchorLocalPosition;

    // Start is called before the first frame update
    void Start()
    {

        m_groupRootXform = transform;
        while(m_groupRootXform.parent != null && m_groupRootXform.parent.gameObject.tag == gameObject.tag)
        {
            m_groupRootXform = m_groupRootXform.parent;
        }

        m_parentXform= m_groupRootXform.parent;
        m_anchorLocalPosition = m_groupRootXform.localPosition;
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
            m_groupRootXform.parent = null;

            m_lastMousePos = Input.mousePosition;
            m_isMoving=true;
        }
        var mousePos = Input.mousePosition;
        var deltaMouse = (mousePos - m_lastMousePos) * m_mouseMovementScale;
        m_lastMousePos = mousePos;

        if (m_groupRootXform != null) 
        {
            m_groupRootXform.position += Camera.main.transform.TransformDirection(deltaMouse);
        }
    }

    private void OnMouseUp()
    {
        m_isMoving = false;
        if (m_parentXform != null)
        {
            float distance = Vector3.Distance(m_groupRootXform.position, m_parentXform.position);
            if(distance < m_attachDistance)
            {
                m_groupRootXform.parent = m_parentXform;
                m_groupRootXform.localPosition = m_anchorLocalPosition;
            }
        }
    }
}

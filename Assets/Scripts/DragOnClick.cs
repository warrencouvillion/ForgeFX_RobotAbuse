using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragOnClick : MonoBehaviour
{
    public float m_attachDistance = 0.1f;
    public float m_mouseMovementScale = 0.001f;
    public GameObject m_textObject = null;
    TextMeshProUGUI m_textMesh = null;

    private Transform m_groupRootXform;
    private Transform m_parentXform;
    private Vector3 m_anchorLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (m_textObject != null)
        {
            m_textMesh = m_textObject.GetComponent<TextMeshProUGUI>();
        }
        m_groupRootXform = transform;
        while (m_groupRootXform.parent != null && m_groupRootXform.parent.gameObject.tag == gameObject.tag)
        {
            m_groupRootXform = m_groupRootXform.parent;
        }

        m_parentXform = m_groupRootXform.parent;
        m_anchorLocalPosition = m_groupRootXform.localPosition;

        if (m_groupRootXform != transform)
        {
            Invoke("GetValuesFromRoot", 0.5f);
        }
    }

    private void GetValuesFromRoot()
    {
        //All parts of a group should have the same anchor distance as the root
        //part.
        var dragger = m_groupRootXform.gameObject.GetComponent<DragOnClick>();
        if( dragger != null )
        {
            m_attachDistance = dragger.m_attachDistance;
            m_textMesh = dragger.m_textMesh;
        }
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
            if (m_textMesh != null && value)
            {
                m_textMesh.text = "Detached";
            }
        }
    }

    Vector3 m_lastMousePos;

    private void OnMouseDrag()
    {
        if(!m_isMoving)
        {
            m_groupRootXform.parent = null;
            m_lastMousePos = Input.mousePosition;
            m_isMoving = true;
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
            //Get anchor point in world coordinates.
            var anchorPointWorld = m_parentXform.TransformPoint(m_anchorLocalPosition);
            //Get distance of root part to that position.
            float distance = Vector3.Distance(m_groupRootXform.position, anchorPointWorld); 
            if(distance < m_attachDistance)
            {
                StartCoroutine("ReturnToStartPosition");
            }
        }
    }

    private IEnumerator ReturnToStartPosition()
    {
        m_groupRootXform.parent = m_parentXform;
        var startPos = m_groupRootXform.localPosition;
        for (float frac = 0; frac < 1.0f; frac += 0.2f) 
        {
            var pos = Vector3.Lerp(startPos, m_anchorLocalPosition, frac);
            m_groupRootXform.localPosition= pos;
            yield return new WaitForSeconds(1.0f/30.0f);
        }
        m_groupRootXform.localPosition = m_anchorLocalPosition;
        if (m_textMesh != null)
        {
            m_textMesh.text = "Attached";
        }
    }
}

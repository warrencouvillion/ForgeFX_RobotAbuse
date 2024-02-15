using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
public class HighlightOnMouseOver : MonoBehaviour
{
    Renderer[] m_renderers;

    public Color m_highlightColor = Color.white;
    public float m_highlightPower = 1.0f;
        
    // Start is called before the first frame update
    void Start()
    {
        m_renderers = GetComponentsInChildren<Renderer>();

    }

    void OnMouseOver()
    {
        setHighlight(true);

    }

    private void OnMouseExit()
    {
        setHighlight(false);
    }

    private void setHighlight(bool state)
    { 
        foreach(var rend in m_renderers)
        {
            var mat = rend.material;
            var shader = mat.shader;
            if(shader != null )
            {
                LocalKeyword keyword = new LocalKeyword(shader, "_HIGHLIGHTON");
                mat.SetKeyword(keyword, state);
                mat.SetFloat("_HighlightPower", m_highlightPower);
                mat.SetColor("_HighlightColor", m_highlightColor);
            }
        }

    }
}

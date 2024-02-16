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
        if (gameObject.tag == "Untagged")
        {
            m_renderers = GetComponentsInChildren<Renderer>();
        }
        else
        {
            var sameTag = GameObject.FindGameObjectsWithTag(gameObject.tag);
            if (sameTag != null) 
            {
                m_renderers = new Renderer[sameTag.Length];
                int index = 0;
                foreach(var g in sameTag)
                {
                    m_renderers[index++] = g.GetComponent<Renderer>();
                }
            }
        }
        DoHighlightAction(mat =>
        {
            mat.SetFloat("_HighlightPower", m_highlightPower);
            mat.SetColor("_HighlightColor", m_highlightColor);
        });

    }
    
    public void setHiglightState(Material mat, bool state)
    {
        Shader shader = mat.shader;
        if (shader != null)
        {
            LocalKeyword keyword = new LocalKeyword(shader, "_HIGHLIGHTON");
            mat.SetKeyword(keyword, state);
        }
    }

    void OnMouseOver()
    {
        //If another piece is being moved, don't highlight
        if (!Input.GetMouseButton(0))
        {
            DoHighlightAction((mat) => setHiglightState(mat, true));
        }
    }

    void OnMouseExit()
    {
        //Sometimes the cursor and part will diverge during motion, but we
        //don't want to lose highlighting during motion.
        if (!Input.GetMouseButton(0))
        {
            DoHighlightAction((mat) => setHiglightState(mat, false));
        }
    }

    private void OnMouseUp()
    {
        DoHighlightAction((mat) => setHiglightState(mat, false));
    }

    void DoHighlightAction(System.Action<Material> action)
    { 
        foreach(var rend in m_renderers)
        {
            var mat = rend.material;
            action(mat);
        }

    }
}

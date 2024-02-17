using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
public class HighlightOnMouseOver : MonoBehaviour
{
    Renderer[] m_renderers;

    [Tooltip("Color of higlight. (Note: Only color of root object will be used!)")]
    public Color m_highlightColor = Color.white;
    [Tooltip("Sharpness of higlight. (Note: Only value of root object will be used!)")]
    public float m_highlightPower = 1.0f;
    [Tooltip("Select if only children of the part are to be hilighted, instead of everything in its tagged group.")]
    public bool m_onlyHighlightChildren = false;

    //Used for testing.
    public IInputInterface m_input;
        
    // Start is called before the first frame update
    void Start()
    {
        if(m_input == null)
        {
            m_input = new RobotInput();
        }

        if (gameObject.tag == "Untagged")
        {
            m_renderers = GetComponentsInChildren<Renderer>();
        }
        else
        {
            var objsWithSameTage = GameObject.FindGameObjectsWithTag(gameObject.tag);
            if (objsWithSameTage != null) 
            {
                m_renderers = new Renderer[objsWithSameTage.Length];
                int index = 0;
                foreach(var gObj in objsWithSameTage)
                {
                    m_renderers[index++] = gObj.GetComponent<Renderer>();
                    if(!CompareTag(gObj.transform.parent.gameObject.tag))
                    {
                        var hiliter = gObj.GetComponent<HighlightOnMouseOver>();
                        if(hiliter != null)
                        {
                            m_highlightColor = hiliter.m_highlightColor;
                        }

                    }
                }
            }
        }
        DoHighlightAction(mat =>
        {
            mat.SetFloat("_HighlightPower", m_highlightPower);
        });

    }
    
    public void SetHiglightState(Material mat, bool state)
    {
        //If another piece is being moved, don't highlight.
        //Sometimes the cursor and part will diverge during motion, but we
        //don't want to lose highlighting during motion.
        if (m_input.GetMouseButton(0))
        {
            return;
        }

        mat.SetColor("_HighlightColor", m_highlightColor);

        Shader shader = mat.shader;
        if (shader != null)
        {
            LocalKeyword keyword = new LocalKeyword(shader, "_HIGHLIGHTON");
            mat.SetKeyword(keyword, state);
        }
    }

    void OnMouseOver()
    {
        DoHighlightAction((mat) => SetHiglightState(mat, true));
    }

    void OnMouseExit()
    {
        DoHighlightAction((mat) => SetHiglightState(mat, false));
    }

    //Motion is over when user releases mouse button, so stop highlighting.
    private void OnMouseUp()
    {
        DoHighlightAction((mat) => SetHiglightState(mat, false));
    }

    void DoHighlightAction(System.Action<Material> action)
    { 
        foreach(var rend in m_renderers)
        {
            if (!m_onlyHighlightChildren || rend.gameObject.transform.IsChildOf(gameObject.transform))
            {
                var mat = rend.material;
                action(mat);
            }
        }

    }
}

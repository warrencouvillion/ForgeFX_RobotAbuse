using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float m_rotationalSpeed = 1.0f;
        
    CharacterController m_controller;
    // Start is called before the first frame update
    void Start()
    {
        m_controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var increment = m_speed * Time.deltaTime;
        m_controller.Move(Input.GetAxis("Horizontal") * transform.right * increment);
        m_controller.Move(Input.GetAxis("Vertical") * transform.forward* increment);
    }
}

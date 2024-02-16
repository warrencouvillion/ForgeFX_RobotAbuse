using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float m_rotationalSpeed = 1.0f;
    public GameObject m_target;
        
    CharacterController m_controller;
    // Start is called before the first frame update
    void Start()
    {
        m_controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Vector3 mouseEulers = 
                new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0) 
                    * m_rotationalSpeed * Time.deltaTime;
            transform.Rotate(mouseEulers);

        }
        else
        {
            float increment = m_speed * Time.deltaTime;
            m_controller.Move(Input.GetAxis("Horizontal") * transform.right * increment);
            m_controller.Move(Input.GetAxis("Vertical") * transform.forward * increment);
        }

        if(Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Keypad7)) 
        {
            StartCoroutine("LookAtTarget");
        }
    }

    IEnumerator LookAtTarget()
    {
        Vector3 relativePos = m_target.transform.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relativePos);
        Quaternion startRotation = transform.rotation;
        for(float frac = 0; frac < 1.0f; frac += 0.1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, lookAtRotation, frac);
            yield return new WaitForSeconds(1.0f / 30.0f);
        }

    }
}

using System.Collections;
using UnityEngine;


//TODO: write a wrapper around input for testing and time
public class UFOMovement : MonoBehaviour
{
    class RealTime : ITimeInterface
    {
        public float deltaTime
        {
            get { return Time.deltaTime; }
        }
    }

    public float m_speed = 1.0f;
    public float m_rotationalSpeed = 1.0f;
    public GameObject m_target;

    //Isoloated interfaces for testing
    public IInputInterface m_input;
    public ITimeInterface m_time;
        
    CharacterController m_controller;

    // Start is called before the first frame update
    void Start()
    {
        m_controller = gameObject.AddComponent<CharacterController>();
        if(m_input == null )
        {
            m_input = new RobotInput();
        }

        if(m_time == null )
        {
            m_time = new RealTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_input.GetKey(KeyCode.LeftShift) || m_input.GetKey(KeyCode.RightShift))
        {
            Vector3 mouseEulers = 
                new Vector3(-m_input.GetAxis("Vertical"), m_input.GetAxis("Horizontal"), 0) 
                    * m_rotationalSpeed * m_time.deltaTime;
            transform.Rotate(mouseEulers);
        }
        else
        {
            float increment = m_speed * m_time.deltaTime;
            m_controller.Move(m_input.GetAxis("Horizontal") * transform.right * increment);
            m_controller.Move(m_input.GetAxis("Vertical") * transform.forward * increment);
        }

        if(m_input.GetKeyDown(KeyCode.Home) || m_input.GetKeyDown(KeyCode.Keypad7)) 
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

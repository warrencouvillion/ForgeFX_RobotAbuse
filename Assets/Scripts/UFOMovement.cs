using System.Collections;
using UnityEngine;

/**
 * Implements a UFO style movement using only the keyboard. WASD or arrow keys
 * will move the attached object. 
 * When either <SHIFT> key is down, the WASD keys affect rotation.
 * The <HOME> or <KeyPad7> keys will make the attached object look at the
 * object designated in the target parameter.
 */
public class UFOMovement : MonoBehaviour
{
    /**
     * Gives access to the Unity Time.deltaTime property
     */
    class UnityTime : ITimeInterface
    {
        public float deltaTime
        {
            get { return Time.deltaTime; }
        }
    }

    [Tooltip("How fast the object will move")]
    public float m_speed = 1.0f;
    [Tooltip("How fast the object will rotate")]
    public float m_rotationalSpeed = 1.0f;
    [Tooltip("Object that will be looked at when <Home> or <KeyPad 7> key is pressed")]
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
            m_input = new UnityInput();
        }

        if(m_time == null )
        {
            m_time = new UnityTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If a shift key is down, pan and tilt based on key input.
        if (m_input.GetKey(KeyCode.LeftShift) || m_input.GetKey(KeyCode.RightShift))
        {
            transform.Rotate(new Vector3(-m_input.GetAxis("Vertical"), m_input.GetAxis("Horizontal"), 0) 
                    * m_rotationalSpeed * m_time.deltaTime);
        }
        else //Otherwise, dolly the camera
        {
            float increment = m_speed * m_time.deltaTime;
            m_controller.Move(m_input.GetAxis("Horizontal") * transform.right * increment);
            m_controller.Move(m_input.GetAxis("Vertical") * transform.forward * increment);
        }

        //If <HOME> or 7 on the numeric keypad got presse, look at the target.
        //Because popping is ugly and confusing, rotate to look at the target. This will also keep the
        //player better aware of his location wrt the target.
        if(m_target != null && (m_input.GetKeyDown(KeyCode.Home) || m_input.GetKeyDown(KeyCode.Keypad7))) 
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

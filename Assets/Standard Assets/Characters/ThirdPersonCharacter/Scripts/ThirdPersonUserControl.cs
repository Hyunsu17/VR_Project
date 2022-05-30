using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
           Transform k =  GameObject.Find("PlayerCar").GetComponent<Transform>();
            float velocity = 0.2f;
            Vector3 destination = new Vector3(13, 7, 27);

            Vector3 dir = (destination - m_Character.transform.position).normalized;

            float acceleration = 0.2f;

            velocity = (velocity + acceleration * Time.deltaTime);

            float distance = Vector3.Distance(k.position, transform.position);

            



            if (distance <= 5.0f)

            {
                Debug.Log("���Ǹ���");

                transform.position = new Vector3(transform.position.x + (dir.x * 0.05f),

                                                       transform.position.y,

                                                         transform.position.z + (dir.z * 0.05f));

                

            }

            else

            {

                velocity = 0.0f;

            }



        }
    }
}

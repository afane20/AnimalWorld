using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class DemoScriptJoystick : MonoBehaviour
    {
        [Tooltip("Fingers Joystick Script")]
        public FingersJoystickScript JoystickScript;

        [Tooltip("Object to move with the joystick")]
        public GameObject Mover;

        [Tooltip("Units per second to move the square with joystick")]
        public float Speed = 250.0f;

        [Tooltip("Whether joystick moves to touch location")]
        public bool MoveJoystickToGestureStartLocation;

        private void Awake()
        {
            JoystickScript.JoystickExecuted = JoystickExecuted;
            JoystickScript.MoveJoystickToGestureStartLocation = MoveJoystickToGestureStartLocation;
        }
        public float xMin;
        public float xMax;
        public float zMin;
        public float zMax;
        private void JoystickExecuted(FingersJoystickScript script, Vector2 amount)
        {
            Vector3 pos = Mover.transform.position;
            pos.x += (amount.x * Speed * Time.deltaTime);
            //pos.z += (amount.y * Speed * Time.deltaTime);
            pos.z += (-5.0f);
            pos.y = (-1.8f);
            Mover.transform.position = pos;

            Mover.transform.position = new Vector3
            (
                Mathf.Clamp(Mover.GetComponent<Rigidbody>().position.x, xMin, xMax),
                0.0f,
                Mathf.Clamp(Mover.GetComponent<Rigidbody>().position.z, zMin, zMax)
                //Mathf.Clamp(Mover.GetComponent<Rigidbody>().position.z, zMin, zMax)

            );


        }
    }
}

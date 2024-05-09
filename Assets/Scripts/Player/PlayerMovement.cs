using System;
using UnityEngine;
using UnityEngine.InputSystem;
using EnchancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Nightmare
{
    public class PlayerMovement : PausibleObject
    {
        public float speed = 6f;            // The speed that the player will move at.
        public float originalSpeed = 6f;    // For speed orb purpose
        public float prevSpeed;             // For twice speed cheat purpose

        // Input actions
        PlayerInput pInput;
        InputAction move;

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
#if !MOBILE_INPUT
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#else 
        Vector2 lookDir;
        int isAR;
#endif

        private Vector3 lastPosition; // Variable to store the last position

        void Awake ()
        {
            pInput = GetComponent<PlayerInput>();
            move = pInput.actions["Move"];
#if !MOBILE_INPUT
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask ("Floor");
#else 
            EnchancedTouch.EnhancedTouchSupport.Enable();
            isAR = PlayerPrefs.GetInt("isAR", 0);
#endif

            // Set up references.
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();

            // Set prev speed to speed
            prevSpeed = speed;

            StartPausible();
        }

        private void OnEnable()
        {
            move.Enable();
        }

        private void OnDisable()
        {
            move.Disable();
        }

        void OnDestroy()
        {
            StopPausible();
        }

        void FixedUpdate ()
        {
            if (isPaused)
                return;

            // Store the input axes.
#if !MOBILE_INPUT
            float h = move.ReadValue<Vector2>().x;
            float v = move.ReadValue<Vector2>().y;
#else
            float h = 0;
            float v = 0;

            foreach (var touch in Touch.activeTouches)
            {
                if (touch.startScreenPosition != Vector2.zero) {
                    Vector2 currDelta = (
                            touch.screenPosition - touch.startScreenPosition).normalized;
                    if (touch.startScreenPosition.x < Screen.width / 2)
                    {
                        h = currDelta.x;
                        v = currDelta.y;
                    } else if (touch.startScreenPosition.x < Screen.width - Screen.width / 7 && isAR == 0)
                    {
                        lookDir = currDelta;
                    }
                }
            }
#endif
            // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            Turning ();

            // Animate the player.
            Animating (h, v);
        }


        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            if (isAR == 0)
            {
                movement.Set(h, 0f, v);
            } else
            {
                movement = transform.right * h + transform.forward * v;
            }

            // Calculate the distance moved since the last frame
            float distanceMoved = Vector3.Distance(transform.position, lastPosition);

            // Update the last position to the current position
            lastPosition = transform.position;

            // If the distance moved is greater than a small threshold (to avoid counting tiny movements due to floating-point imprecision)
            if (distanceMoved > 0.001f)
            {
                // Increment distanceTraveled by the distance moved
                TextStatistics.distanceTraveled += distanceMoved;
                InGameTextStatistics.distanceTraveled += distanceMoved;
            }
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition (transform.position + movement);
        }


        void Turning ()
        {
#if !MOBILE_INPUT
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay (Mouse.current.position.ReadValue());

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation (newRotatation);
            }
#else

            Vector3 turnDir = new Vector3(lookDir.x, 0f, lookDir.y);

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
        }


        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }

        // Reset speed to default
        public void ResetSpeed()
        {
            this.speed = 6f; // Back to default
        }

        // Activate twice speed cheat
        public void ActivateCheatXTwoSpeed()
        {
            if (this.speed > prevSpeed)
                return;
            this.speed *= 2;
        }

    }
}
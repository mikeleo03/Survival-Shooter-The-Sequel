using UnityEngine;
using System.Collections;

namespace Nightmare
{
    public class CameraFollow : MonoBehaviour, IDataPersistance
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.
        Vector3 offset;                     // The initial offset from the target.

        void Start ()
        {
            // Calculate the initial offset.
            offset = transform.position - target.position;
        }


        void FixedUpdate ()
        {
            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target.position + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        }

        public void LoadData(GameData data)
        {
            if (data.cameraPosition != Vector3.zero)
            {
                this.transform.position = data.cameraPosition;
            }
        }

        public void SaveData(ref GameData data)
        {
            data.cameraPosition = this.transform.position;
        }

    }
}
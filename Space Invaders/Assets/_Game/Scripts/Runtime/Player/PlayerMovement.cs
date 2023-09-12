using System;
using Runtime.Input;
using UnityEngine;

namespace Runtime.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const int SIZE_OFFSET = 2;
        
        [SerializeField] private float speed = 15f;

        private float limit;
        
        private CameraLimits cameraLimits;
        private Transform playerTransform;
        private Vector3 previousPosition = Vector3.zero;

        public void Init(CameraLimits cameraLimitsReference)
        {
            cameraLimits = cameraLimitsReference;
        }

        private void Awake()
        {
            playerTransform = transform;
            limit = cameraLimits.GetHorizontalCameraLimits(playerTransform.position.z, SIZE_OFFSET);
        }

        public void Move(float direction)
        {
            Vector3 newPos = playerTransform.position;
            newPos.x += direction * (speed * Time.deltaTime);

            if (previousPosition == newPos)
            {
                return;
            }

            newPos.x = Mathf.Clamp(newPos.x, -limit, limit);

            previousPosition = newPos;
            playerTransform.position = newPos;
        }
    }
}
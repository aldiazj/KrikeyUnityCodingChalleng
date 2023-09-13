using Runtime.DI;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Input
{
    public class CameraLimits : MonoBehaviour, IRequester
    {
        private Camera sceneCamera;
        private Transform sceneCameraTransform;
        public void Init(DependencyContainer dependencyContainer)
        {
            sceneCamera = dependencyContainer.GetComponentDependency<Camera>();
            sceneCameraTransform = sceneCamera.transform;
        }

        public float GetHorizontalCameraLimits(float zPosition, float offset = 0)
        {
            zPosition -= sceneCameraTransform.position.z;
            return sceneCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height, zPosition)).x - offset;
        }
    }
}
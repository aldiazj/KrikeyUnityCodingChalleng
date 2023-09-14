using System;
using UnityEngine;

namespace Runtime.Input
{
    public class InputManager : MonoBehaviour
    {
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";
        
        public Action<float> onHorizontal;
        public Action onFire;
        public Action onEscape;

        private void Update()
        {
            onHorizontal?.Invoke(UnityEngine.Input.GetAxis(HORIZONTAL_AXIS_NAME));
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                onFire?.Invoke();
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                onEscape?.Invoke();
            }
        }
    }
}

using System;
using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Input
{
    public class InputManager : MonoBehaviour, IRequester
    {
        private const string HORIZONTAL_AXIS_NAME = "Horizontal";

        private GameManager gameManager;
        
        public Action<float> onHorizontal;
        public Action onFire;

        private void Update()
        {
                if (gameManager.GameState != GameState.GameState.Play)
            {
                return;
            }
            
            onHorizontal?.Invoke(UnityEngine.Input.GetAxis(HORIZONTAL_AXIS_NAME));
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                onFire?.Invoke();
            }
        }

        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }
    }
}

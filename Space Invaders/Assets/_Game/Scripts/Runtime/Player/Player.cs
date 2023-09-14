using System;
using Runtime.DI;
using Runtime.GameState;
using Runtime.Health;
using Runtime.Input;
using Runtime.Interfaces;
using Runtime.Weapon;
using UnityEngine;

namespace Runtime.Player
{
    public class Player : MonoBehaviour, IRequester
    {
        [SerializeField] private int maxLives;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Weapon.Weapon weapon;

        private GameManager gameManager;
        private HealthSystem healthSystem;
        private InputManager inputManager;
        private Transform playerTransform;

        public bool IsAlive() => healthSystem.Lives > 0;

        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager     = dependencyContainer.GetComponentDependency<GameManager>();
            inputManager    = dependencyContainer.GetComponentDependency<InputManager>();
            playerMovement.Init(dependencyContainer.GetComponentDependency<CameraLimits>());
            healthSystem    = new HealthSystem(maxLives);
            playerTransform = transform;
            
            gameManager.onGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.GameState newGameState)
        {
            switch (newGameState)
            {
                case GameState.GameState.Play:
                    inputManager.onFire         += weapon.Fire;
                    inputManager.onHorizontal   += playerMovement.Move;
                    healthSystem.onAllLivesLost += Die;
                    break;
                case GameState.GameState.Menu:
                case GameState.GameState.LevelSetup:
                case GameState.GameState.Pause:
                case GameState.GameState.GameOver:
                    inputManager.onFire         -= weapon.Fire;
                    inputManager.onHorizontal   -= playerMovement.Move;
                    healthSystem.onAllLivesLost -= Die;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }
        }

        private void Die()
        {
            gameManager.EndGame(hasLost: true);
        }

        private void OnTriggerEnter(Collider colliderEntered)
        {
            Bullet bullet = colliderEntered.GetComponent<Bullet>();

            if (!bullet)
            {
                return;
            }

            healthSystem.TakeDamage();
            Debug.Log($"Player has now {healthSystem.Lives} lives");
        }

        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }

        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }
    }
}

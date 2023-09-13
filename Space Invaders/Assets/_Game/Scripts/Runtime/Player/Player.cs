using Runtime.DI;
using Runtime.Health;
using Runtime.Input;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Player
{
    public class Player : MonoBehaviour, IRequester
    {
        [SerializeField] private int maxLives;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Weapon.Weapon weapon;

        private HealthSystem healthSystem;
        private InputManager inputManager;
        private Transform playerTransform;

        public bool IsAlive() => healthSystem.Lives > 0;

        public void Init(DependencyContainer dependencyContainer)
        {
            inputManager    = dependencyContainer.GetComponentDependency<InputManager>();
            playerMovement.Init(dependencyContainer.GetComponentDependency<CameraLimits>());
            healthSystem    = new HealthSystem(maxLives);
            playerTransform = transform;
        }

        private void OnEnable()
        {
            inputManager.onFire         += weapon.Fire;
            inputManager.onHorizontal   += playerMovement.Move;
            healthSystem.onAllLivesLost += Die;

        }

        private void OnDisable()
        {
            inputManager.onFire         -= weapon.Fire;
            inputManager.onHorizontal   -= playerMovement.Move;
            healthSystem.onAllLivesLost -= Die;

        }

        private void Die()
        {
            Debug.Log("Player is dead");
        }

        private void OnTriggerEnter(Collider colliderEntered)
        {
            Bullet bullet = colliderEntered.GetComponent<Bullet>();
            
            if (bullet)
            {
                healthSystem.TakeDamage();
                Debug.Log($"Player has now {healthSystem.Lives} lives");
            }
        }

        public Vector3 GetPlayerPosition()
        {
            return playerTransform.position;
        }
    }
}

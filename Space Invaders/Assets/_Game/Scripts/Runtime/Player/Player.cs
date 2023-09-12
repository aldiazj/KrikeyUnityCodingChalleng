using Runtime.DI;
using Runtime.Health;
using Runtime.Input;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Player
{
    public class Player : MonoBehaviour, IRequester
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int maxLives;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Weapon.Weapon weapon;

        private HealthSystem healthSystem;
        private InputManager inputManager;

        public bool IsAlive() => healthSystem.Lives > 0;

        public void Init(DependencyContainer dependencyContainer)
        {
            inputManager = dependencyContainer.GetComponentDependency<InputManager>();
            playerMovement.Init(dependencyContainer.GetComponentDependency<CameraLimits>());
            healthSystem = new HealthSystem(maxHealth, maxLives);
        }

        private void OnEnable()
        {
            inputManager.onFire += weapon.Fire;
            inputManager.onHorizontal += playerMovement.Move;
        }

        private void OnDisable()
        {
            inputManager.onFire -= weapon.Fire;
            inputManager.onHorizontal -= playerMovement.Move;
        }

        private void OnTriggerEnter(Collider colliderEntered)
        {
            Bullet bullet = colliderEntered.GetComponent<Bullet>();
            
            if (bullet)
            {
                healthSystem.TakeDamage();
            }
        }
    }
}

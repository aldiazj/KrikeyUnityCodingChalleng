using System.Collections;
using Runtime.GameState;
using Runtime.Health;
using Runtime.Weapon;
using UnityEngine;

namespace Runtime.Enemy
{
    public class BonusUFO : MonoBehaviour
    {
        [SerializeField] private int pointsAwarded;
        [SerializeField] private float speed;

        private Transform ufoTransform;
        private GameManager gameManager;
        private ScoringSystem scoringSystem;
        private HealthSystem healthSystem = new HealthSystem(1);
        private Vector3 direction;

        private void OnDisable()
        {
            healthSystem.onAllLivesLost -= Die;
        }

        private void Update()
        {
            ufoTransform.Translate(direction * (speed * Time.deltaTime));
        }

        public void SetUp(GameManager gameManagerReference, Vector3 movementDirection)
        {
            ufoTransform = transform;
            direction    = movementDirection;
            gameManager  = gameManagerReference;
            scoringSystem = gameManager.GetScoringSystem();
            healthSystem.onAllLivesLost += Die;
            StartCoroutine(AutoDestroy());
        }

        private IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        private void Die()
        {
            scoringSystem.Increase(pointsAwarded);
            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider colliderEntered)
        {
            Bullet bullet = colliderEntered.GetComponent<Bullet>();
            
            if (bullet)
            {
                healthSystem.TakeDamage();
            }
        }
    }
}
using System;
using Runtime.Health;
using Runtime.Player;
using Runtime.ScriptableObjects.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float firingTimeOffset = 3f;
        [SerializeField] private Weapon.Weapon weapon;
        [SerializeField] private Renderer enemyRenderer;
        [SerializeField] private LayerMask friendlyLayer;

        private float firingSpeed;
        private float firingTime = float.PositiveInfinity;
        private float timeSinceLastShot;

        private EnemyHorde enemyHorde;
        private Transform enemyTransform;
        private HealthSystem healthSystem;

        public void Init(EnemySO data, EnemyHorde horde)
        {
            firingSpeed                  = data.firingSpeed;
            firingTime                   = Random.Range(firingSpeed, firingSpeed + firingTimeOffset);
            weapon.Init();
            enemyRenderer.material.color = data.color;
            healthSystem                 = new HealthSystem(1);
            healthSystem.onAllLivesLost += Die;
            enemyHorde                   = horde;
        }

        private void Awake()
        {
            enemyTransform = transform;
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot < firingTime)
            {
                return;
            }

            FireWeapon();
            timeSinceLastShot = 0;
        }

        private void FireWeapon()
        {
            if (!CanShoot())
            {
                return;
            }

            weapon.Fire();
        }

        private bool CanShoot()
        {
            return !Physics.Raycast(weapon.GetFiringPosition(), weapon.GetFiringDirection(), out RaycastHit _, Mathf.Infinity, friendlyLayer);
        }

        public void MoveInDirection(Vector3 direction)
        {
            enemyTransform.position += direction;
        }

        public void MoveToInner(Vector3 newPosition)
        {
            enemyTransform.localPosition = newPosition;
        }

        public Vector3 GetPosition()
        {
            return enemyTransform.position;
        }

        private void Die()
        {
            enemyHorde.RemoveEnemyFromHorde(this);
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
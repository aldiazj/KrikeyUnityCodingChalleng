using Runtime.Player;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float nextShotWaitInSeconds = 2f;
        [SerializeField] private Transform bulletsSpawn;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private LayerMask affectingLayerMask;
        [SerializeField] private LayerMask ignoredLayerMask;
        [SerializeField] private float bulletSpeed;
        
        private static ObjectPool<Bullet> BulletPool;
        
        private float timeUntilNextShotIsPossible;

        public void Init(float timeBetweenShots = 0)
        {
            nextShotWaitInSeconds = timeBetweenShots;
            GameObject bulletsHolder = new GameObject("BulletsHolder");
            BulletPool ??= new ObjectPool<Bullet>(() => CreateBullets(bulletsHolder.transform), maxSize: 20);
        }

        private Bullet CreateBullets(Transform bulletsHolder)
        {
            GameObject bulletGameObject = Instantiate(bulletPrefab, bulletsHolder);
            return bulletGameObject.GetComponent<Bullet>();
        }

        private void Update()
        {
            timeUntilNextShotIsPossible -= Time.deltaTime;
        }

        public void Fire()
        {
            if (timeUntilNextShotIsPossible > 0)
            {
                return;
            }

            timeUntilNextShotIsPossible = nextShotWaitInSeconds;
            Bullet bullet = BulletPool.Get();
            bullet.Init(bulletsSpawn, affectingLayerMask, ignoredLayerMask, this);
            bullet.Fire(bulletSpeed);
        }

        public Vector3 GetFiringPosition()
        {
            return bulletsSpawn.position;
        }

        public Vector3 GetFiringDirection()
        {
            return bulletsSpawn.forward;
        }

        public void ReturnBulletToPool(Bullet bullet)
        {
            BulletPool.Release(bullet);
        }
    }
}
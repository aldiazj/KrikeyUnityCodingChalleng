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
        private static GameObject BulletsHolder;
        
        private float timeUntilNextShotIsPossible;

        public void Init(float timeBetweenShots = 0)
        {
            nextShotWaitInSeconds = timeBetweenShots;
            BulletPool ??= new ObjectPool<Bullet>(CreateBullets, maxSize: 20);
        }

        private Bullet CreateBullets()
        {
            BulletsHolder??= new GameObject("BulletsHolder");
            DontDestroyOnLoad(BulletsHolder);
            GameObject bulletGameObject = Instantiate(bulletPrefab, BulletsHolder.transform);
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
using UnityEngine;

namespace Runtime.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody bulletRigidbody;

        private float lifeSpan;
        private bool hasBeenFired;
        
        private LayerMask affectingLayer;
        private LayerMask ignoreLayer;
        private Transform bulletTransform;
        private Runtime.Weapon.Weapon owner;
        
        private void Awake()
        {
            bulletTransform = transform;
        }

        private void Update()
        {
            if (!hasBeenFired)
            {
                return;
            }

            lifeSpan -= Time.deltaTime;

            if (lifeSpan <= 0)
            {
                Release();
            }
        }

        public void Init(Transform spawnTransform, LayerMask affectingLayerMask, LayerMask ignoreLayerMask, Runtime.Weapon.Weapon weapon)
        {
            affectingLayer           = affectingLayerMask;
            ignoreLayer              = ignoreLayerMask;
            bulletTransform.position = spawnTransform.position;
            bulletTransform.rotation = spawnTransform.rotation;
            owner                    = weapon;
        }

        public void Fire(float speed)
        {
            bulletRigidbody.AddForce(bulletTransform.forward * speed);
            bulletRigidbody.includeLayers = affectingLayer;
            bulletRigidbody.excludeLayers = ignoreLayer;
            lifeSpan                      = 5;
            hasBeenFired                  = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasBeenFired)
            {
                Release();
            }
        }

        private void Release()
        {
            owner.ReturnBulletToPool(this);
            bulletRigidbody.velocity = Vector3.zero;
            bulletTransform.position = new Vector3(1000, 1000, 1000);
            hasBeenFired             = false;
        }
    }
}
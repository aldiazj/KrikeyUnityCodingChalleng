using UnityEngine;

namespace Runtime.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float nextShotWaitInSeconds = 2f;
        private float timeUntilNextShotIsPossible;

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
            Debug.Log("fired!");
        }
    }
}
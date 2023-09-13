using UnityEngine;

namespace Runtime.ScriptableObjects.Enemy
{
    [CreateAssetMenu(menuName = "Create EnemySO", fileName = "EnemySO", order = 0)]
    public class EnemySO: ScriptableObject
    {
        public Color color;
        public float firingSpeed;
    }
}
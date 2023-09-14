using Runtime.ScriptableObjects.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.ScriptableObjects.Level
{
    [CreateAssetMenu(menuName = "Create LevelSO", fileName = "LevelSO", order = 0)]
    public class LevelSO: ScriptableObject
    {
        public EnemySO[] enemiesCatalog;
        public int rows;
        public int enemiesPerRow;
        public float initialSpeedInterval;
    }
}
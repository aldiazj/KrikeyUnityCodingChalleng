using System.Collections.Generic;
using Runtime.DI;
using Runtime.GameState;
using Runtime.Input;
using Runtime.Interfaces;
using Runtime.ScriptableObjects.Enemy;
using UnityEngine;

namespace Runtime.Enemy
{
    public class EnemyHorde : MonoBehaviour, IRequester
    {
        private const float ROWS_SPACING = 2.0f;
        private const float COLUMNS_SPACING = 2.0f;

        private readonly List<Enemy> enemies = new List<Enemy>();
        
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private EnemySO[] enemiesCatalog;
        [SerializeField] private int enemyRows;
        [SerializeField] private int enemiesPerRow;
        [SerializeField] private float hordeMovementInterval = 2;

        private GameManager gameManager;
        private Transform hordeTransform;
        private CameraLimits cameraLimits;
        private float limit;
        private float timeSinceLastMovement;
        private Vector3 hordeDirection = Vector3.right;
        private Player.Player player;
        private float playerYPos;


        public void Init(DependencyContainer dependencyContainer)
        {
            player       = dependencyContainer.GetComponentDependency<Player.Player>();
            cameraLimits = dependencyContainer.GetComponentDependency<CameraLimits>();
            gameManager  = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void Awake()
        {
            hordeTransform = transform;
            limit          = cameraLimits.GetHorizontalCameraLimits(hordeTransform.position.z);
            playerYPos     = player.GetPlayerPosition().y;
        }

        private void OnEnable()
        {
            gameManager.onGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            gameManager.onGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.GameState gameState)
        {
            if (gameState != GameState.GameState.LevelSetup)
            {
                return;
            }

            SetUp();
            gameManager.ChangeState(GameState.GameState.Play);
        }

        private void Update()
        {
            timeSinceLastMovement += Time.deltaTime;

            if (timeSinceLastMovement < hordeMovementInterval)
            {
                return;
            }

            timeSinceLastMovement = 0;

            if (!CanTheHordeMoveFurther(hordeDirection * COLUMNS_SPACING))
            {
                MoveHordeForward();
                return;
            }

            MoveHorde(hordeDirection * COLUMNS_SPACING);
        }

        private void SetUp()
        {
            for (int row = 0; row < enemyRows; row++)
            {
                float width = COLUMNS_SPACING * (enemiesPerRow - 1);
                float height = ROWS_SPACING * (enemyRows - 1);
                Vector2 center = new Vector2(-width * 0.5f, -height * 0.5f);
                Vector3 rowPosition = new Vector3(center.x, center.y - row * ROWS_SPACING, 0);

                for (int column = 0; column < enemiesPerRow; column++)
                {
                    GameObject enemyGameObject = Instantiate(enemyPrefab, hordeTransform);
                    Enemy enemy = enemyGameObject.GetComponent<Enemy>();
                    Vector3 position = rowPosition;
                    position.x += column * COLUMNS_SPACING;
                    enemy.Init(GetEnemyData(), this);
                    enemy.MoveToInner(position);
                    enemies.Add(enemy);
                }
            }
        }

        private void MoveHordeForward()
        {
            Vector3 direction = Vector3.down * ROWS_SPACING;
            
            if (IsHordeLanding(direction))
            {
                gameManager.EndGame(hasLost: true);
            }

            hordeDirection *= -1;
            MoveHorde(direction);
        }

        private void MoveHorde(Vector3 direction)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.MoveInDirection(direction);
            }
        }

        private bool CanTheHordeMoveFurther(Vector3 direction)
        {
            foreach (Enemy enemy in enemies)
            {
                Vector3 position = enemy.GetPosition() + direction;

                if (position.x > limit || position.x < -limit)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsHordeLanding(Vector3 direction)
        {
            foreach (Enemy enemy in enemies)
            {
                Vector3 position = enemy.GetPosition() + direction;
                if (position.y <= playerYPos)
                {
                    return true;
                }
            }
            return false;
        }

        private EnemySO GetEnemyData()
        {
            return enemiesCatalog[0];
        }

        public void RemoveEnemyFromHorde(Enemy enemy)
        {
            enemies.Remove(enemy);

            hordeMovementInterval *= 0.9f;
            
            if (enemies.Count == 0)
            {
                gameManager.EndGame(true);
            }
        }
    }
}
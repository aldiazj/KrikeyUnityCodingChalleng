using System;
using Runtime.DI;
using Runtime.Input;
using Runtime.Interfaces;
using Runtime.Serializer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.GameState
{
    public class GameManager : MonoBehaviour, IRequester
    {
        public GameState State { get; private set; } = GameState.Menu;
        public Action<GameState> onGameStateChanged;

        private string gameOverState;

        private InputManager inputManager;
        private Player.Player player;
        private ScoringSystem scoringSystem;
        private HighScores highScoringSystem;
        private DataSerializer dataSerializer;

        public void Init(DependencyContainer dependencyContainer)
        {
            inputManager      = dependencyContainer.GetComponentDependency<InputManager>();
            player            = dependencyContainer.GetComponentDependency<Player.Player>();
            scoringSystem     = new ScoringSystem();
            dataSerializer    = new DataSerializer();
            highScoringSystem = dataSerializer.LoadHighScores();
        }

        private void Awake()
        {
            Application.focusChanged += OnFocusChanged;
            inputManager.onEscape += OnEscape;
        }

        private void OnEscape()
        {
            switch (State)
            {
                case GameState.Play:
                    ChangeState(GameState.Pause);
                    return;
                case GameState.Pause:
                    ChangeState(GameState.Play);
                    return;
            }
        }

        private void OnDestroy()
        {
            Application.focusChanged -= OnFocusChanged;
        }

        private void OnFocusChanged(bool hasFocus)
        {
            if (!hasFocus && State == GameState.Play)
            {
                ChangeState(GameState.Pause);
            }
        }

        public void ChangeState(GameState newState)
        {
            if (State == newState)
            {
                return;
            }

            switch (newState)
            {
                case GameState.Menu:
                case GameState.Pause:
                case GameState.GameOver:
                {
                    Time.timeScale = 0;
                    break;
                }
                case GameState.LevelSetup:
                    scoringSystem.Reset();
                    break;
                case GameState.Play:
                {
                    Time.timeScale = 1;
                    break;
                }
                default:
                {
                    throw new NotImplementedException($"There isn't an implementation for the case {nameof(newState)}");
                }
            }

            State = newState;
            onGameStateChanged?.Invoke(State);
        }

        public void EndGame(bool hasLost)
        {
            
            gameOverState = hasLost
                ? BuildLostGameOverState(player.IsAlive())
                : "The earth has been saved! thanks for your effort." ;

            gameOverState += $"\nYour score was: {scoringSystem.Score}";
            ChangeState(GameState.GameOver);
        }

        public string GetGameOverState()
        {
            return gameOverState;
        }

        private static string BuildLostGameOverState(bool isPlayerAlive)
        {
            return isPlayerAlive
                ? "The earth is condemned! you survived the attack but the invaders managed to land on earth"
                : "The planet is doomed, without you to protect it, aliens will take control and extinguish life as we know it";
        }

        public ScoringSystem GetScoringSystem()
        {
            return scoringSystem;
        }
        public HighScores GetHighScoringSystem()
        {
            return highScoringSystem;
        }

        public static void Exit()
        {
            Application.Quit();
        }

        public void SaveHighScore(string scoreOwner)
        {
            Score newScore = new Score
            {
                owner = scoreOwner,
                value = scoringSystem.Score,
            };
            
            highScoringSystem.AddScore(newScore);
            dataSerializer.SaveHighScores(highScoringSystem);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
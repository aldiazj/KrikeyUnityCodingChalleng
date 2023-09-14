using System;
using Runtime.DI;
using Runtime.Input;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.GameState
{
    public class GameManager : MonoBehaviour, IRequester
    {
        private GameState gameState = GameState.Menu;

        public Action<GameState> onGameStateChanged;

        private string gameOverState;
        
        private InputManager inputManager;
        private Player.Player player;

        public void Init(DependencyContainer dependencyContainer)
        {
            inputManager = dependencyContainer.GetComponentDependency<InputManager>();
            player       = dependencyContainer.GetComponentDependency<Player.Player>();
        }

        private void Awake()
        {
            Application.focusChanged += OnFocusChanged;
            inputManager.onEscape += OnEscape;
        }

        private void OnEscape()
        {
            switch (gameState)
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
            if (!hasFocus && gameState == GameState.Play)
            {
                ChangeState(GameState.Pause);
            }
        }

        public void ChangeState(GameState newState)
        {
            if (gameState == newState)
            {
                return;
            }

            switch (newState)
            {
                case GameState.Menu:
                case GameState.LevelSetup:
                case GameState.Pause:
                case GameState.GameOver:
                {
                    Time.timeScale = 0;
                    break;
                }
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

            gameState = newState;
            onGameStateChanged?.Invoke(gameState);
        }

        public void EndGame(bool hasLost)
        {
            
            gameOverState = hasLost
                ? BuildLostGameOverState(player.IsAlive())
                : "The earth has been saved! thanks for your effort." ;

            gameOverState += "\nYour score was:";
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

        public static void Exit()
        {
            Application.Quit();
        }
    }
}
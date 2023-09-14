using System;
using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using Runtime.UI.Views;
using UnityEngine;

namespace Runtime.UI
{
    public class ViewsController : MonoBehaviour, IRequester
    {
        private ViewScript activeViewScript;

        private ViewScript[] availableViewScripts;

        private GameManager gameManager;

        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void Awake()
        {
            availableViewScripts = GetComponentsInChildren<ViewScript>(true);
            OpenView<MainMenuView>();
        }

        private void OnEnable()
        {
            availableViewScripts            = GetComponentsInChildren<ViewScript>(true);
            gameManager.onGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            gameManager.onGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.GameState newGameState)
        {
            switch (newGameState)
            {
                case GameState.GameState.Menu:
                    OpenView<MainMenuView>(true);
                    break;
                case GameState.GameState.LevelSetup:
                    OpenView<HUDView>(true);
                    break;
                case GameState.GameState.Play:
                    OpenView<HUDView>(true);
                    break;
                case GameState.GameState.Pause:
                    OpenView<PauseView>();
                    break;
                case GameState.GameState.GameOver:
                    OpenView<GameOverView>(true);
                    break;
                default:
                    throw new NotImplementedException($"No implementation of the possible state {newGameState}");
            }
        }

        public ViewScript OpenView<T>(bool shouldCloseActiveView = false) where T : ViewScript
        {
            foreach (ViewScript availableViewScript in availableViewScripts)
            {
                if (availableViewScript is not T)
                {
                    continue;
                }
                
                if (shouldCloseActiveView && activeViewScript != null)
                {
                    activeViewScript.Close();
                }

                availableViewScript.Open();
                
                activeViewScript = availableViewScript;

                return availableViewScript;
            }

            throw new Exception($"View of type {typeof(T).Name} is not available");
        }

        public ViewScript CloseView<T>() where T : ViewScript
        {
            foreach (ViewScript availableViewScript in availableViewScripts)
            {
                if (availableViewScript is not T)
                {
                    continue;
                }

                availableViewScript.Close();
                return availableViewScript;
            }

            throw new Exception($"View of type {typeof(T).Name} is not available");
        }
    }
}
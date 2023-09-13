using System;
using UnityEngine;

namespace Runtime.GameState
{
    public class GameManager : MonoBehaviour
    {
        public GameState GameState { get; private set; } = GameState.Menu;

        public Action<GameState> onGameStateChanged;

        public void ChangeState(GameState newState)
        {
            if (GameState == newState)
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

            GameState = newState;
            onGameStateChanged?.Invoke(GameState);
        }
    }
}
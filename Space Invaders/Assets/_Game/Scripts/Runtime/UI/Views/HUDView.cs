using Runtime.DI;
using Runtime.GameState;
using Runtime.Health;
using Runtime.Interfaces;
using Runtime.UI.Widgets;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Views
{
    public class HUDView : ViewScript, IRequester
    {
        [SerializeField] private LivesWidget livesWidget;
        [SerializeField] private TextMeshProUGUI scoreTextMesh;
        
        private Player.Player player;
        private GameManager gameManager;
        private HealthSystem healthSystem;

        public void Init(DependencyContainer dependencyContainer)
        {
            player = dependencyContainer.GetComponentDependency<Player.Player>();
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void OnScoreUpdated(int newScore)
        {
            scoreTextMesh.text = newScore.ToString();
        }

        private void OnEnable()
        {
            healthSystem = player.GetHealthSystem();
            healthSystem.onLifeLost += OnLifeLost;
            gameManager.GetScoringSystem().scoreUpdate += OnScoreUpdated;
        }

        private void OnDisable()
        {
            healthSystem.onLifeLost -= OnLifeLost;
            gameManager.GetScoringSystem().scoreUpdate -= OnScoreUpdated;
        }

        private void OnLifeLost()
        {
            livesWidget.UpdateLivesShown(healthSystem.Lives);
        }
    }
}
using Runtime.DI;
using Runtime.Health;
using Runtime.Interfaces;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Views
{
    public class HUDView : ViewScript, IRequester
    {
        [SerializeField] private LivesWidget livesWidget;
        [SerializeField] private TextMeshProUGUI textMesh;
        
        private Player.Player player;
        private HealthSystem healthSystem;

        public void Init(DependencyContainer dependencyContainer)
        {
            player = dependencyContainer.GetComponentDependency<Player.Player>();
        }

        private void OnEnable()
        {
            healthSystem = player.GetHealthSystem();
            healthSystem.onLifeLost += OnLifeLost;
        }

        private void OnDisable()
        {
            healthSystem.onLifeLost -= OnLifeLost;
        }

        private void OnLifeLost()
        {
            livesWidget.UpdateLivesShown(healthSystem.Lives);
        }
    }
}
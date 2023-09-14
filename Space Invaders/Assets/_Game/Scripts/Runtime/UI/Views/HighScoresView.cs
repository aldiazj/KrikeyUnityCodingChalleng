using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using Runtime.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Views
{
    public class HighScoresView : ViewScript, IRequester
    {
        [SerializeField] private ScoreWidget[] scoreWidgets;
        [SerializeField] private Button backButton;
        
        private GameManager gameManager;
        private HighScores highScores;
        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void Awake()
        {
            highScores = gameManager.GetHighScoringSystem();
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(GoBack);
            for (int i = 0; i < scoreWidgets.Length; i++)
            {
                ScoreWidget scoreWidget = scoreWidgets[i];
                scoreWidget.Hide();

                if (highScores.Scores.Count > i)
                {
                    scoreWidget.Show(highScores.Scores[i], i+1);
                }
            }
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(GoBack);
        }

        private void GoBack()
        {
            gameManager.RestartGame();
        }
    }
}
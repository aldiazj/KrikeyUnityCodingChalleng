using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Widgets
{
    public class HighScoresSaveWidget : MonoBehaviour, IRequester
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button saveButton;
        
        private GameManager gameManager;
        private GameObject highScoresSaveGameObject;

        private void OnEnable()
        {
            saveButton.onClick.AddListener(SaveScore);
        }

        private void OnDisable()
        {
            saveButton.onClick.RemoveListener(SaveScore);
        }

        public void Show()
        {
            highScoresSaveGameObject.SetActive(true);
        }

        private void SaveScore()
        {
            gameManager.SaveHighScore(inputField.text);
            gameManager.RestartGame();
        }

        public void Hide()
        {
            highScoresSaveGameObject.SetActive(false);
        }

        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager              = dependencyContainer.GetComponentDependency<GameManager>();
            highScoresSaveGameObject = gameObject;
        }
    }
}
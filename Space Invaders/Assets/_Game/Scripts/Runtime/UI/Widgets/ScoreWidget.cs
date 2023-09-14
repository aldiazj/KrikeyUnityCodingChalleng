using Runtime.GameState;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Widgets
{
    public class ScoreWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI placeTextMesh;
        [SerializeField] private TextMeshProUGUI nameTextMesh;
        [SerializeField] private TextMeshProUGUI scoreTextMesh;
        
        public void Show(Score score, int place)
        {
            nameTextMesh.text = score.owner;
            scoreTextMesh.text = score.value.ToString();
            placeTextMesh.text = place.ToString();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
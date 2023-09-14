using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Views
{
    public class LivesWidget : MonoBehaviour
    {
        [SerializeField] private Image[] livesImages;

        public void UpdateLivesShown(int lives)
        {
            for (int i = 0; i < livesImages.Length; i++)
            {
                livesImages[i].enabled = i < lives;
            }
        }
    }
}
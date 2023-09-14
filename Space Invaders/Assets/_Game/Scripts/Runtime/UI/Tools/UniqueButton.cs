using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime.UI.Tools
{
    /// <summary>
    /// Represents a custom button with unique visual states. Provides functionalities 
    /// to change its appearance based on its interactivity and set custom onClick events.
    /// </summary>
    public class UniqueButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GraphicRepresentation[] graphicRepresentations;

        /// <summary>
        /// Initializes the button with the provided UnityAction, which will be executed on button press.
        /// </summary>
        /// <param name="onPressed">Action to be executed when the button is pressed.</param>
        public void Init(UnityAction onPressed)
        {
            button.onClick.AddListener(onPressed);
        }

        /// <summary>
        /// Updates the button's interactivity and visual state based on the provided active state.
        /// </summary>
        /// <param name="activeState">If set to true, the button becomes interactive; otherwise, it's non-interactive.</param>
        public void SetActiveState(bool activeState)
        {
            button.interactable = activeState;

            foreach (GraphicRepresentation graphicRepresentation in graphicRepresentations)
            {
                graphicRepresentation.graphic.color = activeState
                    ? graphicRepresentation.unselectedColor
                    : graphicRepresentation.selectedColor;
            }
        }
    }
}
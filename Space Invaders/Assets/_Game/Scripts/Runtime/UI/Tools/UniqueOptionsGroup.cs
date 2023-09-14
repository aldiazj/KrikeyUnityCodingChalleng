using UnityEngine;

namespace Runtime.UI.Tools
{
    /// <summary>
    /// Represents a group of unique buttons, ensuring that only one button is active at a time.
    /// When one button is pressed, it releases the others, maintaining the uniqueness of the selection.
    /// </summary>
    public class UniqueOptionsGroup : MonoBehaviour
    {
        protected UniqueButton[] uniqueOptions;

        // Awake function is set virtual as a reminder for child classes that base.Awake needs to be called
        protected virtual void Awake()
        {
            Init();
        }

        /// <summary>
        /// Initializes the options group. Designed to be overridden by child classes for custom behavior.
        /// </summary>
        protected virtual void Init()
        {
        }

        /// <summary>
        /// Sets the active state for buttons. When a button is pressed, all other buttons are released.
        /// </summary>
        /// <param name="buttonPressed">The button which has been pressed.</param>
        protected void OnButtonPressed(Object buttonPressed)
        {
            foreach (UniqueButton footerButton in uniqueOptions)
            {
                footerButton.SetActiveState(footerButton != buttonPressed);
            }
        }
    }
}
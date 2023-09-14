using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Tools
{
    /// <summary>
    /// Struct used to hold information on which UI graphic component is needed to show a visual feedback when it's selected or not
    /// </summary>
    [Serializable]
    public struct GraphicRepresentation
    {
        public Graphic graphic;
        public Color selectedColor;
        public Color unselectedColor;
    }
}
using System;
using UnityEngine;

namespace Runtime.UI.Views
{
    public abstract class ViewScript : MonoBehaviour
    {
        private bool isReady;
        private GameObject viewGameObject;

        private void Init()
        {
            viewGameObject = gameObject;
        }
        
        public void Open()
        {
            if (!isReady)
            {
                Init();
            }

            viewGameObject.SetActive(true);
        }

        public void Close()
        {
            viewGameObject.SetActive(false);
        }
    }
}
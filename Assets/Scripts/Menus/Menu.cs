using InTheShadows.Managers;
using UnityEngine;

namespace InTheShadows.Menus
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        #region Singleton

            public static T Instance { get; private set; }

            protected virtual void Awake()
            {
                Instance = (T) this;
            }

            protected virtual void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        public override void OnBackButtonPressed()
        {
            MenuManager.Instance.CloseMenu();
        }
    }

    public abstract class Menu : MonoBehaviour
    {
        public abstract void OnBackButtonPressed();
    }
}
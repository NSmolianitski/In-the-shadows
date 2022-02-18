using System.Collections.Generic;
using InTheShadows.Menus;
using UnityEngine;

namespace InTheShadows.Managers
{
    public class MenuManager : MonoBehaviour
    {
        #region Singleton

            public static MenuManager Instance { get; private set; }

            private void Awake()
            {
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private IngameMenu ingameMenuPrefab;
        [SerializeField] private WinMenu winMenuPrefab;
        [SerializeField] private OptionsMenu optionsMenuPrefab;

        private readonly Stack<Menu> _menuStack = new Stack<Menu>();

        public void OpenMenu<T>() where T : Menu
        {
            var prefab = GetPrefab<T>();
            var instance = Instantiate(prefab, transform);

            if (_menuStack.Count > 0)
                _menuStack.Peek().gameObject.SetActive(false);

            _menuStack.Push(instance);
        }

        private T GetPrefab<T>() where T : Menu
        {
            if (typeof(T) == typeof(MainMenu))
                return mainMenuPrefab as T;
            else if (typeof(T) == typeof(IngameMenu))
                return ingameMenuPrefab as T;
            else if (typeof(T) == typeof(WinMenu))
                return winMenuPrefab as T;
            else if (typeof(T) == typeof(OptionsMenu))
                return optionsMenuPrefab as T;

            throw new MissingReferenceException("GetPrefab(): No such menu type.");
        }

        public void ClearMenuStack()
        {
            while (_menuStack.Count > 0)
                CloseMenu();
        }
        
        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
                return;
            
            var instance = _menuStack.Pop();
            Destroy(instance.gameObject);

            if (_menuStack.Count > 0)
                _menuStack.Peek().gameObject.SetActive(true);
        }
    }
}
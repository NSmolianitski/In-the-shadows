using InTheShadows.Menus;
using UnityEngine;

namespace InTheShadows.Managers
{
    public class WinManager : MonoBehaviour
    {
        #region Singleton

            public static WinManager Instance { get; private set; }

            private void Awake()
            {
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        [SerializeField] private WinChecker[] winCheckers;

        public bool IsMouseOverLightZone { get; set; } = false;
            
        private bool _winHandling = false;
        
        private void Update()
        {
            if (_winHandling)
                return;
            
            foreach (var checker in winCheckers)
            {
                if (!checker.IsInWinPosition)
                    return;
            }

            _winHandling = true;
            LaunchObjectsWinAnimation();
            HandleWin();
        }

        private void LaunchObjectsWinAnimation()
        {
            foreach (var checker in winCheckers)
            {
                checker.LaunchWinAnimation();
            }
        }
        
        private void HandleWin()
        {
            GameManager.Instance.DisableUserInput();
            MenuManager.Instance.OpenMenu<WinMenu>();
        }
    }
}
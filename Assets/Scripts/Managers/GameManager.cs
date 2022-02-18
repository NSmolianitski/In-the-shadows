using System;
using System.Linq;
using InTheShadows.Menus;
using InTheShadows.UserInput;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InTheShadows.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

            public static GameManager Instance { get; private set; }
            
            private void Awake()
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        public bool IsUserInputEnabled { get; private set; } = true;
        public bool IsNewLevelUnlocked { get; set; } = false;
        public LevelData CurrentLevelData { get; set; }
        public PlayerProfile PlayerProfile { get; private set; }
        public bool IsTestMode { get; set; } = false;

        public Action OptionsMenuOpenedEvent;
        public Action OptionsMenuClosedEvent;

        [SerializeField] private GameObject unclickableLayerPrefab;

        private GameObject _unclickableLayerInstance;
        
        private void Start()
        {
            PlayerPrefs.DeleteAll(); // TODO: delete on release (for development)
            AudioListener.volume = PlayerPrefs.GetFloat(Constants.MasterVolume, 0.2f);
            
            int currentResolutionIndex = PlayerPrefs.GetInt(Constants.ResolutionIndex, -1);
            Resolution resolution = Screen.resolutions.Last();
            if (currentResolutionIndex != -1 && currentResolutionIndex < Screen.resolutions.Length)
                resolution = Screen.resolutions[currentResolutionIndex];
            
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);

            SceneManager.sceneLoaded += OnSceneLoaded;

            MenuManager.Instance.OpenMenu<MainMenu>();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            MenuManager.Instance.ClearMenuStack();
            InputManager.UnlockCursor();
            EnableUserInput();
        }
        
        public void LoadMainProfile()
        {
            IsTestMode = false;
            PlayerProfile = new PlayerProfile("Player", 
                PlayerPrefs.GetInt(Constants.LastUnlockedLevel, 0));
        }
        
        public void LoadTestProfile()
        {
            IsTestMode = true;
            PlayerProfile = new PlayerProfile("TestMode", Int32.MaxValue);
        }
        
        private void Update()
        {
            if (IsUserInputEnabled)
                InputManager.ReadInput();

            if (SceneManager.GetActiveScene().buildIndex > 1 && InputManager.IsBackButtonPressed)
            {
                InputManager.IsBackButtonPressed = false;
                MenuManager.Instance.OpenMenu<IngameMenu>();
            }
        }

        public void EnableUserInput()
        {
            IsUserInputEnabled = true;
        }
        
        public void DisableUserInput()
        {
            IsUserInputEnabled = false;
        }

        public void SaveProgress()
        {
            if (IsTestMode)
                return;
            
            PlayerPrefs.SetInt(Constants.LastUnlockedLevel, PlayerProfile.LastUnlockedLevel);
            PlayerPrefs.Save();
        }

        public void EnableUnclickableLayer()
        {
            _unclickableLayerInstance = Instantiate(unclickableLayerPrefab);
        }

        public void DisableUnclickableLayer()
        {
            Destroy(_unclickableLayerInstance);
        }
    }
}
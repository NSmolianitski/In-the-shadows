using System.Collections;
using InTheShadows.UserInput;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InTheShadows.Managers
{
    public class LevelLoader : MonoBehaviour
    {
        #region Singleton

            public static LevelLoader Instance { get; private set; }

            private void Awake()
            {
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion
        
        [SerializeField] private Animator transitionAnimator;
        [SerializeField] private float transitionTime = 1f;
        
        public bool IsLoading { get; private set; } = false;
        
        private static readonly int StartTrigger = Animator.StringToHash("Start");
        private static readonly int EndTrigger = Animator.StringToHash("End");

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoadCrossfade;
            SceneManager.sceneLoaded += OnSceneLoadHandler;
            
            transitionAnimator.SetTrigger(EndTrigger); // For the first loading
        }

        private void OnSceneLoadCrossfade(Scene scene, LoadSceneMode loadSceneMode)
        {
            transitionAnimator.SetTrigger(EndTrigger);
        }
        
        private void OnSceneLoadHandler(Scene scene, LoadSceneMode loadSceneMode)
        {
            IsLoading = false;
        }

        public void LoadMainMenu()
        {
            Load(Constants.MainMenuSceneIndex);
        }

        public void LoadLevelSelection()
        {
            Load(Constants.LevelSelectionSceneIndex);
        }

        public void LoadLevel(int sceneIndex)
        {
            Load(sceneIndex);
        }
        
        private void Load(int sceneIndex)
        {
            IsLoading = true;
            InputManager.LockCursor();
            StartCoroutine(PlayCrossfadeAnimation(sceneIndex));
        }

        private IEnumerator PlayCrossfadeAnimation(int sceneIndex)
        {
            transitionAnimator.SetTrigger(StartTrigger);
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(sceneIndex);
            InputManager.UnlockCursor();
        }
    }
}
using System.Collections;
using InTheShadows.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InTheShadows.Menus
{
    public class WinMenu : Menu<WinMenu>
    {
        [Header("Components")]
        [SerializeField] private Button restartButton;
        [SerializeField] private Button levelSelectionButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Animator animator;
        
        [Header("Parameters")]
        [SerializeField] private float fadeInDelay = 1f; 
        
        private static readonly int FadeInStartTrigger = Animator.StringToHash("Start");

        private void Start()
        {
            canvas.worldCamera = MainCamera.Instance.UICamera;
            MainCamera.Instance.EnableUICamera();
        }

        private void OnEnable()
        {
            StartCoroutine(PlayFadeInAnimation());
            if (!GameManager.Instance.CurrentLevelData.IsSolved)
            {
                GameManager.Instance.IsNewLevelUnlocked = true;
                ++GameManager.Instance.PlayerProfile.LastUnlockedLevel;
                GameManager.Instance.CurrentLevelData.IsSolved = true;
                GameManager.Instance.SaveProgress();
            }
        }

        private IEnumerator PlayFadeInAnimation()
        {
            yield return new WaitForSeconds(fadeInDelay);
            animator.SetTrigger(FadeInStartTrigger);
        }

        public void OnRestartButtonClicked()
        {
            restartButton.interactable = false;
            LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        public void OnLevelSelectionButtonClicked()
        {
            levelSelectionButton.interactable = false;
            GameManager.Instance.EnableUserInput();
            LevelLoader.Instance.LoadLevelSelection();
        }
        
        public void OnMainMenuButtonClicked()
        {
            mainMenuButton.interactable = false;
            GameManager.Instance.IsNewLevelUnlocked = false;
            GameManager.Instance.EnableUserInput();
            LevelLoader.Instance.LoadMainMenu();
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
using System;
using InTheShadows.Managers;
using InTheShadows.UserInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InTheShadows.Menus
{
    public class IngameMenu : Menu<IngameMenu>
    {
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button levelSelectionButton;

        private Animator _animator;
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger(FadeIn);
            
            GameManager.Instance.DisableUserInput();
            InputManager.LockCursor();
        }

        public void OnRestartButtonClicked()
        {
            LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void OnResumeButtonClicked()
        {
            InputManager.LockCursor();
            _animator.SetTrigger(FadeOut);
        }
        
        public void EnableControls()
        {
            InputManager.UnlockCursor();
        }

        public void CloseIngameMenu()
        {
            GameManager.Instance.EnableUserInput();
            InputManager.UnlockCursor();
            MenuManager.Instance.CloseMenu();
        }
        
        public void OnOptionsButtonClicked()
        {
            MenuManager.Instance.OpenMenu<OptionsMenu>();
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
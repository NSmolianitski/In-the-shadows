using InTheShadows.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace InTheShadows.Menus
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button testModeButton;
        [SerializeField] private Button optionsButton;
        
        public override void OnBackButtonPressed()
        {
            Application.Quit();
        }

        public void OnPlayButtonCLicked()
        {
            GameManager.Instance.LoadMainProfile();
            playButton.interactable = false;
            GameManager.Instance.DisableUserInput();
            LevelLoader.Instance.LoadLevelSelection();
        }
        
        public void OnTestButtonCLicked()
        {
            GameManager.Instance.LoadTestProfile();
            testModeButton.interactable = false;
            GameManager.Instance.DisableUserInput();
            LevelLoader.Instance.LoadLevelSelection();
        }
                
        public void OnOptionsButtonCLicked()
        {
            MenuManager.Instance.OpenMenu<OptionsMenu>();
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
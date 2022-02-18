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
        [SerializeField] private Button deleteButton;
        [SerializeField] private GameObject confirmWindowPrefab;
        
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

        public void OnDeleteSaveButtonClicked()
        {
            var confirmWindow = Instantiate(confirmWindowPrefab);
            confirmWindow.GetComponent<ConfirmWindow>().Initialize("Are you sure you want to delete your saves?", 
                PlayerPrefs.DeleteAll,
                () => {}
                );
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
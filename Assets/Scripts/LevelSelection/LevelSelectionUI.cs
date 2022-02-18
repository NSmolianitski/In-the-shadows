using System;
using InTheShadows.Managers;
using InTheShadows.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace InTheShadows.LevelSelection
{
    public class LevelSelectionUI : MonoBehaviour
    {
        [SerializeField] private Button doorButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject map;

        private void Start()
        {
            GameManager.Instance.OptionsMenuOpenedEvent += DisableMap;
            GameManager.Instance.OptionsMenuClosedEvent += EnableMap;
        }

        private void DisableMap()
        {
            map.SetActive(false);
        }
        
        private void EnableMap()
        {
            map.SetActive(true);
        }
        
        public void OnDoorButtonClicked()
        {
            doorButton.interactable = false;
            LevelLoader.Instance.LoadMainMenu();
        }

        public void OnSettingsButtonClicked()
        {
            settingsButton.enabled = false;
            settingsButton.enabled = true;
            
            MenuManager.Instance.OpenMenu<OptionsMenu>();
        }

        private void OnDestroy()
        {
            GameManager.Instance.OptionsMenuOpenedEvent -= DisableMap;
            GameManager.Instance.OptionsMenuClosedEvent -= EnableMap;
        }
    }
}
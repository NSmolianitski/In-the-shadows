using System;
using System.Collections.Generic;
using System.Linq;
using InTheShadows.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InTheShadows.Menus
{
    public class OptionsMenu : Menu<OptionsMenu>
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeText;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        
        private float _currentVolume;
        private int _currentResolutionIndex;
        private Resolution _oldResolution;
        private Resolution[] _resolutions;

        private void OnEnable()
        {
            LoadSettings();
            GameManager.Instance.OptionsMenuOpenedEvent?.Invoke();
        }

        private void LoadSettings()
        {
            // _currentResolutionIndex = PlayerPrefs.GetInt(Constants.ResolutionIndex, -1);
            // if (_currentResolutionIndex == -1 || _currentResolutionIndex >= _resolutions.Length)
                // _currentResolutionIndex = _resolutions.Length - 1;
            // resolutionDropdown.value = _currentResolutionIndex;
            _resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            
            _currentResolutionIndex = 0;
            var options = new List<string>();
            for (var i = 0; i < _resolutions.Length; ++i)
            {
                if (Screen.currentResolution.Equals(_resolutions[i]))
                    _currentResolutionIndex = i;
                
                options.Add(_resolutions[i].width + " x " + _resolutions[i].height + " " 
                            + _resolutions[i].refreshRate + "hz");
            }
            
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = _currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
            _oldResolution = Screen.currentResolution;
            
            _currentVolume = AudioListener.volume;

            volumeSlider.value = _currentVolume;
            volumeText.text = $"Master volume: {(int) (_currentVolume * 100)}%";
        }

        private void OnDisable()
        {
            GameManager.Instance.OptionsMenuClosedEvent?.Invoke();
        }
        
        public void SetResolution(int resolutionIndex)
        {
            var resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
        }
        
        public void OnVolumeSliderValueChanged()
        {
            AudioListener.volume = volumeSlider.value;
            volumeText.text = $"Master volume: {(int) (volumeSlider.value * 100)}%";
        }
        
        public void OnConfirmButtonClicked()
        {
            SaveChanges();
            MenuManager.Instance.CloseMenu();
        }
        
        private void SaveChanges()
        {
            PlayerPrefs.SetFloat(Constants.MasterVolume, volumeSlider.value);
            PlayerPrefs.SetInt(Constants.ResolutionIndex, _currentResolutionIndex);
        }

        public override void OnBackButtonPressed()
        {
            ResetChanges();
            MenuManager.Instance.CloseMenu();
        }

        private void ResetChanges()
        {
            AudioListener.volume = _currentVolume;
            Screen.SetResolution(_oldResolution.width, _oldResolution.height, FullScreenMode.FullScreenWindow);
        }
    }
}
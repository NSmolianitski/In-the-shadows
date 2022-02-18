using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace InTheShadows.Menus
{
    public class ConfirmWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        
        public void Initialize(string text, Action yesAction, Action noAction)
        {
            textField.text = text;
            yesButton.onClick.AddListener(() =>
            {
                CloseWindow();
                yesAction();
            });
            noButton.onClick.AddListener(() =>
            {
                CloseWindow();
                noAction();
            });
        }

        private void CloseWindow()
        {
            Destroy(gameObject);
        }
    }
}
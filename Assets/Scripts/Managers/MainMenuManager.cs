using InTheShadows.Menus;
using UnityEngine;

namespace InTheShadows.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameManagerPrefab;
        
        private void Start()
        {
            if (GameManager.Instance == null)
                Instantiate(gameManagerPrefab);
            else
                MenuManager.Instance.OpenMenu<MainMenu>();
        }
    }
}
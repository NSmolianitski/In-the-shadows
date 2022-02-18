using InTheShadows.Managers;
using UnityEngine;

namespace InTheShadows.LevelSelection
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private float unlockAnimationDelay = 0.5f;
        
        private void Start()
        {
            ShowUnlockedLevels();
        }

        private void ShowUnlockedLevels()
        {
            int lastUnlockedLevel = GameManager.Instance.PlayerProfile.LastUnlockedLevel;
            if (lastUnlockedLevel >= levels.Length)
                lastUnlockedLevel = levels.Length;

            for (int i = 0; i < lastUnlockedLevel; ++i)
                ShowSolvedLevel(i);

            if (lastUnlockedLevel != levels.Length)
            {
                if (GameManager.Instance.IsNewLevelUnlocked)
                    Invoke(nameof(ShowNewUnlockedLevel), unlockAnimationDelay);
                else
                    ShowUnsolvedLevel(lastUnlockedLevel);
            }
        }

        private void ShowSolvedLevel(int levelIndex)
        {
            levels[levelIndex].Show();
            levels[levelIndex].SetSolvedColor();
        }

        private void ShowUnsolvedLevel(int levelIndex)
        {
            levels[levelIndex].Show();
            levels[levelIndex].SetDefaultColor();
        }

        private void ShowNewUnlockedLevel()
        {
            int lastUnlockedLevel = GameManager.Instance.PlayerProfile.LastUnlockedLevel;
            
            if (lastUnlockedLevel >= levels.Length)
                return;
            
            var unlockedLevel = levels[lastUnlockedLevel];
            unlockedLevel.Unlock();
            GameManager.Instance.IsNewLevelUnlocked = false;
        }
    }
}
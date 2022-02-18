using System.Collections;
using InTheShadows.Managers;
using TMPro;
using UnityEngine;

namespace InTheShadows.LevelSelection
{
    public class Level : MonoBehaviour
    {
        [Header("Parameters")] 
        [SerializeField] private Transform movableSprite;
        [SerializeField] private int levelIndex = 0;
        [SerializeField] private GameObject unlockEffect;
        [SerializeField] private float hoveredHeight = 2f;
        [SerializeField] private bool isLocked = true;
        [SerializeField] private float riseSpeed = 200f;
        [SerializeField] private TextMeshProUGUI levelTopText;

        [Header("Colors")]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hoveredColor;
        [SerializeField] private Color solvedColor;

        private Color _savedColor;
        private Vector3 _hoveredVector;
        private Vector3 _defaultPosition;
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _hoveredVector = new Vector3(0f, hoveredHeight, 0f);
            _defaultPosition = movableSprite.position;
            _spriteRenderer = movableSprite.GetComponent<SpriteRenderer>();
        }

        public void SetDefaultColor()
        {
            _spriteRenderer.color = defaultColor;
        }

        public void SetHoveredColor()
        {
            _spriteRenderer.color = hoveredColor;
        }
        
        public void SetSolvedColor()
        {
            _spriteRenderer.color = solvedColor;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Unlock()
        {
            Show();
            isLocked = false;
            SetDefaultColor();
            PlayUnlockAnimation();
        }

        private void PlayUnlockAnimation()
        {
            SoundManager.Instance.PlayUnlockSound();
            Instantiate(unlockEffect, movableSprite);
        }

        private void OnMouseEnter()
        {
            if (isLocked || LevelLoader.Instance.IsLoading)
                return;

            _savedColor = _spriteRenderer.color;
            SetHoveredColor();
            
            StopAllCoroutines();
            StartCoroutine(SmoothMove(_defaultPosition + _hoveredVector));
        }

        private void OnMouseDown()
        {
            if (LevelLoader.Instance.IsLoading)
                return;
            
            bool isSolved = GameManager.Instance.PlayerProfile.LastUnlockedLevel 
                            > (levelIndex - Constants.FirstLevelBuildIndex);
            GameManager.Instance.CurrentLevelData = new LevelData(levelIndex, isSolved);
            LevelLoader.Instance.LoadLevel(levelIndex);
        }

        public void OnMouseExit()
        {
            if (isLocked || LevelLoader.Instance.IsLoading)
                return;

            StopAllCoroutines();
            StartCoroutine(SmoothMove(_defaultPosition));
            _spriteRenderer.color = _savedColor;
        }

        private IEnumerator SmoothMove(Vector3 target)
        {
            Vector3 velocity = Vector3.zero;
            
            while (Vector3.Distance(movableSprite.position, target) > 0.001f)
            {
                movableSprite.position = Vector3.SmoothDamp(movableSprite.position, target, ref velocity,
                    Time.deltaTime * riseSpeed);
                yield return null;
            }

            movableSprite.position = target;
        }
    }
}
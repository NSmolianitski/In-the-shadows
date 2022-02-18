using UnityEngine;

namespace InTheShadows
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton

            public static SoundManager Instance { get; private set; }

            private void Awake()
            {
                Instance = this;
                _audioSource = GetComponent<AudioSource>();
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion
        
        [SerializeField] private AudioClip levelUnlockSound;
        
        private AudioSource _audioSource;

        public void PlayUnlockSound()
        {
            _audioSource.clip = levelUnlockSound;
            _audioSource.Play();
        }
    }
}
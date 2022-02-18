using UnityEngine;

namespace InTheShadows
{
    public class FireworkSound : MonoBehaviour
    {
        private AudioSource _audioSource;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_particleSystem.time <= 0.02f)
                _audioSource.Play();
        }
    }
}

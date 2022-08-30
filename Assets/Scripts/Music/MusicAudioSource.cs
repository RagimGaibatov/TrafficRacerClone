using UnityEngine;
using Random = System.Random;

namespace Music{
    public class MusicAudioSource : MonoBehaviour{
        private AudioSource _audioSource;

        [SerializeField] private AudioClip[] _audioClips;

        private AudioClip[] _newAudioClips;

        private int _currentIndexClip;


        private void Awake(){
            _audioSource = GetComponent<AudioSource>();
            ShuffleClips();
            PlayAudioClip();
        }

        private void Update(){
            if (!_audioSource.isPlaying){
                PlayAudioClip();
            }
        }

        void ShuffleClips(){
            Random random = new Random();
            int countOfarray = _audioClips.Length;
            while (countOfarray > 1){
                int r = random.Next(countOfarray--);
                (_audioClips[countOfarray], _audioClips[r]) = (_audioClips[r], _audioClips[countOfarray]);
            }
        }

        void PlayAudioClip(){
            _audioSource.PlayOneShot(_audioClips[_currentIndexClip]);
            _currentIndexClip++;
            if (_currentIndexClip >= _audioClips.Length){
                _currentIndexClip = 0;
            }
        }
    }
}
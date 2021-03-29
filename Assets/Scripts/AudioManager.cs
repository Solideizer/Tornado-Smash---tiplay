using UnityEngine;

namespace Managers
{
    [RequireComponent (typeof (AudioSource))]
    public class AudioManager : MonoBehaviour
    {

        #region Variable Declarations
        private static AudioClip _collectSound;
        private static AudioSource _audioSource;
        #endregion
        private void Start ()
        {
            LoadAudioFiles ();
            _audioSource = GetComponent<AudioSource> ();
        }

        private static void LoadAudioFiles ()
        {
            _collectSound = Resources.Load<AudioClip> ("click");
        }

        public static void PlaySound (string clip)
        {
            switch (clip)
            {
                case "collect":
                    _audioSource.PlayOneShot (_collectSound);
                    break;

            }
        }
    }
}
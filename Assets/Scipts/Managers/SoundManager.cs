using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BilliardDemo {
    [RequireComponent (typeof (AudioSource))]
    public class SoundManager : MonoBehaviour {
        public static SoundManager Instance = null;
        public AudioSource audioSource;
        public AudioClip eachOtherC, wallC, firstShot;

        private void Awake () {
            if (Instance == null)
                Instance = this;
        }
        
        public void PlayEachOtherCollideSound () {
            audioSource.PlayOneShot (eachOtherC);
        }

        public void PlayWallCollideSound () {
            audioSource.PlayOneShot (wallC);
        }

        public void PlayFirstShotSound () {
            audioSource.PlayOneShot (firstShot);
        }

    }
}
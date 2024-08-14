using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player.Components
{
    public class FirstPersonAudio : NetworkBehaviour
    {
        public FirstPersonMovement character;
        public GroundCheck groundCheck;

        [Header("Step")]
        public AudioSource stepAudio;
        public AudioSource runningAudio;
        [Tooltip("Minimum velocity for moving audio to play")]
        public float velocityThreshold = .01f;

        private Vector2 _lastCharacterPosition;
        private Vector2 CurrentCharacterPosition => new(character.transform.position.x, character.transform.position.z);

        [Header("Landing")]
        public AudioSource landingAudio;
        public AudioClip[] landingSFX;

        [Header("Jump")]
        public Jump jump;
        public AudioSource jumpAudio;
        public AudioClip[] jumpSFX;

        private IEnumerable<AudioSource> MovingAudios => new[] { stepAudio, runningAudio };

        private Hook _hook;
        
        
        private void Start()
        {
            _hook = gameObject.transform.parent.GetComponent<Hook>();
        }

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeToEvents();

        private void FixedUpdate()
        {
            // Play moving audio if the character is moving and on the ground.
            var velocity = Vector3.Distance(CurrentCharacterPosition, _lastCharacterPosition);
            if (velocity >= velocityThreshold && groundCheck.isGrounded && !_hook.IsHooking)
            {
                // if (character.isRunning)
                //     SetPlayingMovingAudio(runningAudio);
                //
                // else
                SetPlayingMovingAudio(runningAudio); //stepAudio
            }
            else
                SetPlayingMovingAudio(null);

            // Remember lastCharacterPosition.
            _lastCharacterPosition = CurrentCharacterPosition;
        }


        /// <summary>
        /// Pause all MovingAudios and enforce play on audioToPlay.
        /// </summary>
        /// <param name="audioToPlay">Audio that should be playing.</param>
        private void SetPlayingMovingAudio(AudioSource audioToPlay)
        {
            // Pause all MovingAudios.
            foreach (var audioSource in MovingAudios.Where(audioSource => audioSource != audioToPlay && audioSource != null))
                audioSource.Pause();

            // Play audioToPlay if it was not playing.
            if (audioToPlay && !audioToPlay.isPlaying)
                audioToPlay.Play();
        }

        #region Play instant-related audios.

        private void PlayLandingAudio() => PlayRandomClip(landingAudio, landingSFX);
        private void PlayJumpAudio() => PlayRandomClip(jumpAudio, jumpSFX);
        #endregion

        #region Subscribe/unsubscribe to events.

        private void SubscribeToEvents()
        {
            // PlayLandingAudio when Grounded.
            groundCheck.Grounded += PlayLandingAudio;

            // PlayJumpAudio when Jumped.
            if (jump)
                jump.Jumped += PlayJumpAudio;
        }

        private void UnsubscribeToEvents()
        {
            // Undo PlayLandingAudio when Grounded.
            groundCheck.Grounded -= PlayLandingAudio;

            // Undo PlayJumpAudio when Jumped.
            if (jump)
                jump.Jumped -= PlayJumpAudio;
        }
        #endregion

        #region Utility.

        private static void PlayRandomClip(AudioSource audio, IReadOnlyList<AudioClip> clips)
        {
            if (!audio || clips.Count <= 0)
                return;

            // Get a random clip. If possible, make sure that it's not the same as the clip that is already on the audiosource.
            var clip = clips[Random.Range(0, clips.Count)];
            if (clips.Count > 1)
                while (clip == audio.clip)
                    clip = clips[Random.Range(0, clips.Count)];

            // Play the clip.
            audio.clip = clip;
            audio.Play();
        }
        #endregion 
    }
}

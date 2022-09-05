using Game.Character.Addons;
using UnityEngine;

namespace Game.Character
{
    public class Footsteps : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private AudioClips commonFootsteps;
        [SerializeField] private AudioClips terrainFootsteps;

        [Header("references")]
        [SerializeField] private new AudioSource audio;
        [SerializeField] private CharacterMotion motion;
        [SerializeField] private GroundDetection ground;

        [Header("Parameters")]
        [SerializeField] private float movementMagnitudeThreshold = 0.1f;
        [SerializeField] private float walkFrequency = 0.45f;
        [SerializeField, Range(0f, 1f)] private float walkLoudness = 0.25f;

        private LayerMask _noiseNotifyLayerMask;
        private float _lastInvokeTime;

        private void Update()
        {
            if (!ground.IsGrounded)
                return;

            if (Time.time < _lastInvokeTime + walkFrequency)
                return;

            _lastInvokeTime = Time.time;
            OnFootstep();
        }

        private void OnFootstep()
        {
            if (motion.LocalVelocity.magnitude < movementMagnitudeThreshold)
                return;
        
            var c = ground.Hit.collider;
            // ReSharper disable once InvertIf
            if (c)
            {
                audio.volume = walkLoudness;
                audio.PlayOneShot(c is TerrainCollider ? terrainFootsteps.GetRandom() : commonFootsteps.GetRandom());
            
                // todo notify surroundings about the noise
            }
        }
    }
}
using System;
using Game.Character.Addons;
using UnityEngine;

namespace Game.Character
{
    public class FootstepSource : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private AudioClips commonFootsteps;
        [SerializeField] private AudioClips terrainFootsteps;

        [Header("References")]
        [SerializeField] private new AudioSource audio;
        [SerializeField] private CharacterMotion motion;

        [Header("Tweaks")]
        [SerializeField] private float movementMagnitudeThreshold = 0.1f;
        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (motion.LocalVelocity.magnitude < movementMagnitudeThreshold)
                return;

            audio.volume = Mathf.InverseLerp(minSpeed, maxSpeed, motion.LocalVelocity.magnitude);
            audio.PlayOneShot(other is TerrainCollider ? terrainFootsteps.GetRandom() : commonFootsteps.GetRandom());
            
            // todo notify surroundings about the noise
        }
    }
}
using UnityEngine;

namespace Game.Character.Addons
{
    [CreateAssetMenu(fileName = "AudioClips", menuName = "Game/Audio Clips", order = 0)]
    public class AudioClips : ScriptableObject
    {
        public AudioClip[] clips;
        
        public AudioClip GetRandom() => clips[Random.Range(0, clips.Length)];
    }
}
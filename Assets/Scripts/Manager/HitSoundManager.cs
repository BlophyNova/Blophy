using System.Collections;
using Controller;
using Data.ChartData;
using UnityEngine;
using UtilityCode.ObjectPool;
using UtilityCode.Singleton;
namespace Manager
{
    public class HitSoundManager : MonoBehaviourSingleton<HitSoundManager>
    {
        public ObjectPoolQueue<HitSoundController> hitSounds;
        public AudioClip[] sounds;
        protected override void OnAwake()
        {
            hitSounds = new(AssetManager.Instance.hitSoundController, 0, transform);
        }
        public void PlayHitSound(NoteType noteType)
        {
            StartCoroutine(Play(noteType));
        }
        public IEnumerator Play(NoteType noteType)
        {
            HitSoundController hitSoundController = hitSounds.PrepareObject().SetClip(sounds[(int)noteType]).Play();
            yield return new WaitForSeconds(.5f);
            hitSounds.ReturnObject(hitSoundController);
        }
    }
}

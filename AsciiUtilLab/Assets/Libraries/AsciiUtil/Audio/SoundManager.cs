using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

namespace AsciiUtil
{
    [System.Serializable]
    public class SEInfo
    {
        public string name;
        public AudioClip clip;
        public float volume;
        public List<AudioSource> audioSources;
        public AudioSource FindOrAddAudioSource(Transform transform)
        {
            //音が鳴っていないAudioSourceがあればそれを返す
            AudioSource value = null;
            if (audioSources.Count > 0)
            {
                value = audioSources.FirstOrDefault(x => !x.isPlaying);
            }
            if (value != null) return value;

            //なければ新しく作って返す
            var audioSourceObject = new GameObject(($"{name}AudioSource"));
            audioSourceObject.transform.SetParent(transform);
            value = audioSourceObject.AddComponent<AudioSource>();
            audioSources.Add(value);
            return value;
        }
    }
    [System.Serializable]
    public class BGMInfo
    {
        public string name;
        public AudioClip clip;
        public float volume;
        public List<AudioSource> audioSources = new List<AudioSource>();

        public AudioSource FindOrAddAudioSource(Transform transform)
        {
            //音が鳴っていないAudioSourceがあればそれを返す
            AudioSource value = null;
            if (audioSources.Count > 0)
            {
                value = audioSources.FirstOrDefault(x => !x.isPlaying);
            }
            if (value != null) return value;

            //なければ新しく作って返す
            var audioSourceObject = new GameObject("BGMAudioSource");
            audioSourceObject.transform.SetParent(transform);
            value = audioSourceObject.AddComponent<AudioSource>();
            audioSources.Add(value);
            return value;
        }
    }
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [SerializeField]
        private SEInfo[] seInfo;
        public IReadOnlyList<SEInfo> SEInfo => seInfo;
        [SerializeField]
        private BGMInfo[] bgmInfo;
        public IReadOnlyList<BGMInfo> BGMInfo => bgmInfo;
        [SerializeField]
        private FloatReactiveProperty masterVolumeMultiply = new FloatReactiveProperty();

        [SerializeField]
        private AudioSource currentPlayBGMSource;
        [SerializeField]
        private AudioSource previousPlayBGMSource;

        private void Start()
        {
            //マスターボリュームの変更を監視して、それぞれの音量を変更する
            masterVolumeMultiply.Subscribe(x =>
            {
                ChangeVolume(x);
            });
        }

        /// <summary>
        /// BGMを再生します
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="pitchMin"></param>
        /// <param name="pitchMax"></param>
        /// <param name="volume"></param>
        /// <param name="isFeedIn"></param>
        /// <param name="feedDuration"></param> <summary>
        public SoundManager PlayBGM(string clipName, float volume = 1, int priority = 0)
        {
            //BGMクリップを検索し、なければエラーを出して終了
            var info = FindBGMClip(clipName);
            if (info == null) return null;

            //BGM用のAudioSourceを用意
            AudioSource bgmSource = info.FindOrAddAudioSource(transform);
            //AudioSourceの設定
            bgmSource.clip = info.clip;
            bgmSource.priority = priority;
            bgmSource.volume = masterVolumeMultiply.Value * info.volume * volume;
            currentPlayBGMSource = bgmSource;
            currentPlayBGMSource.Play();
            return this;
        }

        public SoundManager ChangePitch(float pitch)
        {
            if (currentPlayBGMSource == null) return null;
            currentPlayBGMSource.pitch = pitch;
            return this;
        }

        public SoundManager ChangePitchRamdom(float pitchMin, float pitchMax)
        {
            if (currentPlayBGMSource == null) return null;
            currentPlayBGMSource.pitch = Random.Range(pitchMin, pitchMax);
            return this;
        }

        public SoundManager SetFadeIn(float duration)
        {
            float targetVolume = currentPlayBGMSource.volume;
            currentPlayBGMSource.volume = 0;
            currentPlayBGMSource.DOFade(targetVolume, duration);
            return this;
        }

        public SoundManager SetFadeOut(float duration, UnityAction unityAction = null)
        {
            ExcuteBeforeBGMEnd(() =>
            {
                currentPlayBGMSource.DOFade(0, duration)
                .OnComplete(() => StopBGM(true));
                unityAction?.Invoke();
            }, duration);
            return this;
        }

        public SoundManager SetExcuteBeforeBGMEnd(UnityAction action, float beforeSec)
        {
            // BGMが終了したときのコールバック設定
            ExcuteBeforeBGMEnd(action, beforeSec);
            return this;
        }

        public SoundManager OnCompleted(UnityAction action)
        {
            // BGMが終了したときのコールバック設定
            PlayingObserbable(action);
            return this;
        }

        public void StopBGM(bool isFeedOut = false)
        {
            //フェードアウトが有効ならフェードアウトして終了
            if (isFeedOut)
            {
                previousPlayBGMSource.Stop();
                previousPlayBGMSource = currentPlayBGMSource;
                return;
            }

            //フェードアウトが無効ならそのまま終了
            currentPlayBGMSource.Stop();
        }
        
        /// <summary>
        /// SEを再生します
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="pitchMin"></param>
        /// <param name="pitchMax"></param>
        /// <param name="volume"></param> <summary>
        public void PlaySE(string clipName, float pitchMin = 1, float pitchMax = 1, float volume = 1)
        {
            var info = FindSEClip(clipName);
            if (info == null) return;
           
            var currentPlaySource = info.audioSources.FirstOrDefault(x => !x.isPlaying);
            if (currentPlaySource == null) return;

            currentPlaySource.pitch = Random.Range(pitchMin, pitchMax);
            currentPlaySource.volume = masterVolumeMultiply.Value * info.volume * volume;
            currentPlaySource.PlayOneShot(info.clip);
        }

        public void SetMasterVolumeMultiply(float value)
        {
            masterVolumeMultiply.Value = value;
        }

        private void ChangeVolume(float masterVolume)
        {
            foreach (var info in seInfo)
            {
                foreach (var source in info.audioSources)
                {
                    if (source == null) continue;
                    source.volume = masterVolume;
                }
            }

            if (currentPlayBGMSource == null) return;
            currentPlayBGMSource.volume = masterVolume;
        }

        private System.IDisposable playingDisposable;
        private void PlayingObserbable(UnityAction action, float beforeSec = 0)
        {
            playingDisposable = Observable.EveryUpdate()
            .Where(_ => !currentPlayBGMSource.isPlaying)
            .Subscribe(_ =>
            {
                action.Invoke();
                playingDisposable.Dispose();
            });
        }
        private System.IDisposable excuteBeforeBGMEndDisposable;
        private void ExcuteBeforeBGMEnd(UnityAction action, float beforeSec)
        {
            float currentTime = 0;
            float bgmTime = currentPlayBGMSource.clip.length;
            bool isExcute = false;
            excuteBeforeBGMEndDisposable = Observable.EveryUpdate()
            .Where(_ => currentPlayBGMSource.isPlaying)
            .Subscribe(_ =>
            {
                currentTime += Time.deltaTime;
                if (currentTime < bgmTime - beforeSec) return;
                if (isExcute) return;
                action.Invoke();
                isExcute = true;
                excuteBeforeBGMEndDisposable.Dispose();
            });
        }

        private SEInfo FindSEClip(string clipName)
        {
            var value = seInfo.FirstOrDefault(x => x.name == clipName);
            if (value != null) return value;

            AsciiDebug.LogError(clipName, "のクリップが見つからないよ");
            return null;
        }

        private BGMInfo FindBGMClip(string clipName)
        {
            var value = bgmInfo.FirstOrDefault(x => x.name == clipName);
            if (value != null) return value;

            AsciiDebug.LogError(clipName, "のクリップが見つからないよ");
            return null;
        }
    }
}

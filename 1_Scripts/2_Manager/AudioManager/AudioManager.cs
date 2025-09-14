/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 27.
 *  Information :   오디오 및 사운드 메니져
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : BaseManager
    {
        #region Data Field
        /// <summary> 배경음악 오디오 </summary>
        private AudioSource _backgroundMusic = default;
        #endregion

        #region Get / Set / Is
        #endregion

        #region Public API
        /// <summary>
        /// 배경음악 셋팅
        /// </summary>
        /// <param name="audioClipBGM"> 셋팅하려는 배경음악 오디오 클립 </param>
        /// <param name="volume"> 볼륨 크기 </param>
        public void SetupBackgroundMusic(AudioClip audioClipBGM, float volume)
        {
            _backgroundMusic.clip = audioClipBGM;
            _backgroundMusic.volume = volume;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }

        /// <summary>
        /// 배경음악 끄기
        /// </summary>
        public void MuteBackgroundMusic()
        {
            _backgroundMusic.Stop();
        }
        #endregion

        #region Initialize
        protected override void Allocate() // 생성
        {
            AddAudioSource();
        }
        #endregion

        #region Main
        #endregion

        #region 내부 함수
        /// <summary>
        /// Add AudioSource
        /// </summary>
        private void AddAudioSource()
        {
            gameObject.AddComponent<AudioListener>();
            _backgroundMusic = gameObject.AddComponent<AudioSource>();
        }
        #endregion
    }
}
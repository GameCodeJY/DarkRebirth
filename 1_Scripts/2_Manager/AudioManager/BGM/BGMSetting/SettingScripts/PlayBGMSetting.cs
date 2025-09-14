/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 28.
 *  Information :   BGM 플레이
 */

namespace MainSystem
{
    using UnityEngine;

    // "재생" 부품. Assets/Create/BGM/Play Setting 메뉴로 생성
    [CreateAssetMenu(fileName = "PlayBGM", menuName = "BGM/Play Setting")]
    public class PlayBGMSetting : BGMSetting
    {
        #region Data Field
        [SerializeField] private AudioClip _backgroundMusicClip;
        [Range(0f, 1f)]
        [SerializeField] private float _backgroundVolume = 0.5f;
        #endregion

        #region Get / Set / Is
        #endregion

        #region Public API
        public override void Apply(AudioManager audioManager)
        {
            audioManager.SetupBackgroundMusic(_backgroundMusicClip, _backgroundVolume);
        }
        #endregion

        #region Initialize
        #endregion

        #region Main
        #endregion

        #region 내부 함수
        #endregion
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 28.
 *  Information :   배경음악 음소거
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // "음소거" 부품. Assets/Create/BGM/Mute Setting 메뉴로 생성
    [CreateAssetMenu(fileName = "MuteBGM", menuName = "BGM/Mute Setting")]
    public class MuteBGMSetting : BGMSetting
    {
        #region Data Field
        #endregion

        #region Get / Set / Is
        #endregion

        #region Public API
        public override void Apply(AudioManager audioManager)
        {
            audioManager.MuteBackgroundMusic();
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
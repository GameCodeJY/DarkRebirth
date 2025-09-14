/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 28.
 *  Information :   BGM Setting Scripable Object
 */

namespace MainSystem
{
    using UnityEngine;

    public abstract class BGMSetting : ScriptableObject
    {
        #region Data Field
        #endregion

        #region Get / Set / Is
        #endregion

        #region Public API
        // 이 설정을 오디오 매니저에 적용하는 기능 (구체적인 내용은 부품마다 다름)
        public abstract void Apply(AudioManager audioManager);
        #endregion

        #region Initialize
        #endregion

        #region Main
        #endregion

        #region 내부 함수
        #endregion
    }
}
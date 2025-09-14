/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 28.
 *  Information :   배경음악 컨트롤러
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BGMController : MonoBehaviour
    {
        #region Data Field
        [Header("연결할 BGM 설정 Scriptable Object")]
        [Tooltip("프로젝트에 생성된 BGMSetting Scriptable Object를 여기에 연결하세요.")]
        [SerializeField] private BGMSetting _bgmSetting; // 부품을 꽂는 슬롯
        #endregion

        #region Get / Set / Is
        #endregion

        #region Public API
        #endregion

        #region Initialize
        private void Initialize()
        {
            // 슬롯에 부품이 꽂혀있으면, 해당 부품의 기능을 실행!
            if (_bgmSetting != null)
            {
                _bgmSetting.Apply(MainSystem.Instance.AudioManager);
            }
        }
        #endregion

        #region Main
        private void Start()
        {
            Initialize();
        }
        #endregion

        #region 내부 함수
        #endregion
    }
}
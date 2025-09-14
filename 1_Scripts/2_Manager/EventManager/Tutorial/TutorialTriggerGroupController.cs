/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   튜토리얼에서 사용하는 트리거 그룹 컨롤러
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class TutorialTriggerGroupController : MonoBehaviour // Data Field
    {
        /// <summary> 튜토리얼 트리거 리스트 </summary>
        [SerializeField] private List<GameObject> ListGameObjectTutorialTrigger = default;
        /// <summary> 현재 튜토리얼 트리거 번호 </summary>
        private int NumTutorialTrigger = 0;
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialTriggerGroupController : MonoBehaviour // Tutorial Event
    {
        /// <summary>
        /// 튜토리얼 트리거 바꾸기
        /// </summary>
        public void ChangeTutorialTrigger()
        {
            ListGameObjectTutorialTrigger[NumTutorialTrigger].SetActive(false); // 이전 트리거 비활성화
            NumTutorialTrigger += 1; // 다음 트리거 번호로 바꾸기
            ListGameObjectTutorialTrigger[NumTutorialTrigger].SetActive(true); // 다음 트리거 활성화
        }
    }
}
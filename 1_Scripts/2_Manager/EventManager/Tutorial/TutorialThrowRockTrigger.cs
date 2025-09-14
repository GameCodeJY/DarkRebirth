/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Tutorial Throw Rock Trigger
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
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Data Field
    {
        #region Const Value
        /// <summary> 바위 던지기 시작 멘트 </summary>
        private const string TEXT_TUTORIAL_THROW_ROCK_EVENT = "바위를 피하려면 \'A\'와 \'D\'를 눌러보세요.";
        #endregion

        /// <summary> 튜토리얼 바위 그룹 컨트롤러 </summary>
        [SerializeField] private TutorialRockGroupController TutorialRockGroupController = default;
        /// <summary> 튜토리얼 설명 Text </summary>
        [SerializeField] private Text TextTutorialExplanation = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Main
    {
        private void Awake()
        {
            UnityEventTriggerEnter.AddListener(StartThrowRock);
        }
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Tutorial Event
    {
        /// <summary>
        /// 바위 던지기 시작
        /// </summary>
        private void StartThrowRock()
        {
            TextTutorialExplanation.text = TEXT_TUTORIAL_THROW_ROCK_EVENT;
            TutorialRockGroupController.StartDropRocks(); //바위 그룹 컨트롤러에게 바위 떨어지게 명령
        }
    }
}
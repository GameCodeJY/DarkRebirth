/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 30.
 *  Information :   게임 내 진행사항 관리
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public partial class ProcessManager : BaseManager
    {
        /// <summary> 상호작용 완료 시 이벤트 </summary>
        private Action _completeInteractionEvent = default;

        #region Property
        // TODO: 이건 무조건 고쳐야 하는 코드
        public BaseStatueTrigger baseStatueTrigger { get; set; }
        public Action CompleteInteractionEvent { private get => _completeInteractionEvent; set => _completeInteractionEvent = value; }
        #endregion
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public partial class ProcessManager : BaseManager // Initialize
    {
        public void SignUpPlayerSpawnController(PlayerSpawnPositionGroupController playerSpawnPositionGroupController)
        {
            MainSystem.Instance.FieldManager.InitializeFieldNumber();

            SpawnPlayer();
        }
    }

    /// <summary>
    /// 실행 및 하위 객체의 종료 알람
    /// </summary>
    public partial class ProcessManager : BaseManager // excute
    {
        /// <summary>
        /// 상호작용 완료 이벤트 실행
        /// </summary>
        public void ExcuteCompleteInteractionEvent()
        {
            _completeInteractionEvent.Invoke();
            _completeInteractionEvent = null;
        }

        /// <summary>
        /// 다음 필드로 변경
        /// </summary>
        public void ChangeToNextField()
        {
            // 1. 필드 바꾸기
            FieldController nextFieldController = MainSystem.Instance.FieldManager.ChangeField();

            // 2. 다음 필드 초기 이벤트 실행
            nextFieldController.FieldInitialEvent.Invoke();
        }
    }

    /// <summary>
    /// 듀토리얼
    /// </summary>
    public partial class ProcessManager : BaseManager // 듀토리얼
    {
        public bool GetFlagPlayMoveTutorial()
        {
            return _isPlayMoveTutorial;
        }

        public void EndMoveTutorial()
        {
            _isPlayMoveTutorial = true;
            MainSystem.Instance.SceneManager.SceneLoadAsync("3_Home");
        }
    }
}


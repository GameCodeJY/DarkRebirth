/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 30.
 *  Information :   ���� �� ������� ����
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
        /// <summary> ��ȣ�ۿ� �Ϸ� �� �̺�Ʈ </summary>
        private Action _completeInteractionEvent = default;

        #region Property
        // TODO: �̰� ������ ���ľ� �ϴ� �ڵ�
        public BaseStatueTrigger baseStatueTrigger { get; set; }
        public Action CompleteInteractionEvent { private get => _completeInteractionEvent; set => _completeInteractionEvent = value; }
        #endregion
    }

    /// <summary>
    /// �ʱ�ȭ
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
    /// ���� �� ���� ��ü�� ���� �˶�
    /// </summary>
    public partial class ProcessManager : BaseManager // excute
    {
        /// <summary>
        /// ��ȣ�ۿ� �Ϸ� �̺�Ʈ ����
        /// </summary>
        public void ExcuteCompleteInteractionEvent()
        {
            _completeInteractionEvent.Invoke();
            _completeInteractionEvent = null;
        }

        /// <summary>
        /// ���� �ʵ�� ����
        /// </summary>
        public void ChangeToNextField()
        {
            // 1. �ʵ� �ٲٱ�
            FieldController nextFieldController = MainSystem.Instance.FieldManager.ChangeField();

            // 2. ���� �ʵ� �ʱ� �̺�Ʈ ����
            nextFieldController.FieldInitialEvent.Invoke();
        }
    }

    /// <summary>
    /// ���丮��
    /// </summary>
    public partial class ProcessManager : BaseManager // ���丮��
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


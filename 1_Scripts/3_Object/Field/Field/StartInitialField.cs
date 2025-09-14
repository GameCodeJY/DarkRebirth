/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 02.
 *  Information :   Register Initial Field Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class StartInitialField : MonoBehaviour // Data Field
    {
        /// <summary> Field Controller </summary>
        [SerializeField] private FieldController _newFieldController = default;
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class StartInitialField : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
        }

        public void Initialize()
        {
            Allocate();
            RegisterInitialFieldControllerToManagers();
            _newFieldController.FieldInitialEvent?.Invoke(); // Field Controller�� �ʱ� �̺�Ʈ ����

            MainSystem.Instance.PlayerManager.MainPlayer.gameObject.SetActive(true); // �÷��̾� Ȱ��ȭ
            MainSystem.Instance.PlayerManager.MainPlayer.ResetPlayer();
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class StartInitialField : MonoBehaviour // Main
    {
        private void Start()
        {
            Initialize();
        }
    }

    /// <summary>
    /// Register
    /// </summary>
    public partial class StartInitialField : MonoBehaviour // Register
    {
        /// <summary>
        /// �ʵ� �޴����� ���� FieldController ���
        /// </summary>
        private void RegisterInitialFieldControllerToManagers()
        {
            MainSystem.Instance.FieldManager.SelectedFieldController = _newFieldController;
            MainSystem.Instance.FieldManager.SelectedRoomType = EnumRoomType.Start;
        }
    }
}
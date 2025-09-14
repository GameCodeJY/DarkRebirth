/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 21.
 *  Information :   Room Type Setting
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class RoomTypeSetting : MonoBehaviour
    {
        #region Data Field
        /// <summary> �� Ÿ�Կ� ���̴� Interactor </summary>
        [SerializeField] private EnumRoomType _roomType = default;
        /// <summary> �� ���� �̺�Ʈ </summary>
        [SerializeField] private UnityEvent<bool> _eventRoomSetting = default;
        /// <summary> �� �̺�Ʈ ���� </summary>
        [SerializeField] private UnityEvent _startRoomEvent = default;
        /// <summary> �� �̺�Ʈ �Ϸ� </summary>
        [SerializeField] private UnityEvent _completeRoomEvent = default;
        /// <summary> �� ��ȣ�ۿ� �Ϸ� </summary>
        [SerializeField] private UnityEvent _completeInteractionEvent = default;
        #endregion

        #region Property
        public EnumRoomType RoomType { get => _roomType; private set => _roomType = value; }
        public UnityEvent<bool> EventRoomSetting { get => _eventRoomSetting; private set => _eventRoomSetting = value; }
        public UnityEvent StartRoomEvent { get => _startRoomEvent; set => _startRoomEvent = value; }
        public UnityEvent CompleteRoomEvent { get => _completeRoomEvent; private set => _completeRoomEvent = value; }
        #endregion

        #region Unity LifeCycle
        private void OnEnable()
        {
            RegisterEventToProcessManager();
        }
        #endregion

        #region Register
        /// <summary>
        /// ProcessManager�� �̺�Ʈ ���
        /// </summary>
        private void RegisterEventToProcessManager()
        {
            MainSystem.Instance.ProcessManager.CompleteInteractionEvent = () => _completeInteractionEvent.Invoke();
        }
        #endregion
    }
}
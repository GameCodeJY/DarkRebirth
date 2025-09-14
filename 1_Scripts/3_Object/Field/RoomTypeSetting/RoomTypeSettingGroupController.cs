/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 24.
 *  Information :   Room Type Setting Group Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class RoomTypeSettingGroupController : MonoBehaviour // Data Field
    {
        /// <summary> Interactor Group </summary>
        [SerializeField] private List<RoomTypeSetting> _roomTypeSettingGroup = default;
    }

    /// <summary>
    /// Event
    /// </summary>
    public partial class RoomTypeSettingGroupController : MonoBehaviour // Event
    {
        /// <summary>
        /// ���ͷ��� �� ���� �̺�Ʈ ���
        /// </summary>
        public void OnRegisterRoomSettingEvent(bool isSetting)
        {
            EnumRoomType nowSelectedRoomType = MainSystem.Instance.FieldManager.SelectedRoomType;

            for (int i = 0; i < _roomTypeSettingGroup.Count; i++)
            {
                if (_roomTypeSettingGroup[i].RoomType.Equals(nowSelectedRoomType))
                {
                    _roomTypeSettingGroup[i].EventRoomSetting.Invoke(isSetting);
                }
            }
        }

        /// <summary>
        /// �� �̺�Ʈ ����
        /// </summary>
        public void OnStartRoomEvent()
        {
            EnumRoomType nowSelectedRoomType = MainSystem.Instance.FieldManager.SelectedRoomType;

            for (int i = 0; i < _roomTypeSettingGroup.Count; i++)
            {
                if (_roomTypeSettingGroup[i].RoomType.Equals(nowSelectedRoomType))
                {
                    _roomTypeSettingGroup[i].StartRoomEvent.Invoke();
                }
            }
        }

        /// <summary>
        /// �� �̺�Ʈ �Ϸ�
        /// </summary>
        public void OnCompleteRoomEvent()
        {
            EnumRoomType nowSelectedRoomType = MainSystem.Instance.FieldManager.SelectedRoomType;

            for (int i = 0; i < _roomTypeSettingGroup.Count; i++)
            {
                if (_roomTypeSettingGroup[i].RoomType.Equals(nowSelectedRoomType))
                {
                    Debug.Log("On Complete Room Event");

                    _roomTypeSettingGroup[i].CompleteRoomEvent.Invoke();
                }
            }
        }
    }
}
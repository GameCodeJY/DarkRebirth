/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   Portal Group Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Data Field
    /// </summary>
    public class PortalGroupController : MonoBehaviour // Data Field
    {
        #region Data Field
        /// <summary> Portal GameObject List </summary>
        [SerializeField] private List<GameObject> _listGameObjectFieldPortal = default;

        /// <summary> 포탈들이 상태 변화할때 실행되는 이벤트 </summary>
        [SerializeField] private UnityEvent<bool> _eventSetStatePortals = default;
        #endregion

        #region Public API
        /// <summary>
        /// 포탈 활성화 셋팅
        /// </summary>
        public void OnSetActivePortal(bool isActive)
        {
            _eventSetStatePortals?.Invoke(isActive);

            for (int i = 0; i < _listGameObjectFieldPortal.Count; i++)
            {
                _listGameObjectFieldPortal[i].SetActive(isActive);
            }
        }
        #endregion
    }
}
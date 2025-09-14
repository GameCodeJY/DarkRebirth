/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   정보를 기입
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BlessingProviderGroupController : MonoBehaviour
    {
        #region Data Field
        /// <summary> 은혜 장치 콜라이더 GameObjects </summary>
        [SerializeField] private List<GameObject> _listGameObjectsBlessingProviderCollider = default;
		#endregion

		#region Public API
		/// <summary>
		/// 은혜 장치 콜라이더들 전부 비활성화
		/// </summary>
		public void OnDeactiveBlessingProviderColliders()
		{
			for(int i = 0; i < _listGameObjectsBlessingProviderCollider.Count; ++i)
			{
				_listGameObjectsBlessingProviderCollider[i].SetActive(false);
            }
		}
		#endregion
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 09. 02.
 *  Information :   은혜 제공 장치
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BlessingProvider : MonoBehaviour
    {
        #region Data Field
        /// <summary> 생성할 스킬 아이템 소켓 </summary>
        [SerializeField] private Transform socketSkillItem = default;
        #endregion

        #region Public API
        /// <summary>
        /// 스킬 선택 이벤트 등록
        /// </summary>
        public void OnRegisterEventInstantiateSkillItemToManager()
        {
            MainSystem.Instance.SceneManager.UIManager.EventSelectSkill += InstantiateSkillItem;
        }
        #endregion

        #region Item
        /// <summary> 스킬 아이템 생성 </summary>
        private void InstantiateSkillItem(SkillConfig skillConfig)
        {
            SkillItemController skillItemController = Instantiate(Resources.Load<SkillItemController>("SkillItem")).GetComponent<SkillItemController>();
            if (skillItemController == null)
                return;

            skillItemController.Initialize(skillConfig);
            skillItemController.transform.SetParent(
                socketSkillItem,
                true  // worldPositionStays를 true로 설정하여 world transform 유지
            );
            skillItemController.transform.localPosition = Vector3.zero;
        }
        #endregion
    }
}
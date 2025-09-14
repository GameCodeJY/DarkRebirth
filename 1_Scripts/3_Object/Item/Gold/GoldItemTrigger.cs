/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 24.
 *  Information :   Gold Item Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class GoldItemTrigger : MonoBehaviour // Data Field
    {
        /// <summary> Main Player </summary>
        private BasePlayer _mainPlayer = default;
        /// <summary> Gold Item </summary>
        [SerializeField] private GoldItem _goldItem = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class GoldItemTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = mainPlayer;
                MainSystem.Instance.SceneManager.UIManager.StageUIController.ActiveFKeyMessageUI("상호작용");
            }
        }

        private void Update()
        {
            if (_mainPlayer != null && Input.GetKey(KeyCode.F))
            {
                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();

                // Process Manager에 상호작용 이벤트 완료 실행 요청
                MainSystem.Instance.ProcessManager.ExcuteCompleteInteractionEvent();

                // 골드 아이템 섭취
                _goldItem.ConsumeGoldItem();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (other.GetComponent<BasePlayer>() != null)
            {
                _mainPlayer = null;
                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
            }
        }
    }
}
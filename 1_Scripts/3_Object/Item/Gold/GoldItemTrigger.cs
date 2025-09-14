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
                MainSystem.Instance.SceneManager.UIManager.StageUIController.ActiveFKeyMessageUI("��ȣ�ۿ�");
            }
        }

        private void Update()
        {
            if (_mainPlayer != null && Input.GetKey(KeyCode.F))
            {
                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();

                // Process Manager�� ��ȣ�ۿ� �̺�Ʈ �Ϸ� ���� ��û
                MainSystem.Instance.ProcessManager.ExcuteCompleteInteractionEvent();

                // ��� ������ ����
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
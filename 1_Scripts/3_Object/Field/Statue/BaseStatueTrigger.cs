/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 28.
 *  Information :   Statue Trigger
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
    public partial class BaseStatueTrigger : MonoBehaviour // Data Field
    {
        /// <summary> Main Player </summary>
        private BasePlayer _mainPlayer = default;

        /// <summary> 석상 활성화 이벤트 </summary>
        [SerializeField] private UnityEvent EventActivateStatue = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class BaseStatueTrigger : MonoBehaviour // Main
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
            if (_mainPlayer != null && Input.GetKeyDown(KeyCode.F))
            {
                _mainPlayer = null;

                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
                //MainSystem.Instance.ProcessManager.baseStatueTrigger = this;
                EventActivateStatue?.Invoke();

                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = null;
                MainSystem.Instance.SceneManager.UIManager.StageUIController.DeActiveFKeyMessageUI();
            }
        }
    }
}
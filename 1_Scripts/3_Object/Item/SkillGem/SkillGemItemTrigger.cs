/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 26.
 *  Information :   Skill Gem Item Trigger
 */

namespace MainSystem
{
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class SkillGemItemTrigger : MonoBehaviour // Data Field
    {
        /// <summary> Picup시 일어날 일들을 콜백으로 등록하여 사용 GameObject </summary>
        public static event Action<string> OnSkillGemItemPickedUp;

        /// <summary> Skill Item Text UI GameObject </summary>
        [SerializeField] private GameObject GameObjectSkillItemTextUI = default;

        /// <summary> SkillGemCode </summary>
        [HideInInspector] public string SkillGemCode = default;

        /// <summary> Main Player </summary>
        private BasePlayer _mainPlayer = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class SkillGemItemTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = mainPlayer;
                GameObjectSkillItemTextUI.SetActive(true);
            }
        }

        private void Update()
        {
            if (_mainPlayer != null && Input.GetKeyDown(KeyCode.F) == true)
            {
                _mainPlayer = null;

                GameObjectSkillItemTextUI.SetActive(false);

                // 픽업시 호출해야 할 함수들을 호출 (UI이벤트 + 효과음 등등)
                OnSkillGemItemPickedUp.Invoke(SkillGemCode);

                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                _mainPlayer = null;
                GameObjectSkillItemTextUI.SetActive(false);
            }
        }
    }
}
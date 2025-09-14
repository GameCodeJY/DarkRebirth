/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 09.
 *  Information :   Skill Text Box Windows Controller
 */

namespace MainSystem
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class SkillTextBoxWindowsController : MonoBehaviour // Data Field
    {
        /// <summary> Title Text </summary>
        [SerializeField] private Text TextTitleTop = default;
        /// <summary> Explanation Text </summary>
        [SerializeField] private Text TextExplanationTop = default;

        /// <summary> Title Text </summary>
        [SerializeField] private Text TextTitleSkillIcon = default;
        /// <summary> Explanation Text </summary>
        [SerializeField] private Text TextExplanationSkillIcon = default;
    }

    /// <summary>
    /// Text
    /// </summary>
    public partial class SkillTextBoxWindowsController : MonoBehaviour // Text
    {
        public void SetExplanationWindowText(Dictionary<string, DR.SkillValue> dataDictionary, string id, bool isTopWindow)
        {
            var skillValue = dataDictionary[id];
            var skill = DR.Skill.SkillMap[skillValue.skillId];

            if (isTopWindow.Equals(true))
            {
                TextTitleTop.text = skill.name;
                TextExplanationTop.text = skillValue.explanation;
            }
            else
            {
                TextTitleSkillIcon.text = skill.name;
                TextExplanationSkillIcon.text = skillValue.explanation;
            }
        }

        /// <summary>
        /// Set Skill Explanation Window Text
        /// </summary>
        public void SetExplanationWindowText<T>(Dictionary<string, T> dataDictionary, string id, bool isTopWindow)
        {
            // 사전에 id에 해당하는 아이템을 가져옴
           T item = dataDictionary[id];

            // T 타입의 정보를 가져옴
            Type itemType = typeof(T);

            // "name" 필드와 "explanation" 필드 정보를 가져옴
            var nameField = itemType.GetField("name", BindingFlags.Public | BindingFlags.Instance);
            var explanationField = itemType.GetField("explanation", BindingFlags.Public | BindingFlags.Instance);
            // var valueSkill = itemType.GetField("skill_value1", BindingFlags.Public | BindingFlags.Instance);

            if (nameField == null || explanationField == null)
            {
                Debug.LogError("해당 타입에 'name' 혹은 'explanation' 필드가 존재하지 않습니다.");
                return;
            }

            string nameValue = nameField.GetValue(item)?.ToString() ?? string.Empty;

            string explanationValue = explanationField.GetValue(item)?.ToString() ?? string.Empty;
            if (isTopWindow.Equals(true))
            {
                TextTitleTop.text = nameValue;
                TextExplanationTop.text = explanationValue;
            }
            else
            {
                TextTitleSkillIcon.text = nameValue;
                TextExplanationSkillIcon.text = explanationValue;
            }
        }
    }
}
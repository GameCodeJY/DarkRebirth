/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 22.
 *  Information :   Skill Icon Mini Controller
 */

namespace MainSystem
{
    using DR;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class SkillIconMiniController : MonoBehaviour // Data Field
    {
        /// <summary> Skill Key Type </summary>
        public ActionType _skillType = ActionType.None;
        /// <summary> Active Skill Icon Image </summary>
        public Image ImageSkillIconA = default;
        /// <summary> Active Skill Icon Image </summary>
        public Image ImageSkillIconB = default;
        /// <summary> Active Skill Icon Image </summary>
        public Image ImageSkillIconC = default;
        /// <summary> Skill Icon Animator </summary>
        public Animator AnimatorSkillIcon = default;
        /// <summary> Pointer Enter Flag </summary>
        public bool IsPointerEnter { get; private set; } = false;

        /// <summary> 선택한 스킬 코드 </summary>
        private SkillConfig _selectSkill = default;

        /// <summary> Skill Gem Icon Mini Controller A </summary>
        public SkillGemIconMiniController SkillGemIconMiniControllerA = default;
        /// <summary> Skill Gem Icon Mini Controller B </summary>
        public SkillGemIconMiniController SkillGemIconMiniControllerB = default;
        /// <summary> Skill Gem Icon Mini Controller C </summary>
        public SkillGemIconMiniController SkillGemIconMiniControllerC = default;
        /// <summary> Skill Icon RectTransform </summary>
        public RectTransform RectTransformSkillIcon = default;

        public SkillConfig SelectSkill { get => _selectSkill; set => _selectSkill = value; }

        public string SkillCode => _selectSkill != null ? _selectSkill.SkillId : "";

        // 복사 생성자
        public SkillIconMiniController(SkillIconMiniController original)
        {
            _selectSkill = original._selectSkill;
            _skillType = original._skillType;
            ImageSkillIconA = original.ImageSkillIconA;
            AnimatorSkillIcon = original.AnimatorSkillIcon;
            ImageSkillIconC = original.ImageSkillIconC;
        }
    }

    /// <summary>
    /// Select
    /// </summary>
    public partial class SkillIconMiniController : MonoBehaviour // Select
    {
        public void UseSkill()
        {
            ImageSkillIconB.gameObject.SetActive(false);
        }

        /// <summary>
        /// Change Skill Icon Image
        /// </summary>
        private void ChangeSkillIconImage(string skillCode)
        {
            Sprite spriteActiveSkillIcon = default;
            Sprite spriteDeactiveSkillIcon = default;

            Skill skillData;
            if(skillCode != null && DR.Skill.SkillMap.TryGetValue(skillCode, out skillData))
            {
                spriteActiveSkillIcon = Resources.Load<Sprite>(skillData.icon_image);
                spriteDeactiveSkillIcon = spriteActiveSkillIcon;

                // 아이콘 이미지 교체
                ImageSkillIconA.enabled = true;
                ImageSkillIconA.sprite = spriteActiveSkillIcon;
                ImageSkillIconC.sprite = spriteDeactiveSkillIcon;
            }
            else
            {
                ImageSkillIconA.enabled = false;
            }
        }

        /// <summary>
        /// Select Skill
        /// </summary>
        public void OnSelectSkill(SkillConfig skillConfig)
        {
            if (MainSystem.Instance.SceneManager.UIManager.StageUIController.SkillMiniUIController.IsSkillIconSizeUp.Equals(false))
                return;

            _selectSkill = skillConfig;

            PlayerSkillController playerSkillController = MainSystem.Instance.PlayerManager.GetSkillController();
            playerSkillController.SetSkill(_skillType, skillConfig);

            ChangeSkillIconImage(_selectSkill.SkillId);
        }

        public void ChangeSkill(SkillIconMiniController beforeSkillIconMiniController)
        {
            // beforeSkillIconMiniController의 현재 상태를 임시 객체에 복사합니다.
            SkillIconMiniController tempSkillIconMiniController = new SkillIconMiniController(beforeSkillIconMiniController);

            // 변경 전: beforeSkillIconMiniController에 현재(this)의 스킬 정보를 적용
            UpdateSkill(this, beforeSkillIconMiniController);

            // 변경 후: 현재(this)에 임시 객체의 스킬 정보를 적용
            UpdateSkill(tempSkillIconMiniController, this);
        }

        private void UpdateSkill(SkillIconMiniController beforeSkillIconMiniController, SkillIconMiniController afterSkillIconMiniController)
        {
            // 컨트롤러의 공격 애니메이션 트리거 타입을 갱신
            afterSkillIconMiniController._selectSkill = beforeSkillIconMiniController._selectSkill;

            PlayerSkillController playerSkillController = MainSystem.Instance.PlayerManager.GetSkillController();
            if (playerSkillController == null)
                return;

            // 스킬 타입에 따른 메인 플레이어 및 액션 매니저의 상태 업데이트
            playerSkillController.SetSkill(afterSkillIconMiniController._skillType, afterSkillIconMiniController._selectSkill);

            // 스킬 아이콘 이미지 갱신
            afterSkillIconMiniController.ChangeSkillIconImage(_selectSkill?.SkillId);
        }   
    }

    /// <summary>
    /// Pointer
    /// </summary>
    public partial class SkillIconMiniController : MonoBehaviour // Pointer
    {
        /// <summary>
        /// Pointer Enter
        /// </summary>
        public void PointerEnter()
        {
            IsPointerEnter = true;
            if (_selectSkill != null)
            {
                MainSystem.Instance.SceneManager.UIManager.StageUIController.SkillMiniUIController.ToggleExplanationWindowBySkill(true, RectTransformSkillIcon, DR.SkillValue.SkillValueMap, _selectSkill.SkillValue.id);
            }
        }

        /// <summary>
        /// Pointer Exit
        /// </summary>
        public void PointerExit()
        {
            IsPointerEnter = true;
            if (_selectSkill != null)
            {
                MainSystem.Instance.SceneManager.UIManager.StageUIController.SkillMiniUIController.ToggleExplanationWindowBySkill(false, RectTransformSkillIcon, DR.SkillValue.SkillValueMap, _selectSkill.SkillValue.id);
            }
        }
    }
}
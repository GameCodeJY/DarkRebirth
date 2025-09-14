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

        /// <summary> ������ ��ų �ڵ� </summary>
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

        // ���� ������
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

                // ������ �̹��� ��ü
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
            // beforeSkillIconMiniController�� ���� ���¸� �ӽ� ��ü�� �����մϴ�.
            SkillIconMiniController tempSkillIconMiniController = new SkillIconMiniController(beforeSkillIconMiniController);

            // ���� ��: beforeSkillIconMiniController�� ����(this)�� ��ų ������ ����
            UpdateSkill(this, beforeSkillIconMiniController);

            // ���� ��: ����(this)�� �ӽ� ��ü�� ��ų ������ ����
            UpdateSkill(tempSkillIconMiniController, this);
        }

        private void UpdateSkill(SkillIconMiniController beforeSkillIconMiniController, SkillIconMiniController afterSkillIconMiniController)
        {
            // ��Ʈ�ѷ��� ���� �ִϸ��̼� Ʈ���� Ÿ���� ����
            afterSkillIconMiniController._selectSkill = beforeSkillIconMiniController._selectSkill;

            PlayerSkillController playerSkillController = MainSystem.Instance.PlayerManager.GetSkillController();
            if (playerSkillController == null)
                return;

            // ��ų Ÿ�Կ� ���� ���� �÷��̾� �� �׼� �Ŵ����� ���� ������Ʈ
            playerSkillController.SetSkill(afterSkillIconMiniController._skillType, afterSkillIconMiniController._selectSkill);

            // ��ų ������ �̹��� ����
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
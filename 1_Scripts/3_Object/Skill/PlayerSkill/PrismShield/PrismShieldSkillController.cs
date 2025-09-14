/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 17.
 *  Information :   Prism Shield Skill Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class PrismShieldSkillController : PlayerBaseSkill // Data Field
    {
        /// <summary> ��ų ��ü �ð� </summary>
        private float TOTAL_TIME_SKILL = 5.0f;
        /// <summary> ������ ���� ���� Ÿ�̹� </summary>
        private float TIMING_PRISM_SHIELD_BOOM_ATTACK = 4.0f;
        /// <summary> ������ �ǵ� Ȱ��ȭ �ð� </summary>
        private float TIME_PRISM_SHIELD_ACTIVATE = 3.0f;

        /// <summary> Prism Shield GameObject </summary>
        [SerializeField] private GameObject GameObjectPrismShield = default;
        /// <summary> Prism Shield Boom GameObject </summary>
        [SerializeField] private GameObject GameObjectPrismShieldBoom = default;
        /// <summary> Attack Trigger GameObject </summary>
        [SerializeField] private GameObject GameObjectAttackTrigger = default;
        /// <summary> Using Skill Time </summary>
        private float TimeUsingSkill = 0.0f;
        /// <summary> ������ �ǵ� ���� ���� Ȱ��ȭ Flag </summary>
        private bool IsPrismShieldBoomActivate = false;
        /// <summary> ������ �ǵ� Ȱ��ȭ Flag </summary>
        private bool IsPrismShieldActivate = false;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class PrismShieldSkillController : PlayerBaseSkill // Main
    {

        private void Update()
        {
            CheckSkillTime();
        }
    }

    /// <summary>
    /// Skill
    /// </summary>
    public partial class PrismShieldSkillController : PlayerBaseSkill //  Skill
    {
        /// <summary>
        /// Check Skill Time
        /// </summary>
        private void CheckSkillTime()
        {
            TimeUsingSkill += Time.deltaTime;

            if (TimeUsingSkill >= TOTAL_TIME_SKILL) // 5�� Ÿ�ӿ� ������ ����Ʈ ��ü�� Destory
            {
                Destroy(gameObject);
            }
            else if (TimeUsingSkill >= TIMING_PRISM_SHIELD_BOOM_ATTACK) // 4�� Ÿ�ֿ̹� ������ ���� ���� Ʈ���� Ȱ��ȭ
            {
                if (IsPrismShieldBoomActivate.Equals(false)) // ������ �ǵ� ���� ���� Ȱ��ȭ
                {
                    IsPrismShieldBoomActivate = true;

                    GameObjectAttackTrigger.SetActive(true);

                    MainSystem.Instance.PlayerManager.MainPlayer.Defence = 0.0f; // ���� 0.0f
                }
            }
            else if (TimeUsingSkill >= TIME_PRISM_SHIELD_ACTIVATE) // 3�� ���Ŀ� ���� ����Ʈ Ȱ��ȭ
            {
                if (IsPrismShieldActivate.Equals(false))
                {
                    IsPrismShieldActivate = true;

                    GameObjectPrismShield.SetActive(false);
                    GameObjectPrismShieldBoom.SetActive(true);
                }
            }
        }
    }
}
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
        /// <summary> 스킬 전체 시간 </summary>
        private float TOTAL_TIME_SKILL = 5.0f;
        /// <summary> 프리즘 폭발 공격 타이밍 </summary>
        private float TIMING_PRISM_SHIELD_BOOM_ATTACK = 4.0f;
        /// <summary> 프리즘 실드 활성화 시간 </summary>
        private float TIME_PRISM_SHIELD_ACTIVATE = 3.0f;

        /// <summary> Prism Shield GameObject </summary>
        [SerializeField] private GameObject GameObjectPrismShield = default;
        /// <summary> Prism Shield Boom GameObject </summary>
        [SerializeField] private GameObject GameObjectPrismShieldBoom = default;
        /// <summary> Attack Trigger GameObject </summary>
        [SerializeField] private GameObject GameObjectAttackTrigger = default;
        /// <summary> Using Skill Time </summary>
        private float TimeUsingSkill = 0.0f;
        /// <summary> 프리즘 실드 폭발 공격 활성화 Flag </summary>
        private bool IsPrismShieldBoomActivate = false;
        /// <summary> 프리즘 실드 활성화 Flag </summary>
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

            if (TimeUsingSkill >= TOTAL_TIME_SKILL) // 5초 타임에 프리즘 이펙트 자체를 Destory
            {
                Destroy(gameObject);
            }
            else if (TimeUsingSkill >= TIMING_PRISM_SHIELD_BOOM_ATTACK) // 4초 타이밍에 프리즘 폭발 공격 트리거 활성화
            {
                if (IsPrismShieldBoomActivate.Equals(false)) // 프리즘 실드 폭발 공격 활성화
                {
                    IsPrismShieldBoomActivate = true;

                    GameObjectAttackTrigger.SetActive(true);

                    MainSystem.Instance.PlayerManager.MainPlayer.Defence = 0.0f; // 방어력 0.0f
                }
            }
            else if (TimeUsingSkill >= TIME_PRISM_SHIELD_ACTIVATE) // 3초 이후에 폭발 이펙트 활성화
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
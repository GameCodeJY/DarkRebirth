/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 22.
 *  Information :   Prism Shield Attack Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class PrismShieldAttackTrigger : BasePlayerSkillTrigger // Data Field
    {
        /// <summary> ���� ���� �ð� </summary>
        private float INTERVAL_ATTACK_TIME = 0.02f;

        /// <summary> Attack Range Capsule Collider </summary>
        public CapsuleCollider CapsuleColliderAttackRange = default;
        /// <summary> Player Attack Zone�� ���� ���� ����Ʈ </summary>
        [HideInInspector] public List<HPComponent> ListMonsterEnterPlayerAttackZone = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class PrismShieldAttackTrigger : BasePlayerSkillTrigger // Main
    {
        private void Awake()
        {
            ListMonsterEnterPlayerAttackZone = new List<HPComponent>();
            StartCoroutine(DisableAfterDelay());
        }

        /// <summary>
        /// Disable After Delay
        /// </summary>
        private IEnumerator DisableAfterDelay()
        {
            CapsuleColliderAttackRange.enabled = true;

            float elapsedTime = 0.0f;
            while (elapsedTime < INTERVAL_ATTACK_TIME)
            {
                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();    // ���� ������ ������ ���
            }

            CapsuleColliderAttackRange.enabled = false;
            gameObject.SetActive(false);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            HPComponent baseMonster = other.GetComponent<HPComponent>();
            if (baseMonster == null)
                return;

            if (ListMonsterEnterPlayerAttackZone.Contains(baseMonster))
                return;

            ListMonsterEnterPlayerAttackZone.Add(baseMonster);
            baseMonster.TakeDamage(_attackInfo);

            base.OnTriggerEnter(other);
        }
    }
}
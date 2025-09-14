/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 07.
 *  Information :   Monster Attack Player Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MonsterAttackPlayerTrigger : MonoBehaviour // Data Field
    {
        #region Serialize Field
        [SerializeField] private GameObject _effect = default;
        [SerializeField] private BaseMonster baseMonster = default;
        [SerializeField] private Collider _attackCollider = default;

        [SerializeField] private float _attackDuration = default;
        #endregion

        #region Property
        public MonsterStat MonsterStat { private get; set; }
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class MonsterAttackPlayerTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer == null) // Player가 공격 범위에 들어옴
                return;

            baseMonster.TargetMainPlayer = mainPlayer;

            HPComponent hpComponent = other.GetComponent<HPComponent>();    
            if (hpComponent == null)
                return;

            hpComponent.TakeDamage(new AttackContext(MonsterStat[EStatType.ATK], transform));
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null) // Player가 공격 범위에서 벗어남
            {
                baseMonster.TargetMainPlayer = null;
            }
        }
    }

    /// <summary>
    /// ActivateCollider
    /// </summary>
    public partial class MonsterAttackPlayerTrigger : MonoBehaviour // ActivateCollider
    {
        public void StartAttack() // 공격 콜라이더 활성화 시작
        {
            StartCoroutine(EnableAttackColliderForDuration());

            if (_effect == null)
                return;

            _effect.gameObject.SetActive(true);
        }

        public void StopAttack() // 공격 강제 종료
        {
            StopCoroutine(EnableAttackColliderForDuration());
            _attackCollider.enabled = false;
        }

        IEnumerator EnableAttackColliderForDuration() // 일정시간 동안 콜라이더 활성화 후. 비활성화 처리
        {
            _attackCollider.enabled = true;

            yield return new WaitForSeconds(_attackDuration);

            _attackCollider.enabled = false;
        }
    }
}
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
            if (mainPlayer == null) // Player�� ���� ������ ����
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
            if (mainPlayer != null) // Player�� ���� �������� ���
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
        public void StartAttack() // ���� �ݶ��̴� Ȱ��ȭ ����
        {
            StartCoroutine(EnableAttackColliderForDuration());

            if (_effect == null)
                return;

            _effect.gameObject.SetActive(true);
        }

        public void StopAttack() // ���� ���� ����
        {
            StopCoroutine(EnableAttackColliderForDuration());
            _attackCollider.enabled = false;
        }

        IEnumerator EnableAttackColliderForDuration() // �����ð� ���� �ݶ��̴� Ȱ��ȭ ��. ��Ȱ��ȭ ó��
        {
            _attackCollider.enabled = true;

            yield return new WaitForSeconds(_attackDuration);

            _attackCollider.enabled = false;
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2024. 11. 28.
 *  Information :   Lightning Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class LightningTrigger : MonoBehaviour // Data Field
    {
        #region Const Value
        /// <summary> Lightning Attack Player 타이밍 </summary>
        private float TIMING_LIGHTNING_ATTACK_PLAYER = 1.0f;
        #endregion

        #region Member Value
        /// <summary> MainPlayer </summary>
        private BasePlayer MainPlayer = default;
        /// <summary> 플레이어를 공격했는 지 </summary>
        private bool IsAttackPlayer = false;
        /// <summary> Lightning Attack Player Time </summary>
        private float TimeLightningAttackPlayer = 0.0f;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class LightningTrigger : MonoBehaviour // Main
    {
        private void Start()
        {
            Destroy(transform.parent.gameObject, 2.0f);
        }

        private void Update()
        {
            TimeLightningAttackPlayer += Time.deltaTime;
            if (IsAttackPlayer.Equals(false) && MainPlayer != null && TimeLightningAttackPlayer >= TIMING_LIGHTNING_ATTACK_PLAYER)
            {
                IsAttackPlayer = true;
                MainSystem.Instance.PlayerManager.MainPlayer.GetComponent<HPComponent>().TakeDamage(new AttackContext(10f, transform));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                MainPlayer = mainPlayer;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BasePlayer mainPlayer = other.GetComponent<BasePlayer>();
            if (mainPlayer != null)
            {
                MainPlayer = null;
            }
        }
    }
}
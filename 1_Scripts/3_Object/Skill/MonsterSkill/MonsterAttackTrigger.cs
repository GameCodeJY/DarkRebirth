/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 09.
 *  Information :   Monster Attack Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Main
    /// </summary>
    public partial class MonsterAttackTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            HPComponent hpComponent = other.GetComponent<HPComponent>();
            if (hpComponent != null)
            {
                hpComponent.TakeDamage(new AttackContext(10f, transform));
                gameObject.SetActive(false);
            }
        }
    }
}

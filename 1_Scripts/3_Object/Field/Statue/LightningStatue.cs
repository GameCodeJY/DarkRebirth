/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 05.
 *  Information :   Lightning Statue
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LightningStatue : Statue
    {
        /// <summary>
        /// Player에게 Debuff
        /// </summary>
        protected override void DebuffToPlayer()
        {
            // 프리팹 로드
            GameObject prefabLightningFX = Resources.Load<GameObject>("Statue/LightningFx");
            Vector3 mainPlayerPosition = MainSystem.Instance.PlayerManager.MainPlayer.transform.position;
            mainPlayerPosition.y += prefabLightningFX.transform.position.y;
            Instantiate(prefabLightningFX, mainPlayerPosition, prefabLightningFX.transform.rotation);
        }
    }
}
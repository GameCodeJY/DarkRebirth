/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 29.
 *  Information :   Combat Room
 */

namespace MainSystem
{
    using DR;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class CombatRoom : MonoBehaviour // Data Field
    {
        #region Const Value
        /// <summary> 몬스터 스탯 증가 비율 </summary>
        private const float RATIO_INCREASE_MONSTER_STAT = 1.5f;
        #endregion
    }

    /// <summary>
    /// Monster Stat
    /// </summary>
    public partial class CombatRoom : MonoBehaviour // Monster Stat
    {
        /// <summary>
        /// 몬스터 스탯 뻥튀기
        /// </summary>
        private MonsterStatContext ChangeMonsterData(MonsterStatContext context)
        {
            context.HP *= RATIO_INCREASE_MONSTER_STAT;
            context.ATK *= RATIO_INCREASE_MONSTER_STAT;
            context.RewardGold = (int)(context.RewardGold * RATIO_INCREASE_MONSTER_STAT);

            return context;
        }

        /// <summary>
        /// 향상된 몬스터 스탯 적용시키기
        /// </summary>
        public void OnChangeMonsterData(bool isSetting)
        {
            if (isSetting)
            {
                MainSystem.Instance.DataManager.EventMonsterDataChange += ChangeMonsterData;
            }
            else 
            {
                MainSystem.Instance.DataManager.EventMonsterDataChange -= ChangeMonsterData;
            }
        }
    }
}
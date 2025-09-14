/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 28.
 *  Information :   Monster Stat Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DR;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MonsterStatController : BaseStatController<MonsterStat> // Data Field
    {
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class MonsterStatController : BaseStatController<MonsterStat> // Initialize
    {
        protected override void Allocate()
        {
            base.Allocate();
        }

        public void Initialize(string monsterIndex)
        {
            Allocate();
            InitializeMonsterStat(monsterIndex);
        }

        private void InitializeMonsterStat(string monsterIndex)
        {
            MonsterStatContext context = new MonsterStatContext();
            MonStat monStat = MonStat.MonStatMap[monsterIndex];

            context.HP = (float)monStat.hp;
            context.ATK = (float)monStat.damage;
            context.Size = monStat.size;
            context.HpRegen = monStat.hpregen;
            context.CriChance = monStat.crichance;
            context.CriDamage = monStat.cridamage;
            context.AttackSpeed = monStat.attackspeed;
            context.AttackCoolTime = monStat.AttackCoolTime;
            context.AttackDist = monStat.attack_dist;
            context.EXP = monStat.exp;
            context.RewardGold = monStat.reward_gold;

            ObjectStat.RebaseFromContext(context);
        }
    }
}
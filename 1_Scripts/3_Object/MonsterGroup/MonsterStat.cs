/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 25.
 *  Information :   Monster Stat
 */

namespace MainSystem
{
    using System;

    public struct MonsterStatContext
    {
        public float HP;
        public float ATK;
        public float Size;
        public float HpRegen;
        public float CriChance;
        public float CriDamage;
        public float AttackCoolTime;
        public float AttackSpeed;
        public float AttackDist;
        public float CriticalPower;
        public float EXP;
        public float MoveSpeed;
        public float RewardGold;
    }

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MonsterStat : BaseStat, IEquatable<MonsterStat> // Data Field
    {
        /// <summary>
        /// 기본 생성자: 모든 필드를 기본값(0 혹은 null)으로 초기화합니다.
        /// </summary>
        public MonsterStat()
        {
            // 필요하면 기본값을 다르게 설정할 수도 있습니다.
            RegisterStat(EStatType.Size);
            RegisterStat(EStatType.HpRegen);
            RegisterStat(EStatType.CriticalProbability);
            RegisterStat(EStatType.CriticalPower);
            RegisterStat(EStatType.AttackSpeed);
            RegisterStat(EStatType.AttackCoolTime);
            RegisterStat(EStatType.AttackDist);
            RegisterStat(EStatType.EXP);
            RegisterStat(EStatType.RewardGold);
        }

        public void RebaseFromContext(in MonsterStatContext context, bool isKeepModifire = true)
        {
            SetBase(EStatType.HP, context.HP);
            SetBase(EStatType.ATK, context.ATK);
            SetBase(EStatType.Size, context.Size);
            SetBase(EStatType.HpRegen, context.HpRegen);
            SetBase(EStatType.CriticalProbability, context.CriChance);
            SetBase(EStatType.CriticalPower, context.CriDamage);
            SetBase(EStatType.AttackCoolTime, context.AttackCoolTime);
            SetBase(EStatType.AttackSpeed, context.AttackSpeed);
            SetBase(EStatType.MoveSpeed, context.MoveSpeed);
            SetBase(EStatType.AttackDist, context.AttackDist);
            SetBase(EStatType.CriticalPower, context.CriticalPower);
            SetBase(EStatType.EXP, context.EXP);
            SetBase(EStatType.RewardGold, context.RewardGold);

            MainSystem.Instance.DataManager.EventMonsterDataChange?.Invoke(context);
        }

        ///// <summary>
        ///// 복사 생성자: 다른 PlayerStat 인스턴스의 모든 필드 값을 깊은 복사합니다.
        ///// </summary>
        //public MonsterStat(MonsterStat other) : base(other)
        //{
        //    if (other == null) throw new ArgumentNullException(nameof(other));
        //}

        public bool Equals(MonsterStat other)
        {
            if (other is null) return false;

            return base.Equals(other);
        }

        public override bool Equals(object obj) =>
            Equals(obj as MonsterStat);

        public override int GetHashCode()
        {
            // ValueTuple.GetHashCode를 호출
            return (
                GetBase(EStatType.HP), GetBase(EStatType.ATK),
                GetBase(EStatType.Size), GetBase(EStatType.HpRegen),
                GetBase(EStatType.CriticalProbability), GetBase(EStatType.CriticalPower),
                GetBase(EStatType.AttackSpeed), GetBase(EStatType.AttackCoolTime), 
                GetBase(EStatType.AttackDist), GetBase(EStatType.EXP), GetBase(EStatType.RewardGold)
            ).GetHashCode();
        }
    }
}
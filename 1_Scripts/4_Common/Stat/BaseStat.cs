/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 14.
 *  Information :   Base Stat
 */

using GoogleSheet.Core.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainSystem
{
    [UGS(typeof(EStatType))]
    public enum EStatType
    {
        None = 0,
        HP,
        ATK,
        Stamina,
        StaminaRecovery,
        AMR,
        REP,
        DamageReduction,
        AttackSpeed,
        MoveSpeed,
        CriticalProbability,
        CriticalPower,
        Luck,
        Size,
        HpRegen,
        AttackCoolTime,
        AttackDist,
        EXP,
        RewardGold,
        Dash
    }

    public class BaseStat
    {
        // 모든 스탯을 여기서 관리 (문자열 키 → StatWithMods)
        private readonly Dictionary<EStatType, StatWithMods> _stats =
            new Dictionary<EStatType, StatWithMods>();

        public BaseStat()
        {
            // 최소 공통 스탯 등록
            RegisterStat(EStatType.HP);
            RegisterStat(EStatType.ATK);
            RegisterStat(EStatType.MoveSpeed);
        }

        public BaseStat(BaseStat other)
        {
            // 베이스 값만 복사(일시 모디파이어는 복사 X)
            var keyList = other._stats.Keys.ToList<EStatType>();
            for (int i = 0; i < keyList.Count; ++i)
            {
                // other에 있는 모든 키값(EStatType)을 돌면서 Base값을 갱신
                var key = keyList[i];
                SetBase(key, other.GetBase(key));
            }
        }

        public float this[EStatType statType] => GetStat(statType);

        public bool Equals(BaseStat other)
        {
            if (other == null) 
                return false;

            foreach (EStatType key in _stats.Keys)
            {
                // 동일하지 않는 값이 나오는 경우 바로 return false;
                if (GetBase(key).Equals(other.GetBase(key)) == false)
                    return false;
            }

            return true;
        }

        // 파생 클래스에서 스탯 등록용
        protected void RegisterStat(EStatType statType, float baseValue = 0f)
        {
            if (!_stats.ContainsKey(statType)) _stats.Add(statType, new StatWithMods(baseValue));
            else _stats[statType].BaseValue = baseValue;
        }

        //베이스 값 접근 (세이브/로드용)
        protected float GetBase(EStatType statType)
        {
            if (_stats.TryGetValue(statType, out var s)) return s.BaseValue;
            throw new ArgumentException($"Unknown stat: {statType}");
        }

        protected void SetBase(EStatType statType, float v)
        {
            if (_stats.TryGetValue(statType, out var s)) s.BaseValue = v;
            else _stats.Add(statType, new StatWithMods(v));
        }

        // 현재 값(모디 적용 후) 읽기
        public float GetStat(EStatType statType)
        {
            if (_stats.TryGetValue(statType, out var s)) return s.Value;
            throw new ArgumentException($"Unknown stat: {statType}");
        }

        // 모디파이어 조작
        public void AddModifier(EStatType statType, ModKind kind, float value, float durationSec, int sourceId = 0, int order = 0)
        {
            if (_stats.TryGetValue(statType, out var s)) s.AddTemp(kind, value, durationSec, sourceId, order);
            else
            {
                // 등록이 안 되어 있었다면 자동 등록 후 적용
                var swm = new StatWithMods(0f);
                swm.AddTemp(kind, value, durationSec, sourceId, order);
                _stats.Add(statType, swm);
            }
        }

        public void RemoveModifier(EStatType statType, int sourceId)
        {
            if (_stats.TryGetValue(statType, out var s)) s.RemoveBySource(sourceId);
        }

        public void RemoveAllBySource(int sourceId)
        {
            foreach (var s in _stats.Values) s.RemoveBySource(sourceId);
        }

        public void TickStats(float now)
        {
            foreach (var s in _stats.Values) s.CullExpired(now);
        }
    }
}
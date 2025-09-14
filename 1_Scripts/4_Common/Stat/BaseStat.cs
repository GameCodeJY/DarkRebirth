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
        // ��� ������ ���⼭ ���� (���ڿ� Ű �� StatWithMods)
        private readonly Dictionary<EStatType, StatWithMods> _stats =
            new Dictionary<EStatType, StatWithMods>();

        public BaseStat()
        {
            // �ּ� ���� ���� ���
            RegisterStat(EStatType.HP);
            RegisterStat(EStatType.ATK);
            RegisterStat(EStatType.MoveSpeed);
        }

        public BaseStat(BaseStat other)
        {
            // ���̽� ���� ����(�Ͻ� ������̾�� ���� X)
            var keyList = other._stats.Keys.ToList<EStatType>();
            for (int i = 0; i < keyList.Count; ++i)
            {
                // other�� �ִ� ��� Ű��(EStatType)�� ���鼭 Base���� ����
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
                // �������� �ʴ� ���� ������ ��� �ٷ� return false;
                if (GetBase(key).Equals(other.GetBase(key)) == false)
                    return false;
            }

            return true;
        }

        // �Ļ� Ŭ�������� ���� ��Ͽ�
        protected void RegisterStat(EStatType statType, float baseValue = 0f)
        {
            if (!_stats.ContainsKey(statType)) _stats.Add(statType, new StatWithMods(baseValue));
            else _stats[statType].BaseValue = baseValue;
        }

        //���̽� �� ���� (���̺�/�ε��)
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

        // ���� ��(��� ���� ��) �б�
        public float GetStat(EStatType statType)
        {
            if (_stats.TryGetValue(statType, out var s)) return s.Value;
            throw new ArgumentException($"Unknown stat: {statType}");
        }

        // ������̾� ����
        public void AddModifier(EStatType statType, ModKind kind, float value, float durationSec, int sourceId = 0, int order = 0)
        {
            if (_stats.TryGetValue(statType, out var s)) s.AddTemp(kind, value, durationSec, sourceId, order);
            else
            {
                // ����� �� �Ǿ� �־��ٸ� �ڵ� ��� �� ����
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
/*
 *  Coder       :   JY
 *  Last Update :   2025. 08. 08.
 *  Information :   스탯 모디파이어
 */

namespace MainSystem
{
    using System;
    using System.Collections.Generic;
    using UnityEngine; // for Time.time

    #region Data Field
    public enum ModKind { Flat, PercentAdd, PercentMult }

    public readonly struct StatModifier
    {
        public readonly ModKind Kind;
        public readonly float Value;     // 0.10f == +10%
        public readonly int SourceId;  // 스킬/콤보/아이템 등 출처 식별
        public readonly float ExpireAt;  // Time.time 기준, 0이면 영구
        public readonly int Order;     // 필요 시 Kind 내 우선순위

        public StatModifier(ModKind kind, float value, int sourceId, float expireAt, int order = 0)
        {
            Kind = kind; Value = value; SourceId = sourceId; ExpireAt = expireAt; Order = order;
        }
        public bool IsExpired(float now) => (ExpireAt > 0f) && (now >= ExpireAt);
    }
    #endregion

    // 데이터 계산용 클래스
    public sealed class StatWithMods
    {
        private float _baseValue;
        private readonly List<StatModifier> _mods = new(8);
        private float _cachedValue;
        private bool _dirty = true;

        public StatWithMods(float baseValue) => _baseValue = baseValue;

        public float BaseValue
        {
            get => _baseValue;
            set { _baseValue = value; _dirty = true; }
        }

        public float Value
        {
            get
            {
                if (!_dirty) return _cachedValue;

                float flat = 0f, addPct = 0f, mult = 1f;
                // 필요 시: _mods.Sort((a,b) => a.Order.CompareTo(b.Order));
                for (int i = 0; i < _mods.Count; i++)
                {
                    var m = _mods[i];
                    switch (m.Kind)
                    {
                        case ModKind.Flat: flat += m.Value; break;
                        case ModKind.PercentAdd: addPct += m.Value; break;
                        case ModKind.PercentMult: mult *= (1f + m.Value); break;
                    }
                }
                _cachedValue = ((_baseValue + flat) * (1f + addPct)) * mult;
                _dirty = false;
                return _cachedValue;
            }
        }

        public void Add(StatModifier mod) { _mods.Add(mod); _dirty = true; }

        public void AddTemp(ModKind kind, float value, float durationSec, int sourceId = 0, int order = 0)
        {
            float expire = durationSec > 0 ? Time.time + durationSec : 0f;
            Add(new StatModifier(kind, value, sourceId, expire, order));
        }

        public void RemoveBySource(int sourceId)
        {
            bool removed = false;
            for (int i = _mods.Count - 1; i >= 0; --i)
                if (_mods[i].SourceId == sourceId) { _mods.RemoveAt(i); removed = true; }
            if (removed) _dirty = true;
        }

        public void CullExpired(float now)
        {
            bool removed = false;
            for (int i = _mods.Count - 1; i >= 0; --i)
                if (_mods[i].IsExpired(now)) { _mods.RemoveAt(i); removed = true; }
            if (removed) _dirty = true;
        }
    }
}

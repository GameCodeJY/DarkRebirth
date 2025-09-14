/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 28.
 *  Information :   Base Stat Controller
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class BaseStatController : MonoBehaviour // Data Field
    {
        /// <summary> 어떤 BaseStat이든 꺼내쓸 수 있는 프로퍼티 </summary>
        public virtual BaseStat BaseStat { get; set; }
    }

    /// <summary>
    /// Generic
    /// </summary>
    public partial class BaseStatController<T> : BaseStatController where T : BaseStat, new() // Generic
    {
        /// <summary> Base Stat Data </summary>
        [HideInInspector] public T ObjectStat = default;

        // non-generic 프로퍼티를 override
        public override BaseStat BaseStat
        {
            get => ObjectStat;
            set => ObjectStat = (T)value;
        }

        protected virtual void Allocate()
        {
            ObjectStat = new T();
        }

        public void AddModifier(EStatType statType, ModKind kind, float value, float durationSec, int sourceId = 0, int order = 0)
        {
            BaseStat.AddModifier(statType, kind, value, durationSec, sourceId, order);
        }


        protected void Update()
        {
            BaseStat.TickStats(Time.time);
        }
    }
}

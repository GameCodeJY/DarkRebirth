/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 25.
 *  Information :   Data Manager
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UGS;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class DataManager : BaseManager // Data Field
    {
        #region Member Value
        /// <summary> Player Stat </summary>
        private PlayerStat _systemPlayerStat = default;
        #endregion

        #region Event
        /// <summary> ���� ���� ��ȭ �̺�Ʈ ���� </summary>
        public Func<MonsterStatContext, MonsterStatContext> EventMonsterDataChange { get; set; } = default;
        #endregion

        #region Property
        public List<ItemType> ListInventorySavedItem { get; set; } = default;
        public List<ItemType> ListInventoryEquippedItem { get; set; } = default;
        public PlayerStat SystemPlayerStat { get => _systemPlayerStat; set => _systemPlayerStat = value; }
        #endregion

        /// <summary> �÷��̾� ������ ��ȭ �̺�Ʈ ���� </summary>
        public event Action<PlayerStat> EventPlayerDataChange;
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class DataManager : BaseManager // Initialize
    {
        #region Initialize
        protected override void Allocate() // ����
        {
            _systemPlayerStat = new PlayerStat();
        }

        public override void Initialize() // ����
        {
            Allocate();

            UnityGoogleSheet.LoadAllData();
            DatabaseManager.LoadAllData();

            InitializeSystemPlayerStat();
        }

        /// <summary>
        /// Player Ability Data �ʱ�ȭ
        /// </summary>
        public PlayerStat InitializeSystemPlayerStat()
        {
            PlayerStatContext context = GetCurrentStatContext();
            _systemPlayerStat.ApplyBaseFromContext(context);

            MainSystem.Instance.DataManager.EventPlayerDataChange?.Invoke(_systemPlayerStat);

            return _systemPlayerStat;
        }
        #endregion
    }
}

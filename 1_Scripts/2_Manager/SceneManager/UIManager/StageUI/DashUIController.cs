/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 16.
 *  Information :   Dash UI Controller
 */

namespace MainSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class DashUIController : MonoBehaviour // Data Field
    {
        /// <summary> Dash Icon GameObject List </summary>
        [SerializeField] private List<GameObject> ListGameObjectDashIcon = default;
        /// <summary> ���� ��� �̹��� ����Ʈ </summary>
        [SerializeField] private List<Image> ListImageDashBackGround = default;
        /// <summary> �÷��̾��� ActionManager </summary>
        private ActionController ActionController = default;
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class DashUIController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            ActionController = MainSystem.Instance.PlayerManager.MainPlayer.GetComponent<ActionController>();
        }

        private void Initialize()
        {
            Allocate();
            InitializeDashUI();
            RegisterDashUIEvent();
        }

        /// <summary>
        /// Dash Icon State �ʱ�ȭ
        /// </summary>
        private void InitializeDashUI()
        {
            for (int i = 0; i < ListGameObjectDashIcon.Count; i++)
            {
                ListGameObjectDashIcon[i].SetActive(true); // ��� ���� ������ Ȱ��ȭ
            }

            for (int i = 0; i < ListImageDashBackGround.Count; i++)
            {
                ListImageDashBackGround[i].fillAmount = 1.0f; // ��� ���� ������ ��� Ǯ�� ä���
            }
        }

        /// <summary>
        /// Dash UI Event ���
        /// </summary>
        private void RegisterDashUIEvent()
        {
            ActionController.EventDashChange += ToggleDashIcon;
            ActionController.EventDashCharging += ChargeDashGaugeUI;
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class DashUIController : MonoBehaviour // Main
    {
        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            ActionController.EventDashChange -= ToggleDashIcon;
            ActionController.EventDashCharging -= ChargeDashGaugeUI;
        }
    }

    /// <summary>
    /// Update
    /// </summary>
    public partial class DashUIController : MonoBehaviour // Update
    {
        /// <summary>
        /// Dash Icon Ȱ��ȭ
        /// </summary>
        private void ToggleDashIcon(int indexDash, bool isActive)
        {
            ListGameObjectDashIcon[indexDash].SetActive(isActive);

            if (isActive.Equals(false)) // ���� �������� ��Ȱ��ȭ �Ѵٸ�
                ResetBeforeChargeGauge(indexDash); // ���� ���� �������� ����
        }

        /// <summary>
        /// ���� ���� �������� ����
        /// </summary>
        private void ResetBeforeChargeGauge(int indexDash)
        {
            if (indexDash + 1 < DR.PlayerStat.PlayerStatMap[DR.PlayerStatKeys.PLAYER_WOMAN].dash)
            {
                ChargeDashGaugeUI(indexDash + 1, 0.0f);
            }
        }

        /// <summary>
        /// ���� ������ UI ����
        /// </summary>
        private void ChargeDashGaugeUI(int indexDash, float dashCharging)
        {
            ListImageDashBackGround[indexDash].fillAmount = dashCharging;
        }
    }
}
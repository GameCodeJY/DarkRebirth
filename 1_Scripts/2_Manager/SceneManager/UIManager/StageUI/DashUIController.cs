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
        /// <summary> 데쉬 배경 이미지 리스트 </summary>
        [SerializeField] private List<Image> ListImageDashBackGround = default;
        /// <summary> 플레이어의 ActionManager </summary>
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
        /// Dash Icon State 초기화
        /// </summary>
        private void InitializeDashUI()
        {
            for (int i = 0; i < ListGameObjectDashIcon.Count; i++)
            {
                ListGameObjectDashIcon[i].SetActive(true); // 모든 데쉬 아이콘 활성화
            }

            for (int i = 0; i < ListImageDashBackGround.Count; i++)
            {
                ListImageDashBackGround[i].fillAmount = 1.0f; // 모든 데쉬 아이콘 배경 풀로 채우기
            }
        }

        /// <summary>
        /// Dash UI Event 등록
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
        /// Dash Icon 활성화
        /// </summary>
        private void ToggleDashIcon(int indexDash, bool isActive)
        {
            ListGameObjectDashIcon[indexDash].SetActive(isActive);

            if (isActive.Equals(false)) // 데쉬 아이콘을 비활성화 한다면
                ResetBeforeChargeGauge(indexDash); // 이전 차지 게이지를 리셋
        }

        /// <summary>
        /// 이전 차지 게이지를 리셋
        /// </summary>
        private void ResetBeforeChargeGauge(int indexDash)
        {
            if (indexDash + 1 < DR.PlayerStat.PlayerStatMap[DR.PlayerStatKeys.PLAYER_WOMAN].dash)
            {
                ChargeDashGaugeUI(indexDash + 1, 0.0f);
            }
        }

        /// <summary>
        /// 데쉬 게이지 UI 차지
        /// </summary>
        private void ChargeDashGaugeUI(int indexDash, float dashCharging)
        {
            ListImageDashBackGround[indexDash].fillAmount = dashCharging;
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 09.
 *  Information :   Information UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public class InformationUIController : MonoBehaviour // Data Field
    {
        /// <summary> Player Menu GameObject </summary>
        [SerializeField] private GameObject _curMenu = default;
        [SerializeField] private GameObject _backGround;
        [SerializeField] private GameObject _selectMenu;

        /// <summary>
        /// Get Player Menu GameObject의 활성화 / 비활성화
        /// </summary>
        public bool GetActivateGameObjectPlayerMenu() => _curMenu.activeSelf;

        public void OnMenuButtonClick(GameObject menu) => _curMenu = menu;

        /// <summary>
        /// Player Menu UI 활성화 / 비활성화
        /// </summary>
        public void ToggleVisiblePlayerMenuUI(bool visible)
        {
            _curMenu.SetActive(visible);
            _backGround.SetActive(visible);
            _selectMenu.SetActive(visible);
        }

        private void Allocate()
        {
            MainSystem.Instance.SceneManager.UIManager.SignUpInformationUIController(this);
        }

        private void Initialize()
        {
        }

        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainSystem.Instance.SceneManager.UIManager.SetActiveInfomationMenu(false);
            }
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 26.
 *  Information :   Map UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MapUIController : MonoBehaviour // Data Field
    {
    }

    /// <summary>
    /// Stage
    /// </summary>
    public partial class MapUIController : MonoBehaviour // Stage
    {
        /// <summary>
        /// �������� ����
        /// </summary>
        public void EnterStage()
        {
            MainSystem.Instance.SceneManager.UIManager.ToggleGlobalMapUI(false); // Map UI ��Ȱ��ȭ
            MainSystem.Instance.SceneManager.SceneLoadAsync("4_Stage"); // �������� ����
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 04.
 *  Information :   Main System Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Main
    /// </summary>
    public partial class MainSystemTrigger : MonoBehaviour //Main
    {
        private void Awake()
        {
            MainSystem mainStstem = MainSystem.Instance;

            FinishMainSystemStart();
        }

    }

    /// <summary>
    /// Property
    /// </summary>
    public partial class MainSystemTrigger : MonoBehaviour //Property
    {
        public void FinishMainSystemStart()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("1_InitializeScene"))
            {
                MainSystem.Instance.SceneManager.SceneLoadStandard("2_Main_Ui");
            }
        }
    }
}
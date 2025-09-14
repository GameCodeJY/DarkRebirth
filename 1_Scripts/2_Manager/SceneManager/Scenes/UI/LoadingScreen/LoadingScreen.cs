/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 17.
 *  Information :   Loading Screen
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class LoadingScreen : MonoBehaviour //Data Field
    {
        #region Serialize Field
        /// <summary> loading Progress Bar </summary>
        [SerializeField] private Slider sliderLoadingProgressBar = default;
        /// <summary> �ε��� �̹��� </summary>
        [SerializeField] private Image ImageLoadingProgressBar = default;
        #endregion

        #region Member Value
        /// <summary> Finish Scene Loading Flag </summary>
        protected bool isFinishSceneLoading = false;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class LoadingScreen : MonoBehaviour // Main
    {
        virtual protected void Update()
        {
            if (isFinishSceneLoading)
            {
                isFinishSceneLoading = false;
                MainSystem.Instance.SceneManager.SceneLoadingDone();
            }
        }        
    }

    /// <summary>
    /// Initailize
    /// </summary>
    public partial class LoadingScreen : MonoBehaviour // Initailize
    {
        private void Allocate() //�Ҵ�
        {

        }
        public void Initialize()
        {
            Allocate();
        }
        
    }

    /// <summary>
    /// Property
    /// </summary>
    public partial class LoadingScreen : MonoBehaviour // Property
    {
        virtual public void RefreshLoadingProgressBar(float value)
        {
            value = value * 100 + 10;
            ImageLoadingProgressBar.fillAmount = value;
        }

        public void FinishSceneLoading()
        {
            isFinishSceneLoading = true;
            Debug.Log("Finish Scene Loading");
        }

        protected void KeyCheckProgress()
        {
            if(Input.anyKeyDown)
            {
                isFinishSceneLoading = false;
                MainSystem.Instance.SceneManager.SceneLoadingDone();
            }
        }
    }
}

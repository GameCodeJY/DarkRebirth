/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 16.
 *  Information :   Story Loading Screen
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class StoryLoadingScreen : LoadingScreen // Data Field
    {
        #region Const Value
        /// <summary> Story Progress Time </summary>
        private float TIME_STORY_PROGRESS = 28.067f;
        #endregion

        #region Member Value
        /// <summary> Story Progress Time </summary>
        private float TimeStoryProgress = 0.0f;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class StoryLoadingScreen : LoadingScreen // Main
    {
        private void Start()
        {
            MainSystem.Instance.SceneManager.UIManager.isShowStoryLoadingScreen = true;
        }

        protected override void Update()
        {
            TimeStoryProgress += Time.deltaTime;
            if (isFinishSceneLoading.Equals(true))
            {
                if (TimeStoryProgress >= TIME_STORY_PROGRESS)
                {
                    isFinishSceneLoading = false;
                    MainSystem.Instance.SceneManager.SceneLoadingDone();
                }

                KeyCheckProgress();
            }
        }
    }

    /// <summary>
    /// Property
    /// </summary>
    public partial class StoryLoadingScreen : LoadingScreen // Property
    {
        override public void RefreshLoadingProgressBar(float value)
        {
        }
    }
}
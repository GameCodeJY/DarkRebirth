/*
 *  Coder       :   JY
 *  Last Update :   2025. 04. 04.
 *  Information :   Scene Manager
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class SceneManager : BaseManager // Data Field
    {
        #region Member Value
        /// <summary> UI Manager </summary>
        public UIManager UIManager = default;
        private bool IsSceneLoading = false; 
        private AsyncOperation AsyncOperation = default;
        private ThreadPriority ThreadPriority = ThreadPriority.Normal;
        private LoadingScreen LoadingScreen = default;
        public BaseScene ActiveScene { get; private set; } = default;
        #endregion

    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class SceneManager : BaseManager // Initialize
    {
        protected override void Allocate()
        {
            UIManager = gameObject.AddComponent<UIManager>();
        }

        public override void Initialize()
        {
            Allocate();
            UIManager.Initialize();
        }
    }

    /// <summary>
    /// Sign up
    /// </summary>
    public partial class SceneManager : BaseManager // Sign up
    {
        public void SignupActiveScene(BaseScene baseSceneValue)
        {
            SceneLoadingDone(); //씬로딩이 끝났을떄 호출하는거

            if (ActiveScene != null)
                Destroy(ActiveScene.gameObject);

            ActiveScene = baseSceneValue;
        }
    }

    /// <summary>
    /// Property
    /// </summary>
    public partial class SceneManager : BaseManager // Property
    {
        public void SceneLoadStandard(string sceneName)
        {
            if(IsSceneLoading == false)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
                IsSceneLoading = true;
            }
        }

        public void SceneLoadAsync(string sceneName)
        {
            if(IsSceneLoading == false)
            {
                IsSceneLoading = true;

                LoadingScreenSetup(sceneName);
                StartCoroutine(SceneLoadingAsyncEnumerator(sceneName));
            }
        }

        private void ActiveSceneDestroy()
        {
            if(ActiveScene != null)
            {
                DestroyImmediate(ActiveScene.gameObject);
            }
        }

        private void StartOperation(string sceneName)
        {
            Application.backgroundLoadingPriority = ThreadPriority;
            AsyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            AsyncOperation.allowSceneActivation = false;
        }

        private bool CheckAsyncLoadingDone()
        {
            return AsyncOperation.progress >= 0.9f; 
        }

        public void SceneLoadingDone()
        {
            IsSceneLoading = false;

            ActiveSceneDestroy(); // 추가

            if (AsyncOperation != null)
                AsyncOperation.allowSceneActivation = true;
        }
    }
    public partial class SceneManager : BaseManager //Enumerator Field
    {
        private IEnumerator SceneLoadingAsyncEnumerator(string sceneName)
        {
            StartOperation(sceneName);

            while(CheckAsyncLoadingDone() == false)
            {
                yield return null;
                LoadingScreen.RefreshLoadingProgressBar(AsyncOperation.progress);
            }

            LoadingScreen.FinishSceneLoading();
            yield return null;            
        }
    }
    public partial class SceneManager : BaseManager // Screen SetUp
    {
        private void LoadingScreenSetup(string sceneName)
        {
            UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            // 씬 스크립트 별로 바꾸기
            LoadingScreen = Instantiate<GameObject>(Resources.Load<GameObject>(pathLoadingScreen)).GetComponent<LoadingScreen>();
            LoadingScreen.Initialize();
        }
    }
}

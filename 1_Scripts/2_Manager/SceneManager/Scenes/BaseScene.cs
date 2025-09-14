/*
 *  Coder       :   JY
 *  Last Update :   2024. 10. 21.
 *  Information :   Base Scene
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class BaseScene : MonoBehaviour // Data Field
    {
        [SerializeField] protected UnityEvent initializeEvent;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class BaseScene : MonoBehaviour //Main Function Field
    {
        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }
    }

    /// <summary>
    /// Initailize
    /// </summary>
    public partial class BaseScene : MonoBehaviour //Initailize 
    {
        private void Allocate() //생성
        {
            MainSystem.Instance.SceneManager.SignupActiveScene(this);
        }
        public virtual void Initialize()//정의
        {
            ExtendInitialize();
            initializeEvent?.Invoke();
        }
        protected virtual void ExtendInitialize()
        {

        }
    }

    /// <summary>
    /// Scene
    /// </summary>
    public partial class BaseScene : MonoBehaviour // Scene
    {
        public void SceneLoadStandard(string sceneName)
        {
            MainSystem.Instance.SceneManager.SceneLoadStandard(sceneName);
        }
        public void SceneLoadAsync(string sceneName)
        {
            MainSystem.Instance.SceneManager.SceneLoadAsync(sceneName);
        }
    }
}

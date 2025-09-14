/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 02.
 *  Information :   Main System
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MainSystem : GenericSingleton<MainSystem> //Data Field
    {
        #region Singleton Class
        public DataManager DataManager { get; private set; } = default;
        public SceneManager SceneManager { get; private set; } = default;
        public InstanceManager InstanceManager { get; private set; } = default;
        public CameraManager CameraManager { get; private set; } = default;
        public EventManager EventManager { get; private set; } = default;
        public ProcessManager ProcessManager { get; private set; } = default;
        public InputManager InputManager { get; private set; } = default;
        public TimeManager TimeManager { get; private set; } = default;
        public PoolManager PoolManager { get; private set; } = default;
        public PlayerManager PlayerManager { get; private set; } = default;
        public FieldManager FieldManager { get; private set; } = default;
        public ActionManager ActionManager { get; private set; } = default;
        public PlayerSkillManager PlayerSkillManager { get; private set; } = default;
        public AudioManager AudioManager { get; private set; } = default;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class MainSystem : GenericSingleton<MainSystem> // Initialize
    {
        private void Allocate() //»ý¼º
        {
            DataManager = gameObject.AddComponent<DataManager>();
            PlayerManager = gameObject.AddComponent<PlayerManager>();
            SceneManager = gameObject.AddComponent<SceneManager>();
            InstanceManager = gameObject.AddComponent<InstanceManager>();
            CameraManager = gameObject.AddComponent<CameraManager>();
            EventManager = gameObject.AddComponent<EventManager>();
            ProcessManager = gameObject.AddComponent<ProcessManager>();
            InputManager = gameObject.AddComponent<InputManager>();
            TimeManager = gameObject.AddComponent<TimeManager>();
            PoolManager = gameObject.AddComponent<PoolManager>();
            FieldManager = gameObject.AddComponent<FieldManager>();
            ActionManager = gameObject.AddComponent<ActionManager>();
            PlayerSkillManager = gameObject.AddComponent<PlayerSkillManager>();
            AudioManager = gameObject.AddComponent<AudioManager>();
        }
        public void Initialize()
        {
            Allocate();

            DataManager.Initialize();
            PlayerManager.Initialize();
            SceneManager.Initialize();
            InstanceManager.Initialize();
            CameraManager.Initialize();
            EventManager.Initialize();
            ProcessManager.Initialize();
            InputManager.Initialize();
            TimeManager.Initialize();
            PoolManager.Initialize();
            FieldManager.Initialize();
            ActionManager.Initialize();
            PlayerSkillManager.Initialize();
            AudioManager.Initialize();
        }
    }

    /// <summary>
    /// Init
    /// </summary>
    public partial class MainSystem : GenericSingleton<MainSystem> //Property
    {
        protected override void Init()
        {
            Initialize();
        }
    }
}


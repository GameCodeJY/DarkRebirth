/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 04.
 *  Information :   Input Manager
 */

namespace MainSystem
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;

    public enum ActionType
    {
        None,
        BaseAttack,
        Parry,
        Skill_1,
        Skill_2,
        Skill_3,
        Skill_4,
        Phaeling,
        FrontMove,
        BackMove,
        LeftMove,
        RightMove,
    }

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class InputManager : BaseManager // Data Field
    {
        #region Data Field
        private KeyBindingSO _userKeyBindingSO;
        private Dictionary<ActionType, KeyCode> _keyBinding = new Dictionary<ActionType, KeyCode>();

        private float _IsMouse0PressTime = 0;
        private bool _isChargeKeyPress = false;
        private bool _isInputReady = true;

        /// <summary> 차지 키를 누르고 있는 지 </summary>
        public bool IsChargeKeyPress { get => _isChargeKeyPress; private set => _isChargeKeyPress = value; }

        public float Mouse0PressDuration{ get { return Time.time - _IsMouse0PressTime; }}

        public bool IsInputReady { get => _isInputReady; set => _isInputReady = value; }
        #endregion

        #region Init
        protected override void Allocate() // 할당
        {
            _userKeyBindingSO = Resources.Load<KeyBindingSO>("UserKeyBingding");
        }

        public override void Initialize() // 초기화
        {
            Allocate();

            if (_userKeyBindingSO == null)
                return;

            // 유저가 저장한 키코드를 매핑
            for(int i = 0; i < _userKeyBindingSO.KeyBinding.Count; ++i)
            {
                _keyBinding.Add(_userKeyBindingSO.KeyBinding[i].ActionType, _userKeyBindingSO.KeyBinding[i].KeyCode);
            }
        }
        #endregion

        #region Public Function
        /// <summary>
        /// ActionType에 키를 눌렀는지 확인하는 용도
        /// </summary>
        public bool GetKeyDown(ActionType actionType)
        {
            KeyCode keyCode;

            // ActionType이 등록되어 있지 않다.
            if (_keyBinding.TryGetValue(actionType, out keyCode) == false)
                return false;

            return Input.GetKeyDown(keyCode);
        }

        public Vector3 GetMoveDirection()
        {
            Vector3 moveDirection = Vector3.zero;

            moveDirection.z += GetKeyDown(ActionType.FrontMove) ? 1f : 0f;
            moveDirection.z += GetKeyDown(ActionType.BackMove) ? -1f : 0f;
            moveDirection.x += GetKeyDown(ActionType.RightMove) ? 1f : 0f;
            moveDirection.x += GetKeyDown(ActionType.LeftMove) ? -1f : 0f;

            return moveDirection.normalized;
        }

        public ActionType GetSkillTypeBySkillType()
        {
            if (GetKeyDown(ActionType.Skill_1))
                return ActionType.Skill_1;

            if (GetKeyDown(ActionType.Skill_2))
                return ActionType.Skill_2;

            if (GetKeyDown(ActionType.Skill_3))
                return ActionType.Skill_3;

            if (GetKeyDown(ActionType.Skill_4))
                return ActionType.Skill_4;

            return ActionType.None;
        }

        /// <summary>
        /// 차징 키를 누르고 있는 지 체크 시작
        /// </summary>
        public void StartCheckPressChargeKey(KeyCode keyCodeCharge)
        {
            StartCoroutine(CheckPressChargeKey(keyCodeCharge));
        }
        #endregion

        #region Main
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _IsMouse0PressTime = Time.time;
            }
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 차징 키를 누르고 있는 지 체크
        /// </summary>
        private IEnumerator CheckPressChargeKey(KeyCode keyCodeCharge)
        {
            while (Input.GetKey(keyCodeCharge))
            {
                IsChargeKeyPress = true;
                MainSystem.Instance.PlayerManager.MainPlayer.transform.rotation = MainSystem.Instance.ActionManager.RotateToMousePoint();
                yield return null;
            }

            IsChargeKeyPress = false;
        }
        #endregion
    }
}
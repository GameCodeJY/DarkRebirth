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

        /// <summary> ���� Ű�� ������ �ִ� �� </summary>
        public bool IsChargeKeyPress { get => _isChargeKeyPress; private set => _isChargeKeyPress = value; }

        public float Mouse0PressDuration{ get { return Time.time - _IsMouse0PressTime; }}

        public bool IsInputReady { get => _isInputReady; set => _isInputReady = value; }
        #endregion

        #region Init
        protected override void Allocate() // �Ҵ�
        {
            _userKeyBindingSO = Resources.Load<KeyBindingSO>("UserKeyBingding");
        }

        public override void Initialize() // �ʱ�ȭ
        {
            Allocate();

            if (_userKeyBindingSO == null)
                return;

            // ������ ������ Ű�ڵ带 ����
            for(int i = 0; i < _userKeyBindingSO.KeyBinding.Count; ++i)
            {
                _keyBinding.Add(_userKeyBindingSO.KeyBinding[i].ActionType, _userKeyBindingSO.KeyBinding[i].KeyCode);
            }
        }
        #endregion

        #region Public Function
        /// <summary>
        /// ActionType�� Ű�� �������� Ȯ���ϴ� �뵵
        /// </summary>
        public bool GetKeyDown(ActionType actionType)
        {
            KeyCode keyCode;

            // ActionType�� ��ϵǾ� ���� �ʴ�.
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
        /// ��¡ Ű�� ������ �ִ� �� üũ ����
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
        /// ��¡ Ű�� ������ �ִ� �� üũ
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
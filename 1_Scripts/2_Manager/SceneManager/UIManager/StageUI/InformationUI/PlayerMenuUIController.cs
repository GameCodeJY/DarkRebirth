/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 11.
 *  Information :   Player Menu UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // Data Field
    {
        [Header("�˶� �̹���")]
        /// <summary> �÷��̾� ���� �˶� ������ GameObject </summary>
        [SerializeField] private GameObject GameObjectPlayerStatAlarmIcon = default;

        [Header("���� ����â")]
        /// <summary> UI Canvas </summary>
        [SerializeField] private Canvas CanvasUI = default;
        /// <summary> �÷��̾� ���� ���� UI�� �θ� RectTransform </summary>
        [SerializeField] private RectTransform ParentRectTransformPlayerStatExplanationUI = default;
        /// <summary> �÷��̾� ���� ���� UI RectTransform </summary>
        [SerializeField] private RectTransform RectTransformPlayerStatExplanationUI = default;
        /// <summary> �÷��̾� ���� ���� �ؽ�Ʈ </summary>
        [SerializeField] private Text TextPlayerStatExplanation = default;
        /// <summary> ���� UI�� Ȱ��ȭ �Ǿ��� �� </summary>
        private bool IsActivatePlayerStatExplanationUI = false;

        [Header("�⺻ �ɷ�ġ")]
        /// <summary> ���ݷ� Text </summary>
        [SerializeField] private Text TextATK = default;
        /// <summary> ü�� Text </summary>
        [SerializeField] private Text TextHP = default;
        /// <summary> ���׹̳� Text </summary>
        [SerializeField] private Text TextStamina = default;
        /// <summary> ���׹̳� ȸ���� Text </summary>
        [SerializeField] private Text TextStaminaRecovery = default;
        /// <summary> ���� Text </summary>
        [SerializeField] private Text TextAMR = default;
        /// <summary> �׸��� Text </summary>
        [SerializeField] private Text TextREP = default;
        /// <summary> ������ ���� Text </summary>
        [SerializeField] private Text TextDamageReduction = default;
        /// <summary> ���ݼӵ� ���� Text </summary>
        [SerializeField] private Text TextAttackSpeed = default;
        /// <summary> �̵��ӵ� Text </summary>
        [SerializeField] private Text TextMoveSpeed = default;
        /// <summary> ũ��Ƽ�� Ȯ�� Text </summary>
        [SerializeField] private Text TextCriticalProbability = default;
        /// <summary> ũ��Ƽ�� ������ Text </summary>
        [SerializeField] private Text TextCriticalPower = default;
        /// <summary> ��� Text </summary>
        [SerializeField] private Text TextLuck = default;

        [Header("����")]
        /// <summary> �� ���� Text </summary>
        [SerializeField] private Text TextTotalStat = default;
        /// <summary> ��(STR) Text </summary>
        [SerializeField] private Text TextStatSTR = default;
        /// <summary> ��ø(DEX) Text </summary>
        [SerializeField] private Text TextStatDEX = default;
        /// <summary> ����(INT) Text </summary>
        [SerializeField] private Text TextStatINT = default;
        /// <summary> ü��(CON) Text </summary>
        [SerializeField] private Text TextStatCon = default;
        /// <summary> ���(LUCK) Text </summary>
        [SerializeField] private Text TextStatLuck = default;

        /// <summary> DR System Player Stat </summary>
        private PlayerStatContext _playerContext = default;
        /// <summary> ���� ���̺� ������ </summary>
        private PlayerSaveData _originSaveData = default;
        /// <summary> ���� ���� ���� </summary>
        private PlayerSaveData _currentSaveData = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour, IInitializable // Main
    {
        public void Allocate()
        {
        }

        public void Initialize()
        {
            Allocate();
            IsActivatePlayerStatExplanationUI = false; // �÷��̾� ����â ��Ȱ��ȭ�� �ʱ�ȭ
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // Main
    {
        private void OnEnable()
        {
            Initialize();
            LoadSystemPlayerData(); // DR, DB ������
        }

        private void Update()
        {
            FollowMouseWithPlayerStatExplanationUI();
        }
    }

    /// <summary>
    /// Pointer
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // Pointer
    {
        /// <summary>
        /// Pointer Enter Event Trigger
        /// </summary>
        public void OnPointerEnter(string idPlayerStatExplanation)
        {
            IsActivatePlayerStatExplanationUI = true; // �÷��̾� ����â Ȱ��ȭ Flag
            ParentRectTransformPlayerStatExplanationUI.gameObject.SetActive(true); // ����â image�� �θ� Ȱ��ȭ
            SetPlayerStatExplanation(idPlayerStatExplanation); // �÷��̾� ���� ���� ����
        }

        /// <summary>
        /// Pointer Exit Event Trigger
        /// </summary>
        public void OnPointerExit()
        {
            IsActivatePlayerStatExplanationUI = false; // �÷��̾� ����â ��Ȱ��ȭ Flag
            ParentRectTransformPlayerStatExplanationUI.gameObject.SetActive(false); // ����â image�� �θ� ��Ȱ��ȭ
        }

        /// <summary>
        /// �÷��̾� ���� ����â�� ���콺 ��ġ ����
        /// </summary>
        private void FollowMouseWithPlayerStatExplanationUI()
        {
            if (IsActivatePlayerStatExplanationUI.Equals(false)) // ����â�� ��Ȱ��ȭ �� ����
                return;

            MainSystem.Instance.SceneManager.UIManager.FollowMouseWithUI(RectTransformPlayerStatExplanationUI, ParentRectTransformPlayerStatExplanationUI, CanvasUI);
        }


        /// <summary>
        /// �÷��̾� ���� ���� ����
        /// </summary>
        private void SetPlayerStatExplanation(string idPlayerStatExplanation)
        {
            TextPlayerStatExplanation.text = Language.UIText.UITextMap[idPlayerStatExplanation].Korean;
        }
    }

    /// <summary>
    /// Refresh
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // Refresh
    {
        /// <summary>
        /// Refresh Player Data
        /// </summary>
        private void LoadSystemPlayerData()
        {
            _playerContext = MainSystem.Instance.DataManager.GetCurrentStatContext();
            _originSaveData = DatabaseManager.AllPlayerSaveData[DatabaseManager.PlayerUID];

            _currentSaveData = new PlayerSaveData();

            RefreshPlayerMenuUIText();
            ResetPlayerMenuUITextColor();
        }


        /// <summary> Refresh Player Menu UI Text </summary>
        private void RefreshPlayerMenuUIText()
        {
            RefreshPlayerStatText(); // �⺻ �ɷ�ġ Text Refresh
            RefreshPlayerStatPointText(); // ���� Text Refresh
        }

        /// <summary>
        /// �÷��̾� ���� �ؽ�Ʈ ��������
        /// </summary>
        private void RefreshPlayerStatText()
        {
            TextATK.text = _playerContext.ATK.ToString();
            TextHP.text = _playerContext.HP.ToString();
            TextStamina.text = _playerContext.Stamina.ToString();
            TextStaminaRecovery.text = _playerContext.StaminaRecovery.ToString();
            TextAMR.text = _playerContext.AMR.ToString();
            TextREP.text = _playerContext.REP.ToString();
            TextDamageReduction.text =  _playerContext.DamageReduction.ToString();
            TextAttackSpeed.text =  _playerContext.AttackSpeed.ToString();
            TextMoveSpeed.text = _playerContext.MoveSpeed.ToString();
            TextCriticalProbability.text = _playerContext.CriticalProbability.ToString();
            TextCriticalPower.text = _playerContext.CriticalPower.ToString();
            TextLuck.text = _playerContext.Luck.ToString();
        }

        /// <summary>
        /// �÷��̾� ���� ����Ʈ �ؽ�Ʈ ��������
        /// </summary>
        private void RefreshPlayerStatPointText()
        {
            int remainStatPoint = GetRemainStatPoint();  // ���� ���� ����Ʈ
            GameObjectPlayerStatAlarmIcon.SetActive(remainStatPoint > 0 ? true : false); // �÷��̾� ���� ������ ���� �� �˶� ������ Ȱ��ȭ

            TextTotalStat.text = remainStatPoint.ToString();
            TextStatSTR.text = (_originSaveData.StrStat + _currentSaveData.StrStat).ToString();
            TextStatDEX.text = (_originSaveData.DexStat + _currentSaveData.DexStat).ToString();
            TextStatINT.text = (_originSaveData.IntStat + _currentSaveData.IntStat).ToString();
            TextStatCon.text = (_originSaveData.ConStat + _currentSaveData.ConStat).ToString();
            TextStatLuck.text = (_originSaveData.LuckStat + _currentSaveData.LuckStat).ToString();
        }

        /// <summary>
        /// ���� ����Ʈ�� ���� �÷��̾� �޴� UI �ؽ�Ʈ ���� ����
        /// </summary>
        private void ChangeStatTextColorByIsIncrese(bool isIncrese, Text statPoint, List<Text> listTextPlayerStat)
        {
            RefreshPlayerMenuUIText();

            // ����Ʈ�� ������ �ʷϻ����� �ƴϸ� �Ͼ������ ǥ�õȴ�.
            Color colorText = isIncrese ? Color.green : Color.white;

            // ���� ����Ʈ�� ����� ���� ����Ʈ�ϰ� ���õ� stat�� ������ ����
            statPoint.color = colorText;
            for (int i = 0; i < listTextPlayerStat.Count; i++)
            {
                listTextPlayerStat[i].color = colorText;
            }
        }

        /// <summary>
        /// Player Menu UI Text Color�� White�� �ʱ�ȭ
        /// </summary>
        private void ResetPlayerMenuUITextColor()
        {
            TextATK.color = Color.white;
            TextHP.color = Color.white;
            TextStamina.color = Color.white;
            TextStaminaRecovery.color = Color.white;
            TextAMR.color = Color.white;
            TextREP.color = Color.white;
            TextDamageReduction.color = Color.white;
            TextAttackSpeed.color = Color.white;
            TextMoveSpeed.color = Color.white;
            TextCriticalProbability.color = Color.white;
            TextCriticalPower.color = Color.white;
            TextLuck.color = Color.white;

            TextStatSTR.color = Color.white;
            TextStatDEX.color = Color.white;
            TextStatINT.color = Color.white;
            TextStatCon.color = Color.white;
            TextStatLuck.color = Color.white;
        }
    }

    /// <summary>
    /// ���� ����Ʈ ��ȭ
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // ���� ����Ʈ ��ȭ
    {
        /// <summary>
        /// ���� ���� ����Ʈ ���
        /// </summary>
        private int GetRemainStatPoint()
        {
            return _originSaveData.PlayerLevel - _originSaveData.StatPoint - _currentSaveData.StatPoint; // ���� ����Ʈ
        }

        /// <summary>
        /// ���� ����Ʈ ��ȭ
        /// </summary>
        private int ChangeStatPoint(string drStatupId, int stat, bool isIncresePoint)
        {
            // �ø��� ��ư�ε� ���� ���� ����Ʈ�� ������ return 
            if (isIncresePoint && GetRemainStatPoint() <= 0)
                return stat;

            // ���̳ʽ� ��ư�ε� ���� ���� ����Ʈ�� ������ return
            if (isIncresePoint == false && stat <= 0)
                return stat;

            var value = isIncresePoint ? 1 : -1;
            _currentSaveData.StatPoint += value;
            stat += value;

            MainSystem.Instance.DataManager.CalculatePlayerStatContext(drStatupId, value, ref _playerContext);

            return stat;
        }

        /// <summary>
        /// ��(STR) ���� ����Ʈ �� ��ȭ
        /// </summary>
        /// <param name="isIncresePoint"> true : ���� ����Ʈ ���� / false : ���� ����Ʈ ���� </param>
        public void ChangeSTRPoint(bool isIncresePoint)
        {
            _currentSaveData.StrStat = ChangeStatPoint(DR.StatupKeys.STR, _currentSaveData.StrStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.StrStat != 0, TextStatSTR, new List<Text> { TextATK, TextHP });
        }

        /// <summary>
        /// ��ø(DEX) ���� ����Ʈ �� ��ȭ
        /// </summary>
        /// <param name="isIncresePoint"> true : ���� ����Ʈ ���� / false : ���� ����Ʈ ���� </param>
        public void ChangeDEXPointValue(bool isIncresePoint)
        {
            _currentSaveData.DexStat = ChangeStatPoint(DR.StatupKeys.DEX, _currentSaveData.DexStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.DexStat != 0, TextStatDEX, new List<Text> { TextAttackSpeed, TextMoveSpeed });
        }

        /// <summary>
        /// ����(INT) ���� ����Ʈ �� ��ȭ
        /// </summary>
        /// <param name="isIncresePoint"> true : ���� ����Ʈ ���� / false : ���� ����Ʈ ���� </param>
        public void ChangeINTPointValue(bool isIncresePoint)
        {
            _currentSaveData.IntStat = ChangeStatPoint(DR.StatupKeys.INT, _currentSaveData.IntStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.IntStat != 0, TextStatINT, new List<Text> { TextREP, TextCriticalPower });
        }

        /// <summary>
        /// ü��(CON) ���� ����Ʈ �� ��ȭ
        /// </summary>
        /// <param name="isIncresePoint"> true : ���� ����Ʈ ���� / false : ���� ����Ʈ ���� </param>
        public void ChangeCONPointValue(bool isIncresePoint)
        {
            _currentSaveData.ConStat = ChangeStatPoint(DR.StatupKeys.CON, _currentSaveData.ConStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.ConStat != 0, TextStatCon, new List<Text> { TextHP, TextAMR });
        }

        /// <summary>
        /// ���(LUCK) ���� ����Ʈ �� ��ȭ
        /// </summary>
        /// <param name="isIncresePoint"> true : ���� ����Ʈ ���� / false : ���� ����Ʈ ���� </param>
        public void ChangeLUCKPointValue(bool isIncresePoint)
        {
            _currentSaveData.LuckStat = ChangeStatPoint(DR.StatupKeys.LUK, _currentSaveData.LuckStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.LuckStat != 0, TextStatLuck, new List<Text> { TextCriticalProbability, TextLuck });
        }
    }

    /// <summary>
    /// Apply
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // Apply
    {
        /// <summary>
        /// ���� ����Ʈ ����
        /// </summary>
        public void OnClickApplyButton()
        {
            if (_currentSaveData.StatPoint.Equals(0)) // ���� ����Ʈ ��ȭ�� ������ ���� ����
                return;

            // ���� �����Ϳ� ������� ���� �����͸� ����
            _originSaveData.StatPoint += _currentSaveData.StatPoint;
            _originSaveData.StrStat += _currentSaveData.StrStat;
            _originSaveData.ConStat += _currentSaveData.ConStat;
            _originSaveData.IntStat += _currentSaveData.IntStat;
            _originSaveData.DexStat+= _currentSaveData.DexStat;
            _originSaveData.LuckStat += _currentSaveData.LuckStat;

            // DB ����
            DatabaseManager.SavePlayerSaveData(_originSaveData);
            MainSystem.Instance.DataManager.InitializeSystemPlayerStat();

            LoadSystemPlayerData();
        }

        /// <summary>
        /// ���� ����Ʈ ����
        /// </summary>
        public void OnClickResetButton()
        {
            // ToDo : ���� ���� ���� // ��� �ı� ����Ҷ�

            // ���� �� �ʱ�ȭ
            _originSaveData.StatPoint = 0;
            _originSaveData.StrStat = 0;
            _originSaveData.DexStat = 0;
            _originSaveData.IntStat = 0;
            _originSaveData.ConStat = 0;
            _originSaveData.LuckStat = 0;

            // DB����
            DatabaseManager.SavePlayerSaveData(_originSaveData);
            MainSystem.Instance.DataManager.InitializeSystemPlayerStat();

            LoadSystemPlayerData();
        }
    }
}
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
        [Header("알람 이미지")]
        /// <summary> 플레이어 스탯 알람 아이콘 GameObject </summary>
        [SerializeField] private GameObject GameObjectPlayerStatAlarmIcon = default;

        [Header("스탯 설명창")]
        /// <summary> UI Canvas </summary>
        [SerializeField] private Canvas CanvasUI = default;
        /// <summary> 플레이어 스탯 설명 UI의 부모 RectTransform </summary>
        [SerializeField] private RectTransform ParentRectTransformPlayerStatExplanationUI = default;
        /// <summary> 플레이어 스탯 설명 UI RectTransform </summary>
        [SerializeField] private RectTransform RectTransformPlayerStatExplanationUI = default;
        /// <summary> 플레이어 스탯 설명 텍스트 </summary>
        [SerializeField] private Text TextPlayerStatExplanation = default;
        /// <summary> 설명 UI가 활성화 되었는 지 </summary>
        private bool IsActivatePlayerStatExplanationUI = false;

        [Header("기본 능력치")]
        /// <summary> 공격력 Text </summary>
        [SerializeField] private Text TextATK = default;
        /// <summary> 체력 Text </summary>
        [SerializeField] private Text TextHP = default;
        /// <summary> 스테미나 Text </summary>
        [SerializeField] private Text TextStamina = default;
        /// <summary> 스테미나 회복력 Text </summary>
        [SerializeField] private Text TextStaminaRecovery = default;
        /// <summary> 방어력 Text </summary>
        [SerializeField] private Text TextAMR = default;
        /// <summary> 항마력 Text </summary>
        [SerializeField] private Text TextREP = default;
        /// <summary> 데미지 감소 Text </summary>
        [SerializeField] private Text TextDamageReduction = default;
        /// <summary> 공격속도 감소 Text </summary>
        [SerializeField] private Text TextAttackSpeed = default;
        /// <summary> 이동속도 Text </summary>
        [SerializeField] private Text TextMoveSpeed = default;
        /// <summary> 크리티컬 확률 Text </summary>
        [SerializeField] private Text TextCriticalProbability = default;
        /// <summary> 크리티컬 데미지 Text </summary>
        [SerializeField] private Text TextCriticalPower = default;
        /// <summary> 행운 Text </summary>
        [SerializeField] private Text TextLuck = default;

        [Header("스탯")]
        /// <summary> 총 스탯 Text </summary>
        [SerializeField] private Text TextTotalStat = default;
        /// <summary> 힘(STR) Text </summary>
        [SerializeField] private Text TextStatSTR = default;
        /// <summary> 민첩(DEX) Text </summary>
        [SerializeField] private Text TextStatDEX = default;
        /// <summary> 지능(INT) Text </summary>
        [SerializeField] private Text TextStatINT = default;
        /// <summary> 체력(CON) Text </summary>
        [SerializeField] private Text TextStatCon = default;
        /// <summary> 행운(LUCK) Text </summary>
        [SerializeField] private Text TextStatLuck = default;

        /// <summary> DR System Player Stat </summary>
        private PlayerStatContext _playerContext = default;
        /// <summary> 원본 세이브 데이터 </summary>
        private PlayerSaveData _originSaveData = default;
        /// <summary> 현재 찍은 스탯 </summary>
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
            IsActivatePlayerStatExplanationUI = false; // 플레이어 설명창 비활성화로 초기화
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
            LoadSystemPlayerData(); // DR, DB 데이터
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
            IsActivatePlayerStatExplanationUI = true; // 플레이어 설명창 활성화 Flag
            ParentRectTransformPlayerStatExplanationUI.gameObject.SetActive(true); // 설명창 image의 부모 활성화
            SetPlayerStatExplanation(idPlayerStatExplanation); // 플레이어 스탯 설명 셋팅
        }

        /// <summary>
        /// Pointer Exit Event Trigger
        /// </summary>
        public void OnPointerExit()
        {
            IsActivatePlayerStatExplanationUI = false; // 플레이어 설명창 비활성화 Flag
            ParentRectTransformPlayerStatExplanationUI.gameObject.SetActive(false); // 설명창 image의 부모 비활성화
        }

        /// <summary>
        /// 플레이어 스탯 설명창이 마우스 위치 따라감
        /// </summary>
        private void FollowMouseWithPlayerStatExplanationUI()
        {
            if (IsActivatePlayerStatExplanationUI.Equals(false)) // 설명창이 비활성화 시 무시
                return;

            MainSystem.Instance.SceneManager.UIManager.FollowMouseWithUI(RectTransformPlayerStatExplanationUI, ParentRectTransformPlayerStatExplanationUI, CanvasUI);
        }


        /// <summary>
        /// 플레이어 스탯 설명 셋팅
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
            RefreshPlayerStatText(); // 기본 능력치 Text Refresh
            RefreshPlayerStatPointText(); // 스탯 Text Refresh
        }

        /// <summary>
        /// 플레이어 스탯 텍스트 리프레쉬
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
        /// 플레이어 스탯 포인트 텍스트 리프레쉬
        /// </summary>
        private void RefreshPlayerStatPointText()
        {
            int remainStatPoint = GetRemainStatPoint();  // 남은 스탯 포인트
            GameObjectPlayerStatAlarmIcon.SetActive(remainStatPoint > 0 ? true : false); // 플레이어 남은 스탯이 있을 때 알람 아이콘 활성화

            TextTotalStat.text = remainStatPoint.ToString();
            TextStatSTR.text = (_originSaveData.StrStat + _currentSaveData.StrStat).ToString();
            TextStatDEX.text = (_originSaveData.DexStat + _currentSaveData.DexStat).ToString();
            TextStatINT.text = (_originSaveData.IntStat + _currentSaveData.IntStat).ToString();
            TextStatCon.text = (_originSaveData.ConStat + _currentSaveData.ConStat).ToString();
            TextStatLuck.text = (_originSaveData.LuckStat + _currentSaveData.LuckStat).ToString();
        }

        /// <summary>
        /// 스탯 포인트에 따른 플레이어 메뉴 UI 텍스트 색깔 변경
        /// </summary>
        private void ChangeStatTextColorByIsIncrese(bool isIncrese, Text statPoint, List<Text> listTextPlayerStat)
        {
            RefreshPlayerMenuUIText();

            // 포인트를 찍으면 초록색으로 아니면 하얀색으로 표시된다.
            Color colorText = isIncrese ? Color.green : Color.white;

            // 찍은 포인트에 색상과 찍은 포인트하고 관련된 stat에 색상을 변경
            statPoint.color = colorText;
            for (int i = 0; i < listTextPlayerStat.Count; i++)
            {
                listTextPlayerStat[i].color = colorText;
            }
        }

        /// <summary>
        /// Player Menu UI Text Color를 White로 초기화
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
    /// 스탯 포인트 변화
    /// </summary>
    public partial class PlayerMenuUIController : MonoBehaviour // 스탯 포인트 변화
    {
        /// <summary>
        /// 남은 스탯 포인트 얻기
        /// </summary>
        private int GetRemainStatPoint()
        {
            return _originSaveData.PlayerLevel - _originSaveData.StatPoint - _currentSaveData.StatPoint; // 남은 포인트
        }

        /// <summary>
        /// 스탯 포인트 변화
        /// </summary>
        private int ChangeStatPoint(string drStatupId, int stat, bool isIncresePoint)
        {
            // 올리는 버튼인데 남은 스탯 포인트가 없으면 return 
            if (isIncresePoint && GetRemainStatPoint() <= 0)
                return stat;

            // 마이너스 버튼인데 찍은 스택 포인트가 없으면 return
            if (isIncresePoint == false && stat <= 0)
                return stat;

            var value = isIncresePoint ? 1 : -1;
            _currentSaveData.StatPoint += value;
            stat += value;

            MainSystem.Instance.DataManager.CalculatePlayerStatContext(drStatupId, value, ref _playerContext);

            return stat;
        }

        /// <summary>
        /// 힘(STR) 스탯 포인트 값 변화
        /// </summary>
        /// <param name="isIncresePoint"> true : 스탯 포인트 증가 / false : 스탯 포인트 감소 </param>
        public void ChangeSTRPoint(bool isIncresePoint)
        {
            _currentSaveData.StrStat = ChangeStatPoint(DR.StatupKeys.STR, _currentSaveData.StrStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.StrStat != 0, TextStatSTR, new List<Text> { TextATK, TextHP });
        }

        /// <summary>
        /// 민첩(DEX) 스탯 포인트 값 변화
        /// </summary>
        /// <param name="isIncresePoint"> true : 스탯 포인트 증가 / false : 스탯 포인트 감소 </param>
        public void ChangeDEXPointValue(bool isIncresePoint)
        {
            _currentSaveData.DexStat = ChangeStatPoint(DR.StatupKeys.DEX, _currentSaveData.DexStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.DexStat != 0, TextStatDEX, new List<Text> { TextAttackSpeed, TextMoveSpeed });
        }

        /// <summary>
        /// 지능(INT) 스탯 포인트 값 변화
        /// </summary>
        /// <param name="isIncresePoint"> true : 스탯 포인트 증가 / false : 스탯 포인트 감소 </param>
        public void ChangeINTPointValue(bool isIncresePoint)
        {
            _currentSaveData.IntStat = ChangeStatPoint(DR.StatupKeys.INT, _currentSaveData.IntStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.IntStat != 0, TextStatINT, new List<Text> { TextREP, TextCriticalPower });
        }

        /// <summary>
        /// 체력(CON) 스탯 포인트 값 변화
        /// </summary>
        /// <param name="isIncresePoint"> true : 스탯 포인트 증가 / false : 스탯 포인트 감소 </param>
        public void ChangeCONPointValue(bool isIncresePoint)
        {
            _currentSaveData.ConStat = ChangeStatPoint(DR.StatupKeys.CON, _currentSaveData.ConStat, isIncresePoint);
            ChangeStatTextColorByIsIncrese(_currentSaveData.ConStat != 0, TextStatCon, new List<Text> { TextHP, TextAMR });
        }

        /// <summary>
        /// 행운(LUCK) 스탯 포인트 값 변화
        /// </summary>
        /// <param name="isIncresePoint"> true : 스탯 포인트 증가 / false : 스탯 포인트 감소 </param>
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
        /// 스탯 포인트 적용
        /// </summary>
        public void OnClickApplyButton()
        {
            if (_currentSaveData.StatPoint.Equals(0)) // 스탯 포인트 변화가 없으면 적용 안함
                return;

            // 기존 데이터에 현재까지 찍힌 데이터를 더함
            _originSaveData.StatPoint += _currentSaveData.StatPoint;
            _originSaveData.StrStat += _currentSaveData.StrStat;
            _originSaveData.ConStat += _currentSaveData.ConStat;
            _originSaveData.IntStat += _currentSaveData.IntStat;
            _originSaveData.DexStat+= _currentSaveData.DexStat;
            _originSaveData.LuckStat += _currentSaveData.LuckStat;

            // DB 저장
            DatabaseManager.SavePlayerSaveData(_originSaveData);
            MainSystem.Instance.DataManager.InitializeSystemPlayerStat();

            LoadSystemPlayerData();
        }

        /// <summary>
        /// 스탯 포인트 리셋
        /// </summary>
        public void OnClickResetButton()
        {
            // ToDo : 루인 조건 연동 // 장비 파괴 기능할때

            // 기존 값 초기화
            _originSaveData.StatPoint = 0;
            _originSaveData.StrStat = 0;
            _originSaveData.DexStat = 0;
            _originSaveData.IntStat = 0;
            _originSaveData.ConStat = 0;
            _originSaveData.LuckStat = 0;

            // DB저장
            DatabaseManager.SavePlayerSaveData(_originSaveData);
            MainSystem.Instance.DataManager.InitializeSystemPlayerStat();

            LoadSystemPlayerData();
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 22.
 *  Information :   Field Manager
 */

namespace MainSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// 방 종류
    /// </summary>
    public enum EnumRoomType
    {
        Start,
        SkillSelect,
        Gold,
        Combat,
        Shop,
        Statue,
        NoReward,
        NPC
    }

    /// <summary>
    /// Field Controller Counter
    /// </summary>
    public class FieldControllerCounter
    {
        public string FieldCode;
        public int Count;

        public FieldControllerCounter(string fieldCode, int count)
        {
            FieldCode = fieldCode;
            Count = count;
        }
    }

    public class FieldManager : BaseManager
    {
        #region Member Value
        /// <summary> New Field Controller Dictionary </summary>
        private Dictionary<string, FieldController> _fieldControllerGroup = default;
        /// <summary> 선택된 Field Controller ID들 [int 값은 몇번 선택이 되었는 지] </summary>
        private Queue<FieldControllerCounter> _queueSelectedFieldController = default;
        /// <summary> 현재 선택된 필드 </summary>
        private FieldController _selectedFieldController = default;
        /// <summary> 방 종류 </summary>
        private EnumRoomType _selectedRoomType = EnumRoomType.NoReward;
        /// <summary> 현재 필드 숫자 </summary>
        private int _nowFieldNumber = 0;
        #endregion

        #region Property
        public FieldController SelectedFieldController { get => _selectedFieldController; set => _selectedFieldController = value; }
        public EnumRoomType SelectedRoomType { get => _selectedRoomType; set => _selectedRoomType = value; }
        public int NowFieldNumber { get => _nowFieldNumber; set => _nowFieldNumber = value; }
        #endregion

        #region Initialize
        protected override void Allocate()
        {
            base.Allocate();

            _fieldControllerGroup = new Dictionary<string, FieldController>();
            _queueSelectedFieldController = new Queue<FieldControllerCounter>();
        }

        /// <summary>
        /// Field Number 초기화
        /// </summary>
        public void InitializeFieldNumber()
        {
            _nowFieldNumber = 0;
        }
        #endregion

        #region Sign Up
        /// <summary>
        /// Sign Up Field Controller
        /// </summary>
        public void SignUpNewFieldController(FieldController newFieldController)
        {
            string fieldCode = newFieldController.FieldCode;

            // 이미 그룹에 FieldController가 추가 되어있거나, 시작 필드일 경우는 제외
            if (_fieldControllerGroup.ContainsKey(fieldCode) || newFieldController.FieldType.field_type.Equals(0))
                return;

            _fieldControllerGroup[fieldCode] = newFieldController;
        }

        /// <summary>
        /// 시작 FieldController 등록
        /// </summary>
        public void RegisterInitialFieldController(FieldController initialFieldController)
        {
            _selectedFieldController = initialFieldController;
        }

        /// <summary>
        /// 필드 메니져 데이터 리셋
        /// </summary>
        public void ResetFieldManagerData()
        {
            _fieldControllerGroup.Clear();
            _queueSelectedFieldController.Clear();
            _selectedFieldController = null;
            _selectedRoomType = EnumRoomType.NoReward;
            _nowFieldNumber = 0;
        }
        #endregion

        #region Select Field 
        /// <summary>
        /// 확률에 맞는 인덱스값 선택
        /// </summary>
        /// /// <typeparam name="T">확률과 ID를 가진 요소 타입</typeparam>
        /// <param name="elements">확률과 ID 요소 리스트</param>
        /// <param name="probabilitySelector">요소에서 확률을 꺼내오는 함수</param>
        /// <param name="idSelector">요소에서 ID를 꺼내오는 함수</param>
        /// <returns>선택된 요소의 ID</returns>
        private string SelectIDByProbabilities<T>(List<T> elements, Func<T, float> probabilitySelector, Func<T, string> idSelector)
        {
            // 1) 전체 확률 합계 계산
            float totalProb = elements.Sum(probabilitySelector);

            // 2) 0 ~ totalProb 범위의 난수 생성
            float rnd = UnityEngine.Random.Range(0f, totalProb);

            // 3) 누적 확률 비교
            foreach (var element in elements)
            {
                float prob = probabilitySelector(element);

                if (rnd <= prob)
                    return idSelector(element);

                rnd -= prob;
            }

            // 안전망: 리스트 마지막 요소의 ID 반환
            return idSelector(elements.Last());
        }

        /// <summary>
        /// 방 종류 선택
        /// rooms에 담긴 Probability 값에 비례해서 하나를 랜덤 선택
        /// </summary>
        private EnumRoomType SelectRoomType(List<Probability.RoomType> roomTypeList)
        {
            // 방 종류의 확률을 기반으로 선택 후 인덱스 반환
            string selectedIndex = SelectIDByProbabilities(
                roomTypeList,
                rt => rt.probabilty,
                rt => rt.room_type
                );

            // 선택된 방 종류를 EnumRoomType으로 변환
            EnumRoomType selectedRoomType = Enum.Parse<EnumRoomType>(selectedIndex);

            _selectedRoomType = selectedRoomType; // 맴버로 저장

            return selectedRoomType;
        }

        /// <summary>
        /// 해당 필드 타입을 가지고 있는 지 체크
        /// </summary>
        public bool HasFieldTypeInFieldControllers(Dictionary<string, FieldController> fieldControllerGroup, Func<DR.FieldType, bool> matches)
        {
            return fieldControllerGroup
                .Any(kv => matches(kv.Value.FieldType));
        }

        /// <summary>
        /// 선택된 방 종류에 따른 FieldController들 얻기
        /// </summary>
        private Dictionary<string, FieldController> SelectFieldControllersByRoomType(Dictionary<string, FieldController> fieldControllerGroup)
        {
            List<Probability.RoomType> roomTypeList = new List<Probability.RoomType>(Probability.RoomType.RoomTypeList);
            int countRoomTypeList = roomTypeList.Count;

            // 룸 타입이 골라질 때까지 반복
            for (int i = 0; i < countRoomTypeList; i++)
            {
                var roomType = SelectRoomType(roomTypeList);

                // roomType에 따른 검사 조건 미리 정의
                Func<DR.FieldType, bool> matches = roomType switch
                {
                    EnumRoomType.Combat => ft => ft.combat == 1,
                    EnumRoomType.Gold => ft => ft.gold == 1,
                    EnumRoomType.NoReward => ft => ft.no_reward == 1,
                    EnumRoomType.NPC => ft => ft.npc == 1,
                    EnumRoomType.Shop => ft => ft.shop == 1,
                    EnumRoomType.SkillSelect => ft => ft.skill_select == 1,
                    EnumRoomType.Statue => ft => ft.statue == 1,
                    _ => ft => false
                };

                // 해당 필드 타입을 가지고 있는 지 체크
                if (HasFieldTypeInFieldControllers(fieldControllerGroup, matches))
                {
                    // 가지고 있으면 해당 필드 반환
                    return fieldControllerGroup
                            .Where(kv => matches(kv.Value.FieldType))
                            .ToDictionary(kv => kv.Key, kv => kv.Value);
                }
                else
                {
                    // 없으면 필드 타입 리스트에서 제거
                    // 첫 번째로 매칭되는 요소를 찾아서 삭제
                    var match = roomTypeList.Find(rt => Enum.Parse<EnumRoomType>(rt.room_type) == roomType);
                    roomTypeList.Remove(match);
                }
            }

            return null; // 해당되는 필드가 없음
        }


        /// <summary>
        /// 방 출구 개수 정하기
        /// </summary>
        private int SelectRoomExitCount()
        {
            return 2;
        }

        /// <summary>
        /// 방 출구 개수에 따른 FieldController들 얻기
        /// </summary>
        private Dictionary<string, FieldController> SelectFieldControllersByRoomExitCount(Dictionary<string, FieldController> fieldControllerGroup)
        {
            return fieldControllerGroup
                .Where(entry => entry.Value.FieldType.max_exit == SelectRoomExitCount())
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// 선택된 FieldController가 queue에 이미 포함이 되어있는 지
        /// </summary>
        private bool CheckSameFieldControllerInSelectedQueue(string fieldCode)
        {
            // 존재할때
            if (_queueSelectedFieldController.Any(kvp => kvp.FieldCode == fieldCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 확률에 따른 FieldController들 얻기
        /// </summary>
        private FieldController SelectFieldControllersByProbability(Dictionary<string, FieldController> fieldControllerGroup)
        {
            var fieldControllerProbability = new Dictionary<string, float>();
            var keys = fieldControllerGroup.Keys.ToList();
            int count = keys.Count;
            float baseProb = 1f / count;
            float accumulated = 0f;

            for (int i = 0; i < count; i++)
            {
                if (i < count - 1)
                {
                    fieldControllerProbability[keys[i]] = baseProb;
                    accumulated += baseProb;
                }
                else
                {
                    // 마지막 항목에 1 - (이전까지 누적값) 만큼 할당
                    fieldControllerProbability[keys[i]] = 1f - accumulated;
                }
            }

            // 4) 선택되었던 FieldController는 선택 확률 보정
            float totalAdjustment = 0.0f;
            int queueCount = 0;
            foreach (var fieldController in _queueSelectedFieldController)
            {
                if (fieldControllerGroup.ContainsKey(fieldController.FieldCode))
                {
                    queueCount += fieldController.Count;

                    float originProbability = fieldControllerProbability[fieldController.FieldCode];
                    fieldControllerProbability[fieldController.FieldCode] *= Mathf.Pow(0.7f, fieldController.Count);
                    totalAdjustment += originProbability - fieldControllerProbability[fieldController.FieldCode];
                }
            }

            float adjustment = totalAdjustment / (fieldControllerGroup.Count - queueCount);
            foreach (var fieldController in fieldControllerGroup)
            {
                // 존재하지 않을때
                if (CheckSameFieldControllerInSelectedQueue(fieldController.Key).Equals(false))
                {
                    fieldControllerProbability[fieldController.Key] += adjustment;
                }
            }

            // 1) Dictionary를 List<KeyValuePair<string,float>>로 변환
            var probList = fieldControllerProbability.ToList();

            // 2) 제네릭 메서드 호출
            string selectedKey = SelectIDByProbabilities(
                probList,
                kvp => kvp.Value,  // 확률 값 추출
                kvp => kvp.Key     // ID(키) 추출
            );

            return fieldControllerGroup[selectedKey];
        }

        /// <summary>
        /// 선택되었던 큐의 개수 얻기
        /// </summary>
        private int GetCountQueueSelectedFieldController()
        {
            int queueCount = 0;

            foreach (var queue in _queueSelectedFieldController)
            {
                queueCount += queue.Count;
            }

            return queueCount;
        }

        /// <summary>
        /// 선택된 FieldController Queue를 Dequeue할 지 Check
        /// </summary>
        private void TryDequeueSelectedFieldController()
        {
            if (GetCountQueueSelectedFieldController() >= 3)
            {
                var fieldController = _queueSelectedFieldController.Peek();
                if (fieldController.Count > 1)
                {
                    fieldController.Count -= 1;
                }
                else
                {
                    _queueSelectedFieldController.Dequeue();
                }
            }
        }

        /// <summary>
        /// 선택된 FieldController 삽입
        /// </summary>
        private void EnqueueSelectedFieldController(string fieldCode)
        {
            // 제거 시도 먼저
            TryDequeueSelectedFieldController();

            // queue가 아직 생성되지 않았다면 초기화
            if (_queueSelectedFieldController == null)
                _queueSelectedFieldController = new Queue<FieldControllerCounter>();


            // 선택된 FieldController가 queue에 이미 포함이 되어있는 지
            if (CheckSameFieldControllerInSelectedQueue(fieldCode).Equals(true))
            {
                // 카운트만 증가
                _queueSelectedFieldController
                    .First(counter => counter.FieldCode == fieldCode)
                    .Count++;
            }
            else // 포함이 안 되어있다면
            {
                _queueSelectedFieldController.Enqueue(new FieldControllerCounter(fieldCode, 1));
            }
        }

        /// <summary>
        /// 필드 선택
        /// </summary>
        private FieldController SelectNextField()
        {
            // 1. 방 종류에 따른 필드 선택
            var selectedFieldControllers = SelectFieldControllersByRoomType(_fieldControllerGroup);

            // 2. 방 출구 개수에 따른 필드 선택
            selectedFieldControllers = SelectFieldControllersByRoomExitCount(selectedFieldControllers);

            // 3. 선택된 필드들 중에 확률에 맞게 필드 하나 선택
            var selectFieldController = SelectFieldControllersByProbability(selectedFieldControllers);

            //4. 선택된 필드 Queue에 삽입하여 기록
            EnqueueSelectedFieldController(selectFieldController.FieldCode);

            // 5. 선택된 필드 현재 Field Controller로 맴버 저장
            _selectedFieldController = selectFieldController;

            return selectFieldController;
        }

        /// <summary>
        /// 필드 바꾸기
        /// </summary>
        public FieldController ChangeField()
        {
            // 1. 현재 필드, 방 종류 가져오기
            FieldController nowFieldController = _selectedFieldController;
            EnumRoomType nowRoomType = _selectedRoomType;

            // 2. 기존 필드의 방 설정 리셋
            nowFieldController.FieldSettingEvent?.Invoke(false);

            // 3. 다음 필드, 방 종류 선택
            FieldController nextFieldController = SelectNextField();
            EnumRoomType nextRoomType = _selectedRoomType;

            // 4. 선택된 방 종류에 따라 필드 셋팅
            nextFieldController.FieldSettingEvent?.Invoke(true);

            if (nextRoomType != EnumRoomType.Shop)
            {
                // 5. 현재 필드 넘버 증가
                _nowFieldNumber += 1;
            }

            return _selectedFieldController;
        }
        #endregion
    }
}
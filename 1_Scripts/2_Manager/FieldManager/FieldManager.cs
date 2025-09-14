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
    /// �� ����
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
        /// <summary> ���õ� Field Controller ID�� [int ���� ��� ������ �Ǿ��� ��] </summary>
        private Queue<FieldControllerCounter> _queueSelectedFieldController = default;
        /// <summary> ���� ���õ� �ʵ� </summary>
        private FieldController _selectedFieldController = default;
        /// <summary> �� ���� </summary>
        private EnumRoomType _selectedRoomType = EnumRoomType.NoReward;
        /// <summary> ���� �ʵ� ���� </summary>
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
        /// Field Number �ʱ�ȭ
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

            // �̹� �׷쿡 FieldController�� �߰� �Ǿ��ְų�, ���� �ʵ��� ���� ����
            if (_fieldControllerGroup.ContainsKey(fieldCode) || newFieldController.FieldType.field_type.Equals(0))
                return;

            _fieldControllerGroup[fieldCode] = newFieldController;
        }

        /// <summary>
        /// ���� FieldController ���
        /// </summary>
        public void RegisterInitialFieldController(FieldController initialFieldController)
        {
            _selectedFieldController = initialFieldController;
        }

        /// <summary>
        /// �ʵ� �޴��� ������ ����
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
        /// Ȯ���� �´� �ε����� ����
        /// </summary>
        /// /// <typeparam name="T">Ȯ���� ID�� ���� ��� Ÿ��</typeparam>
        /// <param name="elements">Ȯ���� ID ��� ����Ʈ</param>
        /// <param name="probabilitySelector">��ҿ��� Ȯ���� �������� �Լ�</param>
        /// <param name="idSelector">��ҿ��� ID�� �������� �Լ�</param>
        /// <returns>���õ� ����� ID</returns>
        private string SelectIDByProbabilities<T>(List<T> elements, Func<T, float> probabilitySelector, Func<T, string> idSelector)
        {
            // 1) ��ü Ȯ�� �հ� ���
            float totalProb = elements.Sum(probabilitySelector);

            // 2) 0 ~ totalProb ������ ���� ����
            float rnd = UnityEngine.Random.Range(0f, totalProb);

            // 3) ���� Ȯ�� ��
            foreach (var element in elements)
            {
                float prob = probabilitySelector(element);

                if (rnd <= prob)
                    return idSelector(element);

                rnd -= prob;
            }

            // ������: ����Ʈ ������ ����� ID ��ȯ
            return idSelector(elements.Last());
        }

        /// <summary>
        /// �� ���� ����
        /// rooms�� ��� Probability ���� ����ؼ� �ϳ��� ���� ����
        /// </summary>
        private EnumRoomType SelectRoomType(List<Probability.RoomType> roomTypeList)
        {
            // �� ������ Ȯ���� ������� ���� �� �ε��� ��ȯ
            string selectedIndex = SelectIDByProbabilities(
                roomTypeList,
                rt => rt.probabilty,
                rt => rt.room_type
                );

            // ���õ� �� ������ EnumRoomType���� ��ȯ
            EnumRoomType selectedRoomType = Enum.Parse<EnumRoomType>(selectedIndex);

            _selectedRoomType = selectedRoomType; // �ɹ��� ����

            return selectedRoomType;
        }

        /// <summary>
        /// �ش� �ʵ� Ÿ���� ������ �ִ� �� üũ
        /// </summary>
        public bool HasFieldTypeInFieldControllers(Dictionary<string, FieldController> fieldControllerGroup, Func<DR.FieldType, bool> matches)
        {
            return fieldControllerGroup
                .Any(kv => matches(kv.Value.FieldType));
        }

        /// <summary>
        /// ���õ� �� ������ ���� FieldController�� ���
        /// </summary>
        private Dictionary<string, FieldController> SelectFieldControllersByRoomType(Dictionary<string, FieldController> fieldControllerGroup)
        {
            List<Probability.RoomType> roomTypeList = new List<Probability.RoomType>(Probability.RoomType.RoomTypeList);
            int countRoomTypeList = roomTypeList.Count;

            // �� Ÿ���� ����� ������ �ݺ�
            for (int i = 0; i < countRoomTypeList; i++)
            {
                var roomType = SelectRoomType(roomTypeList);

                // roomType�� ���� �˻� ���� �̸� ����
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

                // �ش� �ʵ� Ÿ���� ������ �ִ� �� üũ
                if (HasFieldTypeInFieldControllers(fieldControllerGroup, matches))
                {
                    // ������ ������ �ش� �ʵ� ��ȯ
                    return fieldControllerGroup
                            .Where(kv => matches(kv.Value.FieldType))
                            .ToDictionary(kv => kv.Key, kv => kv.Value);
                }
                else
                {
                    // ������ �ʵ� Ÿ�� ����Ʈ���� ����
                    // ù ��°�� ��Ī�Ǵ� ��Ҹ� ã�Ƽ� ����
                    var match = roomTypeList.Find(rt => Enum.Parse<EnumRoomType>(rt.room_type) == roomType);
                    roomTypeList.Remove(match);
                }
            }

            return null; // �ش�Ǵ� �ʵ尡 ����
        }


        /// <summary>
        /// �� �ⱸ ���� ���ϱ�
        /// </summary>
        private int SelectRoomExitCount()
        {
            return 2;
        }

        /// <summary>
        /// �� �ⱸ ������ ���� FieldController�� ���
        /// </summary>
        private Dictionary<string, FieldController> SelectFieldControllersByRoomExitCount(Dictionary<string, FieldController> fieldControllerGroup)
        {
            return fieldControllerGroup
                .Where(entry => entry.Value.FieldType.max_exit == SelectRoomExitCount())
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        /// <summary>
        /// ���õ� FieldController�� queue�� �̹� ������ �Ǿ��ִ� ��
        /// </summary>
        private bool CheckSameFieldControllerInSelectedQueue(string fieldCode)
        {
            // �����Ҷ�
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
        /// Ȯ���� ���� FieldController�� ���
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
                    // ������ �׸� 1 - (�������� ������) ��ŭ �Ҵ�
                    fieldControllerProbability[keys[i]] = 1f - accumulated;
                }
            }

            // 4) ���õǾ��� FieldController�� ���� Ȯ�� ����
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
                // �������� ������
                if (CheckSameFieldControllerInSelectedQueue(fieldController.Key).Equals(false))
                {
                    fieldControllerProbability[fieldController.Key] += adjustment;
                }
            }

            // 1) Dictionary�� List<KeyValuePair<string,float>>�� ��ȯ
            var probList = fieldControllerProbability.ToList();

            // 2) ���׸� �޼��� ȣ��
            string selectedKey = SelectIDByProbabilities(
                probList,
                kvp => kvp.Value,  // Ȯ�� �� ����
                kvp => kvp.Key     // ID(Ű) ����
            );

            return fieldControllerGroup[selectedKey];
        }

        /// <summary>
        /// ���õǾ��� ť�� ���� ���
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
        /// ���õ� FieldController Queue�� Dequeue�� �� Check
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
        /// ���õ� FieldController ����
        /// </summary>
        private void EnqueueSelectedFieldController(string fieldCode)
        {
            // ���� �õ� ����
            TryDequeueSelectedFieldController();

            // queue�� ���� �������� �ʾҴٸ� �ʱ�ȭ
            if (_queueSelectedFieldController == null)
                _queueSelectedFieldController = new Queue<FieldControllerCounter>();


            // ���õ� FieldController�� queue�� �̹� ������ �Ǿ��ִ� ��
            if (CheckSameFieldControllerInSelectedQueue(fieldCode).Equals(true))
            {
                // ī��Ʈ�� ����
                _queueSelectedFieldController
                    .First(counter => counter.FieldCode == fieldCode)
                    .Count++;
            }
            else // ������ �� �Ǿ��ִٸ�
            {
                _queueSelectedFieldController.Enqueue(new FieldControllerCounter(fieldCode, 1));
            }
        }

        /// <summary>
        /// �ʵ� ����
        /// </summary>
        private FieldController SelectNextField()
        {
            // 1. �� ������ ���� �ʵ� ����
            var selectedFieldControllers = SelectFieldControllersByRoomType(_fieldControllerGroup);

            // 2. �� �ⱸ ������ ���� �ʵ� ����
            selectedFieldControllers = SelectFieldControllersByRoomExitCount(selectedFieldControllers);

            // 3. ���õ� �ʵ�� �߿� Ȯ���� �°� �ʵ� �ϳ� ����
            var selectFieldController = SelectFieldControllersByProbability(selectedFieldControllers);

            //4. ���õ� �ʵ� Queue�� �����Ͽ� ���
            EnqueueSelectedFieldController(selectFieldController.FieldCode);

            // 5. ���õ� �ʵ� ���� Field Controller�� �ɹ� ����
            _selectedFieldController = selectFieldController;

            return selectFieldController;
        }

        /// <summary>
        /// �ʵ� �ٲٱ�
        /// </summary>
        public FieldController ChangeField()
        {
            // 1. ���� �ʵ�, �� ���� ��������
            FieldController nowFieldController = _selectedFieldController;
            EnumRoomType nowRoomType = _selectedRoomType;

            // 2. ���� �ʵ��� �� ���� ����
            nowFieldController.FieldSettingEvent?.Invoke(false);

            // 3. ���� �ʵ�, �� ���� ����
            FieldController nextFieldController = SelectNextField();
            EnumRoomType nextRoomType = _selectedRoomType;

            // 4. ���õ� �� ������ ���� �ʵ� ����
            nextFieldController.FieldSettingEvent?.Invoke(true);

            if (nextRoomType != EnumRoomType.Shop)
            {
                // 5. ���� �ʵ� �ѹ� ����
                _nowFieldNumber += 1;
            }

            return _selectedFieldController;
        }
        #endregion
    }
}
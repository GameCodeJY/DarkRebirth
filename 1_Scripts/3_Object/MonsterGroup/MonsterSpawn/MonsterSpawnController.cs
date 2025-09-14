/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 18
 *  Information :   Monster Spawn Controller
 */

namespace MainSystem
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class MonsterSpawnController : MonoBehaviour // Data Field
    {
        /// <summary> ���̺� ���� ���� �ð� (3, 6, 9) </summary>
        private List<float> TIME_SPAWN_MONSTER_WAVE = default;

        /// <summary> ���� ���� ���� ��ġ ����Ʈ </summary>
        [SerializeField] private List<Transform> _listTransformMonsterSpawnSockets = default;
        /// <summary> ���� ���� ���� ��ġ ����Ʈ </summary>
        private List<List<int>> _listMonsterWave;
        /// <summary> ������ �ִ� ���̺� �� </summary>
        private int _maxWaveCount = default;
        /// <summary> ���� ���̺� �� </summary>
        private int _nowNumberWave = 0;
        /// <summary> ���� ���� �� </summary>
        private int _monsterCount = 0;
        /// <summary> ���� ���̺갡 ������ �̺�Ʈ </summary>
        [SerializeField] private UnityEvent _eventCompleteMonsterWave = default;

        #region Property
        public UnityEvent EventCompleteMonsterWave { get => _eventCompleteMonsterWave; set => _eventCompleteMonsterWave = value; }
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class MonsterSpawnController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            TIME_SPAWN_MONSTER_WAVE = new List<float> { 6.0f, 9.0f, 12.0f };
        }

        public void Initialize()
        {
            Allocate();
        }

        /// <summary>
        /// ���̺� ���� �ʱ�ȭ
        /// </summary>
        private void InitializeWaveValue()
        {
            _maxWaveCount = UnityEngine.Random.Range(2, 6); // �ִ� ���̺� �� ���� (2 ~ 5ȸ�� ���̺갡 ����)
            _nowNumberWave = 0;
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class MonsterSpawnController : MonoBehaviour // Main
    {
        private void Start()
        {
            Initialize();
        }
    }

    /// <summary>
    /// Spawn
    /// </summary>
    public partial class MonsterSpawnController : MonoBehaviour // Spawn
    {
        /// <summary>
        /// ���� ���̺� �Ҵ�
        /// </summary>
        private void AllocateListMonsterWave()
        {
            // ���� �ʵ� ��ȣ + 1 �� �ش��ϴ� DR ������ ��������
            int drFieldNumber = MainSystem.Instance.FieldManager.NowFieldNumber + 1;
            var fieldData = DR.Field.FieldMap[drFieldNumber.ToString()];

            // monster_a ~ monster_f_wave_id ���ڿ����� �迭�� �غ�
            var waveIdStrings = new[]
            {
                fieldData.monster_a_wave_id,
                fieldData.monster_b_wave_id,
                fieldData.monster_c_wave_id,
                fieldData.monster_d_wave_id,
                fieldData.monster_e_wave_id,
                fieldData.monster_f_wave_id,
                fieldData.monster_g_wave_id
            };

            // �� ���ڿ��� "/"�� split �ؼ� int ����Ʈ�� ��ȯ�� ��, ����Ʈ ����Ʈ�� ��ȯ
            _listMonsterWave = waveIdStrings
                .Select(waveIds => waveIds
                    .Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
                )
                .ToList();
        }

        /// <summary>
        /// ���͵� ����
        /// </summary>
        public void OnStartMonsterWaves()
        {
            // ���̺� ���� �ʱ�ȭ
            InitializeWaveValue();

            // ���� ���̺� �Ҵ�
            AllocateListMonsterWave();

            // ���ο��� ���� ����
            TryUpdateWave();
        }

        /// <summary>
        /// ���� ���̺� ����
        /// </summary>
        private void SpawnMonsterWave()
        {
            List<int> listMonsterType = _listMonsterWave[UnityEngine.Random.Range(0, _listMonsterWave.Count)];

            // ���� ���̺갡 0�� ��� ���� ó�� ����.
            if (listMonsterType[0].Equals(0))
            {
                TryUpdateWave();
                return;
            }

            for (int i = 0; i < listMonsterType.Count; i++)
            {
                if (DR.MonStat.MonStatMap.ContainsKey(listMonsterType[i].ToString()) == false)
                    continue;

                Transform spownSoket = _listTransformMonsterSpawnSockets[UnityEngine.Random.Range(0, _listTransformMonsterSpawnSockets.Count)];

                BaseMonster monster = SpawnMonster(spownSoket, listMonsterType[i].ToString());
                Coroutine coroutine = StartCoroutine(ActiveMonster(monster.gameObject, spownSoket));
            }

            _nowNumberWave += 1; // ���� ���̺� ����
        }

        public BaseMonster SpawnMonster(Transform spownSoket, string monsterId)
        {
            BaseMonster monster = MainSystem.Instance.PoolManager.GetMonster(monsterId);
            monster.transform.parent = spownSoket.transform;
            // TODO: ���ʹ� Reset����� ���� position�� �ʱ�ȭ ������ �ϴ� ����
            monster.transform.position = spownSoket.transform.position;

            monster.RegisterMonsterEvent(this);

            _monsterCount += 1;

            return monster;
        }

        public IEnumerator ActiveMonster(GameObject monster, Transform soketTransform)
        {
            monster.SetActive(false);
            Instantiate(Resources.Load<GameObject>("Monster/MonsterSpawnEffect"), soketTransform);

            yield return new WaitForSeconds(2f);

            monster.SetActive(true);
        }

        /// <summary>
        /// ���� ���̺갡 ������ �� üũ
        /// </summary>
        public bool CheckMonsterWaveEnd() => IsMaxWave() == true && _monsterCount <= 0;

        public void MonsterDie(BaseMonster monster)
        {
            _monsterCount -= 1;
            if (_monsterCount > 0)
                return;

            TryUpdateWave();
        }

        /// <summary>
        /// Update Wave ����
        /// </summary>
        private void TryUpdateWave()
        {
            if (IsMaxWave() == true) // �ִ� ���̺� ���̶�� �ݺ�
                return;

            StartCoroutine(UpdateWave());
        }

        private IEnumerator UpdateWave()
        {
            SpawnMonsterWave();

            yield return new WaitForSeconds(TIME_SPAWN_MONSTER_WAVE[UnityEngine.Random.Range(0, TIME_SPAWN_MONSTER_WAVE.Count)]); // 3, 6, 9�� �ϳ��� �����ð��� ����

            TryUpdateWave();
        }

        private bool IsMaxWave()
        {
            return _maxWaveCount <= _nowNumberWave;
        }

        public void TryCompleteMonsterWave(BaseMonster baseMonster)
        {
            if (CheckMonsterWaveEnd() == true)
            {
                _eventCompleteMonsterWave?.Invoke();
            }
        }
    }
}
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
        /// <summary> 웨이브 스폰 랜덤 시간 (3, 6, 9) </summary>
        private List<float> TIME_SPAWN_MONSTER_WAVE = default;

        /// <summary> 몬스터 스폰 소켓 위치 리스트 </summary>
        [SerializeField] private List<Transform> _listTransformMonsterSpawnSockets = default;
        /// <summary> 몬스터 스폰 소켓 위치 리스트 </summary>
        private List<List<int>> _listMonsterWave;
        /// <summary> 설정된 최대 웨이브 수 </summary>
        private int _maxWaveCount = default;
        /// <summary> 현재 웨이브 수 </summary>
        private int _nowNumberWave = 0;
        /// <summary> 현재 몬스터 수 </summary>
        private int _monsterCount = 0;
        /// <summary> 몬스터 웨이브가 끝나는 이벤트 </summary>
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
        /// 웨이브 변수 초기화
        /// </summary>
        private void InitializeWaveValue()
        {
            _maxWaveCount = UnityEngine.Random.Range(2, 6); // 최대 웨이브 수 설정 (2 ~ 5회의 웨이브가 생성)
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
        /// 몬스터 웨이브 할당
        /// </summary>
        private void AllocateListMonsterWave()
        {
            // 현재 필드 번호 + 1 에 해당하는 DR 데이터 가져오기
            int drFieldNumber = MainSystem.Instance.FieldManager.NowFieldNumber + 1;
            var fieldData = DR.Field.FieldMap[drFieldNumber.ToString()];

            // monster_a ~ monster_f_wave_id 문자열들을 배열로 준비
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

            // 각 문자열을 "/"로 split 해서 int 리스트로 변환한 뒤, 리스트 리스트로 변환
            _listMonsterWave = waveIdStrings
                .Select(waveIds => waveIds
                    .Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList()
                )
                .ToList();
        }

        /// <summary>
        /// 몬스터들 스폰
        /// </summary>
        public void OnStartMonsterWaves()
        {
            // 웨이브 변수 초기화
            InitializeWaveValue();

            // 몬스터 웨이브 할당
            AllocateListMonsterWave();

            // 내부에서 몬스터 생성
            TryUpdateWave();
        }

        /// <summary>
        /// 몬스터 웨이브 스폰
        /// </summary>
        private void SpawnMonsterWave()
        {
            List<int> listMonsterType = _listMonsterWave[UnityEngine.Random.Range(0, _listMonsterWave.Count)];

            // 몬스터 웨이브가 0일 경우 로직 처리 안함.
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

            _nowNumberWave += 1; // 현재 웨이브 증가
        }

        public BaseMonster SpawnMonster(Transform spownSoket, string monsterId)
        {
            BaseMonster monster = MainSystem.Instance.PoolManager.GetMonster(monsterId);
            monster.transform.parent = spownSoket.transform;
            // TODO: 몬스터는 Reset기능을 통해 position이 초기화 되지만 일단 넣음
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
        /// 몬스터 웨이브가 끝났는 지 체크
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
        /// Update Wave 시작
        /// </summary>
        private void TryUpdateWave()
        {
            if (IsMaxWave() == true) // 최대 웨이브 전이라면 반복
                return;

            StartCoroutine(UpdateWave());
        }

        private IEnumerator UpdateWave()
        {
            SpawnMonsterWave();

            yield return new WaitForSeconds(TIME_SPAWN_MONSTER_WAVE[UnityEngine.Random.Range(0, TIME_SPAWN_MONSTER_WAVE.Count)]); // 3, 6, 9중 하나의 스폰시간을 설정

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
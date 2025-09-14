/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 30.
 *  Information :   Field Controller
 */

namespace MainSystem
{
    using UnityEngine;
    using UnityEngine.Events;
    using DR;

    public class FieldController : MonoBehaviour
    {
        #region Member Value
        /// <summary> DR의 필드 코드 </summary>
        [SerializeField] private string _fieldCode = default;
        /// <summary> 필드별 카메라 회전 값 </summary>
        [SerializeField] private float _cameraRotationDegree = default;
        /// <summary> DR의 Field Type </summary>
        private DR.FieldType _fieldType = default;
        /// <summary> 필드 셋팅 이벤트 </summary>
        [SerializeField] private UnityEvent<bool> _fieldSettingEvent = default;
        /// <summary> 필드 시작 이벤트 </summary>
        [SerializeField] private UnityEvent _fieldInitialEvent = default;
        #endregion

        #region Property
        public string FieldCode { get => _fieldCode; private set => _fieldCode = value; }
        public FieldType FieldType { get => _fieldType; private set => _fieldType = value; }
        public UnityEvent<bool> FieldSettingEvent { get => _fieldSettingEvent; set => _fieldSettingEvent = value; }
        public UnityEvent FieldInitialEvent { get => _fieldInitialEvent; private set => _fieldInitialEvent = value; }
        #endregion

        #region Initialize
        private void Allocate()
        {
            _fieldType = FieldType.GetDictionary()[_fieldCode];

            AllocateFieldInitialEvent();

            MainSystem.Instance.FieldManager.SignUpNewFieldController(this);
        }

        /// <summary>
        /// FieldInitialEvent에 함수 할당
        /// </summary>
        private void AllocateFieldInitialEvent()
        {
            _fieldInitialEvent.AddListener(SetCameraRotation);
        }

        private void Initialize()
        {
        }
        #endregion

        #region Unity LifeCycle
        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }
        #endregion

        #region Camera
        /// <summary>
        /// 카메라 회전 셋팅
        /// </summary>
        private void SetCameraRotation()
        {
            MainSystem.Instance.CameraManager.MainCameraController.SetCameraRotation(_cameraRotationDegree);
        }
        #endregion 
    }
}
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
        /// <summary> DR�� �ʵ� �ڵ� </summary>
        [SerializeField] private string _fieldCode = default;
        /// <summary> �ʵ庰 ī�޶� ȸ�� �� </summary>
        [SerializeField] private float _cameraRotationDegree = default;
        /// <summary> DR�� Field Type </summary>
        private DR.FieldType _fieldType = default;
        /// <summary> �ʵ� ���� �̺�Ʈ </summary>
        [SerializeField] private UnityEvent<bool> _fieldSettingEvent = default;
        /// <summary> �ʵ� ���� �̺�Ʈ </summary>
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
        /// FieldInitialEvent�� �Լ� �Ҵ�
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
        /// ī�޶� ȸ�� ����
        /// </summary>
        private void SetCameraRotation()
        {
            MainSystem.Instance.CameraManager.MainCameraController.SetCameraRotation(_cameraRotationDegree);
        }
        #endregion 
    }
}
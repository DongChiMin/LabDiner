using UnityEngine;
using LabDiner.LevelSystem.Domain;
using System.Collections.Generic;
using LabDiner.Restaurant.SO;
using LabDiner.Restaurant.Interface;
using LabDiner.Restaurant.Event;
using LabDiner.Shared;

namespace LabDiner.Restaurant.Managers
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelConfigEvent _onLevelInit;
        [SerializeField] private LevelConfigEvent _onLevelIntroStart;

        [Header("Level Load")]
        [SerializeField] private IntEvent _onLevelComplete;

        [Header("Level Init")]
        [SerializeField] private List<Transform> _initRoots;

        [Header("Level Progress")]
        [SerializeField] private ProgressRuntimeSO _progressRuntimeSO;


        void OnEnable()
        {
            _onLevelComplete.Register(SaveLevel);
        }

        void OnDisable()
        {
            _onLevelComplete.Unregister(SaveLevel);
        }

        private void SaveLevel(int levelCompleted)
        {
            _progressRuntimeSO.PlayerSave.SetCurrentLevelIndex(levelCompleted + 1); 
        }

        public void LoadLevel(LevelConfigSO configSO)
        {
            StopAllCoroutines();
            // Phase 1: Sinh ra thực thể/Sắp xếp thực thể vào list
            ExecutePhase1_SetupLayout();

            // Phase 2: Bơm dữ liệu cấu hình
            ExecutePhase2_InitLogic(configSO);

            // Phase 3: Khôi phục tiến độ (Mồi cho cậu triển khai sau)
            ExecutePhase3_RestoreProgress(configSO);
        }

        private void ExecutePhase1_SetupLayout()
        {
            foreach (var root in _initRoots)
            {
                ILevelRebuildable[] initializables = root.GetComponentsInChildren<ILevelRebuildable>();
                foreach (var init in initializables)
                {
                    init.Rebuild();
                }
            }
        }

        private void ExecutePhase2_InitLogic(LevelConfigSO configSO)
        {
            //Init theo thứ tự
            foreach (var root in _initRoots)
            {
                ILevelInitializable[] initializables = root.GetComponentsInChildren<ILevelInitializable>();
                foreach (var init in initializables)
                {
                    init.Init(configSO);
                }
            }

            //Cuối cùng thì mới Init các object ko cần thứ tự
            _onLevelInit.Raise(configSO);
        }

        private void ExecutePhase3_RestoreProgress(LevelConfigSO configSO)
        {
            _progressRuntimeSO.Init();
            LevelProgressSave progress = _progressRuntimeSO.LevelProgressSave;

            //Nếu chưa xem intro == bắt đầu chơi mới level này
            //- Đưa danh sách nhiệm vụ và upgrade vào progress để sau này lưu lại
            //- Set lại cờ đã xem intro = true
            //- Raise sự kiện bắt đầu intro
            if (!progress.hasSeenIntro)
            {
                _progressRuntimeSO.InitializeProgress(configSO);
                _progressRuntimeSO.LevelProgressSave.UpdateHasSeenIntro(true);
                _onLevelIntroStart.Raise(configSO);
            }

            //Nếu đã xem intro rồi
            //- Raise event để các thành phần trong game tự lấy tiến độ chơi và cập nhật
            else
            {
                _progressRuntimeSO.OnProgressInject?.Invoke();
            }
        }

        
    }
}

using LabDiner.Shared;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LabDiner.Player
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private float dragThreshold = 0.2f; // Khoảng cách để tính là drag

        private IDraggable _currentDragging;
        private IClickable _currentClicking;
        private Vector3 _clickStartPosition;
        private bool _isDraggingActive;

        private void Start()
        {
            // Đăng ký nhận sự kiện từ trạm phát Global
            GlobalInputReader.Instance.OnInputStarted += HandleInputStarted;
            GlobalInputReader.Instance.OnInputCanceled += HandleInputCanceled;
        }

        private void OnDestroy()
        {
            // Hủy đăng ký để tránh lỗi memory leak khi chuyển scene
            if (GlobalInputReader.Instance != null)
            {
                GlobalInputReader.Instance.OnInputStarted -= HandleInputStarted;
                GlobalInputReader.Instance.OnInputCanceled -= HandleInputCanceled;
            }
        }

        private void Update()
        {
            // Nếu đang trong trạng thái drag, cập nhật vị trí liên tục
            if (_isDraggingActive && _currentDragging != null)
            {
                _currentDragging.OnDragContinue(GlobalInputReader.Instance.GetPointerWorldPosition());
            }
        }

        private void HandleInputStarted(Vector2 screenPos)
        {
            // 1. CHẶN CLICK XUYÊN UI
            // Nếu chuột đang đè lên một phần tử UI (Canvas), không xử lý logic game bên dưới
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector3 worldPos = GlobalInputReader.Instance.GetPointerWorldPosition();
            _clickStartPosition = worldPos;

            // 2. RAYCAST CÓ LAYERMASK
            // Chỉ tương tác với các Object thuộc Layer bạn cho phép (tránh nắm nhầm Background)
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, interactableLayer);

            if (hit.collider != null)
            {
                // TH 1: Kiểm tra xem có thể Click không (Lưu lại để dành khi thả tay)
                if (hit.collider.TryGetComponent(out IClickable clickable))
                {
                    _currentClicking = clickable;
                    //Sau khi click , có thể sẽ tạo ra object có thể drag 
                    IDraggable handedOverDraggable = _currentClicking.OnPointerDown(worldPos);

                    if (handedOverDraggable != null)
                    {
                        _currentDragging = handedOverDraggable;
                        _currentDragging.OnDragStart(worldPos);
                        _isDraggingActive = true;
                        return; // Thoát sớm vì đã tìm thấy vật để Drag
                    }
                }

                // TH 2: Kiểm tra xem có thể Drag không
                if (hit.collider.TryGetComponent(out IDraggable draggable))
                {
                    _currentDragging = draggable;
                    _currentDragging.OnDragStart(worldPos);
                    _isDraggingActive = true;
                }
            }
        }

        private void HandleInputCanceled(Vector2 screenPos)
        {
            if (_currentDragging == null && _currentClicking == null) return;

            Vector3 worldPos = GlobalInputReader.Instance.GetPointerWorldPosition();
            float moveDistance = Vector3.Distance(_clickStartPosition, worldPos);

            // Nếu di chuyển ít -> Ưu tiên Click
            if (moveDistance < dragThreshold)
            {
                _currentClicking?.OnPointerUp(worldPos);
            }

            // Nếu đang drag thì kết thúc
            if (_currentDragging != null)
            {
                _currentDragging.OnDragEnd();
            }

            // Dọn dẹp
            _currentDragging = null;
            _currentClicking = null;
            _isDraggingActive = false;
        }
    }
}
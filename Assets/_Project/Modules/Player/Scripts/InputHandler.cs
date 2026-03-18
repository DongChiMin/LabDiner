using System.Collections.Generic;
using LabDiner.Shared;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
            if (IsPointerOverUI(screenPos))
            {
                // Nếu đè lên UI thì thoát, không xử lý logic game (như gắp nguyên liệu)
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

        private bool IsPointerOverUI(Vector2 screenPosition)
        {
            // Tạo một sự kiện pointer giả lập tại vị trí click
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = screenPosition;

            // Danh sách chứa các kết quả va chạm với UI
            List<RaycastResult> results = new List<RaycastResult>();

            // Yêu cầu EventSystem thực hiện Raycast ngay lập tức tại vị trí đó
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            // Nếu danh sách kết quả > 0, nghĩa là chuột đang đè lên UI
            return results.Count > 0;
        }
    }
}
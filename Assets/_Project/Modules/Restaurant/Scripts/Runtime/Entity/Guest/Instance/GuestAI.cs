using System.Collections;
using LabDiner.Restaurant.Environment;
using LabDiner.Restaurant.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Humanoid
{
    public class GuestAI : MonoBehaviour
    {
        [SerializeField] private GuestEvent _onGuestLeft;
        [SerializeField] private DiningSeatEvent _onTableDirty;
        [SerializeField] GuestContext _context;

        //Các biến lưu trữ MainRoutine
        private DiningSeat _targetSeat;
        private Vector3 _exitPos;
        void OnEnable()
        {
            _targetSeat = null;
            _exitPos = Vector3.zero;
        }

        // // Đọc luồng logic ở đây như một câu chuyện
        public IEnumerator MainRoutine(Vector3 destination, Vector3 exitPos, DiningSeat seat = null)
        {
            _targetSeat = seat;
            _exitPos = exitPos;

            // 1. Đi đến ghế hoặc waitingLine
            yield return MoveTo(destination);

            //2.1. Nếu là đi đến waitingLine
            if(_targetSeat == null)
            {
                yield return _context.CtxBehavior.WaitInLine();
            }

            // 2.2. Nếu đã được chỉ định ghế ngay từ đầu, họ sẽ chờ phục vụ đến nhận order
            yield return _context.CtxBehavior.WaitForServe(_targetSeat);

            // 3. Đợi mang đồ ăn đến
            yield return _context.CtxBehavior.WaitForFood();

            // 4. Thực hiện hành động ăn
            yield return _context.CtxBehavior.Eat();

            // 5. Trả tiền (Dễ dàng thêm bước mới vào giữa)
            yield return _context.CtxBehavior.Pay();

            // 6. Giải phóng ghế (chuyển ghế đó thành ghế bẩn, chưa ngồi được)
            _onTableDirty.Raise(_targetSeat);

            // 7. Đi ra cửa
            yield return MoveTo(exitPos);

            // 8. Biến mất
            _onGuestLeft.Raise(_context);
        }

        public IEnumerator LeaveRoutine(Vector3 exitPos)
        {
            yield return MoveTo(exitPos);
            _onGuestLeft.Raise(_context);
        }

        IEnumerator MoveTo(Vector3 destination)
        {
            // Không cần check moveCoroutine thủ công nữa, cứ chạy trực tiếp
            yield return StartCoroutine(_context.CtxMover.MoveTo(destination));
        }

        public void FromWaitingLineToDiningSeat(DiningSeat seat)
        {
            StopAllCoroutines();
            _targetSeat = seat;
            StartCoroutine(MainRoutine(seat.transform.position, _exitPos, seat));
        }

        public void LeaveAngry()
        {
            StopAllCoroutines();
            StartCoroutine(LeaveRoutine(_exitPos));
        }

        void LateUpdate() => _context.CtxMover.SetZToZero();
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEventSO : ScriptableObject
    {
        // Danh sách các hàm sẽ chạy khi event được gọi
        private readonly List<IGameEventListener> eventListeners = new List<IGameEventListener>();

        public void Raise()
        {
            // Duyệt ngược danh sách để tránh lỗi nếu có ai đó tự hủy đăng ký khi đang chạy
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(IGameEventListener listener) => eventListeners.Add(listener);
        public void UnregisterListener(IGameEventListener listener) => eventListeners.Remove(listener);
    }

    // Interface để các Script khác kế thừa và lắng nghe
    public interface IGameEventListener
    {
        void OnEventRaised();
    }
}
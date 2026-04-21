using UnityEngine;

namespace LabDiner.Shared
{
    [ExecuteAlways] // Dòng này giúp script chạy trong cả Edit Mode và Play Mode
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        private Rect lastSafeArea = Rect.zero;

        void Awake()
        {
            if(rectTransform == null)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            gameObject.SetActive(true);
        }

        void Start()
        {
            ApplySafeArea();
        }

#if UNITY_EDITOR
        void Update()
        {
            // Khi ở chế độ Editor, Update vẫn sẽ chạy, 
            // nhưng chúng ta chỉ tính toán lại khi có sự thay đổi
            if (Screen.safeArea != lastSafeArea)
            {
                ApplySafeArea();
            }
        }
        #endif

        void ApplySafeArea()
        {
            //reset về mặc định trước khi áp dụng safe area mới
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            Rect safeArea = Screen.safeArea;
            lastSafeArea = safeArea;

            // Lưu ý: Trong Editor, Screen.safeArea lấy giá trị từ cửa sổ Game View
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}

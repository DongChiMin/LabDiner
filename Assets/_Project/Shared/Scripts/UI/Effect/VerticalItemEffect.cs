using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace LabDiner.Shared.UI
{
    /// <summary>
    /// Hiệu ứng mở rộng chiều cao item dần dần, dùng cho các item trong Vertical Layout Group
    /// Lưu ý: set preferred Height, nằm dưới veritcal layout group có control child height 
    /// </summary>
    [RequireComponent(typeof(LayoutElement))]
public class VerticalItemEffect : BaseUIEffect
{
    [SerializeField] private Ease _easeType = Ease.InOutQuad;

    private LayoutElement _layoutElement;
    private float _targetHeight;
    private Tween _currentTween;

    private void Awake()
    {
        _layoutElement = GetComponent<LayoutElement>();
        // Lấy chiều cao hiện tại làm mốc
        _targetHeight = GetComponent<RectTransform>().rect.height;
    }

    // Gọi cái này thay vì SetActive(false)
    public override void Hide(Action onComplete = null)
    {
        float lastPreferredHeight = _layoutElement.preferredHeight;
        _currentTween?.Kill();
        
        _currentTween = DOTween.To(() => _layoutElement.preferredHeight, 
            x => _layoutElement.preferredHeight = x, 
            0f, _duration)
            .SetEase(_easeType)
            .SetUpdate(true) // Chạy cả khi pause
            .OnComplete(() => {
                gameObject.SetActive(false);
                _layoutElement.preferredHeight = lastPreferredHeight;
                onComplete?.Invoke();
            });
    }

    public override void Show(Action onComplete = null)
    {
        // gameObject.SetActive(true);
        // _currentTween?.Kill();

        // _currentTween = DOTween.To(() => _layoutElement.preferredHeight, 
        //     x => _layoutElement.preferredHeight = x, 
        //     _targetHeight, _duration)
        //     .SetEase(_easeType)
        //     .SetUpdate(true)
        //     .OnComplete(() => onComplete?.Invoke());
    }
}
}
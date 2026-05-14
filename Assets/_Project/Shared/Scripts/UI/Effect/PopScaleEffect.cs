using UnityEngine;
using DG.Tweening;
using System;

namespace LabDiner.Shared.UI
{
    public class PopScaleEffect : BaseUIEffect
    {
        [Header("Settings")]
        [SerializeField] private Vector3 _targetScale = Vector3.one;
        
        [SerializeField] private Ease _showEase = Ease.OutBack; 
        [SerializeField] private Ease _hideEase = Ease.InBack;

        public override void Show(Action onComplete = null)
        {
            transform.DOKill();

            transform.localScale = Vector3.zero;
            
            transform.DOScale(_targetScale, _duration)
                     .SetEase(_showEase)
                     .SetUpdate(true)
                     .OnComplete(() =>
                    {
                        onComplete?.Invoke();
                    }).SetLink(gameObject);
        }

        //isHiding flag để tránh trường hợp gọi Hide nhiều lần liên tiếp
        private bool _isHiding = false;
        public override void Hide(Action onComplete = null)
        {
            if (_isHiding) return;
            _isHiding = true;

            transform.DOKill();

            transform.DOScale(Vector3.zero, _duration)
                     .SetEase(_hideEase)
                     .SetUpdate(true)
                     .OnComplete(() =>
                     {
                        _isHiding = false;
                         onComplete?.Invoke();
                     }).SetLink(gameObject);
        }
    }
}
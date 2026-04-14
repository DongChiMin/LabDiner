using System;
using System.Collections.Generic;
using DG.Tweening;
using LabDiner.Shared;
using LabDiner.Shared.SO;
using LabDiner.Shared.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LabDiner.Restaurant
{
    public class LevelUpgradePanel : BasePanel
    {
        public Button CloseButton => _closeButton;
        
        [Header("UI References")]
        [SerializeField] private Button _closeButton;
        [SerializeField] private ClickOutsideEffect _clickOutsideEffect;

        void OnEnable()
        {
            _clickOutsideEffect.OnClickOutside += HandleClickOutside;
        }

        void OnDisable()
        {
            _clickOutsideEffect.OnClickOutside -= HandleClickOutside;
        }

        private void HandleClickOutside()
        {
            Hide();
        }
    }
}

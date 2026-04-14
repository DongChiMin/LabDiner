using System.Collections.Generic;
using LabDiner.Shared.Events;
using LabDiner.Shared.SO;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "OnUIPanelOpened", menuName = "Events/UI/UI Panel Event")]
    public class UIPanelEvent : GameEvent<BasePanel> { }
}
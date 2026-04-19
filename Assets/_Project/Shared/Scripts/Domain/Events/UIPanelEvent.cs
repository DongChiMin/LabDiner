using LabDiner.Shared.UI;
using UnityEngine;

namespace LabDiner.Shared.Event
{
    [CreateAssetMenu(fileName = "OnUIPanelOpened", menuName = "Events/UI/UI Panel Event")]
    public class UIPanelEvent : GameEvent<BasePanel> { }
}
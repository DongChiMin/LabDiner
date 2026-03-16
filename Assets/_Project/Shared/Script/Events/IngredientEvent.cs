using UnityEngine;

namespace LabDiner.Shared.Events
{
    [CreateAssetMenu(fileName = "OnIngredientAdded", menuName = "Events/Ingredient Event")]
    public class IngredientEvent : GameEvent<IngredientSO> { }
}
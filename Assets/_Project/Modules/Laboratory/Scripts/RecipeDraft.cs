using System.Collections.Generic;
using LabDiner.Shared;
using LabDiner.Shared.Events;
using UnityEngine;
using TMPro;

namespace LabDiner.Laboratory
{
    public class RecipeDraft : MonoBehaviour
    {
        [Header("Broadcasting Events")]
        [SerializeField] private IngredientEvent onIngredientDroppedIn;
        [SerializeField] private TextMeshPro onRecipeTextUpdated;

        private HashSet<IngredientSO> currentIngredients = new HashSet<IngredientSO>();

        void OnEnable()
        {
            onIngredientDroppedIn.Register(HandleIngredientDroppedIn);
        }

        void OnDisable()
        {
            onIngredientDroppedIn.Unregister(HandleIngredientDroppedIn);
        }

        private void HandleIngredientDroppedIn(IngredientSO ingredient)
        {
            currentIngredients.Add(ingredient);
            UpdateRecipeText();
        }

        private void UpdateRecipeText()
        {
            // Implementation for updating the recipe text
            string recipeText = "Current Recipe:\n";
            foreach (var ingredient in currentIngredients)
            {
                recipeText += $"- {ingredient.name}\n";
            }
            onRecipeTextUpdated.text = recipeText;
        }
    }
}
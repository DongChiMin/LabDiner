using UnityEngine;

namespace LabDiner.Shared
{
    public interface IClickable
    {
        void OnPointerUp(Vector3 worldPosition);
        IDraggable OnPointerDown(Vector3 worldPosition);
    }
    public interface IDraggable
    {
        void OnDragStart(Vector3 worldPosition);
        void OnDragContinue(Vector3 worldPosition);
        void OnDragEnd();
        Transform Transform { get; }
        object GetData(); // Trả về IngredientSO hoặc bất cứ thứ gì
    }

    public interface IIngredient
    {
        IngredientSO IngredientData { get; }
        // Có thể thêm các thuộc tính nấu ăn như:
        // float Freshness { get; } 
    }

    public interface IDropZone
    {
        bool CanAccept(IDraggable draggable);
        void OnDrop(IDraggable draggable);
    }
}

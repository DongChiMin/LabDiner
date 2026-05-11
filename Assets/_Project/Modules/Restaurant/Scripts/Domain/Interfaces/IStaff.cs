using UnityEngine;

namespace LabDiner.Restaurant.Interface
{
    public interface IStaff
    {
        Transform RestPosition { get; set;}

        void UpgradeMoveSpeed(float speedBuffValue);
    }
}
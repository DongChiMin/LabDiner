using LabDiner.Restaurant.SO;
using LabDiner.Shared;

namespace LabDiner.Restaurant.Interface
{
    public interface ILevelProgress
    {
        public void LoadProgress();
        public void UpdateProgress();
    }
}
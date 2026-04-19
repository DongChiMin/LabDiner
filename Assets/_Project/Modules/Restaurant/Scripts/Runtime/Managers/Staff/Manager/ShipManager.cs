using LabDiner.Restaurant.Humanoid;
using LabDiner.Restaurant.Model;

namespace LabDiner.Restaurant.Manager
{
    public class ShipManager : StaffManager<WaiterContext, CookingTask>
    {
        //Event: _OnCookingTaskComplete
        //Event: _OnWaiterAvailable
    }
}
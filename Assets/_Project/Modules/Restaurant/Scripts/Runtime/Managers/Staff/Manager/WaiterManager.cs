using LabDiner.Restaurant.Humanoid;
using LabDiner.Restaurant.Model;

namespace LabDiner.Restaurant.Manager
{
    public class WaiterManager : StaffManager<WaiterContext, Order>
    {
        //Event: _OnNewUnservedOrder
        //Event: _OnWaiterAvailable
    }
}
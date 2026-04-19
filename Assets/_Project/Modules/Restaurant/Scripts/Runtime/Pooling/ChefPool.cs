using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.DesignPattern;

namespace LabDiner.Restaurant.Pooling
{
    public class ChefPool : SceneObjectPooling<ChefContext>
    {
        // Nếu cần thêm logic đặc biệt cho ChefContext khi trả về pool, có thể override ở đây
    }
}

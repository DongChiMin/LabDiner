using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.DesignPattern;

namespace LabDiner.Restaurant.Pooling
{
    public class GuestPool : SceneObjectPooling<GuestContext>
    {
        // Nếu cần thêm logic đặc biệt cho GuestContext khi trả về pool, có thể override ở đây
    }
}

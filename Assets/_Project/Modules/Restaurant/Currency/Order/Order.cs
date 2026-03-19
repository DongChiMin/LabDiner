
namespace LabDiner.Restaurant
{
    public class Order
    {
        public string DishName;
        public float CookTime;
        public int TableIndex; // Để biết nấu xong thì mang ra bàn nào (hoặc quầy nào)

        public Order(string name, float time, int table)
        {
            DishName = name;
            CookTime = time;
            TableIndex = table;
        }
    }
}
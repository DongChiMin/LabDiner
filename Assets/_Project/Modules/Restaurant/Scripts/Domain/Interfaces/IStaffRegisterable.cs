namespace LabDiner.Restaurant.Interface
{
    public interface IStaffRegisterable<TStaff> where TStaff : IStaff
    {
        void AssignNewStaff(TStaff staff);
    }
}
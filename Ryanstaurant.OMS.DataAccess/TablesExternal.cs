namespace Ryanstaurant.OMS.DataAccess
{
    public partial class Tables
    {
        public bool IsInUse
        {
            get
            {
                return Disabled == 0 && (CurrentStatus == 0 || CurrentStatus == 1 || CurrentStatus == 2);
            }
        }
    }
}

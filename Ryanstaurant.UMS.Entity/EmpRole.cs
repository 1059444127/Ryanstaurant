using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Entity
{
    public class EmpRole:IEntity
    {
        public int EmpID { get; set; }
        public int RoleID { get; set; }


        public string Exception { get; set; }

        public ExceptionType ExpType { get; set; }

        public string ExceptionStackTrace { get; set; }
    }
}

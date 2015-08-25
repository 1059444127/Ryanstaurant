using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Entity
{
    public class EmpAuth:IEntity
    {

        public int EmpID { get; set; }
        public int AuthID { get; set; }

        public string Exception { get; set; }

        public ExceptionType ExpType { get; set; }

        public string ExceptionStackTrace { get; set; }
    }
}

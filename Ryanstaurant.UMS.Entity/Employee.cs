using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Entity
{
    public class Employee : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int Authority { get; set; }
        public string Exception { get; set; }

        public ExceptionType ExpType { get; set; }

        public string ExceptionStackTrace { get; set; }
    }
}

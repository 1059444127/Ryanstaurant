using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Entity
{
    public class Role : IEntity
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Exception { get; set; }

        public ExceptionType ExpType{ get; set; }

        public string ExceptionStackTrace{ get; set; }
    }
}

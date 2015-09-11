using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class AuthorityViewModel : ViewModelBase
    {
        public AuthorityViewModel()
        {
            Authority=new AuthorityModel();
        }




        public AuthorityViewModel(int id,string name)
        {
            Authority = new AuthorityModel
            {
                ID = id,
                Name = name
            };
            Authority.Refresh();
        }




        public AuthorityModel Authority { get; set; }


        public long ID
        {
            get { return Authority.ID; }
            set
            {
                Authority.ID = value;
                RaisePropertyChanged("ID");
            }
        }

        public string Name
        {
            get { return Authority.Name; }
            set
            {
                Authority.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return Authority.Description; }
            set
            {
                Authority.Description = value;
                RaisePropertyChanged("Description");
            }
        }



        private bool _isChecked;

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
    }
}

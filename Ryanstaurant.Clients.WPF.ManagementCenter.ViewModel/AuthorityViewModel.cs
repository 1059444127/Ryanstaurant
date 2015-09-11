using System;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
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

        internal void AddAuthority()
        {
            Authority.Add();
        }

        internal void DeleteAuthority()
        {
            Authority.Delete();
        }

        internal void ModifyAuthority()
        {
            Authority.Modify();
        }


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    try
                    {

                        switch (Operation)
                        {
                            case OperationType.Add:
                                AddAuthority();
                                break;
                            case OperationType.Modify:
                                ModifyAuthority();
                                break;
                        }

                        if (Close != null)
                            Close();

                    }
                    catch (Exception ex)
                    {
                        ForeColor = new SolidColorBrush(Color.FromRgb(0xe5, 0x14, 0x00));
                        Information = ex.Message;
                    }
                });
            }
        }


        public event Action Close; 
    }
}

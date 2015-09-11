using System;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class RoleViewModel:ViewModelBase
    {



        public RoleViewModel()
        {
            Role = new RoleModel();
        }


        public RoleViewModel(int id, string name)
        {
            Role = new RoleModel
            {
                ID = id,
                Name = name
            };
            Role.Refresh();

        }





        public RoleModel Role { get; set; }



        public int ID
        {
            get { return Role.ID; }
            set
            {
                Role.ID = value;
                RaisePropertyChanged("ID");
            }
        }

        public string Name
        {
            get { return Role.Name; }
            set
            {
                Role.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return Role.Description; }
            set
            {
                Role.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public long Authority
        {
            get
            {
                return Role.Authority;
            }
            set
            {
                Role.Authority = value;
                RaisePropertyChanged("Authority");

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



        internal void AddAuthority(long authority)
        {
            Role.Authority |= authority;
            Role.Modify();
        }

        internal void DeleteAuthority(long authority)
        {
            Role.Authority &= ~authority;
            Role.Modify();
        }



        public void AddEmployee()
        {
            Role.Add();
        }

        public void DeleteEmployee()
        {
            Role.Delete();
        }

        public void ModifyEmployee()
        {
            Role.Modify();
        }

        public OperationType Operation
        {
            get;
            set;
        }


        public bool IsModify
        {
            get
            {
                return Operation == OperationType.Modify;
            }
        }


        private Brush _forColor = new SolidColorBrush(Color.FromRgb(0x00, 0x50, 0xef));

        public Brush ForeColor
        {
            get
            {
                return _forColor;
            }
            set
            {
                _forColor = value;
                RaisePropertyChanged("ForeColor");
            }
        }

        private string _information = string.Empty;

        public string Information
        {
            get { return _information; }
            set { _information = value; RaisePropertyChanged("Information"); }

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
                                AddEmployee();
                                break;
                            case OperationType.Modify:
                                ModifyEmployee();
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

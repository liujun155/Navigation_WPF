using Navigation_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Navigation_WPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Properties
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands
        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; }
        }

        private RelayCommand _menuClickCommand;

        public RelayCommand MenuClickCommand
        {
            get { return _menuClickCommand; }
            set { _menuClickCommand = value; }
        }
        #endregion

        public MainViewModel()
        {
            CurrentView = new HomeViewModel();
            CloseCommand = new RelayCommand(CloseApp);
            MenuClickCommand = new RelayCommand(MenuClick);
        }

        #region Methods
        private void CloseApp(object obj)
        {
            Application.Current.Shutdown();
        }

        private void MenuClick(object obj)
        {
            if (obj == null) return;
            string menuName = obj.ToString();

            string vmName = $"{Assembly.GetExecutingAssembly().GetName().Name}.ViewModels.{menuName}ViewModel";
            Type t = Type.GetType(vmName);
            if (t == null)
            {
                CurrentView = null;
                return;
            }
            CurrentView = Activator.CreateInstance(t);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.App.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books;
        private int _selectedItemIndex;

        public ViewModel()
        {
            _books = new ObservableCollection<Book>();
            _selectedItemIndex = -1;
        }

        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set { _selectedItemIndex = value; NotifyPropertyChanged("SelectedItemIndex"); }
        }

        public ObservableCollection<Book> Books
        {
            get { return _books; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.App
{
    /// <summary>
    /// 
    /// </summary>
    public class Book : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        private string _title, _author, _keywords, _description;
        private bool _save;
        private int _ISBN;

        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string Title 
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged("Title"); }
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Author
        {
            get { return _author; }
            set { _author = value; NotifyPropertyChanged("Author"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; NotifyPropertyChanged("ISBN"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value; NotifyPropertyChanged("Keywords"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged("Description"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Save
        {
            get { return _save; }
            set { _save = value; NotifyPropertyChanged("Saved"); }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propName"></param>
        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

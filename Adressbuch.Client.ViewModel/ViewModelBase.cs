using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Client.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private Guid _id;
        private DateTime _created;
        private string _createdBy;
        private DateTime? _modified;
        private string _modifiedBy;

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Created
        {
            get { return _created; }
            set
            {
                if (_created != value)
                {
                    _created = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? Modified
        {
            get { return _modified; }
            set
            {
                if (_modified != value)
                {
                    _modified = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set {
                if (_modifiedBy != value)
                {
                    _modifiedBy = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

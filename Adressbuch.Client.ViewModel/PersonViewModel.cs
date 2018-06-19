using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Client.ViewModel
{
    public class PersonViewModel : ViewModelBase
    {
        private string _name;
        private string _vorname;
        private DateTime? _geburtsdatum;

        public string Name
        {
            get { return _name; }
            set {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Vorname
        {
            get { return _vorname; }
            set {
                if(_vorname!=value)
                {
                    _vorname = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? Geburtsdatum
        {
            get { return _geburtsdatum; }
            set {
                if (_geburtsdatum != value)
                {
                    _geburtsdatum = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}

using Adressbuch.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Client.ViewModel
{
    public class PersonSearchViewModel : ViewModelBase
    {
        private string _name;
        private string _vorname;
        private DateTime? _geburtsdatumVon;
        private DateTime? _geburtsdatumBis;
        private LogicalOperators _nameLO;
        private LogicalOperators _vornameLO;
        private LogicalOperators _geburtsdatumVonLO;
        private LogicalOperators _geburtsdatumBisLO;

        public PersonSearchViewModel()
        {
            var lOs = Enum.GetValues(typeof(LogicalOperators));

            NameLOs = lOs.Cast<LogicalOperators>();
            VornameLOs= lOs.Cast<LogicalOperators>();
            GeburtsdatumVonLOs = lOs.Cast<LogicalOperators>();
            GeburtsdatumBisLOs = lOs.Cast<LogicalOperators>();
        }

        public string Name
        {
            get { return _name; }
            set
            {
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
            set
            {
                if (_vorname != value)
                {
                    _vorname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DateTime? GeburtsdatumVon
        {
            get { return _geburtsdatumVon; }
            set
            {
                if (_geburtsdatumVon != value)
                {
                    _geburtsdatumVon = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? GeburtsdatumBis
        {
            get { return _geburtsdatumBis; }
            set
            {
                if (_geburtsdatumBis != value)
                {
                    _geburtsdatumBis = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public LogicalOperators NameLO
        {
            get { return _nameLO; }
            set
            {
                if (_nameLO != value)
                {
                    _nameLO = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public LogicalOperators VornameLO
        {
            get { return _vornameLO; }
            set
            {
                if (_vornameLO != value)
                {
                    _vornameLO = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public LogicalOperators GeburtsdatumVonLO
        {
            get { return _geburtsdatumVonLO; }
            set
            {
                if (_geburtsdatumVonLO != value)
                {
                    _geburtsdatumVonLO = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public LogicalOperators GeburtsdatumBisLO
        {
            get { return _geburtsdatumBisLO; }
            set
            {
                if (_geburtsdatumBisLO != value)
                {
                    _geburtsdatumBisLO = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IEnumerable<LogicalOperators> NameLOs { get; private set; }
        public IEnumerable<LogicalOperators> VornameLOs { get; private set; }
        public IEnumerable<LogicalOperators> GeburtsdatumVonLOs { get; private set; }
        public IEnumerable<LogicalOperators> GeburtsdatumBisLOs { get; private set; }
    }
}

using Adressbuch.Client.DataAccess;
using Adressbuch.Client.DataViewModel;
using Adressbuch.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Client.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PersonViewModel> _personen;
        private PersonViewModel _person;
        private bool _detailsTabSelected;

        public MainWindowViewModel()
        {
            PersonenGetAllCmd = new RelayCommand(async p => await PersonenGetAllCmdExec(p), p => PersonenGetAllCmdCanExec(p));
            PersonGetByIdCmd = new RelayCommand(async p => await PersonGetByIdCmdExec(p), p => PersonGetByIdCmdCanExec(p));
            PersonDeleteCmd = new RelayCommand(async p => await PersonDeleteCmdExec(p), p => PersonDeleteCmdCanExec(p));
            PersonSaveCmd = new RelayCommand(async p => await PersonSaveCmdExec(p), p => PersonSaveCmdCanExec(p));
            PersonNewCmd = new RelayCommand(p => PersonNewCmdExec(p), p => PersonNewCmdCanExec(p));
            PersonSearchCmd = new RelayCommand(async p => await PersonSearchCmdExec(p), p => PersonSearchCmdCanExec(p));
        }

        private bool PersonSearchCmdCanExec(object p)
        {
            return
                !(string.IsNullOrWhiteSpace(PersonSuchKriterien.Name.Value)) ||
                !(string.IsNullOrWhiteSpace(PersonSuchKriterien.Vorname.Value)) ||
                PersonSuchKriterien.GeburtsdatumVon.Value.HasValue ||
                PersonSuchKriterien.GeburtsdatumBis.Value.HasValue;
        }

        private async Task PersonSearchCmdExec(object p)
        {
            PersonRepository personRepository = new PersonRepository();
            Personen = new ObservableCollection<PersonViewModel>(await personRepository.GetFiltered(PersonSuchKriterien));
        }

        private bool PersonNewCmdCanExec(object p)
        {
            return true;
        }

        private void PersonNewCmdExec(object p)
        {
            Person = new PersonViewModel()
            {

                Name = "",
                Vorname = "",
                Geburtsdatum = null,
                Created = DateTime.Now,
                CreatedBy = "Rolf"
            };
            DetailsTabSelected = true;
        }

        private bool PersonSaveCmdCanExec(object p)
        {
            return p is PersonViewModel;
        }

        private async Task PersonSaveCmdExec(object p)
        {
            PersonViewModel personViewModel = p as PersonViewModel;

            PersonRepository personDataAccess = new PersonRepository();

            if (null == Personen)
            {
                Personen = new ObservableCollection<PersonViewModel>();
            }

            PersonViewModel personToExchange = Personen.SingleOrDefault(x => x.Id == personViewModel.Id);

            if (null != personToExchange)
            {
                personViewModel.Modified = DateTime.Now;
                personViewModel.ModifiedBy = "Auch Rolf";

                await personDataAccess.Update(personViewModel.Id, personViewModel);

                Personen.Remove(personToExchange);
                Personen.Add(personViewModel);
            }
            else
            {
                await personDataAccess.Insert(personViewModel);
                Personen.Add(personViewModel);
            }
        }

        private bool PersonDeleteCmdCanExec(object p)
        {
            return p is PersonViewModel;
        }

        private async Task PersonDeleteCmdExec(object p)
        {
            PersonViewModel personViewModel = p as PersonViewModel;
            if (null != Person && Person.Id == personViewModel.Id)
            {
                Person = null;
            }

            PersonRepository personRepository = new PersonRepository();
            await personRepository.Delete(personViewModel.Id);
            Personen.Remove(personViewModel);
        }

        private bool PersonGetByIdCmdCanExec(object p)
        {
            return p is PersonViewModel;
        }

        private async Task PersonGetByIdCmdExec(object p)
        {
            PersonViewModel personViewModel = p as PersonViewModel;
            Person = null;
            PersonRepository personRepository = new PersonRepository();
            Person = await personRepository.GetById(personViewModel.Id);
            DetailsTabSelected = true;
        }

        private bool PersonenGetAllCmdCanExec(object p)
        {
            return true;
        }

        private async Task PersonenGetAllCmdExec(object p)
        {
            PersonRepository personRepository = new PersonRepository();
            Personen = new ObservableCollection<PersonViewModel>(await personRepository.GetAll());
        }

        private async Task LoadPeople()
        {
            if (PersonenGetAllCmdCanExec(null))
            {
                await PersonenGetAllCmdExec(null);
            }
        }

        public RelayCommand PersonenGetAllCmd { get; private set; }
        public RelayCommand PersonGetByIdCmd { get; private set; }
        public RelayCommand PersonDeleteCmd { get; private set; }
        public RelayCommand PersonSaveCmd { get; private set; }
        public RelayCommand PersonNewCmd { get; set; }
        public RelayCommand PersonSearchCmd { get; set; }

        public ObservableCollection<PersonViewModel> Personen
        {
            get
            {
                return _personen;
            }
            private set
            {
                if (_personen != value)
                {
                    _personen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PersonViewModel Person
        {
            get { return _person; }
            private set
            {
                if (_person != value)
                {
                    _person = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PersonSearchDto PersonSuchKriterien { get; } = new PersonSearchDto();

        public bool DetailsTabSelected
        {
            get { return _detailsTabSelected; }
            set
            {
                if (_detailsTabSelected != value)
                {
                    _detailsTabSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

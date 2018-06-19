using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.DataTransfer
{
    public class DtoBase : INotifyPropertyChanged
    {
        public DtoBase() { }

        public DtoBase(string createdBy, DateTime created)
        {
            Id = Guid.NewGuid();
            CreatedBy = createdBy;
            Created = created;
        }

        public DtoBase(Guid id, string createdBy, DateTime created, string modifiedBy, DateTime? modified)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(string.Format(
                    "'{0}' darf nicht leer sein.\n" +
                    "Wenn Sie ein neues {1}-Object erzeugen möchten, verwenden Sie den Konstruktor ohne 'id'.",
                    nameof(id), nameof(DtoBase)), nameof(id));
            }

            Id = id;
            CreatedBy = createdBy;
            Created = created;
            ModifiedBy = modifiedBy;
            Modified = modified;
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

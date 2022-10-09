using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Interfaces
{
    public interface IKomitentiDetailViewModel
    {
        bool HasChanges { get; }
        KomitentWrapper Komitent { get; set; }
        KomitentWrapper CreateNewKomitent();
        Task LoadAsync(int? komitentId);
        Task SaveKomitentAsync();
        Task DeleteKomitentAsync();
        Task CancelChange();
        void Dispose();

        event EventHandler<KomitentDeletedArgs> OnKomitentDeleted;
        event EventHandler<KomitentSavedArgs> OnKomitentSaved;
    }

    public class KomitentSavedArgs
    {
        public readonly int id;
        public readonly string naziv;
        public readonly bool pravno;
        public readonly bool fizicko;
        public readonly string mesto;

        public KomitentSavedArgs(int id, string naziv, bool pravno, bool fizicko, string? mesto)
        {
            this.id = id;
            this.naziv = naziv;
            this.pravno = pravno;
            this.fizicko = fizicko;
            this.mesto = mesto;
        }
    }

    public class KomitentDeletedArgs
    {
        public readonly int id;
        public KomitentDeletedArgs(int id)
        {
            this.id = id;
        }
    }
}
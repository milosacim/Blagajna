﻿using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.ViewModels.Komitenti.Navigation;
using MivexBlagajna.UI.Views.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public class KomitentiViewModel : ViewModelBase, IDockElement, IKomitentiViewModel
    {
        #region Fields

        private readonly IMessageDialogService _messageDialogService;
        private IKomitentiDetailViewModel _komitentiDetailViewModel;
        private readonly Func<IKomitentiDetailViewModel> _komitentiDetailViewModelCreator;
        private readonly string _header;
        private readonly DockState _state;

        #endregion

        #region Constructor
        public KomitentiViewModel(
            Func<IKomitentiDetailViewModel> komitentiDetailViewModelCreator,
            IKomitentiNavigationViewModel komitentiNavigationViewModel,
            IMessageDialogService messageDialogService,
            string header = "Komitenti",
            DockState state = DockState.Document)
        {
            _messageDialogService = messageDialogService;
            _komitentiDetailViewModelCreator = komitentiDetailViewModelCreator;
            _header = header;
            _state = state;

            KomitentiNavigationViewModel = komitentiNavigationViewModel;

            KomitentiNavigationViewModel.OnkomitentSelected += OnOpenDetails;
            KomitentiNavigationViewModel.OnkomitentSelected += UpdateisSelected;
        }

        private void UpdateisSelected(object? sender, SelectedKomitentArgs e)
        {
            if (e.oldId != null)
            {
                var oldItem = KomitentiNavigationViewModel.Komitenti.Where(k => k.Id == e.oldId && k.IsSelected == true).FirstOrDefault();
                oldItem.IsSelected = false;
                KomitentiNavigationViewModel.SelectedKomitent.IsSelected = true;
            } else
            {
                KomitentiNavigationViewModel.SelectedKomitent.IsSelected = true;
            }
        }

        private async void OnOpenDetails(object? sender, SelectedKomitentArgs e)
        {
            if (KomitentiDetailViewModel != null && KomitentiDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Otkazi)
                {
                    return;
                }
            }

            KomitentiDetailViewModel = _komitentiDetailViewModelCreator();
            await KomitentiDetailViewModel.LoadAsync(e.newid);
        }


        #endregion

        #region Properties
        public string? Header
        {
            get { return _header; }
            set { }
        }
        public DockState State
        {
            get { return _state; }
            set { }
        }
        public AsyncCommand CreateNewKomitentCommand { get; }
        public IKomitentiNavigationViewModel KomitentiNavigationViewModel { get; }
        public IKomitentiDetailViewModel KomitentiDetailViewModel
        {
            get { return _komitentiDetailViewModel; }
            set
            {
                var oldValue = _komitentiDetailViewModel;
                _komitentiDetailViewModel = value;
                OnModelPropertyChanged(oldValue, value);
                _komitentiDetailViewModel.OnKomitentDeleted += OnKomitentDeleted;
                _komitentiDetailViewModel.OnKomitentSaved += OnKomitentSaved;
            }
        }

        private void OnKomitentSaved(object? sender, KomitentSavedArgs e)
        {
            var lookupitem = KomitentiNavigationViewModel.Komitenti.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                KomitentiNavigationViewModel.Komitenti.Add(new KomitentiNavigationItemViewModel(e.id, e.naziv, e.pravno, e.fizicko, e.mesto));
                KomitentiNavigationViewModel.SelectedKomitent = KomitentiNavigationViewModel.Komitenti.Last();
            }

            else { lookupitem.PunNaziv = e.naziv; }
        }

        private void OnKomitentDeleted(object? sender, KomitentDeletedArgs e)
        {
            var komitent = KomitentiNavigationViewModel.Komitenti.SingleOrDefault(k => k.Id == e.id);

            if (komitent != null)
            {
                KomitentiNavigationViewModel.Komitenti.Remove(komitent);
                KomitentiNavigationViewModel.SelectedKomitent = KomitentiNavigationViewModel.Komitenti.Last();
            }
        }



        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            if (KomitentiNavigationViewModel != null)
            {
                await KomitentiNavigationViewModel.LoadAsync();
            }
        }
        public override void Dispose()
        {
            if (KomitentiDetailViewModel != null && KomitentiNavigationViewModel != null)
            {
                if (KomitentiDetailViewModel.HasChanges)
                {
                    var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                    if (result == MessageDialogResult.Potvrdi)
                    {
                        KomitentiNavigationViewModel.OnkomitentSelected -= OnOpenDetails;
                        KomitentiDetailViewModel.OnKomitentDeleted -= OnKomitentDeleted;
                        KomitentiDetailViewModel.OnKomitentSaved -= OnKomitentSaved;

                        KomitentiDetailViewModel.Dispose();
                        KomitentiNavigationViewModel.Dispose();
                    }
                }
            }

            KomitentiNavigationViewModel.OnkomitentSelected -= OnOpenDetails;
            KomitentiDetailViewModel.OnKomitentDeleted -= OnKomitentDeleted;
            KomitentiDetailViewModel.OnKomitentSaved -= OnKomitentSaved;

            KomitentiDetailViewModel.Dispose();
            KomitentiNavigationViewModel.Dispose();

            base.Dispose();
        }
        #endregion
    }
}

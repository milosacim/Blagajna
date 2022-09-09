using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Events;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public class MestaTroskaDetailsViewModel : ViewModelBase, IMestaTroskaDetailsViewModel
    {
        #region Fields
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        private MestoTroskaWrapper _mestoTroska;
        private bool _hasChanges;
        #endregion

        #region Constructor
        public MestaTroskaDetailsViewModel(IMestoTroskaRepository mestoTroskaRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)

        {
            _mestoTroskaRepository = mestoTroskaRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
        }
        #endregion

        #region Properties
        public MestoTroskaWrapper MestoTroska
        {
            get { return _mestoTroska; }
            set { _mestoTroska = value; OnModelPropertyChanged();}
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set { _hasChanges = value; OnModelPropertyChanged(); }
        }
        #endregion

        #region Methods

        public async Task LoadAsync(int? mestoTroskaId)
        {
            var mestoTroska = await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value);
            var hasChanges = _mestoTroskaRepository.HasChanges();

            MestoTroska = new MestoTroskaWrapper(mestoTroska);

            MestoTroska.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                }
            };
        }

        #endregion
    }
}

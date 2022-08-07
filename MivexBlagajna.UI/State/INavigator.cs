using MivexBlagajna.UI.ViewModels;

namespace MivexBlagajna.UI.State
{
    public enum ViewModelType
    {
        Komitenti,
        MostaTroska,
        UplateIsplate,
        FinansijskaKartica
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
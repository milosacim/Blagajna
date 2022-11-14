using System;
using System.Windows;
using System.Windows.Controls;

namespace MivexBlagajna.UI.Views.Navigations
{
    /// <summary>
    /// Interaction logic for KomitentiNavigationView.xaml
    /// </summary>
    public partial class KomitentiNavigationView : UserControl
    {
        public KomitentiNavigationView()
        {
            InitializeComponent();
            //DataContextChanged += OnDataContextChanged;
        }

        //private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if(this.DataContext!= null)
        //    {
        //        var test = DataContext;
        //    }
        //}
    }
}

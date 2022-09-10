using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MivexBlagajna.UI.Views
{
    /// <summary>
    /// Interaction logic for MestaTroskaView.xaml
    /// </summary>
    public partial class MestaTroskaView : UserControl
    {
        public MestaTroskaView()
        {
            InitializeComponent();

            (this.dockingManager.DocContainer as DocumentContainer).Loaded += MestaTroskaView_Loaded;
        }

        private void MestaTroskaView_Loaded(object sender, RoutedEventArgs e)
        {
            TabControlExt tabControlExt = VisualUtils.FindDescendant(sender as Visual, typeof(TabControlExt)) as TabControlExt;
            if (tabControlExt != null)
            {
                foreach (TabItemExt tabItemExt in tabControlExt.Items)
                {
                    tabItemExt.Width = 200;
                }
            }
        }
    }
}

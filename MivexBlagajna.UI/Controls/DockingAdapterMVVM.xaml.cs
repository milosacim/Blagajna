using Syncfusion.Windows.Tools.Controls;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.ViewModels;

namespace MivexBlagajna.UI.Controls
{
    public partial class DockingAdapter : UserControl
    {
        public DockingAdapter()
        {
            InitializeComponent();
            PART_DockingManager.Loaded += PART_DockingManager_Loaded;
        }

        private void PART_DockingManager_Loaded(object sender, RoutedEventArgs e)
        {
            ((DocumentContainer)PART_DockingManager.DocContainer).AddTabDocumentAtLast = true;
        }

        public IDockElement ActiveDocument
        {
            get { return (IDockElement)GetValue(ActiveDocumentProperty); }
            set { SetValue(ActiveDocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveDocument.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveDocumentProperty =
            DependencyProperty.Register("ActiveDocument", typeof(IDockElement), typeof(DockingAdapter), new PropertyMetadata(null, new PropertyChangedCallback(OnActiveDocumentChanged)));

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(DockingAdapter), new PropertyMetadata(null, new PropertyChangedCallback(OnActiveDocumentChanged)));

        private static void OnActiveDocumentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DockingAdapter? adapter = sender as DockingAdapter;
            if (adapter != null)
            {
                foreach (FrameworkElement element in adapter.PART_DockingManager.Children)
                {
                    if (element is ContentControl)
                    {
                        ContentControl? control = element as ContentControl;
                        if (control != null && control.Content == args.NewValue)
                        {
                            adapter.PART_DockingManager.ActiveWindow = control;
                            break;
                        }
                    }
                }
            }
        }

        public DataTemplate FindDataTemplate(Type type, FrameworkElement element)
        {
            var dataTemplate = element.TryFindResource(type) as DataTemplate;
            if (dataTemplate != null)
            {
                return dataTemplate;
            }

            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                dataTemplate = FindDataTemplate(type.BaseType, element);
                if (dataTemplate != null)
                {
                    return dataTemplate;
                }
            }
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (dataTemplate != null)
                {
                    return dataTemplate;
                }
            }
            return null;
        }

        private void PART_DockingManager_ActiveWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ContentControl)
            {
                var content = e.NewValue as ContentControl;
                if (content != null)
                {
                    if (((IDockElement)content.Content).State == DockState.Document)
                    {
                        ActiveDocument = (IDockElement)content.Content;
                    }
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "ItemsSource")
            {
                if (e.OldValue != null)
                {
                    var oldCollection = e.OldValue as INotifyCollectionChanged;
                    oldCollection.CollectionChanged -= CollectionChanged;
                }

                if (e.NewValue != null)
                {
                    ((DocumentContainer)PART_DockingManager.DocContainer).AddTabDocumentAtLast = true;
                    var newCollection = e.NewValue as INotifyCollectionChanged;
                    int count = 0;
                    foreach (var item in (IList)e.NewValue)
                    {
                        if (item is IDockElement)
                        {
                            ContentControl control = new ContentControl() { Content = item };
                            DockingManager.SetHeader(control, ((IDockElement)item).Header);
                            if (((IDockElement)control.Content).State == DockState.Document)
                            {
                                DockingManager.SetState(control, Syncfusion.Windows.Tools.Controls.DockState.Document);
                            }
                            else
                            {
                                if (count != 0)
                                {
                                    DockingManager.SetTargetNameInDockedMode(control, "item" + (count + 1).ToString());
                                    DockingManager.SetSideInDockedMode(control, DockSide.Bottom);
                                }
                                DockingManager.SetDesiredWidthInDockedMode(control, 220);
                                control.Name = "item" + (count + 1).ToString();
                            }
                            PART_DockingManager.Children.Add(control);
                        }
                    }
                    newCollection.CollectionChanged += CollectionChanged;
                }
            }

            base.OnPropertyChanged(e);
        }
        private void CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    ContentControl control = (from ContentControl element in PART_DockingManager.Children
                                              where element.Content == item
                                              select element).FirstOrDefault();

                    PART_DockingManager.Children.Remove(control);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is IDockElement)
                    {
                        ContentControl control = new ContentControl() { Content = item };
                        DockingManager.SetHeader(control, ((IDockElement)item).Header);
                        if (((IDockElement)item).State == DockState.Document)
                        {
                            DockingManager.SetState(control, Syncfusion.Windows.Tools.Controls.DockState.Document);
                        }
                        PART_DockingManager.Children.Add(control);
                    }
                }
            }
        }
    }
}

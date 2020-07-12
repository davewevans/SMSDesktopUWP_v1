using System;

using SMSDesktopUWP.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SMSDesktopUWP.Views
{
    public sealed partial class OrphanMasterDetailDetailControl : UserControl
    {
        public Orphan MasterMenuItem
        {
            //get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            get { return GetValue(MasterMenuItemProperty) as Orphan; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public char SymbolDisplay
        {
            get { return (char)57661; }
        }

        //public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(OrphanMasterDetailDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));
        public static readonly DependencyProperty MasterMenuItemProperty =
            DependencyProperty.Register("MasterMenuItem", typeof(Orphan), typeof(OrphanMasterDetailDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public OrphanMasterDetailDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as OrphanMasterDetailDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}

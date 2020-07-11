using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using SMSDesktopUWP.Core.Models;
using SMSDesktopUWP.Core.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace SMSDesktopUWP.Views
{
    public sealed partial class OrphanMasterDetailPage : Page, INotifyPropertyChanged
    {
        //private SampleOrder _selected;
        private Orphan _selected;

        //public SampleOrder Selected
        //{
        //    get { return _selected; }
        //    set { Set(ref _selected, value); }
        //}

        public Orphan Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        //public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public ObservableCollection<Orphan> OrphanItems { get; private set; } = new ObservableCollection<Orphan>();

        public OrphanMasterDetailPage()
        {
            InitializeComponent();
            Loaded += OrphanMasterDetailPage_Loaded;
        }

        private async void OrphanMasterDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            //SampleItems.Clear();
            OrphanItems.Clear();

            //var data = await SampleDataService.GetMasterDetailDataAsync();
            var data = await OrphanDataService.AllOrphans();

            foreach (var item in data)
            {
                //SampleItems.Add(item);
                OrphanItems.Add(item);
            }

            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                //Selected = SampleItems.FirstOrDefault();
                Selected = OrphanItems.FirstOrDefault();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

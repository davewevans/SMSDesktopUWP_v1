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

        //list for AutoSuggestBox
        public List<Orphan> orphanList = new List<Orphan>();
        List<Orphan> listOrphanSuggestion = null;

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

            //Initialize the autoSuggestBox with orphans
            //orphanSearchList = OrphanItems.ToList();
            //txtAutoSuggest.ItemsSource = orphanSearchList;
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

        private void AutoSuggestionBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            orphanList = OrphanItems.ToList();

            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                listOrphanSuggestion = orphanList.Where(o => o.FullName.Contains(sender.Text)).ToList();
                sender.ItemsSource = listOrphanSuggestion;
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            var selectedItem = args.SelectedItem as Orphan;
            sender.Text = selectedItem.FullName;

            Selected = selectedItem;

        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            //if (args.ChosenSuggestion != null)
            //{
            //    // User selected an item from the suggestion list, take an action on it here.

            //}
            //else
            //{
            //    // Use args.QueryText to determine what to do.
            //}

            var searchTerm = args.QueryText;
            var results = orphanList.Where(i => i.FullName.Contains(searchTerm)).ToList();
            sender.ItemsSource = results;
            sender.IsSuggestionListOpen = true;
        }
    }
}

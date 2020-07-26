﻿using SMSDesktopUWP.Core.Models;
using SMSDesktopUWP.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SMSDesktopUWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditOrphanPage : Page
    {
        private Orphan _inOrphan;

        private bool isNew = true;

        public Orphan InOrphan
        {
            get { return _inOrphan; }
            set { _inOrphan = value; }
        }

        public EditOrphanPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                InOrphan = (Orphan)e.Parameter;

                isNew = false;
            }
            else
            {
                isNew = true;
            }

            base.OnNavigatedTo(e);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }

        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Orphan outOrphan = new Orphan();
            // Add the save stuff to see if it works.
            if (!isNew)
            {
                outOrphan = InOrphan;
            }


            outOrphan.FirstName = txtFirstName.Text;
            outOrphan.MiddleName = txtMiddleName.Text;
            outOrphan.LastName = txtLastName.Text;
            outOrphan.Gender = txtGender.Text;
            //outOrphan.DateOfBirth = dtDateOfBirth.Date;
            outOrphan.LCMStatus = txtLCMStatus.Text;
            //outOrphan.EntryDate = dtEntryDate.Date;
            if (String.IsNullOrWhiteSpace(txtMiddleName.Text))
            {
                outOrphan.FullName = txtFirstName.Text + " " + txtLastName.Text;
            }
            else
            {
                outOrphan.FullName = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;

            }

            if (isNew)
            {
                // Update to Database
                OrphanDataService.AddOrphan(outOrphan);
            }
            else
            {
                // Go get the one of interest, then overwrite.

                // Update to Database
                OrphanDataService.SaveOrphan(outOrphan);

            }

            // Go Back
            On_BackRequested();

        }
    }
}

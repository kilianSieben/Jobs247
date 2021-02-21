using Jobs247.Model;
using Jobs247.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobFilterPage : ContentPage
    {
        private string matchingJobString;
        public string MatchingJobString
        {
            get { return matchingJobString; }
            set
            {
                matchingJobString = value;
                OnPropertyChanged(nameof(MatchingJobString));
            }
        }

        public List<Job> MatchingJobs { get; set; }
        public RestService RestService { get; set; }

        public JobFilterPage()
        {
            InitializeComponent();
            RestService = new RestService();
            MatchingJobs = new List<Job>();
        }

        protected async override void OnAppearing()
        {
            
            MatchingJobs = await RestService.GetJobPostings();
        }


        private async void OnShowMatchesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowMatchesPage());
        }
    }
}
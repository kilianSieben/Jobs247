using Jobs247.Model;
using Jobs247.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobFilterPage : ContentPage
    {
        public string MatchingJobString { get; set; }
        public List<string> JobTitleList { get; set; }
        public List<string> CompanyNameList { get; set; }
        public List<Job> JobList { get; set; }
        public List<Job> MatchingJobs { get; set; }
        public RestService RestService { get; set; }
        
        public JobFilterPage()
        {
            InitializeComponent();

            JobTitleList = new List<string>();
            CompanyNameList = new List<string>();
            JobList = new List<Job>();
            MatchingJobs = new List<Job>();
            RestService = new RestService();
        }

        protected async override void OnAppearing()
        {
            JobList.Clear();
            JobTitleList.Clear();
            CompanyNameList.Clear();
            JobList = await RestService.GetJobPostings();

            //If the server is not running or no job postings were found, this message will be shown on screen
            if (JobList.Count == 0)
            {
                await DisplayAlert("Exception", "No jobs were found", "OK");
            }

            //Adding all unique job titles and company names to the picker.
            foreach(var item in JobList)
            {
                JobTitleList.Add(item.Position.name);
                CompanyNameList.Add(item.Company.name);
            }

            JobTitleList = JobTitleList.Distinct().ToList();
            CompanyNameList = CompanyNameList.Distinct().ToList();
            JobTitlePicker.ItemsSource = JobTitleList;
            CompanyNamePicker.ItemsSource = CompanyNameList;

            ChangeMatchingJobsLabel();
        }
        private void MyPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMatchingJobsLabel();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeMatchingJobsLabel();
        }

        private void ChangeMatchingJobsLabel()
        {
            MatchingJobs.Clear();

            foreach(var item in JobList)
            {
                MatchingJobs.Add(item);
            }

            //Searching the job list for matching jobs with the used filters
            if (JobTitlePicker.SelectedIndex != -1)
            {
                MatchingJobs = MatchingJobs.Where(x => x.Position.name == JobTitleList[JobTitlePicker.SelectedIndex]).ToList();
            }

            if (CompanyNamePicker.SelectedIndex != -1)
            {
                MatchingJobs = MatchingJobs.Where(x => x.Company.name == CompanyNameList[CompanyNamePicker.SelectedIndex]).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SearchBar.Text))
            {
                MatchingJobs = MatchingJobs.Where(x => x.SearchAllText().Contains(SearchBar.Text.ToLower())).ToList();
            }

            if (MatchingJobs.Count() > 0)
            {
                ShowMatchesButton.IsVisible = true;
            }
            else
            {
                ShowMatchesButton.IsVisible = false;
            }

            PageStackLayout.IsVisible = true;
            MatchingJobsFound.Text = $"We've found {MatchingJobs.Count()} matching Jobs for you";
        }

        private async void OnShowMatchesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowMatchesPage(MatchingJobs));
        }

        
    }
}
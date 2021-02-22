using Jobs247.Model;
using Jobs247.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        private List<string> jobTitleList;
        public List<string> JobTitleList
        {
            get { return jobTitleList; }
            set
            {
                jobTitleList = value;
                OnPropertyChanged(nameof(JobTitleList));
            }
        }

        private List<string> companyNameList;
        public List<string> CompanyNameList
        {
            get { return companyNameList; }
            set
            {
                companyNameList = value;
                OnPropertyChanged(nameof(CompanyNameList));
            }
        }
        public List<Job> JobList { get; set; }
        public RestService RestService { get; set; }

        public List<Job> MatchingJobs { get; set; }
 

        public JobFilterPage()
        {
            InitializeComponent();
            RestService = new RestService();
            JobList = new List<Job>();
            JobTitleList = new List<string>();
            CompanyNameList = new List<string>();
            MatchingJobs = new List<Job>();
            JobTitlePicker.SelectedIndexChanged += this.MyPickerSelectedIndexChanged;
            CompanyNamePicker.SelectedIndexChanged += this.MyPickerSelectedIndexChanged;
        }

        protected async override void OnAppearing()
        {
            JobList = await RestService.GetJobPostings();
            foreach(var item in JobList)
            {
                JobTitleList.Add(item.Position.name);
                CompanyNameList.Add(item.Company.name);
            }
            JobTitleList = JobTitleList.Distinct().ToList();
            CompanyNameList = CompanyNameList.Distinct().ToList();
            JobTitlePicker.ItemsSource = JobTitleList;
            CompanyNamePicker.ItemsSource = CompanyNameList;

            string MatchingJobsFoundCount = JobList.Count().ToString();
            MatchingJobsFound.Text = $"We've found {MatchingJobsFoundCount} matching Jobs for you";
        }


        private async void OnShowMatchesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowMatchesPage(MatchingJobs));
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(SearchBar.Text);
        }
        public void MyPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMatchingJobsLabel();
        }

        public void ChangeMatchingJobsLabel()
        {
            MatchingJobs.Clear();
            Debug.WriteLine(CompanyNamePicker.SelectedIndex);
            Debug.WriteLine(JobTitlePicker.SelectedIndex);
            if (JobTitlePicker.SelectedIndex == -1)
            {
                foreach(var item in JobList)
                {
                    if (CompanyNameList[CompanyNamePicker.SelectedIndex] == item.Company.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
            }
            else if (CompanyNamePicker.SelectedIndex == -1)
            {
                foreach (var item in JobList)
                {
                    if (JobTitleList[JobTitlePicker.SelectedIndex] == item.Position.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in JobList)
                {
                    if (CompanyNameList[CompanyNamePicker.SelectedIndex] == item.Company.name && JobTitleList[JobTitlePicker.SelectedIndex] == item.Position.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
            }

            string MatchingJobsFoundCount = MatchingJobs.Count().ToString();
            MatchingJobsFound.Text = $"We've found {MatchingJobsFoundCount} matching Jobs for you";
        }
    }
}
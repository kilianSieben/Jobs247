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
        public string MatchingJobString { get; set; }
        public List<string> JobTitleList { get; set; }
        public List<string> CompanyNameList { get; set; }
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
            JobList.Clear();
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

            ChangeMatchingJobsLabel();
        }


        private async void OnShowMatchesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowMatchesPage(MatchingJobs));
        }

        private void SearchBarTextChanged(object sender, EventArgs e)
        {
            ChangeMatchingJobsLabel();
        }
        private void MyPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMatchingJobsLabel();
        }

        private void ChangeMatchingJobsLabel()
        {
            MatchingJobs.Clear();
            string MatchingJobsFoundCount = JobList.Count().ToString();
            if (JobTitlePicker.SelectedIndex == -1 && CompanyNamePicker.SelectedIndex != -1)
            {
                foreach(var item in JobList)
                {
                    if (CompanyNameList[CompanyNamePicker.SelectedIndex] == item.Company.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
                MatchingJobsFoundCount = MatchingJobs.Count().ToString();
            }
            else if (CompanyNamePicker.SelectedIndex == -1 && JobTitlePicker.SelectedIndex != -1)
            {
                foreach (var item in JobList)
                {
                    if (JobTitleList[JobTitlePicker.SelectedIndex] == item.Position.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
                MatchingJobsFoundCount = MatchingJobs.Count().ToString();
            }
            else if (CompanyNamePicker.SelectedIndex != -1 && JobTitlePicker.SelectedIndex != -1)
            {
                foreach (var item in JobList)
                {
                    if (CompanyNameList[CompanyNamePicker.SelectedIndex] == item.Company.name && JobTitleList[JobTitlePicker.SelectedIndex] == item.Position.name)
                    {
                        MatchingJobs.Add(item);
                    }
                }
                MatchingJobsFoundCount = MatchingJobs.Count().ToString();
            }
            if (SearchBar.Text != null)
            {
                if(CompanyNamePicker.SelectedIndex == -1 && JobTitlePicker.SelectedIndex == -1)
                {
                    foreach (var item in JobList)
                    {
                        if (item.Company.name.Contains(SearchBar.Text)) MatchingJobs.Add(item);
                        else if (item.Position.name.Contains(SearchBar.Text)) MatchingJobs.Add(item);
                        else if (item.Description.Contains(SearchBar.Text)) MatchingJobs.Add(item);
                    }
                    MatchingJobsFoundCount = MatchingJobs.Count().ToString();
                }
                else
                {
                    var tempMatchingJobs = new ObservableCollection<Job>(MatchingJobs);
                    foreach (var item in tempMatchingJobs)
                    {
                        if (item.Company.name.Contains(SearchBar.Text) == false && 
                            item.Position.name.Contains(SearchBar.Text) == false && 
                            item.Description.Contains(SearchBar.Text) == false)
                        {
                            MatchingJobs.Remove(item);
                        }
                    }
                    MatchingJobsFoundCount = MatchingJobs.Count().ToString();
                }
                
            }
            else if (SearchBar.Text == null && CompanyNamePicker.SelectedIndex == -1 && JobTitlePicker.SelectedIndex == -1)
            {
                foreach(var item in JobList)
                {
                    MatchingJobs.Add(item);
                }
            }
            if (MatchingJobsFoundCount != "0")
            {
                MatchingJobsFound.Text = $"We've found {MatchingJobsFoundCount} matching Jobs for you";
                ShowMatchesButton.IsVisible = true;

            }
            else
            {
                MatchingJobsFound.Text = $"We've found {MatchingJobsFoundCount} matching Jobs for you";
                ShowMatchesButton.IsVisible = false;
            }

            
        }

        
    }
}
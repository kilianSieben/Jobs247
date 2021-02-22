using Jobs247.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMatchesPage : ContentPage
    {
        public List<Job> MatchingJobsListViewItems { get; set; }

        public ShowMatchesPage(List<Job> MatchingJobs)
        {
            InitializeComponent();

            MatchingJobsListViewItems = new List<Job>();
            MatchingJobsListViewItems = MatchingJobs;

            MatchingJobsListView.ItemsSource = MatchingJobs;

        }

        private async void OnMatchingJobsItemClicked(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new SpecificJobPage());
        }

    }
}
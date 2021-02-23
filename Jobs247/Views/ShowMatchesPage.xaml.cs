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
        public ShowMatchesPage(List<Job> MatchingJobs)
        {
            InitializeComponent();

            MatchingJobsListView.ItemsSource = new ObservableCollection<Job>(MatchingJobs);
        }

        protected override void OnAppearing()
        {
            MatchingJobsListView.SelectedItem = null;
        }

        private async void OnMatchingJobsItemClicked(object sender, SelectedItemChangedEventArgs e)
        {
            if (MatchingJobsListView.SelectedItem != null)
            {
                await Navigation.PushAsync(new SpecificJobPage(e.SelectedItem as Job));
            }
            
        }
    }
}
using Jobs247.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMatchesPage : ContentPage
    {
        private ObservableCollection<Job> matchingJobs;
        public ObservableCollection<Job> MatchingJobs
        {
            get { return matchingJobs; }
            set { matchingJobs = value; }
        }

        public ShowMatchesPage()
        {
            InitializeComponent();

            MatchingJobs = new ObservableCollection<Job>();
            //MatchingJobs.Add(new Job { JobTitle = "Software Developer" });
            //MatchingJobs.Add(new Job { JobTitle = "Support" });
            //MatchingJobs.Add(new Job { JobTitle = "App Developer" });
            //MatchingJobs.Add(new Job { JobTitle = "Actor" });

            MatchingJobsListView.ItemsSource = MatchingJobs;

        }

        private async void OnMatchingJobsItemClicked(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new SpecificJobPage());
        }

    }
}
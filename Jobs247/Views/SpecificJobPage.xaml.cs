using Jobs247.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpecificJobPage : ContentPage
    {
        public SpecificJobPage(Job SpecificJob)
        {
            InitializeComponent();
            PositionLabel.Text = "Position: " + SpecificJob.Position.name;
            CompanyLabel.Text = "Company: " + SpecificJob.Company.name;
            DescriptionLabel.Text = "Description: " + SpecificJob.Description;
        }

        private async void OnApplyClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Application", "Thank you for your application.", "OK");
        }
    }
}
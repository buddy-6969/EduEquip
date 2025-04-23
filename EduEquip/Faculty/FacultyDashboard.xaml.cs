using Microsoft.Maui.Controls;

namespace EduEquip;

public partial class FacultyDashboard : ContentPage
{
    private string _facultyId;

    public FacultyDashboard(string facultyId)
    {
        InitializeComponent();
        _facultyId = facultyId;
        WelcomeLabel.Text = $"Welcome, Professor {facultyId}";
    }

    private async void OnCreateSubjectClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateSubject(_facultyId));
    }

    private async void OnManageProjectsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageProjects(_facultyId));
    }

    private async void OnViewEquipmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewBorrowedEquipment(_facultyId));
    }

    private async void OnViewOverdueClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewOverdueEquipment());
    }
}
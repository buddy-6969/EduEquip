using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EduEquip
{
    public class Subject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Units { get; set; }
    }

    public partial class CreateSubject : ContentPage, INotifyPropertyChanged
    {
        private string _facultyId;
        private ObservableCollection<Subject> _subjects;

        public ObservableCollection<Subject> Subjects
        {
            get => _subjects;
            set
            {
                _subjects = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasSubjects));
                OnPropertyChanged(nameof(HasNoSubjects));
            }
        }

        public bool HasSubjects => Subjects != null && Subjects.Count > 0;
        public bool HasNoSubjects => !HasSubjects;

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CreateSubject(string facultyId)
        {
            InitializeComponent();
            _facultyId = facultyId;
            Subjects = new ObservableCollection<Subject>();

            // Load any existing subjects for this faculty
            LoadSubjects();

            // Set up commands
            EditCommand = new Command<Subject>(OnEditSubject);
            DeleteCommand = new Command<Subject>(OnDeleteSubject);

            // Set binding context
            BindingContext = this;

            // Update welcome label
            WelcomeLabel.Text = $"Welcome, Professor {facultyId}";
        }

        private void LoadSubjects()
        {
            // In a real app, we would load from a database or service
            // For this demo, we'll add some sample data
            Subjects.Add(new Subject
            {
                Code = "CPE101",
                Name = "Introduction to Computer Engineering",
                Description = "Basic concepts and principles of computer engineering",
                Units = 3
            });

            Subjects.Add(new Subject
            {
                Code = "CPE205",
                Name = "Digital Logic Design",
                Description = "Fundamentals of digital electronics and logic circuits",
                Units = 4
            });
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Clear all fields
            SubjectCodeEntry.Text = string.Empty;
            SubjectNameEntry.Text = string.Empty;
            SubjectDescriptionEditor.Text = string.Empty;
            SubjectUnitsEntry.Text = string.Empty;
        }

        private async void OnCreateSubjectClicked(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(SubjectCodeEntry.Text) ||
                string.IsNullOrWhiteSpace(SubjectNameEntry.Text) ||
                string.IsNullOrWhiteSpace(SubjectUnitsEntry.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in all required fields.", "OK");
                return;
            }

            // Parse units
            if (!int.TryParse(SubjectUnitsEntry.Text, out int units))
            {
                await DisplayAlert("Validation Error", "Units must be a number.", "OK");
                return;
            }

            // Create new subject
            var newSubject = new Subject
            {
                Code = SubjectCodeEntry.Text,
                Name = SubjectNameEntry.Text,
                Description = SubjectDescriptionEditor.Text ?? "",
                Units = units
            };

            // Add to collection
            Subjects.Add(newSubject);

            // Clear form
            SubjectCodeEntry.Text = string.Empty;
            SubjectNameEntry.Text = string.Empty;
            SubjectDescriptionEditor.Text = string.Empty;
            SubjectUnitsEntry.Text = string.Empty;

            // Show confirmation
            await DisplayAlert("Success", $"Subject {newSubject.Code} has been created.", "OK");
        }

        private void OnSubjectSelected(object sender, SelectionChangedEventArgs e)
        {
            // Reset selection
            SubjectsCollection.SelectedItem = null;
        }

        private async void OnEditSubject(Subject subject)
        {
            // In a full implementation, we would navigate to an edit page or show a modal
            await DisplayAlert("Edit Subject", $"Editing subject {subject.Code} is not implemented in this demo.", "OK");
        }

        private async void OnDeleteSubject(Subject subject)
        {
            bool confirm = await DisplayAlert("Confirm Delete",
                $"Are you sure you want to delete subject {subject.Code}?",
                "Yes", "No");

            if (confirm)
            {
                Subjects.Remove(subject);
                await DisplayAlert("Success", $"Subject {subject.Code} has been deleted.", "OK");
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
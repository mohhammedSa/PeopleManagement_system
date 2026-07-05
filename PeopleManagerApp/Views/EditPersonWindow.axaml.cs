using Avalonia.Controls;
using PeopleManagerApp.Models;
using PeopleManagerApp.ViewModels;

namespace PeopleManagerApp.Views;

public partial class EditPersonWindow : Window
{
    public EditPersonWindow(string title)
    {
        Title = title;
        InitializeComponent();
    }
}
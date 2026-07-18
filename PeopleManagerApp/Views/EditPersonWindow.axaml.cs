using Avalonia.Controls;
using PeopleManagerApp.ViewModels;

namespace PeopleManagerApp.Views;

public partial class EditPersonWindow : Window
{
    public EditPersonWindow(string title)
    {
        Title = title;
        InitializeComponent();
        var vm = new AddEditPersonViewModel();
        DataContext = vm;
    }
}
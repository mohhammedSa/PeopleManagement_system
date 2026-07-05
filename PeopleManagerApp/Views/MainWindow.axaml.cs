using System;
using Avalonia.Controls;
using PeopleManagerApp.Models;
using PeopleManagerApp.ViewModels;

namespace PeopleManagerApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var vm = new MainWindowViewModel();
        vm.OpenEditFormWindow = person =>
        {
            var win = new EditPersonWindow("Edit Person Info");
            var addEditVm = new AddEditPersonViewModel(vm.People, person)
            {
                ClosePage = win.Close
            };
            win.DataContext = addEditVm;
            win.Show();
        };
        vm.OpenAddPersonForm = () =>
        {
            var win = new EditPersonWindow("Add new Person");
            var addVm = new AddEditPersonViewModel(vm.People)
            {
                ClosePage = win.Close
            };
            win.DataContext = addVm;
            win.Show();
        };
        DataContext = vm;
    }

    private void OpenForm(string title, MainWindowViewModel vm)
    {
        var win = new EditPersonWindow(title);
        win.Show();
        var addVm = new AddEditPersonViewModel(vm.People)
        {
            ClosePage = win.Close
        };
        win.DataContext = addVm;
    }
}
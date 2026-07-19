using System;
using Avalonia.Controls;
using PeopleManagerApp.Models;
using PeopleManagerApp.Services;
using PeopleManagerApp.ViewModels;

namespace PeopleManagerApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        var peopleService = new PeopleService();
        InitializeComponent();
        var vm = new MainWindowViewModel(peopleService);
        vm.OpenEditFormWindow = personCopy =>
        {
            var win = new EditPersonWindow("Edit Person Info");
            var addEditVm = new AddEditPersonViewModel(personCopy);
            addEditVm.PersonSaved += vm.SavePerson;
            addEditVm.OnDelete += vm.DeletePerson;
            addEditVm.ClosePage = win.Close;
            win.DataContext = addEditVm;
            win.Show();
        };
        vm.OpenAddPersonForm = () => 
        {
            var win = new EditPersonWindow("Add new Person");
            var addVm = new AddEditPersonViewModel();
            addVm.PersonSaved += vm.SavePerson;
            addVm.ClosePage = win.Close;
            win.DataContext = addVm;
            win.Show();
        };
        DataContext = vm;
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeopleManagerApp.Models;

namespace PeopleManagerApp.ViewModels;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public MainWindowViewModel()
    {
        _addPerson = new RelayCommand(OpenAddForm);
        _editPersonCommand = new RelayCommand(OpenEditForm);
        _deletePersonCommand = new RelayCommand(DeletePerson);
    }

    public ObservableCollection<Person> People {get;} = [
        new() { Name= "Hamouda",Age= "30"},
        new() { Name= "Wafae",Age= "22"}
    ];

    private readonly RelayCommand _addPerson;
    public ICommand AddPersonCommand => _addPerson;
    public Action? OpenAddPersonForm { get; set;}

    private void OpenAddForm(object? _)
    {
        OpenAddPersonForm?.Invoke();
    }

    private readonly RelayCommand _editPersonCommand;
    public ICommand EditPersonCommand => _editPersonCommand;

    public Action<Person?>? OpenEditFormWindow { get; set; }
    private void OpenEditForm(object? parameter)
    {
        if (parameter is Person person)
        {
            Console.WriteLine(person.Name);
            OpenEditFormWindow?.Invoke(person);
        }
    }

    private readonly RelayCommand _deletePersonCommand;
    public ICommand DeletePersonCommand => _deletePersonCommand;

    private void DeletePerson(object? parameter)
    {
        if (parameter is Person person)
        {
            People.Remove(person);
        }
    }
}
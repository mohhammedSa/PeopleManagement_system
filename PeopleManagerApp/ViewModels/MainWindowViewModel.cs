using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeopleManagerApp.Models;
using PeopleManagerApp.Services;

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

    public MainWindowViewModel(PeopleService peopleService)
    {
        _peopleService = peopleService;
        People = _peopleService.People;
        _addPersonCommand = new RelayCommand(OpenAddForm);
        _editPersonCommand = new RelayCommand(OpenEditForm);
        _deletePersonCommand = new RelayCommand(DeletedPerson);
    }

    public ReadOnlyObservableCollection<Person> People { get; }
    
    private readonly PeopleService _peopleService;

    private string _header = string.Empty;

    public string Header
    {
        get => _header;
        set
        {
            SetField(ref _header, value);
        }
    }

    private readonly RelayCommand _addPersonCommand;
    public ICommand AddPersonCommandCommand => _addPersonCommand;
    public Action OpenAddPersonForm { get; set;}

    private void OpenAddForm(object? _)
    {
        OpenAddPersonForm.Invoke();
    }
    
    private readonly RelayCommand _editPersonCommand;
    public ICommand EditPersonCommand => _editPersonCommand;

    public Action<Person?>? OpenEditFormWindow { get; set; }
    private void OpenEditForm(object? parameter)
    {
        if (parameter is Person person)
        {
            Person personCopy = new ()
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };
            OpenEditFormWindow?.Invoke(personCopy);
        }
    }

    public void SavePerson(Person? person, AddEditPersonViewModel.EnMode mode)
    {
        if(person == null) return;
        if(mode == AddEditPersonViewModel.EnMode.AddMode)
            _peopleService.AddPerson(person);
        else
            _peopleService.UpdatePerson(person);
    }

    private readonly RelayCommand _deletePersonCommand;
    public ICommand DeletePersonCommand => _deletePersonCommand;

    private void DeletedPerson(object? parameter)
    {
        if(parameter is Person person) 
            DeletePerson(person);
    }
    public void DeletePerson(Person? person)
    {
        if (person == null) return;
        _peopleService.DeletePerson(person.Id);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeopleManagerApp.Libraries;
using PeopleManagerApp.Models;

namespace PeopleManagerApp.ViewModels;

public class AddEditPersonViewModel : INotifyPropertyChanged
{
    enum  EnMode
    {
        AddMode = 1,
        EditMode=2
    }

    private readonly EnMode _mode;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public AddEditPersonViewModel(ObservableCollection<Person>? people = null, Person? person = null)
    {
        _people = people;
        _person = person;
        _mode = _person == null ? EnMode.AddMode : EnMode.EditMode;
        _saveCommand = new RelayCommand(SavePerson, CanSave);
        _deleteCommand = new RelayCommand(DeletePerson,CanDelete);
        _cancelCommand = new RelayCommand(Cancel);

        if (_person == null) return;
        PersonName = _person.Name;
        PersonAge = _person.Age;
    }

    private readonly ObservableCollection<Person>? _people;
    private readonly Person? _person;
    private string _personName = string.Empty;
    private string _personAge = string.Empty;

    public string PersonName
    {
        get => _personName;
        set
        {
             if(!SetField(ref _personName, value)) return;
            _saveCommand.RaiseCanExecuteChanged();
        }
    }
    public string PersonAge
    {
        get => _personAge;
        set
        {
            if(!SetField(ref _personAge, value)) return;
            AgeError = InputValidation.IsValidNumber(PersonAge);
            IsErrorVisible = !string.IsNullOrWhiteSpace(AgeError);
            _saveCommand.RaiseCanExecuteChanged();
        }
    }

    private string _ageError = string.Empty;

    public string AgeError
    {
        get => _ageError;
        set => SetField(ref _ageError, value);
    }
    private bool _isErrorVisible;
    public bool IsErrorVisible
    {
        get => _isErrorVisible;
        set => SetField(ref _isErrorVisible, value);
    }

    private readonly RelayCommand _saveCommand;
    public ICommand SaveCommand => _saveCommand;
    
    private readonly RelayCommand _deleteCommand;
    public ICommand DeleteCommand => _deleteCommand;
    
    
    private readonly RelayCommand _cancelCommand;
    public ICommand CancelCommand => _cancelCommand;
    

    public Action? ClosePage { get; set; }
    private void SavePerson(object? _)
    { 
        if (_mode == EnMode.EditMode)
        {
            if (_person != null)
            {
                _person.Name = PersonName;
                _person.Age = PersonAge;
            }
        }
        else 
            _people?.Add(new Person()
            {
             Name   = PersonName,
             Age = PersonAge
            });
        ClosePage?.Invoke();
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(PersonName) && !string.IsNullOrWhiteSpace(PersonAge);
    }

    public void DeletePerson(object? _)
    {
        if(_people == null) return;
        if (_person == null) return;
        _people.Remove(_person);
        ClosePage?.Invoke();
    }

    private bool CanDelete()
    {
        return _mode == EnMode.EditMode;
    }

   private void Cancel(object? _)
   {
       ClosePage?.Invoke();
   }
}
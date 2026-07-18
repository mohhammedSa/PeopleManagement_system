using System;
using System.Collections.Generic;
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

    public AddEditPersonViewModel(Person? person = null)
    {
        _mode = person == null ? EnMode.AddMode : EnMode.EditMode;
        _saveCommand = new RelayCommand(SavePerson, CanSave);
        _deleteCommand = new RelayCommand(DeletePerson,CanDelete);
        _cancelCommand = new RelayCommand(Cancel);

        InitializePersonObject(person);
    }
    
    private Person? _person = new ();
    
    private void InitializePersonObject(Person? person)
    {
        if (person == null)
        {
            _person = new Person()
            {
                Id = null,
                Name = "",
                Age = ""
            };
        }
        else
        {
            _person = person;
            PersonName = _person.Name;
            PersonAge = _person.Age;
        }
    }
    
    private string? _personName = string.Empty;
    private string? _personAge = string.Empty;

    public string? PersonName
    {
        get => _personName;
        set
        {
             if(!SetField(ref _personName, value)) return;
            _saveCommand.RaiseCanExecuteChanged();
        }
    }
    public string? PersonAge
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
    
    private void SavePerson(object? parameter)
    {
        if(_person == null) return;
        _person.Name = PersonName;
        _person.Age = PersonAge;
        OnPersonSaved(_person);
        ClosePage?.Invoke();
    }
    
    public event Action<Person?>? PersonSaved;
    protected virtual void OnPersonSaved(Person? person)
    {
        PersonSaved?.Invoke(person);
    }
    
    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(PersonName) && !string.IsNullOrWhiteSpace(PersonAge);
    }
    
    private readonly RelayCommand _deleteCommand;
    public ICommand DeleteCommand => _deleteCommand;
    
    private readonly RelayCommand _cancelCommand;
    public ICommand CancelCommand => _cancelCommand;

    public Action? ClosePage { get; set; }
    public event Action<Person?>? OnDelete;
    
    protected virtual void DeletePerson(Person? person)
    {
        OnDelete?.Invoke(person);
    }

    public void DeletePerson(object? obj)
    {
            DeletePerson(_person);
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleManagerApp.Models;

public sealed class Person : INotifyPropertyChanged
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

    private int _id;

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    private string? _name = string.Empty;

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    private string? _age = string.Empty;

    public string? Age
    {
        get => _age;
        set => SetField(ref _age, value);
    }
}
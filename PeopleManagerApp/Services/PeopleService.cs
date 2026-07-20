using System.Collections.ObjectModel;
using System.Linq;
using PeopleManagerApp.Models;

namespace PeopleManagerApp.Services;

public class PeopleService : IPeopleService
{
    private  readonly ObservableCollection<Person> _people = new();
    public ReadOnlyObservableCollection<Person> People { get; }

    public PeopleService()
    {
        People = new ReadOnlyObservableCollection<Person>(_people);
    }

    public Person? FindPerson(int id)
    {
        return _people.FirstOrDefault(p => p.Id == id);
    }

    private int GetNextId()
    {
        return _people.Max(p => p.Id) + 1;
    }

    public void AddPerson(Person person)
    {
        int id = _people.Any() ? GetNextId(): 1;
        _people.Add(new Person()
        {
            Id = id,
            Name = person.Name,
            Age = person.Age
        });
    }

    public void UpdatePerson(Person person)
    {
        Person? found = FindPerson(person.Id);
        if (found != null)
        {
            found.Name = person.Name;
            found.Age = person.Age;
        }
    }

    public void DeletePerson(int id)
    {
        Person? found = FindPerson(id);
        if (found != null)
        {
            _people.Remove(found);
        }
    }
}
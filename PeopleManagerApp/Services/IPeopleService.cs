using System.Collections.ObjectModel;
using PeopleManagerApp.Models;

namespace PeopleManagerApp.Services;

public interface IPeopleService
{
    public ReadOnlyObservableCollection<Person> People { get; }
    public void AddPerson(Person person);
    public void DeletePerson(int id);
    public void UpdatePerson(Person person);
}
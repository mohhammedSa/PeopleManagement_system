namespace PeopleManagerApp.Libraries;

public static class InputValidation
{
    public static string IsValidNumber(string? strNumber)
    {
        if(string.IsNullOrWhiteSpace(strNumber)) 
            return  "Field required.";
        
        if (!int.TryParse(strNumber, out int age))
            return "Should be a number.";

        if (age < 18) return "Should be bigger than oe equal 18.";
        return "";
    }
}
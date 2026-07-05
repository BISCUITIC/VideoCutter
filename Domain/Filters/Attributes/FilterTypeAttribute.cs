namespace Domain.Filters.Attributes;

public class FilterTypeAttribute : Attribute
{
    public string Type { get; }

    public FilterTypeAttribute(string type)
    {
        Type = type;
    }
}

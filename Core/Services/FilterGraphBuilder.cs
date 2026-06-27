using System.Text;

namespace Core.Services;

public class FilterGraphBuilder
{
    private readonly StringBuilder _builder;
    private int _index;

    public FilterGraphBuilder()
    {
        _index = 0;
        _builder = new StringBuilder();
    }

    public void Init()
    {
        _builder.Clear();

        _builder.Append($"-filter_complex \"[0:v]null[out{_index}];");
    }

    public void Add(string filter)
    {
        _builder.Append($"[out{_index++}]");
        _builder.Append(filter);
        _builder.Append($"[out{_index}];");
    }

    public string Build()
    {
        _builder.Append($"\" -map \"[out{_index}]\"");

        return _builder.ToString();
    }
}

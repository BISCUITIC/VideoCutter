using System.Text;

namespace Core.Services;

public class FilterGraphBuilder
{
    private const string VideoStream = "v_out";
    private const string AudioStream = "a_out";

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

        _builder.Append("-filter_complex \"");
        _builder.Append($"[0:v]null[{VideoStream}{_index}];");
        _builder.Append($"[0:a]anull[{AudioStream}];");
    }

    public void Add(string filter)
    {
        _builder.Append($"[{VideoStream}{_index++}]");
        _builder.Append(filter);
        _builder.Append($"[{VideoStream}{_index}];");
    }

    public string Build()
    {
        _builder.Append("\"");
        _builder.Append($" -map \"[{VideoStream}{_index}]\"");
        _builder.Append($" -map \"[{AudioStream}]\"");

        return _builder.ToString();
    }
}

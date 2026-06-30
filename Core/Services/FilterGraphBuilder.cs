using System.Text;

namespace Core.Services;

public class FilterGraphBuilder
{
    private readonly string _outputVideoStream = "v_out";
    private readonly string _outputAudioStream = "a_out";

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
        _builder.Append($"[0:v]null[{_outputVideoStream}{_index}];");
        _builder.Append($"[0:a]anull[{_outputAudioStream}];");
    }

    public void Add(string filter)
    {
        _builder.Append($"[{_outputVideoStream}{_index++}]");
        _builder.Append(filter);
        _builder.Append($"[{_outputVideoStream}{_index}];");
    }

    public string Build()
    {
        _builder.Append("\" ");
        _builder.Append($"-map \"[{_outputVideoStream}{_index}]\"");
        _builder.Append($"-map \"[{_outputAudioStream}]\"");

        return _builder.ToString();
    }
}

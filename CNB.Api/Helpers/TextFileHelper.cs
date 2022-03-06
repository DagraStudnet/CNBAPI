using System.Text;

namespace CNB.Api.Helpers;

public static class TextFileHelper
{
    public static IEnumerable<string> ReadTextFileLine(Stream stream)
    {
        using var sr = new StreamReader(stream, Encoding.UTF8);
        var line = string.Empty;

        while ((line = sr.ReadLine()) != null)
        {
            yield return line;
        }
    }
}
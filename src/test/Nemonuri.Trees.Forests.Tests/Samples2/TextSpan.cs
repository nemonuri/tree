namespace Nemonuri.Trees.Forests.Tests.Samples2;

public readonly record struct TextSpan(int Offset, int Length)
{
    public static readonly TextSpan None = new(0, -1);

    public int End => Offset + Length;

    public static TextSpan CreateFromBound(int offset, int end)
    {
        return new TextSpan(offset, end - offset);
    }

    public static TextSpan Merge(IEnumerable<TextSpan> textSpans)
    {
        var ensuredTextSpans = textSpans.Where(static a => !a.IsNone);
        if (!ensuredTextSpans.Any()) { return TextSpan.None; }

        int offset = ensuredTextSpans.Select(static a => a.Offset).Min();
        int end = ensuredTextSpans.Select(static a => a.End).Max();
        return CreateFromBound(offset, end);
    }

    public TextSpan ReduceFromLeft(int length)
    {
        return new TextSpan(Offset + length, Length - length);
    }

    public bool IsNone => Offset < 0 || Length < 0;
}
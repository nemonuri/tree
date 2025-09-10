using Nemonuri.Trees.Parsers.Tests.Samples;

namespace Nemonuri.Trees.Parsers.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        SourceString sourceString = new("a1b2c3d4e5");
        StartWithParser parser = new("a1b2");

        // Act
        SampleSyntaxForestBuilder parsed = parser.Parse(sourceString, Range.All);
        var actual = parsed.MatchLengths.Select(parsedLength => parsed.SourceString[0..parsedLength].InternalString);

        // Assert
        string[] expected = ["a1b2"];
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        SourceString sourceString = new("a1b2c3d4e5");
        MultiStartWithParser parser = new(["a1b2c3d", "a2", "a1b2"]);

        // Act
        SampleSyntaxForestBuilder parsed = parser.Parse(sourceString, Range.All);
        var actual = parsed.MatchLengths.Select(parsedLength => parsed.SourceString[0..parsedLength].InternalString).ToHashSet();

        // Assert
        HashSet<string> expected = ["a1b2c3d", "a1b2"];
        Assert.Equal(expected, actual);
    }
}

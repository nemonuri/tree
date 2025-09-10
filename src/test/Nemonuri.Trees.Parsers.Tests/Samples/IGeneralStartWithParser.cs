
namespace Nemonuri.Trees.Parsers.Tests.Samples;

public interface IGeneralStartWithParser : IParser<char, SourceString, SampleSyntaxForestBuilder, IGeneralStartWithParser>
{ }

public interface IGeneralStartWithParserTree :
    IParserTree<char, SourceString, SampleSyntaxForestBuilder, IGeneralStartWithParserTree>,
    IGeneralStartWithParser
{ }
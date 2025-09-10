namespace Nemonuri.Trees.Parsers;

public interface IString<out TChar, TString> : IReadOnlyList<TChar>, ISupportSlice<TString>
    where TString : IString<TChar, TString>
{ 
}

public interface IGeneralString<TChar> : IString<TChar, IGeneralString<TChar>>
{ }
namespace Nemonuri.Grammars.Infrastructure;

public struct ValueWithIdealContext<T, TIdealContext>
{
    public T Value;
    public TIdealContext IdealContext;

    public ValueWithIdealContext(T value, TIdealContext idealContext)
    {
        Value = value;
        IdealContext = idealContext;
    }
}

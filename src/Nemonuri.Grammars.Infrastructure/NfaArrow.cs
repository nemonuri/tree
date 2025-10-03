namespace Nemonuri.Grammars.Infrastructure;


public class NfaArrow<T, TId> : IArrow<TId, TId>
    where TId : IEquatable<TId>
{
    public TId Tail => throw new NotImplementedException();

    public TId Head => throw new NotImplementedException();
}

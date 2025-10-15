namespace Nemonuri.Graphs.Infrastructure;

public readonly record struct ValueNull
{
    private static ValueNull s_valueNull;

    public static ref ValueNull ValueNullRef => ref s_valueNull;
}

using System;
using static System.Console;

public static class Util
{
    public static unsafe void Bin<T>(in T value) where T : unmanaged
    {
        var s = stackalloc byte[sizeof(T)];
        *(T*)s = value;
        for (int i = sizeof(T) - 1; i >= 0; i--)
        {
            Write(Convert.ToString(s[i], 2).PadLeft(8, '0'));
        }
        WriteLine();
    }

    public static unsafe void Hex<T>(in T value) where T : unmanaged
    {
        var s = stackalloc byte[sizeof(T)];
        *(T*)s = value;
        for (int i = sizeof(T) - 1; i >= 0; i--)
        {
            Write(s[i].ToString("X").PadLeft(2, '0'));
        }
        WriteLine();
    }
}

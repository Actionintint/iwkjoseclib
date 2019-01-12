using System;

public class Xorshift
{
    uint x = 123456789;
    uint y = 362436069;
    uint z = 521288629;
    uint w = 88675123;

    public Xorshift() : this(Environment.TickCount)
    {

    }

    public Xorshift(int seed)
    {
        var t = (uint)seed;
        x ^= t;
        y ^= Shift(t, 17);
        z ^= Shift(t, 31);
        w ^= Shift(t, 18);

        uint Shift(uint u, int n) => u << n | u >> 32 - n;
    }

    public int Next()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        t = w = w ^ w >> 19 ^ t ^ t >> 8;
        t &= int.MaxValue;
        return t == int.MaxValue ? int.MaxValue - 1 : (int)t;
    }

    public int Next(int minValue, int maxValue) => Next(maxValue - minValue) + minValue;

    uint Xorshift128()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        return w = w ^ w >> 19 ^ t ^ t >> 8;
    }

    public int Next(int maxValue) => (int)(Xorshift128() % maxValue);

    public double NextDouble() => Next() * (1.0 / int.MaxValue);

    public unsafe void NextBytes(Span<byte> buffer)
    {
        fixed (byte* p = buffer)
        {
            var last = p + buffer.Length;
            var i = p;
            while (i + 3 < last)
            {
                *(uint*)i = Xorshift128();
                i += 4;
            }
            if (i + 1 < last)
            {
                *(ushort*)i = (ushort)Xorshift128();
                i += 2;
            }
            if (i < last)
            {
                *i = (byte)Xorshift128();
            }
        }
    }
}

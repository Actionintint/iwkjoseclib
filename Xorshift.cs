using System;

class Xorshift
{
    uint x = 123456789;
    uint y = 362436069;
    uint z = 521288629;
    uint w = 88675123;

    public Xorshift()
    {
        var t = (uint)Environment.TickCount;
        x ^= t;
        y ^= Shift(t, 17);
        z ^= Shift(t, 31);
        w ^= Shift(t, 18);
    }

    uint Shift(uint u, int n) => u << n | u >> 32 - n;

    public int Next()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        t = w = w ^ w >> 19 ^ t ^ t >> 8;
        t &= int.MaxValue;
        return t == int.MaxValue ? int.MaxValue - 1 : (int)t;
    }

    public int Next(int maxValue) => Next() % maxValue;
}

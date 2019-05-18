using System;

public class Xorshift
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

        uint Shift(uint u, int n) => u << n | u >> 32 - n;
    }

    uint Xorshift128()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        return w = w ^ w >> 19 ^ t ^ t >> 8;
    }

    public int Next(int maxValue) => (int)(Xorshift128() % (uint)maxValue);
}

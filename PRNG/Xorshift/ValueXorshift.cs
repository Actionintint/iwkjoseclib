using System;

public struct ValueXorshift
{
    uint x, y, z, w;

    public ValueXorshift(uint t)
    {
        x = 123456789;
        y = 362436069;
        z = 521288629;
        w = 88675123;

        x ^= t;
        y ^= Shift(t, 17);
        z ^= Shift(t, 31);
        w ^= Shift(t, 18);

        uint Shift(uint u, int n) => u << n | u >> 32 - n;
    }

    public uint Xorshift128()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        return w = w ^ w >> 19 ^ t ^ t >> 8;
    }

    public int Next(int maxValue) => (int)(Xorshift128() % (uint)maxValue);
}

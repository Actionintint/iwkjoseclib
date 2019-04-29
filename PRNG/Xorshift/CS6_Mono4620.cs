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
    }

    uint Shift(uint u, int n) => u << n | u >> 32 - n;

    uint Xorshift128()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        return w = w ^ w >> 19 ^ t ^ t >> 8;
    }

    public int Next()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        var k = w = w ^ w >> 19 ^ t ^ t >> 8;
        k &= int.MaxValue;
        return k == int.MaxValue ? int.MaxValue - 1 : (int)k;
    }

    public int Next(int maxValue) => (int)(Xorshift128() % (uint)maxValue);

    public int Next(int minValue, int maxValue) => Next(maxValue - minValue) + minValue;

    public double NextDouble() => Xorshift128() * (1.0 / ((double)uint.MaxValue + 1));

    public void NextBytes(byte[] buffer)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (byte)Xorshift128();
        }
    }
}


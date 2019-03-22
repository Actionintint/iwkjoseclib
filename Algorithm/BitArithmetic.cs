using System.Runtime.CompilerServices;

static class BitArithmetic
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountBits(int v)
    {
        return CountBits(unchecked((uint)v));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountBits(uint v)
    {
        unchecked
        {
            v = v - ((v >> 1) & 0x55555555u);
            v = (v & 0x33333333u) + ((v >> 2) & 0x33333333u);
            return (int)((v + (v >> 4) & 0xF0F0F0Fu) * 0x1010101u) >> 24;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountBits(long v)
    {
        return CountBits(unchecked((ulong)v));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountBits(ulong v)
    {
        const ulong Mask01010101 = 0x5555555555555555UL;
        const ulong Mask00110011 = 0x3333333333333333UL;
        const ulong Mask00001111 = 0x0F0F0F0F0F0F0F0FUL;
        const ulong Mask00000001 = 0x0101010101010101UL;
        v = v - ((v >> 1) & Mask01010101);
        v = (v & Mask00110011) + ((v >> 2) & Mask00110011);
        return (int)(unchecked(((v + (v >> 4)) & Mask00001111) * Mask00000001) >> 56);
    }
}

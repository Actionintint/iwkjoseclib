public class Xoroshiro128_ // http://xoshiro.di.unimi.it/xoroshiro128plus.c
{
    /* This is xoroshiro128+ 1.0, our best and fastest small-state generator
   for floating-point numbers. We suggest to use its upper bits for
   floating-point generation, as it is slightly faster than
   xoroshiro128**. It passes all tests we are aware of except for the four
   lower bits, which might fail linearity tests (and just those), so if
   low linear complexity is not considered an issue (as it is usually the
   case) it can be used to generate 64-bit outputs, too; moreover, this
   generator has a very mild Hamming-weight dependency making our test
   (http://prng.di.unimi.it/hwd.php) fail after 5 TB of output; we believe
   this slight bias cannot affect any application. If you are concerned,
   use xoroshiro128** or xoshiro256+.

   We suggest to use a sign test to extract a random Boolean value, and
   right shifts to extract subsets of bits.

   The state must be seeded so that it is not everywhere zero. If you have
   a 64-bit seed, we suggest to seed a splitmix64 generator and use its
   output to fill s. 

   NOTE: the parameters (a=24, b=16, b=37) of this version give slightly
   better results in our test than the 2016 version (a=55, b=14, c=36).
*/

    static ulong rotl(ulong x, int k) => (x << k) | (x >> (64 - k));


    static ulong[] s = new ulong[] { 1, 2 };

    public ulong Next()
    {
        ulong s0 = s[0];
        ulong s1 = s[1];
        ulong result = s0 + s1;

        s1 ^= s0;
        s[0] = rotl(s0, 24) ^ s1 ^ (s1 << 16); // a, b
        s[1] = rotl(s1, 37); // c

        return result;
    }


    /* This is the jump function for the generator. It is equivalent
       to 2^64 calls to next(); it can be used to generate 2^64
       non-overlapping subsequences for parallel computations. */

    static ulong[] JUMP = new ulong[] { 0xdf900294d8f554a5, 0x170865df4b3201fc };
    void jump()
    {

        ulong s0 = 0;
        ulong s1 = 0;
        for (int i = 0; i < JUMP.Length; i++)
            for (int b = 0; b < 64; b++)
            {
                if ((JUMP[i] & 1UL << b) != 0)
                {
                    s0 ^= s[0];
                    s1 ^= s[1];
                }
                Next();
            }

        s[0] = s0;
        s[1] = s1;
    }


    /* This is the long-jump function for the generator. It is equivalent to
       2^96 calls to next(); it can be used to generate 2^32 starting points,
       from each of which jump() will generate 2^32 non-overlapping
       subsequences for parallel distributed computations. */
    static ulong[] LONG_JUMP = new ulong[] { 0xd2a98b26625eee7b, 0xdddf9b1090aa7ac1 };

    void long_jump()
    {

        ulong s0 = 0;
        ulong s1 = 0;
        for (int i = 0; i < LONG_JUMP.Length; i++)
            for (int b = 0; b < 64; b++)
            {
                if ((LONG_JUMP[i] & 1UL << b) != 0)
                {
                    s0 ^= s[0];
                    s1 ^= s[1];
                }
                Next();
            }

        s[0] = s0;
        s[1] = s1;
    }
}


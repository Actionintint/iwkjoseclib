using System;

public class MT // http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/MT2002/CODES/mt19937ar.c
{
    const int N = 624;
    const int M = 397;
    const uint MATRIX_A = 0x9908b0df;   // constant vector a
    const uint UPPER_MASK = 0x80000000; // most significant w-r bits
    const uint LOWER_MASK = 0x7fffffff; // least significant r bits

    uint[] mt = new uint[N]; // the array for the state vector
    uint mti = N + 1; // mti==N+1 means mt[N] is not initialized

    // mag01[x] = x * MATRIX_A  for x=0,1
    uint[] mag01 = new uint[] { 0x0U, MATRIX_A };

    // initializes mt[N] with a seed
    void init_genrand(uint s)
    {
        mt[0] = s & 0xffffffffU;
        for (mti = 1; mti < N; mti++)
        {
            mt[mti] = 1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti;
            // See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier.
            // In the previous versions, MSBs of the seed affect
            // only MSBs of the array mt[].             
            // 2002/01/09 modified by Makoto Matsumoto
            mt[mti] &= 0xffffffffU;
            // for >32 bit machines
        }
    }
    // initialize by an array with array-length
    // init_key is the array for initializing keys
    // init_key.Length is its length
    // slight change for C++, 2004/2/26
    void init_by_array(uint[] init_key)
    {
        init_genrand(19650218U);
        uint i = 1;
        uint j = 0;
        int k = Math.Max(N, init_key.Length);
        for (; k != 0; k--)
        {
            mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) + init_key[j] + j; // non linear
            mt[i] &= 0xffffffffU; // for WORDSIZE > 32 machines
            i++;
            j++;
            if (i >= N)
            {
                mt[0] = mt[N - 1];
                i = 1;
            }
            if (j >= init_key.Length) j = 0;
        }
        for (k = N - 1; k != 0; k--)
        {
            mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - i; // non linear
            mt[i] &= 0xffffffffU; // for WORDSIZE > 32 machines
            i++;
            if (i >= N)
            {
                mt[0] = mt[N - 1];
                i = 1;
            }
        }

        mt[0] = 0x80000000U; // MSB is 1; assuring non-zero initial array
    }

    // generates a random number on [0,0xffffffff]-interval
    uint MT19937() 
    {
        uint y = 0;
        

        if (mti >= N)
        { // generate N words at one time
            int kk;
            // if init_genrand() has not been called,
            if (mti == N + 1) init_genrand(5489U); // a defaUt initial seed is used

            for (kk = 0; kk < N - M; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
            }
            for (; kk < N - 1; kk++)
            {
                y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
            }
            y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
            mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

            mti = 0;
        }

        y = mt[mti++];

        // Tempering
        y ^= y >> 11;
        y ^= (y << 7) & 0x9d2c5680U;
        y ^= (y << 15) & 0xefc60000U;
        y ^= y >> 18;

        return y;
    }

    // generates a random number on [0,0x7fffffff]-interval
    int genrand_int31() => (int)(MT19937() >> 1);

    // generates a random number on [0,1]-real-interval
    double genrand_real1() => MT19937() * (1.0 / 4294967295.0);
    // divided by 2^32-1


    // generates a random number on [0,1)-real-interval
    double genrand_real2() => MT19937() * (1.0 / 4294967296.0);
    // divided by 2^32


    // generates a random number on (0,1)-real-interval
    double genrand_real3() => (MT19937() + 0.5) * (1.0 / 4294967296.0);
    // divided by 2^32


    // generates a random number on [0,1) with 53-bit resolution
    double genrand_res53() => ((MT19937() >> 5) * 67108864.0 + (MT19937() >> 6)) * (1.0 / 9007199254740992.0);

    // These real versions are due to Isaku Wada, 2002/01/09 added

    public void main()
    {
        init_by_array(new uint[] { 0x123, 0x234, 0x345, 0x456 });
        Console.WriteLine("1000 outputs of genrand_int32()");
        for (int i = 0; i < 1000; i++)
        {
            Console.Write("{0,10} ", MT19937());
            if (i % 5 == 4) Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine("1000 outputs of genrand_real2()");
        for (int i = 0; i < 1000; i++)
        {
            Console.Write("{0,10} ", genrand_real2().ToString("G8"));
            if (i % 5 == 4) Console.WriteLine();
        }
    }
}

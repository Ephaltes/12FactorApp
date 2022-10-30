namespace TwelveFactorApp.Api;

public class PiCalculator : IPiCalculator
{
    public string GetPi(int precision)
    {
        //algorithm credits https://stackoverflow.com/questions/11677369/how-to-calculate-pi-to-n-number-of-places-in-c-sharp-using-loops
        precision++;

        uint[] x = new uint[precision * 10 / 3 + 2];
        uint[] r = new uint[precision * 10 / 3 + 2];

        uint[] pi = new uint[precision];

        for (int j = 0; j < x.Length; j++)
            x[j] = 20;

        for (int i = 0; i < precision; i++)
        {
            uint carry = 0;
            for (int j = 0; j < x.Length; j++)
            {
                uint num = (uint)(x.Length - j - 1);
                uint dem = num * 2 + 1;

                x[j] += carry;

                uint q = x[j] / dem;
                r[j] = x[j] % dem;

                carry = q * num;
            }

            pi[i] = x[^1] / 10;

            r[x.Length - 1] = x[^1] % 10;

            for (int j = 0; j < x.Length; j++)
                x[j] = r[j] * 10;
        }

        string result = "";

        uint c = 0;

        for (int i = pi.Length - 1; i >= 0; i--)
        {
            pi[i] += c;
            c = pi[i] / 10;

            result = (pi[i] % 10) + result;
        }

        return result.Insert(1, ".");
    }
}
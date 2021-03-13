
namespace Exercise2
{
    public class PalindromeChecker
    {
        private PalindromeChecker()
        {
        }

        public static bool Check(string word)
        {
            word = word.ToLower().Trim();
            int j = word.Length - 1;
            for (int i = 0; i < word.Length / 2; i++)
            {
                if (word[i].Equals(word[j]))
                {
                    j--;
                    continue;
                }
                return false;

            }

            return true;
        }
    }
}

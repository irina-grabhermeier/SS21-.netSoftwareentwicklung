
namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            var palindromeEventHandler = new PalindromeEventHandler();
            var palindromeEventSubscriber = new InputEventSubscriber(palindromeEventHandler);

            palindromeEventHandler.StartInputLoop();
        }
    }
}

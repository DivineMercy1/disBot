
namespace rollItBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsnyc().GetAwaiter().GetResult();
        }
    }
}

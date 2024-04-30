using static LetterInterface;

class Program
{
    static void Main(string[] args)
    {
        LetterInterface li = new LetterInterface();
        li.println(args[0]);
    }
}
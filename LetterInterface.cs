public interface ILetterService
{
    void println(string var);
}

public class LetterInterface : ILetterService
{
    public void println(string var)
    {
        Console.WriteLine(var);
    }
}
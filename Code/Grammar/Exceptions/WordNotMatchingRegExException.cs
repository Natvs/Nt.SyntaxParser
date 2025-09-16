namespace GrammarReader.Code.Grammar.Exceptions
{
    public class WordNotMatchingRegExException : Exception
    {
        public WordNotMatchingRegExException(string word, string regExp) : base($"The word {word} does not match the regular expresion {regExp}") { }
    }
}

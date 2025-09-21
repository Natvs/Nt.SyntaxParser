namespace Nt.SyntaxParser.Syntax.Exceptions
{

    public class NotDeclaredTerminalException : Exception
    {
        public NotDeclaredTerminalException(string name) : base($"Symbol {name} is not declared as a terminal") { }
    }

    public class NotDeclaredNonTerminalException : Exception
    {
        public NotDeclaredNonTerminalException(string name) : base($"Symbol {name} is not declared as non terminal") { }
    }
}

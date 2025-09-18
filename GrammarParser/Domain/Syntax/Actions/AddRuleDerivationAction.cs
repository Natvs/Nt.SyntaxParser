using GrammarParser.Domain.Syntax.Structures;
using GrammarParser.Domain.Parsing.Structures;
using GrammarParser.Domain.Syntax.Exceptions;

namespace GrammarParser.Domain.Syntax.Actions
{
    public class AddRuleDerivationAction(Grammar grammar, TokensList tokens) : RuleAction
    {
        public override Rule? Perform(Rule? rule, ParsedToken word)
        {
            if (rule == null) throw new NullRuleException("Attempting to write to a derivation of a non existent rule");

            string token = tokens[word.TokenIndex].Name;
            if (token.StartsWith('\\')) token = token.Substring(1); // Handles an escape char

            if (grammar.Terminals.Contains(token))
            {
                rule.AddTerminal(grammar.GetTerminalIndex(token), word.Line);
                return rule;
            }
            else if (grammar.NonTerminals.Contains(token))
            {
                rule.AddNonTerminal(grammar.GetNonTerminalIndex(token), word.Line);
                return rule;
            }
            throw new Exceptions.UnknownSymbolException(token, word.Line);
        }
    }
}

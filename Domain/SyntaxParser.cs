using System.Text;
using Nt.Automaton.States;
using Nt.Parser;
using Nt.Syntax.Actions;

using StateAutomaton = Nt.Automaton.Automatons.StateAutomaton<string>;
using State = Nt.Automaton.States.State<string>;
using Transition = Nt.Automaton.Transitions.Transition<string>;
using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;


namespace Nt.Syntax
{

    public class SyntaxParser
    {

        #region Private

        private Grammar Grammar { get; set; } = new();
        private PreParserAutomaton? PreAutomaton { get; set; }
        private ParserAutomaton? Automaton { get; set; }
        private List<string> ParserSymbols { get; } = [":", ",", "=", "{", "}", ";", "-", ">", "+", "*"];
        
        #endregion

        #region Public

        /// <summary>
        /// Applies the pre-parser on a given grammar string
        /// </summary>
        /// <param name="content">String to pre-parse</param>
        /// <returns>A pre-parsed string of the grammar</returns>
        public string PreParseString(string content)
        {
            try
            {
                PreAutomaton = new PreParserAutomaton(Grammar);

                var configuration = SyntaxParserConfig.GetInstance();
                var parser = new SymbolsParser(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], ["import", "IMPORT", "addtopath", "ADDTOPATH", "escape", "ESCAPE", ";"]);
                var parsed = parser.Parse(content);

                var sb = new StringBuilder();
                foreach (var token in parsed.GetParsed())
                {
                    PreAutomaton.Read(new AutomatonToken(token));

                    var importedString = PreAutomaton.GetImportedString();
                    if (importedString != null)
                    {
                        sb.Append(importedString);
                        PreAutomaton.ResetImportedString();
                    }
                }

                var imported = false;
                var contentReader = new StringReader(content);
                string? line;
                while ((line = contentReader.ReadLine()) != null)
                {
                    if (line.StartsWith("import", StringComparison.CurrentCultureIgnoreCase)) { imported = true; continue; }
                    if (line.StartsWith("addtopath", StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (line.StartsWith("escape", StringComparison.CurrentCultureIgnoreCase)) continue;
                    sb.AppendLine(line);
                }

                var new_content = sb.ToString();
                if (imported)
                {
                    return PreParseString(new_content);
                }
                return new_content;
            }
            catch (InternalException)
            {
                throw;
            }
            catch
            {
                throw new Exception("An error occurred while trying to pre-parse the string.");
            }
        }

        /// <summary>
        /// Reads a string and generates a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>Grammar data structure from the given string</returns>
        public Grammar ParseString(string content)
        {
            try
            {
                Grammar = new Grammar();
                Automaton = new ParserAutomaton(Grammar);

                content = PreParseString(content);

                var configuration = SyntaxParserConfig.GetInstance();
                var parser = new SymbolsParser(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], ParserSymbols);
                var parsed = parser.Parse(content);

                foreach (var token in parsed.GetParsed())
                {
                    Automaton.Read(new AutomatonToken(token));
                }
                Automaton.Stop();

                return Grammar;
            }
            catch (InternalException)
            {
                throw;
            }
            catch
            {
                throw new Exception("An error occurred while trying to parse the string.");
            }
        }

        /// <summary>
        /// Read a file and generate a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns>Grammar structure from content of the given file</returns>
        public Grammar ParseFile(string path)
        {
            try { 
                if (!File.Exists(path)) throw new FileNotFoundException($"Cannot parse {path}. The file cannot be found.");
                string content = File.ReadAllText(path);
                return ParseString(content);
            }
            catch (InternalException)
            {
                throw;
            }
            catch
            {
                throw new Exception($"An error occurred while trying to parse the file at {path}.");
            }
        }

        #endregion

    }
}

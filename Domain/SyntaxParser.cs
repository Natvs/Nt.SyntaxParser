using System.Text;
using Nt.Parser;

using Nt.Syntax.Automaton;
using Nt.Syntax.Structures;
using Nt.Syntax.Exceptions;


namespace Nt.Syntax
{

    public class SyntaxParser
    {


        private Grammar Grammar { get; set; } = new();
        internal PreParserAutomaton PreAutomaton { get; } = new PreParserAutomaton();
        internal ParserAutomaton Automaton { get; } = new ParserAutomaton();
        private List<string> ParserSymbols { get; } = [];


        /// <summary>
        /// Apply the pre-parser on a given grammar string
        /// </summary>
        /// <param name="content">String to pre-parse</param>
        /// <returns>A pre-parsed string of the grammar</returns>
        internal void PreParseString(string content)
        {
            try
            {
                PreAutomaton.BuildNodes(Grammar);

                // Parse the content
                var configuration = SyntaxParserConfig.GetConfig();
                var parser = new SymbolsParser(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], []);
                var parsed = parser.Parse(content);

                // Read the parsed content into the pre-automaton
                foreach (var token in parsed.GetParsed())
                {
                    PreAutomaton.Read(new AutomatonToken(token));
                }

                // Parse each file recursively
                foreach (string file in PreAutomaton.Context.Files)
                {
                    var syntaxparser = new SyntaxParser();
                    var imported = false;
                    foreach (string path in PreAutomaton.Context.GetPath())
                    {
                        if (File.Exists(path + "/" + file)) 
                        {
                            var new_grammar = syntaxparser.ParseFile(path + "/" + file);
                            Grammar += new_grammar;
                        }
                        imported = true;
                    }

                    if (imported) continue;
                    if (!imported)
                    {
                        if (File.Exists(file)) 
                        { 
                            var new_grammar = syntaxparser.ParseFile(file);
                            Grammar += new_grammar;
                        }
                        continue;
                    }

                    throw new ImportFileNotFoundException(file);
                }
            }
            catch (InternalException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Read a string and generate a grammar structure from it. Also applies pre-parsing on it.
        /// </summary>
        /// <param name="content">String to read</param>
        /// <returns>Grammar data structure from the given string</returns>
        public Grammar ParseString(string content)
        {
            try
            {
                PreParseString(content);
                Automaton.BuildNodes(Grammar);

                // Replace the pre parsing commands with empty lines
                var contentReader = new StringReader(content);
                var sb = new StringBuilder();
                string? line;
                while ((line = contentReader.ReadLine()) != null)
                {
                    if (line.StartsWith("import", StringComparison.CurrentCultureIgnoreCase)) { sb.AppendLine(); continue; }
                    if (line.StartsWith("addtopath", StringComparison.CurrentCultureIgnoreCase)) { sb.AppendLine(); continue; }
                    if (line.StartsWith("escape", StringComparison.CurrentCultureIgnoreCase)) { sb.AppendLine(); continue; }
                    sb.AppendLine(line);
                }
                content = sb.ToString();

                // Parse the content
                var configuration = SyntaxParserConfig.GetConfig();
                var parser = new SymbolsParser(configuration.SymbolFactory, [' ', '\0', '\n', '\t'], ParserSymbols);
                var parsed = parser.Parse(content);

                // Read the parsed tokens into the automaton
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
                //throw new InternalException("An error occurred while trying to parse the string.");
            }
            catch
            {
                throw;
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
            catch (InternalException) { throw; }
            catch (FileNotFoundException) { throw; }
            catch
            {
                throw new InternalException($"An error occurred while trying to parse the file at {path}.");
            }
        }

    }
}

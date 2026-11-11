using Nt.Syntax.Branching.Exceptions;
using Nt.Syntax.Structures;
using System.Diagnostics.CodeAnalysis;

namespace Nt.Syntax.Branching
{
    public static class DataManager
    {
        private static IDataFactory? RuleDataFactory { get; set; } = null;
        private static IDataFactory? RegexDataFactory { get; set; } = null;
        private static IDataFactory? TerminalDataFactory { get; set; } = null;
        private static IDataFactory? NonTerminalDataFactory { get; set; } = null;


        // Factories

        /// <summary>
        /// Set a factory to create a rule data
        /// </summary>
        /// <param name="factory">Factory that creates rule data</param>
        public static void SetRuleFactory(IDataFactory factory)
        {
            RuleDataFactory = factory;
        }

        /// <summary>
        /// Set a factory to create a regular expression data
        /// </summary>
        /// <param name="factory">Factory that creates regular expression data</param>
        public static void SetRegularExpressionFactory(IDataFactory factory)
        {
            RegexDataFactory = factory;
        }

        /// <summary>
        /// Set a factory to create a terminal data
        /// </summary>
        /// <param name="factory">Factory that creates terminal data</param>
        public static void SetTerminalFactory(IDataFactory factory)
        {
            TerminalDataFactory = factory;
        }

        /// <summary>
        /// Set a factory to create a non terminal data
        /// </summary>
        /// <param name="factory">Factory that creates non terminal data</param>
        public static void SetNonTerminalFactory(IDataFactory factory)
        {
            NonTerminalDataFactory = factory;
        }

        // Create Data

        /// <summary>
        /// Associate data to this rule
        /// </summary>
        /// <param name="rule">Rule to associate</param>
        /// <param name="data">Data to associate to the rule</param>
        public static void CreateData(this Rule rule, IData data)
        {
            CreateData((object)rule, data);
        }

        /// <summary>
        /// Associate data to this regular expression
        /// </summary>
        /// <param name="rule">Regular expression to associate</param>
        /// <param name="data">Data to associate to the regular expression</param>
        public static void CreateData(this RegularExpression regex, IData data)
        {
            CreateData((object)regex, data);
        }

        /// <summary>
        /// Associate data to this token
        /// </summary>
        /// <param name="rule">Token to associate</param>
        /// <param name="data">Data to associate to the token</param>
        public static void CreateData(this GrammarToken token, IData data)
        {
            CreateData((object)token, data);
        }
        
        private static void CreateData(object obj, IData data)
        {
            var storage = DataStorage.GetInstance();
            storage.AddData(obj, data);
        }

        // Get Data

        /// <summary>
        /// Get the data associated to this rule
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Rule associated to the data</param>
        /// <returns>The data associated to the rule</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized and no factory is available</exception>
        public static T GetData<T>(this Rule rule) where T : IData
        {
            return GetData<T>(rule, RuleDataFactory);
        }

        /// <summary>
        /// Get the data associated to this regular expression
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Regular expression associated to the data</param>
        /// <returns>The data associated to the regular expression</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized and no factory is available</exception>
        public static T GetData<T>(this RegularExpression regex) where T : IData
        {
            return GetData<T>(regex, RegexDataFactory);
        }

        /// <summary>
        /// Get the data associated to this terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Terminal associated to the data</param>
        /// <returns>The data associated to the terminal</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized and no factory is available</exception>
        public static T GetData<T>(this Terminal token) where T : IData
        {
            return GetData<T>(token, TerminalDataFactory);
        }

        /// <summary>
        /// Get the data associated to this non terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Non terminal associated to the data</param>
        /// <returns>The data associated to the non terminal</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized and no factory is available</exception>
        public static T GetData<T>(this NonTerminal token) where T : IData
        {
            return GetData<T>(token, NonTerminalDataFactory);
        }

        /// <summary>
        /// Get the data associated to this rule
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Rule associated to the data</param>
        /// <param name="data">Data associated to the rule, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this Rule rule, [NotNullWhen(true)] out T? data) where T : IData
        {
            return TryGetData(rule, RuleDataFactory, out data);
        }

        /// <summary>
        /// Get the data associated to this regular expression
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="regex">Regular expression associated to the data</param>
        /// <param name="data">Data associated to the regular expression, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this RegularExpression regex, [NotNullWhen(true)] out T? data) where T : IData
        {
            return TryGetData(regex, RegexDataFactory, out data);
        }
        
        /// <summary>
        /// Get the data associated to this terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="token">Terminal token associated to the data</param>
        /// <param name="data">Data associated to the terminal token, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this Terminal token, [NotNullWhen(true)] out T? data) where T : IData
        {
            return TryGetData(token, TerminalDataFactory, out data);
        }

        /// <summary>
        /// Get the data associated to this non terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="token">Non terminal token associated to the data</param>
        /// <param name="data">Data associated to the non terminal token, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this NonTerminal token, [NotNullWhen(true)] out T? data) where T : IData
        {
            return TryGetData(token, NonTerminalDataFactory, out data);
        }

        private static T GetData<T>(object obj, IDataFactory? factory) where T : IData
        {
            if (TryGetData<T>(obj, factory, out var data))
            {
                return data;
            }
            throw new UninitializedDataException(obj, typeof(T));
        }
        private static bool TryGetData<T>(object obj, IDataFactory? factory, [NotNullWhen(true)] out T? data) where T : IData
        {
            var storage = DataStorage.GetInstance();
            var stored = storage.GetData(obj);

            // Create the data if null and a factory is provided
            if (stored == null && factory != null)
            {
                stored = factory.Create();
                storage.AddData(obj, stored);
                data = (T)stored;
                return true;
            }

            // Check the type of the data
            if (stored is T typedData)
            {
                data = typedData;
                return true;
            }
            data = default;
            return false;
        }
    }
}

using Nt.Syntax.Structures;

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
        /// <returns></returns>
        public static T GetData<T>(this Rule rule) where T : IData
        {
            return GetData<T>(rule, RuleDataFactory);
        }

        /// <summary>
        /// Get the data associated to this regular expression
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Regular expression associated to the data</param>
        /// <returns></returns>
        public static T GetData<T>(this RegularExpression regex) where T : IData
        {
            return GetData<T>(regex, RegexDataFactory);
        }

        /// <summary>
        /// Get the data associated to this terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Terminal associated to the data</param>
        /// <returns></returns>
        public static T GetData<T>(this Terminal token) where T : IData
        {
            return GetData<T>(token, TerminalDataFactory);
        }

        /// <summary>
        /// Get the data associated to this non terminal token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Non terminal associated to the data</param>
        /// <returns></returns>
        public static T GetData<T>(this NonTerminal token) where T : IData
        {
            return GetData<T>(token, NonTerminalDataFactory);
        }

        private static T GetData<T>(object obj, IDataFactory? factory = null) where T : IData
        {
            var storage = DataStorage.GetInstance();
            var data = storage.GetData(obj);

            // Create the data if null and a factory is provided
            if (data == null && factory != null) {
                data = factory.Create();
                storage.AddData(obj, data);
                return (T)data;
            }

            // Check the type of the data
            if (data is not T typedData)
            {
                throw new TypeLoadException($"Data associated with rule {obj} is not of type {typeof(T).Name}");
            }
            
            return typedData;
        }

    }
}

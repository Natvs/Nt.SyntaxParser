using Nt.Syntax.Branching.Exceptions;
using Nt.Syntax.Structures;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Nt.Syntax.Branching
{
    public static class DataManager
    {

        /// <summary>
        /// Associate data to this rule
        /// </summary>
        /// <param name="rule">Rule to associate</param>
        /// <param name="data">Data to associate to the rule</param>
        public static void CreateData(this Rule rule, IData data)
        {
            rule.Data = data;
        }

        /// <summary>
        /// Associate data to this regular expression
        /// </summary>
        /// <param name="regex">Regular expression to associate</param>
        /// <param name="data">Data to associate to the regular expression</param>
        public static void CreateData(this RegularExpression regex, IData data)
        {
            regex.Data = data;
        }

        /// <summary>
        /// Associate data to this token
        /// </summary>
        /// <param name="token">Token to associate</param>
        /// <param name="data">Data to associate to the token</param>
        public static void CreateData(this GrammarToken token, IData data)
        {
            token.Data = data;
        }




        /// <summary>
        /// Get the data associated to this rule
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Rule associated to the data</param>
        /// <returns>The data associated to the rule</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized</exception>
        /// <exception cref="IncorrectDataTypeException">Thrown if the data exists but is not of the correct type</exception>
        public static T GetData<T>(this Rule rule) where T : IData
        {
            if (rule.Data == null) throw new UninitializedDataException(rule, typeof(T));
            if (rule.Data is T data)
            {
                return data;
            };
            throw new IncorrectDataTypeException(rule, rule.Data.GetType(), typeof(T));
        }

        /// <summary>
        /// Get the data associated to this regular expression
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="regex">Regular expression associated to the data</param>
        /// <returns>The data associated to the regular expression</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized</exception>
        /// <exception cref="IncorrectDataTypeException">Thrown if the data exists but is not of the correct type</exception>
        public static T GetData<T>(this RegularExpression regex) where T : IData
        {
            if (regex.Data == null) throw new UninitializedDataException(regex, typeof(T));
            if (regex.Data is T data)
            {
                return data;
            }
            throw new IncorrectDataTypeException(regex, regex.Data.GetType(), typeof(T));
        }

        /// <summary>
        /// Get the data associated to this token
        /// </summary>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="token">Token associated to the data</param>
        /// <returns>The data associated to the token</returns>
        /// <exception cref="UninitializedDataException">Thrown if the data is not initialized</exception>
        /// <exception cref="IncorrectDataTypeException">Thrown if the data exists but is not of the correct type</exception>
        public static T GetData<T>(this GrammarToken token) where T : IData
        {
            if (token.Data == null) throw new UninitializedDataException(token, typeof(T));
            if (token.Data is T data)
            {
                return data;
            }
            throw new IncorrectDataTypeException(token, token.Data.GetType(), typeof(T));
        }




        /// <summary>
        /// Get or create data associated to this rule.
        /// </summary>
        /// <remarks>If the data is not of the requested type, the old data will be overwritten.</remarks>
        /// <typeparam name="T">Type of data to get</typeparam>
        /// <param name="rule">Rule associated to the data</param>
        /// <param name="factory">Factory to create the data if non existent</param>
        /// <returns>Data associated to the rule</returns>
        public static T GetOrCreateData<T>(this Rule rule, IDataFactory<T> factory) where T : IData
        {
            if (rule.TryGetData(out T? data)) return data;
            return factory.Create();
        }

        /// <summary>
        /// Get or create data associated to this regular expression.
        /// </summary>
        /// <remarks>If the data is not of the requested type, the old data will be overwritten.</remarks>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="regex">Regular expression associated to the data</param>
        /// <param name="factory">Factory to create the data if non existent</param>
        /// <returns>Data associated to the regular expression</returns>
        public static T GetOrCreateData<T>(this RegularExpression regex, IDataFactory<T> factory) where T : IData
        {
            if (regex.TryGetData(out T? data)) return data;
            return factory.Create();
        }

        /// <summary>
        /// Get or create data associated to this token
        /// </summary>
        /// <remarks>If the data is not of the requested type, the old data will be overwritten.</remarks>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="token">Token associated to the data</param>
        /// <param name="factory">Factory to create the data if non existent</param>
        /// <returns>Data associated to the token</returns>
        public static T GetOrCreateData<T>(this GrammarToken token, IDataFactory<T> factory) where T : IData
        {
            if (token.TryGetData(out T? data)) return data;
            return factory.Create();
        }




        /// <summary>
        /// Get the data associated to this rule
        /// </summary>
        /// <remarks>If the data exists but is not of the requested type, this method will return false.</remarks>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="rule">Rule associated to the data</param>
        /// <param name="data">Data associated to the rule, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this Rule rule, [NotNullWhen(true)] out T? data) where T : IData
        {
            if (rule.Data is T typedData)
            {
                data = typedData;
                return true;
            }
            data = default;
            return false;
        }

        /// <summary>
        /// Get the data associated to this regular expression
        /// </summary>
        /// <remarks>If the data exists but is not of the requested type, this method will return false.</remarks>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="regex">Regular expression associated to the data</param>
        /// <param name="data">Data associated to the regular expression, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this RegularExpression regex, [NotNullWhen(true)] out T? data) where T : IData
        {
            if (regex.Data is T typedData)
            {
                data = typedData;
                return true;
            }
            data = default;
            return false;
        }

        /// <summary>
        /// Get the data associated to this token
        /// </summary>
        /// <remarks>If the data exists but is not of the requested type, this method will return false.</remarks>
        /// <typeparam name="T">Type of the data to get</typeparam>
        /// <param name="token">Token associated to the data</param>
        /// <param name="data">Data associated to the token, if the method returns true</param>
        /// <returns>True if the date exists, otherwise false</returns>
        public static bool TryGetData<T>(this GrammarToken token, [NotNullWhen(true)] out T? data) where T : IData
        {
            if (token.Data is T typedData)
            {
                data = typedData;
                return true;
            }
            data = default;
            return false;
        }

    }
}

using Nt.Syntax.Structures;
using Nt.Syntax.Payload.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Nt.Syntax.Payload
{
    public static class PayloadExtrensions
    {

        // Set Payload

        /// <summary>
        /// Set the payload of a grammar token.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Grammar token to set the payload for.</param>
        /// <param name="payload">Payload to set.</param>
        public static void SetPayload<T>(this GrammarToken token, T payload) where T : IPayload
        {
            token.Data = payload;
        }

        /// <summary>
        /// Set the payload of rule.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Rule to set the payload for.</param>
        /// <param name="payload">Payload to set.</param>
        public static void SetPayload<T>(this Rule token, T payload) where T : IPayload
        {
            token.Data = payload;
        }

        /// <summary>
        /// Set the payload of a regular expression.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Regular expression to set the payload for.</param>
        /// <param name="payload">Payload to set.</param>
        public static void SetPayload<T>(this RegularExpression token, T payload) where T : IPayload
        {
            token.Data = payload;
        }



        // Get Payload

        /// <summary>
        /// Get the payload of a terminal.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Terminal to set the payload for.</param>
        /// <returns>The payload of the terminal.</returns>
        /// <exception cref="NullPayloadException">Thrown when the payload is null.</exception>
        /// <exception cref="PayloadTypeException">Thrown when the payload is not of the expected type.</exception>
        public static T GetPayload<T>(this Terminal token) where T : IPayload
        {
            if (token.Data == null) throw new NullPayloadException(token);
            if (token.Data is T payload) return payload;
            throw new PayloadTypeException(token, typeof(T));
        }

        /// <summary>
        /// Get the payload of a non-terminal.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Non-terminal to set the payload for.</param>
        /// <returns>The payload of the non-terminal.</returns>
        /// <exception cref="NullPayloadException">Thrown when the payload is null.</exception>
        /// <exception cref="PayloadTypeException">Thrown when the payload is not of the expected type.</exception>
        public static T GetPayload<T>(this NonTerminal token) where T : IPayload
        {
            if (token.Data == null) throw new NullPayloadException(token);
            if (token.Data is T payload) return payload;
            throw new PayloadTypeException(token, typeof(T));
        }

        /// <summary>
        /// Get the payload of a rule.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Rule to set the payload for.</param>
        /// <returns>The payload of the rule.</returns>
        /// <exception cref="NullPayloadException">Thrown when the payload is null.</exception>
        /// <exception cref="PayloadTypeException">Thrown when the payload is not of the expected type.</exception>
        public static T GetPayload<T>(this Rule token) where T : IPayload
        {
            if (token.Data == null) throw new NullPayloadException(token);
            if (token.Data is T payload) return payload;
            throw new PayloadTypeException(token, typeof(T));
        }

        /// <summary>
        /// Get the payload of a regular expression.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Regular expression to set the payload for.</param>
        /// <returns>The payload of the regular expression.</returns>
        /// <exception cref="NullPayloadException">Thrown when the payload is null.</exception>
        /// <exception cref="PayloadTypeException">Thrown when the payload is not of the expected type.</exception>
        public static T GetPayload<T>(this RegularExpression token) where T : IPayload
        {
            if (token.Data == null) throw new NullPayloadException(token);
            if (token.Data is T payload) return payload;
            throw new PayloadTypeException(token, typeof(T));
        }




        // Try Get Payload

        /// <summary>
        /// Request the payload of a terminal, if it exists and is of the expected type.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Terminal to request the payload for.</param>
        /// <param name="payload">The payload of the terminal, if it exists and is of the expected type.</param>
        /// <returns>True if the payload exists and is of the expected type; otherwise, false.</returns>
        public static bool TryGetPayload<T>(this Terminal token, [NotNullWhen(true)] out T? payload) where T : IPayload
        {
            if (token.Data == null)
            {
                payload = default;
                return false;
            }
            if (token.Data is T p)
            {
                payload = p;
                return true;
            }
            payload = default;
            return false;
        }

        /// <summary>
        /// Request the payload of a non-terminal, if it exists and is of the expected type.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Non-terminal to request the payload for.</param>
        /// <param name="payload">The payload of the non-terminal, if it exists and is of the expected type.</param>
        /// <returns>True if the payload exists and is of the expected type; otherwise, false.</returns>
        public static bool TryGetPayload<T>(this NonTerminal token, [NotNullWhen(true)] out T? payload) where T : IPayload
        {
            if (token.Data == null)
            {
                payload = default;
                return false;
            }
            if (token.Data is T p)
            {
                payload = p;
                return true;
            }
            payload = default;
            return false;
        }

        /// <summary>
        /// Request the payload of a rule, if it exists and is of the expected type.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Rule to request the payload for.</param>
        /// <param name="payload">The payload of the rule, if it exists and is of the expected type.</param>
        /// <returns>True if the payload exists and is of the expected type; otherwise, false.</returns>
        public static bool TryGetPayload<T>(this Rule token, [NotNullWhen(true)] out T? payload) where T : IPayload
        {
            if (token.Data == null)
            {
                payload = default;
                return false;
            }
            if (token.Data is T p)
            {
                payload = p;
                return true;
            }
            payload = default;
            return false;
        }

        /// <summary>
        /// Request the payload of a regular expression, if it exists and is of the expected type.
        /// </summary>
        /// <typeparam name="T">Type of the payload, which must implement the IData interface.</typeparam>
        /// <param name="token">Regular expression to request the payload for.</param>
        /// <param name="payload">The payload of the regular expression, if it exists and is of the expected type.</param>
        /// <returns>True if the payload exists and is of the expected type; otherwise, false.</returns>
        public static bool TryGetPayload<T>(this RegularExpression token, [NotNullWhen(true)] out T? payload) where T : IPayload
        {
            if (token.Data == null)
            {
                payload = default;
                return false;
            }
            if (token.Data is T p)
            {
                payload = p;
                return true;
            }
            payload = default;
            return false;
        }

    }
}

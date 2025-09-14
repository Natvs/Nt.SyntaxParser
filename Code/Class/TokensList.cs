using System.Text;

namespace GrammarReader.Code.Class
{
    public class TokensList : List<Token>
    {

        #region Parameters

        #endregion


        #region Constructors

        #endregion


        #region Methods

        public int Add(string name)
        {
            var token = new Token(name);
            Add(token);
            return Count-1;
        }

        public bool Contains(string name)
        {
            foreach (var token in this)
            {
                if (token.Name == name) { return true; }
            }
            return false;
        }

        public Token Get(string name)
        {
            foreach (var token in this)
            {
                if (token.Name == name) { return token; }
            }
            throw new KeyNotFoundException("No token " + name + " found in list");
        }

        public int IndexOf(string name)
        {
            for (var i = 0; i < Count; i++)
            {
                if (this[i].Name == name)
                {
                    return i;
                }
            }
            throw new KeyNotFoundException("No token " + name + " found in list");
        }

        public override string ToString()
        {
            var sb = new StringBuilder().Append('{');
            for (int i = 0; i < Count-1; i++)
            {
                sb.Append(this[i].Name).Append(", ");
            }
            if (Count > 0) sb.Append(this[Count - 1].Name);
            sb.Append('}');
            return sb.ToString();
        }

        #endregion

    }
}

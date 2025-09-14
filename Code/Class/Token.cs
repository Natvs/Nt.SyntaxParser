namespace GrammarReader.Code.Class
{
    public class Token
    {
        #region Parameters

        public string Name { get; private set; }

        #endregion

        #region Constructors
        public Token(string name)
        {
            this.Name = name;
        }
        #endregion

    }
}

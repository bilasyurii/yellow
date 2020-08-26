namespace Yellow.Assets.JSON
{
    public class Token
    {
        public enum TokenType
        {
            Scope,
            String,
            Colon,
            Float,
            Integer,
            Boolean,
            Null,
        };

        public TokenType type;

        public string data;
    }
}

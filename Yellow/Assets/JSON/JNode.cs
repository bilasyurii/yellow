using System.Collections.Generic;

namespace Yellow.Assets.JSON
{
    public class JNode
    {
        public enum NodeType
        {
            Scope,
            String,
            Colon,
            Float,
            Integer,
            Boolean,
            Null,
            Dictionary,
            List,
        };

        public NodeType type;

        public object data;

        public JNode this[string key]
        {
            get
            {
                return ((Dictionary<string, JNode>)data)[key];
            }
        }

        public JNode this[int index]
        {
            get
            {
                return ((List<JNode>)data)[index];
            }
        }

        public string String
        {
            get
            {
                return (string)data;
            }
        }

        public char Char
        {
            get
            {
                return (char)data;
            }
        }

        public bool Boolean
        {
            get
            {
                return (bool)data;
            }
        }

        public float Float
        {
            get
            {
                return (float)data;
            }
        }

        public int Integer
        {
            get
            {
                return (int)data;
            }
        }

        public Dictionary<string, JNode> Dictionary
        {
            get
            {
                return (Dictionary<string, JNode>)data;
            }
        }

        public List<JNode> List
        {
            get
            {
                return (List<JNode>)data;
            }
        }
    }
}

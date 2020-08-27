﻿using System.Collections.Generic;

namespace Yellow.Assets.JSON
{
    public class Node
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

        public Dictionary<string, Node> Dictionary
        {
            get
            {
                return (Dictionary<string, Node>)data;
            }
        }

        public List<Node> List
        {
            get
            {
                return (List<Node>)data;
            }
        }
    }
}
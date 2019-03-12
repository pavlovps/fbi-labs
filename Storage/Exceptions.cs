namespace pavlovLab.Storage
{
    [System.Serializable]
    public class IncorrectLabDataException : System.Exception
    {
        public IncorrectLabDataException() { }
        public IncorrectLabDataException(string message) : base(message) { }
        public IncorrectLabDataException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectLabDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
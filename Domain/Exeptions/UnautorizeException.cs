namespace Domain.Exeptions
{
    public class UnautorizeException : Exception
    {
        public UnautorizeException(string massage) : base(massage) { }

        public UnautorizeException() { }
    }
}

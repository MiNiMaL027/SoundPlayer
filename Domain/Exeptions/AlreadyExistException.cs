namespace Domain.Exeptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string massage) : base(massage) { }

        public AlreadyExistException() { }
    }
}

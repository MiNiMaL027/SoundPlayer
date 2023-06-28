namespace Domain.Exeptions
{
    public class LoginException : Exception
    {
        public LoginException(string massage) : base(massage) { }

        public LoginException() { }
    }
}

using System;

namespace siberianoilrush_server.Exceptions
{
    public class GameException : Exception
    {
        public GameException() { }

        public GameException(string message) : base(message) { }
    }
}

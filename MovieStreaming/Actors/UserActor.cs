using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _userId = userId;
            Console.WriteLine("Creating a UserActor");

            ColorConsole.WriteLineYellow("Setting initial behaviour to stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message =>
                    ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping the existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow("User Actor has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => 
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));

            ColorConsole.WriteLineYellow("User Actor has now become Stopped");
        }


        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(string.Format("User has stopped watching '{0}'", _currentlyWatching));
            _currentlyWatching = null;

            Become(Stopped);
        }
        
        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            ColorConsole.WriteLineYellow(string.Format("User is currently watching '{0}'", _currentlyWatching));

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow(string.Format("UserActor {0} PreStart", _userId));
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow(string.Format("UserActor {0} PostStop", _userId));
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow(string.Format("UserActor {0} PreRestart because: {1}", _userId, reason));

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow(string.Format("UserActor {0} PostRestart because: {1}", _userId ,reason));

            base.PostRestart(reason);
        }
    }
}
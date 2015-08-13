using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            ColorConsole.WriteLineCyan("Setting initial behaviour to stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message =>
                    ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping the existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineCyan("User Actor has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => 
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing"));

            ColorConsole.WriteLineCyan("User Actor has now become Stopped");
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
            ColorConsole.WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("UserActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
    }
}
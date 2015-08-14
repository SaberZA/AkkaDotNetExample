using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Actor.Internal;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    public class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        private static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine(string.Format("{0} started...", _movieStreamingActorSystem.Name));

            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, "Playback");

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("Enter a command and hit enter");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    var userId = int.Parse(command.Split(',')[1]);
                    var movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    var userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    _movieStreamingActorSystem.Shutdown();
                    _movieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);

        }



        //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: The Movie", 42));
            //playbackActorRef.Tell(new PlayMovieMessage("Partial Recall", 99));

        //    var userActorProps = Props.Create<UserActor>();
        //    var userActorRef = _movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

        //    Console.ReadKey();
        //    Console.WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer)");
        //    userActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 42));


        //    Console.ReadKey();
        //    Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
        //    userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));

        //    Console.ReadKey();
        //    Console.WriteLine("Sending a StopMovieMessage");
        //    userActorRef.Tell(new StopMovieMessage());

        //    Console.ReadKey();
        //    Console.WriteLine("Sending a StopMovieMessage");
        //    userActorRef.Tell(new StopMovieMessage());

        //    playbackActorRef.Tell(PoisonPill.Instance);
        //    userActorRef.Tell(PoisonPill.Instance);


        //    Console.ReadKey();
        //    _movieStreamingActorSystem.Shutdown();

        //    _movieStreamingActorSystem.AwaitTermination();
        //    Console.WriteLine("Actor system shutdown");


        //    Console.ReadKey();
        //}

        private static void ShortPause()
        {
            Thread.Sleep(500);
        }
    }
}

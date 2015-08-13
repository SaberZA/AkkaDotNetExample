using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Actor.Internal;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    public class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine(string.Format("{0} started...",_movieStreamingActorSystem.Name));

            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: The Movie", 42));
            //playbackActorRef.Tell(new PlayMovieMessage("Partial Recall", 99));

            var userActorProps = Props.Create<UserActor>();
            var userActorRef = _movieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer)");
            userActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 42));


            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
            userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 77));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            playbackActorRef.Tell(PoisonPill.Instance);
            userActorRef.Tell(PoisonPill.Instance);


            Console.ReadKey();
            _movieStreamingActorSystem.Shutdown();

            _movieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor system shutdown");


            Console.ReadKey();
        }
    }
}

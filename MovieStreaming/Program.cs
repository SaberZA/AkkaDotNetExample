using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Actor.Internal;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine(string.Format("{0} started...",_movieStreamingActorSystem.Name));
            Console.ReadLine();

            _movieStreamingActorSystem.Shutdown();
        }
    }
}

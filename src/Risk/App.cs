namespace Risk {
    public class App {
        public static void Main(string[] args) {
            Game game = new Game();
            Thread gameThread = new Thread(new ThreadStart(game.Run));
            gameThread.Start();
        }
    }
}

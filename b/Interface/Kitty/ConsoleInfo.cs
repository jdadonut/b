namespace b.Interface.Kitty
{
    public class ConsoleInfo
    {
        public int x;
        public int y;
        public ConsoleInfo()
        {
            // Automatically runs TIOCGWINSZ ioctl
        }
    }
}
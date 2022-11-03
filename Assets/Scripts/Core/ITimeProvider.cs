namespace Core
{
    public interface ITimeProvider
    {
        public bool IsPause { get; }
    }

    public class TimeProvider : ITimeProvider
    {
        public bool IsPause { get; private set; }

        public void SetPause(bool isPause)
        {
            IsPause = isPause;
        }
    }
}
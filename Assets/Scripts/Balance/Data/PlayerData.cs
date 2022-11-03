namespace Balance.Data
{
    public class PlayerData
    {
        public int Offset { get;  }
        public float Speed { get;  }
        public int Count { get;  }

        public PlayerData(int offset, float speed, int count)
        {
            Offset = offset;
            Speed = speed;
            Count = count;
        }
    }
}
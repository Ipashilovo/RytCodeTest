using Balance.Configs;
using Balance.Data;
using Core;
using Core.LevelSystem;
using Models;
using UnityEngine;

namespace Initialize
{
    public class LevelInitializer
    {
        private readonly LevelGeneratorData _data;
        private readonly TimeProvider _timeProvider;

        public LevelInitializer(LevelGeneratorData data, TimeProvider timeProvider)
        {
            _data = data;
            _timeProvider = timeProvider;
        }

        public Level Get()
        {
            var startPoint = Resources.Load<StartPoint>("StartPoint");
            var endPoint = Resources.Load<EndPoint>("EndPoint");
            var point = Resources.Load<PathPoint>("PathPoint");
            IPointFactory pointFactory = new PointFactory(startPoint, endPoint, point, _timeProvider);
            return new LevelGenerator(_data, pointFactory).Generate();
        }
    }
}
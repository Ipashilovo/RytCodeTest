using System;
using System.Collections.Generic;
using System.Linq;
using Core.LevelSystem;
using UnityEngine;

namespace Core
{
    public class PathCreator
    {
        private readonly Level _level;

        private struct PathStep
        {
            public IPathPoint PathPoint { get; }
            public List<IPathPoint> Neighbours { get;  }

            public PathStep(IPathPoint pathPoint, List<IPathPoint> neighbours)
            {
                PathPoint = pathPoint;
                Neighbours = neighbours;
            }
        }

        public PathCreator(Level level)
        {
            _level = level;
        }
        
        public void SetPath(IEnumerable<IPlayer> players)
        {
            foreach (var player in players)
            {
                var path = GetPath(player.TargetPoint);
                player.SetPath(path);
            }
        }

        private Path GetPath(IPathPoint startPoint)
        {
            if (Vector2Int.Distance(startPoint.PositionInLevel, _level.EndPoint.Position) - 1 < 0.1f)
            {
                return new Path(new[]{ startPoint.PositionInLevel, _level.EndPoint.Position }, true);
            }
            Stack<PathStep> steps = new Stack<PathStep>();
            HashSet<IPathPoint> lostPoints = new HashSet<IPathPoint>();
            lostPoints.Add(startPoint);
            var neighbours = _level.GetNeighbours(startPoint);
            var pointsWithoutCompleted = neighbours.ToList();
            List<IPathPoint> maxSteps = new List<IPathPoint>(){startPoint};

            if (!pointsWithoutCompleted.Any())
            {
                return new Path(new []{startPoint.PositionInLevel}, false);
            }
            
            var pathStep = new PathStep(startPoint, pointsWithoutCompleted);
            steps.Push(pathStep);
            var nextPoint = GetClosestPoint(pathStep.Neighbours);
            lostPoints.Add(nextPoint);
            pathStep.Neighbours.Remove(nextPoint); 

            while (steps.Count > 0)
            {
                neighbours = _level.GetNeighbours(nextPoint);
                pointsWithoutCompleted = neighbours.Except(lostPoints).ToList();
                
                if (!pointsWithoutCompleted.Any())
                {
                    if (steps.Count > maxSteps.Count)
                    {
                        maxSteps = steps.Select(s => s.PathPoint).Reverse().ToList();
                        maxSteps.Add(nextPoint);
                    }
                    do
                    {
                        var previousStep = steps.Peek();
                        if (previousStep.Neighbours.Count > 0)
                        {
                            nextPoint = GetClosestPoint(previousStep.Neighbours);
                            lostPoints.Add(nextPoint);
                            previousStep.Neighbours.Remove(nextPoint);
                            break;
                        }
                        steps.Pop();
                    } while (steps.Count > 0);
                    continue;
                }
                pathStep = new PathStep(nextPoint, pointsWithoutCompleted);
                steps.Push(pathStep);
                nextPoint = GetClosestPoint(pathStep.Neighbours);
                if (Vector2Int.Distance(nextPoint.PositionInLevel, _level.EndPoint.Position) - 1 < 0.1f)
                {
                    return new Path(steps.Select(s => s.PathPoint.PositionInLevel).Reverse()
                        .Concat(new[] { nextPoint.PositionInLevel, _level.EndPoint.Position }).ToArray(), true);
                }
                lostPoints.Add(nextPoint);
                pathStep.Neighbours.Remove(nextPoint);
            }
            
            return new Path(maxSteps.Select(v => v.PositionInLevel).ToArray(), false);
        }

        private IPathPoint GetClosestPoint(List<IPathPoint> pathStepNeighbours)
        {
            if (pathStepNeighbours.Count == 1)
            {
                return pathStepNeighbours[0];
            }

            int index = 0;
            float minDistance = float.MaxValue;
            var endPoint = _level.EndPoint.Position;

            for (int i = 0; i < pathStepNeighbours.Count; i++)
            {
                var distance = Vector2Int.Distance(endPoint, pathStepNeighbours[i].PositionInLevel);
                if (distance < minDistance)
                {
                    index = i;
                    minDistance = distance;
                }
            }
            return pathStepNeighbours[index];
        }
    }
}
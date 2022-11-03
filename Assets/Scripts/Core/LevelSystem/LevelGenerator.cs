using System;
using System.Collections.Generic;
using Balance.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.LevelSystem
{
    public class LevelGenerator
    {
        private readonly LevelGeneratorData _levelGeneratorData;
        private readonly IPointFactory _pointFactory;

        public LevelGenerator(LevelGeneratorData levelGeneratorData, IPointFactory pointFactory)
        {
            _levelGeneratorData = levelGeneratorData;
            _pointFactory = pointFactory;
        }

        public Level Generate()
        {
            var cells = GetCells(_levelGeneratorData.MaxDistance, out List<LevelCell> freeCell);
            IStartPoint startPoint = SetStartPoint(cells, freeCell);
            IEndPoint endPoint = GetEndPoint(cells, freeCell, startPoint.Position);
            List<IPathPoint> points = GetPoints(cells, freeCell, startPoint.Position, endPoint.Position);
            return new Level(cells, startPoint, endPoint, points);
        }

        private List<IPathPoint> GetPoints(LevelCell[,] cells, List<LevelCell> freeCells, Vector2Int startPoint, Vector2Int endPoint)
        {
            var minCount = (Mathf.Abs(startPoint.x - endPoint.x) + Mathf.Abs(startPoint.y - endPoint.y)) * 2;
            var maxArea = _levelGeneratorData.MaxDistance.x * _levelGeneratorData.MaxDistance.y;
            var maxCountWithoutStartAndEnd = maxArea - 2;
            var count = Random.Range(minCount, maxCountWithoutStartAndEnd);
            List<IPathPoint> points = new List<IPathPoint>();
            for (int i = 0; i < count; i++)
            {
                var freeCell = GetFreeCell(freeCells, cells);
                var point = _pointFactory.GetPathPoint(freeCell.Position);
                points.Add(point);
            }

            return points;
        }

        private IEndPoint GetEndPoint(LevelCell[,] cells, List<LevelCell> freeCells, Vector2Int startPointPosition)
        {
            var freeCell = GetFreeCell(freeCells, cells, startPointPosition);
            return _pointFactory.GetEndPoint(freeCell.Position);
        }

        private IStartPoint SetStartPoint(LevelCell[,] cells, List<LevelCell> freeCells)
        {
            var freeCell = GetFreeCell(freeCells, cells);
            return _pointFactory.GetStartPoint(freeCell.Position);
        }

        private LevelCell GetFreeCell(List<LevelCell> freeCells, LevelCell[,] cells, Vector2Int? fullPoint = null)
        {
            var freeCell = freeCells[Random.Range(0, freeCells.Count)];
            if (fullPoint.HasValue)
            {
                Vector2Int vector2Int = fullPoint.Value;
                while (freeCell.Position.x == vector2Int.x && freeCell.Position.y == vector2Int.y)
                {
                    freeCell = freeCells[Random.Range(0, freeCells.Count)];
                }
            }
            freeCells.Remove(freeCell);
            cells[freeCell.Position.x, freeCell.Position.y] = new LevelCell(true, freeCell.Position);
            return freeCell;
        }


        private LevelCell[,] GetCells(Vector2Int distance, out List<LevelCell> levelCells)
        {
            levelCells = new List<LevelCell>();
            var result = new LevelCell[distance.x, distance.y];
            for (int i = 0; i < distance.x; i++)
            {
                for (int j = 0; j < distance.y; j++)
                {
                    var levelCell = new LevelCell(false, new Vector2Int(i, j));
                    levelCells.Add(levelCell);
                    result[i, j] = levelCell;
                }
            }

            return result;
        }
    }
}

﻿using System.Collections.Generic;
using System.Drawing;

namespace VectorEditorProject.Figures.Utility
{
    public class PointsSettings
    {
        private int _limitPoint = 0;
        private List<Point> _points = new List<Point>();

        /// <summary>
        /// Конструктор для безлимитных фигур
        /// </summary>
        public PointsSettings()
        {
        }

        /// <summary>
        /// Конструктор для фигур с лимитным числом точек
        /// </summary>
        /// <param name="limitPoint"></param>
        public PointsSettings(int limitPoint)
        {
            _limitPoint = limitPoint;
        }

        /// <summary>
        /// Добавление точки
        /// </summary>
        /// <param name="point">Точка</param>
        public void AddPoint(Point point)
        {
            if (CanAddPoint())
            {
                _points.Add(point);
            }
        }

        /// <summary>
        /// Возможно ли добавить точку
        /// </summary>
        /// <returns></returns>
        public bool CanAddPoint()
        {
            if (_limitPoint == 0)
            {
                // Если лимита нет (= 0), можно добавлять точки
                return true;
            }

            else
            {
                if (_points.Count < _limitPoint)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Возвращает список точек, доступный только для чтения
        /// </summary>
        /// <returns>Список точек</returns>
        public IReadOnlyList<Point> GetPoints()
        {
            return _points;
        }

        /// <summary>
        /// Перемещение точки
        /// </summary>
        /// <param name="n">Индекс точки в массиве точек</param>
        /// <param name="p">Точка</param>
        public void ReplacePoint(int n, Point p)
        {
            _points[n] = p;
        }

        /// <summary>
        /// Удаление точки
        /// </summary>
        /// <param name="point">Точка</param>
        public void DeletePoint(Point point)
        {
            _points.Remove(point);
        }

        public void RemoveLast()
        {
            _points.RemoveAt(_points.Count - 1);
        }
    }
}

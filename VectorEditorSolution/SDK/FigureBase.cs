using System;
using System.Drawing;

namespace SDK
{
    /// <summary>
    /// Базовая фигура
    /// </summary>
    [Serializable]
    public abstract class FigureBase
    {
        /// <summary>
        /// Guid
        /// </summary>
        public Guid guid = Guid.NewGuid();

        /// <summary>
        /// Настройки линии
        /// </summary>
        protected LineSettings _lineSettings;

        /// <summary>
        /// Настройки точек
        /// </summary>
        protected PointsSettings _pointsSettings;

        /// <summary>
        /// Настройки линии
        /// </summary>
        public LineSettings LineSettings
        {
            get => _lineSettings;
            set => _lineSettings = value;
        }

        /// <summary>
        /// Настройки точек
        /// </summary>
        public PointsSettings PointsSettings
        {
            get => _pointsSettings;
        }

        /// <summary>
        /// Получить границы фигуры
        /// </summary>
        /// <returns>Границы фигуры</returns>
        public virtual Rectangle GetBorderRectangle()
        {
            if (PointsSettings.GetPoints().Count < 2)
            {
                return new Rectangle();
            }

            Point leftTopPoint = Point.Round(_pointsSettings.LeftTopPointF());
            Point rightBottomPoint = Point.Round(_pointsSettings.RightBottomPointF());

            return new Rectangle(leftTopPoint.X, leftTopPoint.Y,
                rightBottomPoint.X - leftTopPoint.X,
                rightBottomPoint.Y - leftTopPoint.Y);
        }

        /// <summary>
        /// Получить хэш-код
        /// </summary>
        /// <returns>Хэш</returns>
        public override int GetHashCode()
        {
            return _lineSettings.GetHashCode() + _pointsSettings.GetHashCode();
        }

        /// <summary>
        /// Название фигуры
        /// </summary>
        /// <returns></returns>
        public abstract string FigureName { get; }

        /// <summary>
        /// Копирование фигуры
        /// </summary>
        /// <param name="figure">Фигура</param>
        /// <returns>Копия фигуры</returns>
        public virtual FigureBase CopyFigure()
        {
            var copy = (FigureBase)Activator.CreateInstance(GetType());
            copy.guid = guid;
            copy.PointsSettings.Clear();
            foreach (var point in PointsSettings.GetPoints())
            {
                copy.PointsSettings.AddPoint(new PointF(point.X, point.Y));
            }

            copy.LineSettings.Color = LineSettings.Color;
            copy.LineSettings.Style = LineSettings.Style;
            copy.LineSettings.Width = LineSettings.Width;

            return copy;
        }
    }
}

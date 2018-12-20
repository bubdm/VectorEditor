﻿using System;
using VectorEditorProject.Core.Figures.Utility;

namespace VectorEditorProject.Core.Figures
{
    [Serializable]
    public class PolyLine : BaseFigure
    {
        /// <summary>
        /// Конструктор "Полилиния"
        /// </summary>
        public PolyLine()
        {
            _lineSettings = new LineSettings();
            _pointsSettings = new PointsSettings();
        }
    }
}

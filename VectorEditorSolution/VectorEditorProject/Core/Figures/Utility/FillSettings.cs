﻿using System.Drawing;

namespace VectorEditorProject.Figures.Utility
{
    public class FillSettings
    {
        public Color Color { get; set; }

        /// <summary>
        /// Конструктор настройки заливки по умолчанию
        /// </summary>
        public FillSettings()
        {
            Color = Color.Transparent;
        }

        /// <summary>
        /// Конструктор настройки заливки
        /// </summary>
        /// <param name="color">Цвет</param>
        public FillSettings(Color color)
        {
            Color = color;
        }
    }
}
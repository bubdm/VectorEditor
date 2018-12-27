﻿using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VectorEditorProject.Core.Commands;
using VectorEditorProject.Core.Figures;

namespace VectorEditorProject.Core.States
{
    public class AddFigureState : StateBase
    {
        private EditContext _editContext;
        private ControlUnit _controlUnit;

        /// <summary>
        /// Конструктор состояния добавления фигуры
        /// </summary>
        /// <param name="controlUnit"></param>
        /// <param name="editContext"></param>
        public AddFigureState(ControlUnit controlUnit, EditContext editContext)
        {
            _controlUnit = controlUnit;
            _editContext = editContext;
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            FigureFactory figureFactory = new FigureFactory();
            FigureBase figure = figureFactory.CreateFigure(_controlUnit.GetActiveFigureType());
            if (figure is FilledFigureBase filledFigure)
            {
                filledFigure.FillSettings = _controlUnit.GetActiveFillSettings();
            }
            figure.LineSettings = _controlUnit.GetActiveLineSettings();
            figure.PointsSettings.AddPoint(new PointF(e.X, e.Y));

            var command = CommandFactory.CreateAddFigureCommand(_controlUnit, figure);
            _controlUnit.StoreCommand(command);
            _controlUnit.Do();

            if (_controlUnit.GetDocument().GetFigures().Last().PointsSettings.CanAddPoint())
            {
                _editContext.SetActiveFigure(figure);
                _editContext.SetActiveState(EditContext.States.AddPointState);
            }
            else
            {
                _editContext.SetActiveState(EditContext.States.SelectionState);
            }
        }
    }
}

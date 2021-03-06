using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SDK;
using VectorEditorProject.Core.Commands;

namespace VectorEditorProject.Core.States
{
    /// <summary>
    /// Состояние добавления фигуры
    /// </summary>
    public class AddFigureState : StateBase
    {
        /// <summary>
        /// Edit Context
        /// </summary>
        protected readonly IEditContext _editContext;

        /// <summary>
        /// Control Unit
        /// </summary>
        protected readonly IControlUnit _controlUnit;

        /// <summary>
        /// Конструктор состояния добавления фигуры
        /// </summary>
        /// <param name="controlUnit">Control Unit</param>
        /// <param name="editContext">Edit Context</param>
        public AddFigureState(IControlUnit controlUnit, IEditContext editContext)
        {
            _controlUnit = controlUnit;
            _editContext = editContext;
        }

        /// <summary>
        /// Нажатие кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void MouseDown(object sender, MouseEventArgs e)
        {
            //FigureFactory figureFactory = new FigureFactory();
            //FigureBase figure =
            //    figureFactory.CreateFigure(_controlUnit.GetActiveFigureType());

            FigureBase figure = DI.GetInstance().Container
                .GetInstance<FigureBase>(_controlUnit
                    .GetActiveFigureType());

            if (figure is FilledFigureBase filledFigure)
            {
                filledFigure.FillSettings =
                    _controlUnit.GetActiveFillSettings();
            }

            figure.LineSettings = _controlUnit.GetActiveLineSettings();
            figure.PointsSettings.AddPoint(new PointF(e.X, e.Y));

            var command = CommandFactory.CreateAddFigureCommand(_controlUnit,
                figure);
            _controlUnit.StoreCommand(command);
            _controlUnit.Do();

            if (_controlUnit.GetDocument().GetFigures().Last()
                .PointsSettings.CanAddPoint())
            {
                _editContext.SetActiveFigure(figure);
                _editContext.SetActiveState(States.AddPointState);
            }
            else
            {
                _editContext.SetActiveState(States.SelectionState);
            }
        }
    }
}

using System.Windows.Forms;
using Moq;
using NUnit.Framework;
using VectorEditorProject.Core;
using VectorEditorProject.Forms;
using VectorEditorTest.Stubs;

namespace VectorEditorTest
{
    [TestFixture]
    class AddFigureStateTest
    {
        [TestCase(TestName = "Позитивное создание состояния добавления фигуры " +
                             "через конструктор")]
        public void ConstructorTest()
        {
            // Arragne
            var controlUnit = new Mock<IControlUnit>();
            var editContext = new Mock<IEditContext>();

            // Act
            var state =
                new AddFigureStateStub(controlUnit.Object, editContext.Object);

            // Assert
            Assert.IsNotNull(state.ControlUnit);
            Assert.IsNotNull(state.EditContext);
        }

        [TestCase(TestName = "Позитивная проверка вызова нажатия кнопки мыши")]
        public void MouseDownTest()
        {
            // Arragne
            var toolsControl = new ToolsControl();
            toolsControl.SetActiveFigureType("Circle");
            var controlUnit = new ControlUnit(new PictureBox(),
                new FigureSettingsControl(), toolsControl, new PropertyGrid());
            var editContext = new EditContextStub(controlUnit);
            controlUnit.EditContext = editContext;
            var state = new AddFigureStateStub(controlUnit, editContext);
            editContext.ActiveState = state;

            // Act
            editContext.MouseDown(this,
                new MouseEventArgs(MouseButtons.Left, 1, 2, 3, 4));

            // Assert
            Assert.IsNotEmpty(state.ControlUnit.GetDocument().GetFigures());
        }
    }
}

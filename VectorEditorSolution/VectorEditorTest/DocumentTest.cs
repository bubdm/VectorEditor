using System.Drawing;
using Moq;
using NUnit.Framework;
using SDK;
using VectorEditorProject.Core;

namespace VectorEditorTest
{
    class DocumentTest
    {
        [Test]
        [TestCase(5, 5, 
            TestName = "Конструктор с граничными 5, 5 параметрами")]
        [TestCase(int.MaxValue, int.MaxValue, 
            TestName = "Конструктор с максимальными параметрами")]
        public void ConstructorTest1(int a, int b)
        {
            // Arrange
            var doc = new Document("Test", Color.Aqua, new Size(a, b));

            // Act 
            // Assert
            Assert.AreEqual("Test", doc.Name);
            Assert.AreEqual(Color.Aqua, doc.Color);
            Assert.IsNotNull(doc.Size);
            Assert.AreEqual(new Size(a, b), doc.Size);
        }

        [Test]
        [TestCase(4, 4,
            TestName = "Негативный конструктор с граничными 4, 4 параметрами")]
        [TestCase(int.MinValue, int.MinValue,
            TestName = "Негативный конструктор с минимальными параметрами")]
        public void ConstructorTest2(int a, int b)
        {
            // Arrange 
            var doc = new Document("Test", Color.Aqua, new Size(a, b));

            // Act 
            // Assert
            Assert.AreEqual("Test", doc.Name);
            Assert.AreEqual(Color.Aqua, doc.Color);
            Assert.IsNotNull(doc.Size);
            Assert.AreNotEqual(new Size(a, b), doc.Size);
        }

        [Test]
        [TestCase(5, 5, 
            TestName = "Свойство Размер холста с граничными 5, 5 параметрами")]
        [TestCase(int.MaxValue, int.MaxValue,
            TestName = "Свойство Размер холста с максимальными параметрами")]
        public void SizeTest1(int a, int b)
        {
            // Arrange 
            var doc = new Document("", Color.AliceBlue, new Size(0, 0));

            // Act 
            doc.Size = new Size(a, b);

            // Assert
            Assert.IsNotNull(doc.Size);
            Assert.AreEqual(new Size(a, b), doc.Size);
        }

        [Test]
        [TestCase(4, 4,
            TestName = "Негативный тест свойство Размер холста " +
                       "с граничными 4, 4 параметрами")]
        [TestCase(int.MinValue, int.MinValue,
            TestName = "Негативный тест свойство Размер холста " +
                       "с минимальными параметрами")]
        public void SizeTest2(int a, int b)
        {
            // Arrange 
            var doc = new Document("", Color.AliceBlue, new Size(0, 0));

            // Act 
            doc.Size = new Size(a, b);

            // Assert
            Assert.IsNotNull(doc.Size);
            Assert.AreNotEqual(new Size(a, b), doc.Size);
        }

        [Test]
        [TestCase("Test", TestName = "Проверка с типовым значением для имени")]
        [TestCase(" ", TestName = "Проверка одного пробела в имени документа")]
        public void NameTest(string a)
        {
            // Arrange 
            var doc = new Document("", Color.AliceBlue, new Size(0, 0));

            // Act 
            doc.Name = a;

            // Assert
            Assert.AreEqual(a, doc.Name);
        }

        [TestCase(TestName = "Позитивный тест очистки холста")]
        public void ClearCanvasTest()
        {
            // Arrange 
            var doc = new Document("", Color.AliceBlue, new Size(0, 0));
            Mock<FigureBase> figureMock = new Mock<FigureBase>();

            // Act 
            int countBeforeAdd = doc.GetFigures().Count;
            doc.AddFigure(figureMock.Object);
            int countAfterAdd = doc.GetFigures().Count;
            doc.ClearCanvas();

            // Assert
            Assert.AreEqual(0, countBeforeAdd);
            Assert.AreEqual(1, countAfterAdd);
            Assert.AreEqual(0, doc.GetFigures().Count);
        }

        [TestCase(TestName = "Позитивное получение фигуры по guid")]
        public void GetFigureTest()
        {
            // Arrange 
            var doc = new Document("", Color.AliceBlue, new Size(0, 0));
            Mock<FigureBase> figureMock = new Mock<FigureBase>();

            // Act 
            FigureBase shouldBeNull =
                doc.GetFigure(figureMock.Object.guid);
            doc.AddFigure(figureMock.Object);
            FigureBase shouldBeFigureMock =
                doc.GetFigure(figureMock.Object.guid);

            // Assert
            Assert.IsNull(shouldBeNull);
            Assert.AreEqual(figureMock.Object, shouldBeFigureMock);
        }

        [TestCase(TestName = "Позитивное получение фигуры через список")]
        public void GetFiguresTest()
        {
            // Arrange 
            var doc =
                new Document("", Color.AliceBlue, new Size(0, 0));
            Mock<FigureBase> figureMock = new Mock<FigureBase>();

            // Act 
            int shouldBeZero = doc.GetFigures().Count;
            doc.AddFigure(figureMock.Object);
            int shouldBeOne = doc.GetFigures().Count;
            var shouldBeFigureMock = doc.GetFigures()[0];

            // Assert
            Assert.AreEqual(0, shouldBeZero);
            Assert.AreEqual(1, shouldBeOne);
            Assert.AreSame(figureMock.Object, shouldBeFigureMock);
        }


        [TestCase(TestName = "Позитивный тест на добавление фигур")]
        public void AddFigureTest()
        {
            // Arrange 
            var doc =
                new Document("", Color.AliceBlue, new Size(0, 0));
            Mock<FigureBase> figureMock = new Mock<FigureBase>();

            // Act 
            int shouldBeZero = doc.GetFigures().Count;
            doc.AddFigure(figureMock.Object);
            int shouldBeOne = doc.GetFigures().Count;
            var shouldBeFigureMock = doc.GetFigures()[0];

            // Assert
            Assert.AreEqual(0, shouldBeZero);
            Assert.AreEqual(1, shouldBeOne);
            Assert.AreSame(figureMock.Object, shouldBeFigureMock);
        }

        [TestCase(TestName = "Позитивный тест на удаление фигур")]
        public void DeleteFigureTest()
        {
            // Arrange 
            var doc =
                new Document("", Color.AliceBlue, new Size(0, 0));
            Mock<FigureBase> figureMock1 = new Mock<FigureBase>();
            Mock<FigureBase> figureMock2 = new Mock<FigureBase>();

            // Act 
            doc.AddFigure(figureMock1.Object);
            doc.AddFigure(figureMock2.Object);
            int shouldBeTwo = doc.GetFigures().Count;
            doc.DeleteFigure(figureMock1.Object); //by figure
            int shouldBeOne = doc.GetFigures().Count;
            doc.DeleteFigure(figureMock2.Object.guid); //by Guid
            int shouldBeZero = doc.GetFigures().Count;

            // Assert
            Assert.AreEqual(0, shouldBeZero);
            Assert.AreEqual(1, shouldBeOne);
            Assert.AreEqual(2, shouldBeTwo);
        }
    }
}

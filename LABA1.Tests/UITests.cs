using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Windows.Forms;
using laba1.Forms;

namespace laba1.Tests
{
    // На этом этапе (лаб. №2) элементы управления в DeliveryForm ещё не
    // имеют свойства Name (оно будет добавлено в лаб. №6 для FlaUI),
    // поэтому поиск ведётся по тексту кнопки.
    [TestClass]
    public class UITests
    {
        private DeliveryForm _form = null!;

        [TestInitialize]
        public void SetUp()
        {
            _form = new DeliveryForm();
        }

        private T? FindControl<T>(string text) where T : Control =>
            _form.Controls.OfType<T>().FirstOrDefault(c => c.Text == text);

        [TestMethod]
        public void AddDeliveryButton_IsPresentAndEnabled()
        {
            var button = FindControl<Button>("Добавить");
            Assert.IsNotNull(button);
            Assert.IsTrue(button!.Enabled);
        }

        [TestMethod]
        public void RemoveDeliveryButton_IsPresent()
        {
            var button = FindControl<Button>("Удалить");
            Assert.IsNotNull(button);
        }

        [TestMethod]
        public void UpdateStatusButton_IsPresent()
        {
            var button = FindControl<Button>("Обновить статус");
            Assert.IsNotNull(button);
        }

        [TestMethod]
        public void DeliveriesListBox_IsPresentAndEnabled()
        {
            var listBox = _form.Controls.OfType<ListBox>().FirstOrDefault();
            Assert.IsNotNull(listBox);
            Assert.IsTrue(listBox!.Enabled);
        }
    }
}

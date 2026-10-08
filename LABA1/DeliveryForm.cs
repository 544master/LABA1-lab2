using System;
using System.Drawing;
using System.Windows.Forms;
using laba1.Models;
using laba1.Services;

namespace laba1.Forms
{
    public class DeliveryForm : Form
    {
        private DeliveryManager deliveryManager;
        private TextBox customerNameTextBox;
        private TextBox addressTextBox;
        private DateTimePicker deliveryDatePicker;
        private ComboBox statusComboBox;
        private Button addDeliveryButton;
        private Button removeDeliveryButton;
        private Button updateStatusButton;
        private ListBox deliveriesListBox;

        public DeliveryForm()
        {
            this.Text = "Управление доставкой";
            this.Width = 600;
            this.Height = 500;

            // Инициализация элементов управления
            customerNameTextBox = new TextBox
            {
                Location = new Point(10, 10),
                Width = 150,
                PlaceholderText = "Имя клиента"
            };

            addressTextBox = new TextBox
            {
                Location = new Point(170, 10),
                Width = 200,
                PlaceholderText = "Адрес"
            };

            deliveryDatePicker = new DateTimePicker
            {
                Location = new Point(380, 10)
            };

            statusComboBox = new ComboBox
            {
                Location = new Point(10, 40),
                Width = 100,
                Items = { "Новый", "В пути", "Доставлен" }
            };

            addDeliveryButton = new Button
            {
                Location = new Point(10, 70),
                Text = "Добавить",
                Width = 100
            };
            addDeliveryButton.Click += AddDeliveryButton_Click;

            removeDeliveryButton = new Button
            {
                Location = new Point(120, 70),
                Text = "Удалить",
                Width = 100
            };
            removeDeliveryButton.Click += RemoveDeliveryButton_Click;

            updateStatusButton = new Button
            {
                Location = new Point(220, 70),
                Text = "Обновить статус",
                Width = 120
            };
            updateStatusButton.Click += UpdateStatusButton_Click;

            deliveriesListBox = new ListBox
            {
                Location = new Point(10, 100),
                Width = 560,
                Height = 250
            };

            // Добавление на форму
            this.Controls.Add(customerNameTextBox);
            this.Controls.Add(addressTextBox);
            this.Controls.Add(deliveryDatePicker);
            this.Controls.Add(statusComboBox);
            this.Controls.Add(addDeliveryButton);
            this.Controls.Add(removeDeliveryButton);
            this.Controls.Add(updateStatusButton);
            this.Controls.Add(deliveriesListBox);

            // Инициализация менеджера
            deliveryManager = new DeliveryManager();
            UpdateDeliveriesList();
        }

        private void UpdateDeliveriesList()
        {
            deliveriesListBox.Items.Clear();
            foreach (var delivery in deliveryManager.Deliveries)
            {
                deliveriesListBox.Items.Add($"{delivery.CustomerName} - {delivery.Address} ({delivery.Status})");
            }
        }

        private void AddDeliveryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customerNameTextBox.Text) ||
                string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            DateTime deliveryDate = deliveryDatePicker.Value;
            Delivery newDelivery = new Delivery(customerNameTextBox.Text, addressTextBox.Text, deliveryDate);

            try
            {
                deliveryManager.AddDelivery(newDelivery);
                customerNameTextBox.Clear();
                addressTextBox.Clear();
                UpdateDeliveriesList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveDeliveryButton_Click(object sender, EventArgs e)
        {
            if (deliveriesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите доставку для удаления!");
                return;
            }

            string selectedItem = deliveriesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);

            if (parts.Length >= 2)
            {
                string customerName = parts[0].Trim();
                string addressPart = parts[1];
                for (int i = 2; i < parts.Length - 1; i++)
                {
                    addressPart += "-" + parts[i];
                }
                string address = addressPart.Trim();

                var deliveryToRemove = deliveryManager.Deliveries.Find(d => d.CustomerName == customerName && d.Address == address);

                if (deliveryToRemove != null)
                {
                    try
                    {
                        deliveryManager.RemoveDelivery(deliveryToRemove);
                        UpdateDeliveriesList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void UpdateStatusButton_Click(object sender, EventArgs e)
        {
            if (deliveriesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите доставку для обновления статуса!");
                return;
            }

            string selectedItem = deliveriesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);

            if (parts.Length >= 2)
            {
                string customerName = parts[0].Trim();
                string addressPart = parts[1];
                for (int i = 2; i < parts.Length - 1; i++)
                {
                    addressPart += "-" + parts[i];
                }
                string address = addressPart.Trim();

                var deliveryToUpdate = deliveryManager.Deliveries.Find(d => d.CustomerName == customerName && d.Address == address);

                if (deliveryToUpdate != null)
                {
                    DeliveryStatus newStatus = (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), statusComboBox.SelectedItem.ToString());

                    try
                    {
                        deliveryManager.UpdateDeliveryStatus(deliveryToUpdate, newStatus);
                        UpdateDeliveriesList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
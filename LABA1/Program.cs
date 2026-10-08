using System;
using System.Windows.Forms;
using laba1.Forms; // Подключаем нашу форму из новой папки

namespace laba1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем форму из пространства имен laba1.Forms
            Application.Run(new DeliveryForm());
        }
    }
}
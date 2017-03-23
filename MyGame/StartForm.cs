using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    /// <summary>
    /// Форма запроса размеров игрового поля
    /// </summary>
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void tbWidth_KeyPress(object sender, KeyPressEventArgs e) //Обработка нажатия клавиш
        {
            if (e.KeyChar<=47 || e.KeyChar>=58 && e.KeyChar!=8)  //Если нажатая клавиша не цифра и не bacspase, то событие не отработает.
                e.Handled = true;
        }

        private void tbHight_KeyPress(object sender, KeyPressEventArgs e) //аналогично предыдущему
        {
            if (e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void btnStart_Click(object sender, EventArgs e) //Обработка нажатия на кнопку "Старт"
        {
            if (tbHight.Text!=""&&tbWidth.Text!="") //Если поля текстбоксов не пустые запускаем форму игры
            {
               
                Form form = new Form(); 
                form.Width = int.Parse(tbWidth.Text); //т.к. в текстбоксах 100% числовые значения использую простой Parse
                form.Height = int.Parse(tbHight.Text);
                
                try //Если введеные значения будут больше 1000 или меньше 0, то сработает исключение
                {
                    Game.Init(form);                  
                }
                catch (ArgumentOutOfRangeException ex) //Отловим исключение и выведем через мессадж_бокс
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                this.Visible = false; //Если все нормально, скроем первую форму и откроем форму игры
                form.Show();
                Game.Draw();
               
            }
        }
    }
}

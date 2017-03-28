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
    /// Класс описывающий форму Журнала
    /// </summary>
    public partial class LogForm : Form
    {
        /// <summary>
        /// Свойство для записи в журнал
        /// </summary>
        public string StrLog 
        {
            set { tbLog.Text += value + Environment.NewLine; }
        }
        public LogForm()
        {
            InitializeComponent();
        }
    }
}

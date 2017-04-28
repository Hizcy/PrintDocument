using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        string skExpenseContext = string.Format(Class1.Content(),"0004867702", "1407-推拿专用粉", 3, 1, 3, "", 3, "现金：", 1, "银行卡：", 2, "000201704221409", "2017-04-22 14:09", "1024-王超", "二环东路与华龙路交叉口发展大厦B座\r\n16a", "0531-68465852", "");
        public Form2()
        {
            InitializeComponent();
        }
        TextPrinter textPrinter;
        private void button1_Click(object sender, EventArgs e)
        {
            textPrinter = new TextPrinter();
            textPrinter.Context = skExpenseContext;
            textPrinter.Font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            textPrinter.Title = "承康小儿推拿总店";
            textPrinter.TitleFont = new Font("宋体", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            textPrinter.TitleType = TitleType.OnlyFirstPage;
            textPrinter.FooterType = FooterType.None;
            //textPrinter.FooterFormat = "Page {0}"; 
            //txtPrinter = new TextPrinter();
            //txtPrinter.Title = "承康小儿推拿总店";
            //txtPrinter.Context = skExpenseContext;
            //txtPrinter.FooterFormat = string.Format(txtPrinter.FooterFormat, 1); 
            //txtPrinter.RowPadding = 10;
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            textPrinter.PrintSetting();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textPrinter.PageSetting();
        } 
        private void button4_Click(object sender, EventArgs e)
        {
            textPrinter.Print();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TextPrinter.TestPreview(skExpenseContext);
        }

    }
}

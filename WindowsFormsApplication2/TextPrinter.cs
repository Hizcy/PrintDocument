using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public class TextPrinter
    {
        private string m_Title;
        private string m_Context;
        private Font m_Font;
        private Font m_TitleFont;
        private SizeF m_WordSize;
        private int m_CurrentPage;
        private Rectangle m_PrintableArea;
        private string m_LeftContext;
        private int m_RowPadding;
        private TitleType m_TitleType;
        private FooterType m_FooterType;
        //private int m_TotalPage;
        //private bool m_Prepare;
        private string m_FooterFormat;
        private PrintDocument printDoc;
        public TextPrinter()
        {
            printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            m_Font = SystemFonts.DefaultFont;
            m_TitleFont = SystemFonts.CaptionFont;
            m_TitleType = TitleType.None;
            m_FooterType = FooterType.None;
            FooterFormat = "第{0}页";
        }
        /**/
        /// <summary>
        /// Report Title
        /// </summary>
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        /**/
        /// <summary>
        /// Report Context string
        /// </summary>
        public string Context
        {
            get { return m_Context; }
            set { m_Context = value; }
        }
        /**/
        /// <summary>
        /// Report Context Font（报告内容的字体）
        /// </summary>
        public Font Font
        {
            get { return m_Font; }
            set { m_Font = value; }
        }
        /**/
        /// <summary>
        /// Report Title Font（报告标题字体）
        /// </summary>
        public Font TitleFont
        {
            get { return m_TitleFont; }
            set { m_TitleFont = value; }
        }
        /**/
        /// <summary>
        /// Report Title Type（报告标题类型）
        /// </summary>
        public TitleType TitleType
        {
            get { return m_TitleType; }
            set { m_TitleType = value; }
        }
        /**/
        /// <summary>
        /// Report Footer Type
        /// </summary>
        public FooterType FooterType
        {
            get { return m_FooterType; }
            set { m_FooterType = value; }
        }
        /**/
        /// <summary>
        /// Report Footer Format. etc "第{0}页"
        /// </summary>
        public string FooterFormat
        {
            get { return m_FooterFormat; }
            set { m_FooterFormat = value; }
        }
        //public int TotalPage
        //{
        //    get { return m_TotalPage; }
        //}
        /**/
        /// <summary>
        /// Report row padding. 行间距
        /// </summary>
        public int RowPadding
        {
            get { return m_RowPadding; }
            set { m_RowPadding = value; }
        }
        /**/
        /// <summary>
        /// 初始化打印
        /// </summary>
        private void InitPrint()
        {
            m_CurrentPage = 1;
        }
        /**/
        /// <summary>
        /// 打印机设置
        /// </summary>
        public void PrintSetting()
        {
            PrintDialog pd = new PrintDialog();
            pd.Document = printDoc;
            pd.ShowDialog();
        }
        /**/
        /// <summary>
        /// 纸张设置
        /// </summary>
        public void PageSetting()
        {
            PageSetupDialog psd = new PageSetupDialog();
            psd.Document = printDoc;
            psd.ShowDialog();
        }
        /**/
        /// <summary>
        /// 预览报表
        /// </summary>
        public void Preview()
        {
            InitPrint();
            PrintPreviewDialog pdlg = new PrintPreviewDialog();
            pdlg.Document = printDoc;
            pdlg.WindowState = FormWindowState.Maximized;
            pdlg.PrintPreviewControl.Zoom = 1;
            //printDoc.Print();
            //m_Prepare = true;
            pdlg.ShowDialog();
        }
        /**/
        /// <summary>
        /// 打印报表
        /// </summary>
        public void Print()
        {
            InitPrint();
            printDoc.Print();
        }
        /**/
        /// <summary>
        /// 从内容Context中读出一行
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private string GetLine(Graphics g)
        {
            string tmp = String.Empty;
            try
            {
                SizeF size;
                for (int i = 0; i < m_LeftContext.Length; i++)
                {
                    size = g.MeasureString(m_LeftContext.Substring(0, i), Font);
                    if (size.Width > m_PrintableArea.Width)
                    {
                        tmp = m_LeftContext.Substring(0, i - 1);
                        m_LeftContext = m_LeftContext.Substring(i - 1);
                        return tmp;
                    }
                    if (m_LeftContext.Substring(0, i).IndexOf("/n", 0) >= 0)
                    {
                        tmp = m_LeftContext.Substring(0, i);
                        m_LeftContext = m_LeftContext.Substring(i);
                        return tmp;
                    }
                }
                tmp = m_LeftContext;
                m_LeftContext = String.Empty;
                return tmp;
            }
            finally
            {
                tmp = tmp.Replace("/n", "");
            }
        }
        /**/
        /// <summary>
        /// 打印主过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            int top = 0;
            if (m_CurrentPage == 1)
            {
                m_LeftContext = Context;
                m_LeftContext = m_LeftContext.Replace("/r", "");
                m_PrintableArea = e.MarginBounds;
                m_WordSize = e.Graphics.MeasureString("W", Font);
                if (TitleType == TitleType.OnlyFirstPage)
                    top += DrawTitle(e.Graphics);
            }
            if (TitleType == TitleType.AllPage)
                top += DrawTitle(e.Graphics);
            while (top < m_PrintableArea.Height - (int)Math.Floor(m_WordSize.Height))
            {
                string tmp = GetLine(e.Graphics);
                e.Graphics.DrawString(tmp, Font, SystemBrushes.MenuText, e.MarginBounds.X, e.MarginBounds.Y + top);
                top += (int)Math.Floor(m_WordSize.Height) + RowPadding;
            }
            if (FooterType != FooterType.None)
                DrawFooter(e.Graphics);
            e.HasMorePages = m_LeftContext != string.Empty;
            m_CurrentPage++;
        }
        /**/
        /// <summary>
        /// Print Report Title
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private int DrawTitle(Graphics g)
        {
            SizeF size = g.MeasureString(Title, TitleFont);
            DrawString(g, Title, TitleFont, SystemBrushes.WindowText, m_PrintableArea, StringAlignment.Center, StringAlignment.Near);
            return (int)Math.Floor(size.Height);
        }
        /**/
        /// <summary>
        /// Print Report Footer
        /// </summary>
        /// <param name="g"></param>
        private void DrawFooter(Graphics g)
        {
            string tmp = string.Empty;
            if (FooterType == FooterType.OnlyPageNum)
                tmp = string.Format(FooterFormat, m_CurrentPage);
            //else if (FooterType == FooterType.PageNumOfTotal)
            //    tmp = string.Format("{0} of {1}", m_CurrentPage, 0);
            SizeF size = g.MeasureString(tmp, SystemFonts.DefaultFont);
            DrawString(g, tmp, SystemFonts.DefaultFont, SystemBrushes.WindowText,
                    new Rectangle(m_PrintableArea.X, m_PrintableArea.Bottom - (int)size.Height,
                    m_PrintableArea.Width, (int)size.Height), StringAlignment.Far, StringAlignment.Center);
        }
        /**/
        /// <summary>
        /// Draw a string with alignment parameter
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="rect"></param>
        /// <param name="alignment"></param>
        /// <param name="lineAlignment"></param>
        private void DrawString(Graphics g, string s, Font font, Brush brush, Rectangle rect, StringAlignment alignment, StringAlignment lineAlignment)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = alignment;
            sf.LineAlignment = lineAlignment;
            g.DrawString(s, font, brush, rect, sf);
        }
        /// <summary>
        /// 测试预览
        /// </summary>
        /// <param name="pStr"></param>
        public static void TestPreview(string pStr)
        {
            TextPrinter textPrinter = new TextPrinter();
            textPrinter.Context = pStr;
            textPrinter.Font = new Font("宋体", 6, FontStyle.Regular, GraphicsUnit.Pixel);
            textPrinter.Title = "打印测试页";
            textPrinter.TitleFont = new Font("宋体", 8, FontStyle.Bold, GraphicsUnit.Pixel);
            textPrinter.TitleType = TitleType.AllPage;
            textPrinter.FooterType = FooterType.OnlyPageNum;
            textPrinter.FooterFormat = "Page {0}";
            //textPrinter.PrintSetting();
            //textPrinter.PageSetting();
            textPrinter.Preview();
            //textPrinter.Print();
        }
    }
    public enum TitleType
    {
        None,
        OnlyFirstPage,
        AllPage
    }
    public enum FooterType
    {
        None,
        OnlyPageNum
        //PageNumOfTotal
    }
}

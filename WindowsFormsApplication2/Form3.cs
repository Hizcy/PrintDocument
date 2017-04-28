using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SkConsume("{\"shopName\":\"承康小儿推拿总店\",\"cardNumber\":\"0004867702\",\"customer\":\"梁玲\",\"productName\":\"1407-推拿专用粉\",\"number\":\"1\",\"price\":\"3\",\"total\":\"3\",\"totalPrice\":\"3\",\"operation\":\"梁玲\",\"oddNumber\":\"000201704221745\",\"telephone\":\"0531-67818167\",\"staff\":\"1024-王超\",\"address\":\"二环东路与华龙路交叉口发展大厦B座16a\",\"remark\":\"祝宝宝快乐成长！\",\"payMethods\":[{\"payMethod\":\"现金\",\"price\":\"1\"},{\"payMethod\":\"支付宝\",\"price\":\"2\"}],\"storedMoney\":\"100\",\"giveMoney\":\"20\"}");
        }

        BillInfo model = new BillInfo();
        /// <summary> 散客消费
        /// 散客消费
        /// </summary>
        public void SkConsume(string billInfo)
        {
            model = JsonConvert.DeserializeObject<BillInfo>(billInfo);
            //初始化打印设备
            PrintDocument printDocument = new PrintDocument();
            //设置打印用的纸张 当设置为Custom的时候，可以自定义纸张的大小，还可以选择A4,A5等常用纸型  
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 250, 400);
            printDocument.PrintPage += new PrintPageEventHandler(this.SkPrintDocument_PrintPage);
            printDocument.Print();
        }

        /// <summary> 散客消费打印的格式  
        /// 散客消费打印的格式  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void SkPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //头部设置 100    250         
            e.Graphics.DrawString(model.shopName, new Font(new FontFamily("黑体"), 11, FontStyle.Bold), System.Drawing.Brushes.Black, 58, 0);
            e.Graphics.DrawString("消费凭证", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Blue, 99, 17);
            //信息的名称                  
            e.Graphics.DrawString("会员卡号：" + model.cardNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 50);

            //左 上   长   上(x,y,x1,y1:y=y1)+15
            e.Graphics.DrawLine(Pens.Black, 0, 65, 245, 65);
            e.Graphics.DrawString("项目/产品", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 72);
            e.Graphics.DrawString("数量", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 72);
            e.Graphics.DrawString("单价", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 169, 72);
            e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 209, 72);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 87, 245, 87);
            int shopFlag = 0;
            foreach (Shop shopModel in model.shops)
            {
                e.Graphics.DrawString(shopModel.productName, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 94 + shopFlag);
                e.Graphics.DrawString(shopModel.number.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 137, 94 + shopFlag);
                e.Graphics.DrawString(double.Parse(shopModel.price.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 173, 94 + shopFlag);
                e.Graphics.DrawString(double.Parse(shopModel.total.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 94 + shopFlag);
                shopFlag += 12;
            }
            shopFlag = shopFlag >= 12 ? shopFlag - 12 : 0;
            //服务人员
            int isOperationEmpty = model.operation == "" ? 0 : 12;
            e.Graphics.DrawString(model.operation, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 108 + shopFlag);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 109 + isOperationEmpty + shopFlag, 245, 109 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString("合计：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 116 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString(double.Parse(model.totalPrice.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 116 + isOperationEmpty + shopFlag);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 131 + isOperationEmpty + shopFlag, 245, 131 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString("付款：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 138 + isOperationEmpty + shopFlag);
            int i = 0;
            //付款方式
            foreach (Paymethod payMoney in model.payMethods)
            {
                e.Graphics.DrawString(payMoney.payMethod, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 138 + i + isOperationEmpty + shopFlag);
                e.Graphics.DrawString(double.Parse(payMoney.price.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 138 + i + isOperationEmpty + shopFlag);
                i += 12;
            }
            //流水信息 
            e.Graphics.DrawString("流水单号：" + model.oddNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 170 + isOperationEmpty + shopFlag);

            e.Graphics.DrawString("单据时间:" + DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 190 + isOperationEmpty + shopFlag);

            e.Graphics.DrawString("收银员:" + model.staff, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 210 + isOperationEmpty + shopFlag);
            //地址
            if (model.address.Length > 19)
            {
                model.address = model.address.Insert(19, "\r\n");
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 230 + isOperationEmpty + shopFlag);

                e.Graphics.DrawString("电话:" + "0531-67818167", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 262 + isOperationEmpty + shopFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 282 + isOperationEmpty);
            }
            else
            {
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 230 + isOperationEmpty + shopFlag);

                e.Graphics.DrawString("电话:" + model.telephone, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 250 + isOperationEmpty + shopFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 270 + isOperationEmpty + shopFlag);
            }

        }
        /// <summary> 会员消费打印格式
        /// 会员消费打印格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //头部设置 100    250         
            e.Graphics.DrawString(model.shopName, new Font(new FontFamily("黑体"), 11, FontStyle.Bold), System.Drawing.Brushes.Black, 58, 0);
            e.Graphics.DrawString("消费凭证", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Blue, 99, 17);
            //信息的名称                  
            e.Graphics.DrawString("会员卡号：" + model.cardNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 50);
            e.Graphics.DrawString("顾客姓名：" + model.customer, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 64);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 79, 245, 79);
            e.Graphics.DrawString("项目/产品", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 86);
            e.Graphics.DrawString("数量", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 86);
            e.Graphics.DrawString("单价", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 169, 86);
            e.Graphics.DrawString("总金额", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 209, 86);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 101, 245, 101);
            int shopFlag = 0;
            foreach (Shop shopModel in model.shops)
            {
                e.Graphics.DrawString(shopModel.productName, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 108 + shopFlag);
                e.Graphics.DrawString(shopModel.number.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 137, 108 + shopFlag);
                e.Graphics.DrawString(double.Parse(shopModel.price.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 173, 108 + shopFlag);
                e.Graphics.DrawString(double.Parse(shopModel.total.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 108 + shopFlag);
                shopFlag += 12;
            }
            shopFlag = shopFlag >= 12 ? shopFlag - 12 : 0;
            //服务人员
            int isOperationEmpty = model.operation == "" ? 0 : 12;
            e.Graphics.DrawString(model.operation, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 120 + shopFlag);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 121 + isOperationEmpty + shopFlag, 245, 121 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString("合计：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 128 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString(double.Parse(model.totalPrice.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 128 + isOperationEmpty + shopFlag);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 143 + isOperationEmpty + shopFlag, 245, 143 + isOperationEmpty + shopFlag);
            e.Graphics.DrawString("付款：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 150 + isOperationEmpty + shopFlag);
            int payMoneyFlag = 0;
            //付款方式
            foreach (Paymethod payMoney in model.payMethods)
            {
                e.Graphics.DrawString(payMoney.payMethod, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 150 + payMoneyFlag + isOperationEmpty + shopFlag);
                e.Graphics.DrawString(double.Parse(payMoney.price.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 150 + payMoneyFlag + isOperationEmpty + shopFlag);
                payMoneyFlag += 12;
            }
            payMoneyFlag = payMoneyFlag >= 12 ? payMoneyFlag - 12 : 0;
            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 165 + isOperationEmpty + shopFlag + payMoneyFlag, 245, 165 + isOperationEmpty + shopFlag + payMoneyFlag);
            //余额
            e.Graphics.DrawString("余额：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 178 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString("储值账户：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 172 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString("赠送账户:", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 184 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString(double.Parse(model.storedMoney.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 172 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString(double.Parse(model.giveMoney.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 184 + isOperationEmpty + shopFlag + payMoneyFlag);

            //会员签名
            e.Graphics.DrawString("会员签名:_____________", new Font(new FontFamily("黑体"), 10, FontStyle.Bold), System.Drawing.Brushes.Black, 105, 210 + isOperationEmpty + shopFlag + payMoneyFlag);

            //流水信息 
            e.Graphics.DrawString("流水单号：" + model.oddNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 235 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString("单据时间:" + DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 255 + isOperationEmpty + shopFlag + payMoneyFlag);
            e.Graphics.DrawString("收银员:" + model.staff, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 275 + isOperationEmpty + shopFlag + payMoneyFlag);
            //地址
            if (model.address.Length > 19)
            {
                model.address = model.address.Insert(19, "\r\n");
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 295 + isOperationEmpty + shopFlag + payMoneyFlag);

                e.Graphics.DrawString("电话:" + "0531-67818167", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 327 + isOperationEmpty + shopFlag + payMoneyFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 347 + isOperationEmpty + shopFlag + payMoneyFlag);
            }
            else
            {
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 295 + isOperationEmpty + shopFlag + payMoneyFlag);

                e.Graphics.DrawString("电话:" + model.telephone, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0,315  + isOperationEmpty + shopFlag + payMoneyFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 335 + isOperationEmpty + shopFlag + payMoneyFlag);
            } 
        }
        /// <summary>会员充值
        /// 会员充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CzPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //头部设置 100    250         
            e.Graphics.DrawString(model.shopName, new Font(new FontFamily("黑体"), 11, FontStyle.Bold), System.Drawing.Brushes.Black, 58, 0);
            e.Graphics.DrawString("消费凭证", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Blue, 99, 17);
            //信息的名称                  
            e.Graphics.DrawString("会员卡号：" + model.cardNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 50);
            e.Graphics.DrawString("会员姓名：" + model.customer, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 64);
            e.Graphics.DrawString("卡类别：" + model.customer, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 78);
            e.Graphics.DrawString("金额：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 92);
            e.Graphics.DrawString(double.Parse(model.storedMoneyc.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 92);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 107, 245, 107);
            //余额
            e.Graphics.DrawString("余额：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 121);
            e.Graphics.DrawString("储值账户：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 114);
            e.Graphics.DrawString("赠送账户：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 126);
            e.Graphics.DrawString(double.Parse(model.storedMoney.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 114);
            e.Graphics.DrawString(double.Parse(model.giveMoney.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 126);

            //左 上   长   上(x,y,x1,y1:y=y1)
            e.Graphics.DrawLine(Pens.Black, 0, 141, 245, 141);
            e.Graphics.DrawString("付款：", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 148);
            int paymethodFlag = 0;
            foreach (Paymethod paymethodModel in model.payMethods)
            {
                e.Graphics.DrawString(paymethodModel.payMethod, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 129, 148 + paymethodFlag);
                e.Graphics.DrawString(double.Parse(paymethodModel.price.ToString()).ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 214, 148 + paymethodFlag);
                paymethodFlag += 12;
            }
            paymethodFlag = paymethodFlag >= 12 ? paymethodFlag - 12 : 0;

            //会员签名
            e.Graphics.DrawString("会员签名:_____________", new Font(new FontFamily("黑体"), 10, FontStyle.Bold), System.Drawing.Brushes.Black, 105, 180 + paymethodFlag);

            //流水信息 
            e.Graphics.DrawString("流水单号：" + model.oddNumber, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 210 + paymethodFlag);
            e.Graphics.DrawString("单据时间:" + DateTime.Now.ToString(), new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 230 + paymethodFlag);
            e.Graphics.DrawString("收银员:" + model.staff, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 250 + paymethodFlag);
            //地址
            if (model.address.Length > 19)
            {
                model.address = model.address.Insert(19, "\r\n");
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 270 + paymethodFlag);

                e.Graphics.DrawString("电话:" + "0531-67818167", new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 302 + paymethodFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 322 + paymethodFlag);
            }
            else
            {
                e.Graphics.DrawString("地址:" + model.address, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 270 + paymethodFlag);

                e.Graphics.DrawString("电话:" + model.telephone, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 290 + paymethodFlag);

                e.Graphics.DrawString(model.remark, new Font(new FontFamily("黑体"), 8), System.Drawing.Brushes.Black, 0, 310 + paymethodFlag);
            } 
        } 
    }
}

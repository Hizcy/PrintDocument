using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class BillInfo
    {
        /// <summary>
        /// 店铺名
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNumber { get; set; }
        /// <summary>
        /// 顾客
        /// </summary>
        public string customer { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public Decimal totalPrice { get; set; }
        /// <summary>
        /// 服务员工
        /// </summary>
        public string operation { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string oddNumber { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 收银员
        /// </summary>
        public string staff { get; set; }
        /// <summary>
        /// 店铺地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 客户充值时 的充值金额
        /// </summary>
        public Decimal storedMoneyc { get; set; }
        /// <summary>
        /// 客户充值时的赠送金额
        /// </summary>
        public Decimal giveMoneyc { get; set; }
        /// <summary>
        /// 储值金额
        /// </summary>
        public Decimal storedMoney { get; set; }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public Decimal giveMoney { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string cType { get; set; }
        /// <summary>
        /// 购买商品
        /// </summary>
        public List<Shop> shops { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public List<Paymethod> payMethods { get; set; }
        public BillInfo()
        {
            shops = new List<Shop>();
            payMethods = new List<Paymethod>();
        }
    }

    public class Paymethod
    {
        /// <summary>
        /// 支付方式
        /// </summary>   
        public string payMethod { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public Decimal price { get; set; }
    }
    public class Shop
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public Decimal price { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public Decimal total { get; set; }
    }

}

using FootballPrime_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPrime_Website.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        FootballPrimeDbContext db = new FootballPrimeDbContext();
        //Hàm lấy danh sách các item trong cart
        public List<Cart> GetList()
        {
            List<Cart> orders = Session["Cart"] as List<Cart>;//khai báo đối orders = session cart 
            if (orders == null) //nếu order rỗng
            {
                orders = new List<Cart>();//gán biến orders = list các item
                Session["Cart"] = orders;//gán session bằng biến orders
            }

            return orders;
        }
        public ActionResult Index()
        {
            List<Cart> orders = GetList();//gọi list
            if (orders.Count == 0)
            {
                return RedirectToAction("EmptyCart", "Cart");
            }
            ViewBag.CartTotal = CartTotal();
            return View(orders);
        }

        public ActionResult CartPartial()
        {
            ViewBag.Amount = Amount();
            return PartialView();
        }

        //Hàm đém số lượng item
        private int Amount()
        {
            int Amt = 0; //gán số lượng = 0
            List<Cart> orders = Session["Cart"] as List<Cart>;//lấy danh sách item
            if (orders != null)//nếu danh sách ko rỗng
            {
                Amt = orders.Sum(n => n.TempAmount);// số lượng item = tổng của số lượng item
            }
            return Amt;
        }
        //Hàm tính tiền tạm tính
        private double CartTotal()
        {
            double Ttal = 0; //gán tiền =0
            List<Cart> orders = Session["Cart"] as List<Cart>;// lấy danh sách
            if (orders != null)
            {
                Ttal = orders.Sum(n => n.TempTotal);//nếu list ko rỗng thì cộng tất cả giá tiền lại
            }
            return Ttal;
        }

        //Hàm tính phí vận chuyển
        private double Shipping()
        {
            double Ship;
            if (Amount() < 5)
            {
                Ship = 15000;
            }
            else
            {
                Ship = 0;
            }
            return Ship;
        }
        //Hàm tính tổng đơn hàng
        private double OrderTotal()
        {
            double total = Shipping() + CartTotal();
            return total;
        }

        public ActionResult AddToCart(int id, string URL)
        {
            List<Cart> orders = GetList();//gọi list

            Cart product = orders.Find(n => n.TempID == id);
            if (product == null)
            {
                product = new Cart(id);
                orders.Add(product);
                return Redirect(URL);
            }
            else
            {
                product.TempAmount++;
                return Redirect(URL);
            }
        }
        public ActionResult DeleteItem(int itemId)
        {
            List<Cart> orders = GetList();//Lấy danh sách
            Cart product = orders.SingleOrDefault(n => n.TempID == itemId);//chọn sản phẩm có id = itemId
            orders.RemoveAll(n => n.TempID == itemId);//Xóa item đã chọn

            return RedirectToAction("Index");//trả về view cart
        }

        public ActionResult UpdateCart(int itemId, FormCollection form)
        {
            List<Cart> order = GetList();//lấy danh sách
            Cart product = order.SingleOrDefault(n => n.TempID == itemId);//lấy sản phẩm có id = id của item chọn để update
            product.TempAmount = int.Parse(form["Quantity"].ToString());//gán lại số lượng

            return RedirectToAction("Index");//refresh trang
        }

        public ActionResult DeleteCart()
        {
            List<Cart> orders = GetList();
            orders.Clear();

            return RedirectToAction("EmptyCart", "Cart");
        }

        public ActionResult EmptyCart()
        {
            return View();
        }

        /*Payment Section*/

        [HttpGet]
        public ActionResult Payment()
        {
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "Customer");
            }
            List<Cart> orders = GetList();
            ViewBag.CartTotal = CartTotal();
            ViewBag.Shipping = Shipping();
            ViewBag.OrderTotal = OrderTotal();

            return View(orders);
        }
        [HttpPost]
        public ActionResult Payment(FormCollection collection)
        {
            if (Session["Account"] == null || Session["Account"].ToString() == "")
            {
                return RedirectToAction("Login", "Customer");
            }

            Order bill = new Order();
            Customer customer = (Customer)Session["CustomerObject"];
            List<Cart> orders = GetList();

            bill.CtmID = customer.CtmID;
            bill.Odate = DateTime.Now;
            bill.DeliveryStatus = false;
            bill.PaymentCheck = false;
            db.Orders.Add(bill);
            db.SaveChanges();

            foreach (var item in orders)
            {
                int id = item.TempID;
                Product product = db.Products.SingleOrDefault(n => n.PrID == id);
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderID = bill.OrderID;
                orderDetail.PrID = item.TempID;
                orderDetail.Quantity = item.TempAmount;
                orderDetail.Total = (decimal)item.TempTotal;
                db.OrderDetails.Add(orderDetail);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("Confirm", "Cart");
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}
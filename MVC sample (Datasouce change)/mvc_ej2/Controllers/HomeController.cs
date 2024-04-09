using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Syncfusion.EJ2.Grids;


namespace mvc_ej2.Controllers
{
    public class HomeController : Controller
    {

        public static List<OrdersDetails> orddata = new List<OrdersDetails>();
        public static List<DropDownData> Dropdata = DropDownData.GetAllRecords();
        public ActionResult Index()
        {
            if (orddata.Count() == 0)
                BindData();

            ViewBag.DataSource = orddata;

            return View();
        }

        public class OperatorsTemp
        {
            public string @operator { get; set; }
        }

        public class clsNode : GridColumn
        {
            public object format { get; set; }
            public string VQDT { get; set; }
        }

        public ActionResult Child()
        {
            if (orddata.Count() == 0)
                BindData();

            ViewBag.datasource = orddata.ToArray();
            return View();
        }
        public void BindData()
        {
            int code = 10000;
            for (int i = 1; i < 10; i++)
            {
                orddata.Add(new OrdersDetails(code + 2, "ANATR", i + 0, 3.3 * i, true, "2021-04-15T23:40:00.000Z", "Madrid", "Queen Cozinha", "Brazil", new DateTime(1996, 9, 11), "Avda. Azteca 123"));
                orddata.Add(new OrdersDetails(code + 3, "ANTON", i + 1, 4.3 * i, true, "2044-06-01T13:20:00.000Z", "Cholchester", "Frankenversand", "Germany", new DateTime(1996, 10, 7), "Carrera 52 con Ave. Bolívar #65-98 Llano Largo"));
                orddata.Add(new OrdersDetails(code + 4, "%BLONP", i + 2, 5.3 * i, false, "2021-06-15T20:10:42.000Z", "Marseille", "Ernst Handel", "Austria", new DateTime(1996, 12, 30), "Magazinweg 7"));
                orddata.Add(new OrdersDetails(code + 5, "BOL%ID", i + 3, 6.3 * i, true, "2020-04-15T00:00:00.000Z", "Tsawassen", "Hanari Carnes", "Switzerland", new DateTime(1997, 12, 3), "1029 - 12th Ave. S."));
                code += 5;
            }
        }


        public ActionResult UrlDatasource(TestDm dm)
        {

            IEnumerable DataSource = orddata.ToList();
            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<OrdersDetails>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        public ActionResult Update(CRUDModel<OrdersDetails> myObject)
        {
            var ord = myObject;
            OrdersDetails val = orddata.Where(or => or.OrderID == ord.Value.OrderID).FirstOrDefault();
            if (val != null)
            {
                val.OrderID = ord.Value.OrderID;
                val.EmployeeID = ord.Value.EmployeeID;
                val.CustomerID = ord.Value.CustomerID;
                val.Freight = ord.Value.Freight;
                val.OrderDate = ord.Value.OrderDate;
                val.ShipCity = ord.Value.ShipCity;
                val.ShipAddress = ord.Value.ShipAddress;
                val.ShippedDate = ord.Value.ShippedDate;
            }

            return Json(ord.Value);

        }
        public ActionResult Insert(OrdersDetails value)
        {
            var ord = value;
            orddata.Insert(0, ord);
            return Json(value);
        }
        public void Remove(int key)
        {
            orddata.Remove(orddata.Where(or => or.OrderID == key).FirstOrDefault());
        }


        public class TestDm : DataManagerRequest
        {
            public int flag { get; set; }
        }

        public class CRUDModel
        {
            //public List<OrdersDetails> Added { get; set; }
            //public List<OrdersDetails> Changed { get; set; }
            //public List<OrdersDetails> Deleted { get; set; }
            public OrdersDetails value { get; set; }
            public int key { get; set; }
            public string action { get; set; }
            public string keycolumn { get; set; }
            public int IDMASTER { get; set; }
        }

        public class OrdersDetails
        {
            public OrdersDetails()
            {

            }
            public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, string OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
            {
                this.OrderID = OrderID;
                this.CustomerID = CustomerId;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.ShipCity = ShipCity;
                this.Verified = Verified;
                this.OrderDate = OrderDate;
                this.ShipName = ShipName;
                this.ShipCountry = ShipCountry;
                this.ShippedDate = ShippedDate;
                this.ShipAddress = ShipAddress;
            }

            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public string OrderDate { get; set; }

            public string ShipName { get; set; }

            public string ShipCountry { get; set; }

            public DateTime ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }
        public class DropDownData
        {
            public static List<DropDownData> times = new List<DropDownData>();
            public DropDownData()
            {

            }
            public DropDownData(double TimeValue, string timeZone)
            {
                this.TimeValue = TimeValue;
                this.TimeZone = timeZone;
            }
            public static List<DropDownData> GetAllRecords()
            {
                if (times.Count() == 0)
                {
                    times.Add(new DropDownData(-5.5, "India UTC+5.30 "));
                    times.Add(new DropDownData(-3, "Russia UTC+ 3 "));
                    times.Add(new DropDownData(-10, "Australia UTC+ 10 "));
                    times.Add(new DropDownData(4, "Canada UTC-4"));
                    times.Add(new DropDownData(0, "UTC"));

                }
                return times;
            }


            public double? TimeValue { get; set; }
            public string TimeZone { get; set; }
        }
    }
}
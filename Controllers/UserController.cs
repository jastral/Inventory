using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(User user)
        {
            string connstr;
            SqlConnection conn;
            connstr = @"Data Source=DESKTOP-6UO6ER9; Initial Catalog=InventoryDB; Integrated Security=true";
            conn = new SqlConnection(connstr);
            conn.Open();
            string sql = "";

            string email = user.Email;
            string pass = user.Password;

            SqlCommand query;
            SqlDataReader datareader;
            string output = "";

            sql = $"select * from users where user_email='{email}' and user_password='{pass}'";

            query = new SqlCommand(sql, conn);

            datareader = query.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    Session["id"] = datareader.GetValue(0);
                }
                return RedirectToAction("Details");
            }

            
            //Response.Write("connection made successfully");
            conn.Close();

            return View("../Shared/Error");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            string connstr;
            SqlConnection conn;
            connstr = @"Data Source=DESKTOP-6UO6ER9; Initial Catalog=InventoryDB; Integrated Security=true";

            conn = new SqlConnection(connstr);
            conn.Open();
            string sql = "";

            SqlCommand query;
            SqlDataAdapter adapter = new SqlDataAdapter();
            sql = "Insert into users (user_name,user_email,user_password,contact) values ('" + user.Name + "','"+user.Email+"','"+user.Password+"')";
            query = new SqlCommand(sql, conn);
            adapter.InsertCommand = new SqlCommand(sql, conn);
            adapter.InsertCommand.ExecuteNonQuery();
            query.Dispose();
            conn.Close();

            //Response.Write(user.email+" "+user.name);
            return RedirectToAction("Details");
        }



        //// GET: User/Details/5
        public ActionResult Details()
        {
            var list = new List<Products>();
            string connstr;
            SqlConnection conn;
            connstr = @"Data Source=DESKTOP-6UO6ER9; Initial Catalog=InventoryDB; Integrated Security=true";
            conn = new SqlConnection(connstr);
            conn.Open();
            string sql = "";
                       
            SqlCommand query;
            SqlDataReader datareader;
            string output = "";

            sql = $"select * from products";

            query = new SqlCommand(sql, conn);

            datareader = query.ExecuteReader();

            while (datareader.HasRows)
            {
                Products prod = new Products();
                prod.ProductName = (string)datareader[1];
                prod.Quantity = (int)datareader[2];
                prod.ProductDescription = (string)datareader[3];
                list.Add(prod);
            }

            return View();
        }

        //// GET: User/Create
        public ActionResult AddItems()
        {
            return View();
        }

        //// POST: User/Create
        [HttpPost]
        public ActionResult AddItems(Products prod)
        {
            string fileName = System.IO.Path.GetFileName(prod.Image.FileName);

            //Set the Image File Path.
            string filePath = "~/Uploads/" + fileName;

            //Save the Image File in Folder.
            prod.Image.SaveAs(Server.MapPath(filePath));

            string connstr;
            SqlConnection conn;
            connstr = @"Data Source=DESKTOP-6UO6ER9; Initial Catalog=InventoryDB; Integrated Security=true";
            conn = new SqlConnection(connstr);
            conn.Open();
            string sql = "";

            SqlCommand query;
            SqlDataAdapter adapter = new SqlDataAdapter();
            sql = "Insert into products (product_name,quantity,product_description,product_image) values ('" + prod.ProductName + "','" + prod.Quantity + "','" + prod.ProductDescription + "','" +filePath+ "','" + "')";
            query = new SqlCommand(sql, conn);
            adapter.InsertCommand = new SqlCommand(sql, conn);
            adapter.InsertCommand.ExecuteNonQuery();
            query.Dispose();
            conn.Close();

            return RedirectToAction("Details");
            
        }

        //// GET: User/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: User/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: User/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: User/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

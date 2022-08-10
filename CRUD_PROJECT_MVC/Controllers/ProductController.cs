using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using CRUD_PROJECT_MVC.Models;

namespace CRUD_PROJECT_MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        string connectionstring = @"Data Source=DESKTOP-ISK321A\SQLEXPRESS01;Initial Catalog=MVCCRUDDB;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dbtproduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Product", sqlcon);
                sqlDa.Fill(dbtproduct);
            }
                return View(dbtproduct);
        }


        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()

        {
            return View(new ProductModel());


        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "INSERT INTO Product VALUES(@ProductName,@Price,@Count)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlcmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblproduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "Select * from Product Where ProductID = @productID ";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.SelectCommand.Parameters.AddWithValue("@ProductId", id);
                sqlda.Fill(dtblproduct);

            }
            if (dtblproduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtblproduct.Rows[0][0].ToString());
                productModel.ProductName = dtblproduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblproduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblproduct.Rows[0][3].ToString());
                return View(productModel);

            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Count = @Count Where ProductID = @ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlcmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
                
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionstring))
            {
                sqlcon.Open();
                string query = "DELETE FROM Product Where ProductID = @ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@ProductID",id);
                
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
        
    }

}

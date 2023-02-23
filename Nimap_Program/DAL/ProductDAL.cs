using Microsoft.AspNetCore.Mvc;
using Nimap_Program.Models;
using System.Data.SqlClient;

namespace Nimap_Program.DAL
{
    public class ProductDAL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string str = this.configuration.GetConnectionString("defaultConnection");
            con = new SqlConnection(str);

        }

       
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string qry = "select * from Products";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();

            // var fpath = Environment.WebRootPath;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product prod = new Product();
                    prod.Pid = Convert.ToInt32(dr["Pid"]);
                    
                    prod.Name = dr["Name"].ToString();
                    prod.Price = Convert.ToInt32(dr["Price"]);

                    prod.Categoryid = Convert.ToInt32(dr["Categoryid"]);
                    prod.CategoryName = dr["CategoryName"].ToString();
                    products.Add(prod);


                }
            }
            con.Close();
            return products;

        }


        public int AddProduct(Product product)
        {
            string qry = "insert into Products values(@name,@price,@categoryid,@categoryname)";
            cmd = new SqlCommand(qry, con);
            
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@categoryid", product.Categoryid);
            cmd.Parameters.AddWithValue("@categoryname", product.CategoryName);





            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Product GetProductById(int pid)
        {
            Product prod = new Product();
            string qry = "select * from Products where pid=@pid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@pid", pid);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    prod.Pid = Convert.ToInt32(dr["pid"]);
                    
                    prod.Name = dr["name"].ToString();
                    prod.Price = Convert.ToInt32(dr["price"]);
                    prod.Categoryid = Convert.ToInt32(dr["Categoryid"]);
                    prod.CategoryName = dr["CategoryName"].ToString();



                }

            }
            con.Close();
            return prod;
        }

        public int UpdateProduct(Product pr)
        {
            string qry = "update Products set Categoryid=@categoryid,name=@name,price=@price, CategoryName = @categoryname where pid=@pid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@categoryid", pr.Categoryid);
            cmd.Parameters.AddWithValue("@name", pr.Name);
            cmd.Parameters.AddWithValue("@price", pr.Price);
            cmd.Parameters.AddWithValue("@categoryname", pr.CategoryName);

            //string imgurl = "/content/Image/" + pr.Pro_pic.FileName;

            cmd.Parameters.AddWithValue("@pid", pr.Pid);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteProduct(int pid)
        {
            string qry = "delete from Products where pid = @pid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@pid", pid);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
    }
}

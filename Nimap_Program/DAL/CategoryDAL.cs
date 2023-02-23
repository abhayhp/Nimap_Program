using Nimap_Program.Models;
using System.Data.SqlClient;

namespace Nimap_Program.DAL
{
    public class CategoryDAL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public CategoryDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string str = this.configuration.GetConnectionString("defaultConnection");
            con = new SqlConnection(str);
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //categories.Add(dr["Category"].ToString());

                    Category cat = new Category();
                    cat.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                    categories.Add(cat);
                }


            }
            con.Close();
            return categories;

        }


        public int AddCategory(Category cat)
        {
            string qry = "insert into Category values(@categoryid, @categoryname)";
            cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@categoryid", cat.CategoryID);


            cmd.Parameters.AddWithValue("@categoryname", cat.CategoryName);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;

        }

        public Category GetCategoryById(int catid)
        {
            Category cat = new Category();

            string qry = "select * from Category where CategoryID = @catid";

            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@catid", catid);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cat.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    cat.CategoryName = dr["CategoryName"].ToString();
                }
            }
            con.Close();
            return cat;




        }


        public int UpdateCategory(Category cat)
        {
            string qry = "update Category set CategoryName=@categoryname where CategoryID=@catid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@categoryname", cat.CategoryName);

            cmd.Parameters.AddWithValue("@catid", cat.CategoryID);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int DeleteCategory(int catid)
        {

            string qry = "delete from Category where CategoryID = @catid";

            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@catid", catid);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();
            return result;


        }



    }
}

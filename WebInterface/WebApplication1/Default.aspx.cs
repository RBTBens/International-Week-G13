using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Timer = System.Timers.Timer;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBoxTotal.Text = Total().ToString();
        }
        public double Total()
        {
            SqlConnection conn = new SqlConnection("Data Source=mssql8.unoeuro.com;Initial Catalog=tgaarde_dk_db;Persist Security Info=True;User ID=tgaarde_dk;Password=iotWorkshop");
            conn.Open();
            
            string sqlQuery = "SELECT * FROM products ";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            double total = 0;

            if (reader.HasRows)
            {
                foreach (var item in reader)
                {
                    total = total + (Convert.ToDouble(reader["Price"].ToString()));
                }
            }
            conn.Close();
            return total;
        }
        protected void GridTimer_OnTick(object sender, EventArgs e)
        {
            GridView1.DataBind();
            GridView2.DataBind();
        }
    }
}
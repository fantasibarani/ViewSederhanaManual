using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.NETCOREMANUAL.Models;
using System.Data.SqlClient;
using System.Data;

namespace ASP.NETCOREMANUAL.Controllers
{
    public class HomeController : Controller
    {
        string ConnectionInformation = "Data Source = FANTAR-DV3478; Initial Catalog=asp.net_ManualDB; Integrated Security = true";
        SqlDataReader myReader;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //menggunakan DataTable untuk tempat penyimpanan sementara
            DataTable datashowtampil = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(ConnectionInformation))
            {
                sqlcon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM UserInfo", sqlcon);
                sqlDa.Fill(datashowtampil);

            }
            return View(datashowtampil);

        }
        [HttpPost]
        public IActionResult Index(UserInfo UI)
        {
            /* == ADO Command == */
            SqlConnection MainConnection = new SqlConnection(ConnectionInformation);
            MainConnection.Open();
            string MyCommand = "Insert Into UserInfo(Username) values('" + UI.Username + "')";
            SqlCommand myCommand = new SqlCommand(MyCommand, MainConnection);
            myCommand.ExecuteNonQuery();
            MainConnection.Close();
            return View(UI);
            /* == ADO Command == */
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

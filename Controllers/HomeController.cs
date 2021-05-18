using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PiTempWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PiTempWebApp.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<PiTempData> entries = new List<PiTempData>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = PiTempWebApp.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FectchData();
            return View(entries);
        }

        private void FectchData()
        {
            if(entries.Count > 0)
            {
                entries.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Temperature_F], [Temperature_C], [Humidity], [LogDate] FROM[dbo].[PiTempData] ORDER BY [LogDate] DESC";
                dr = com.ExecuteReader();
                while(dr.Read())
                {
                    entries.Add(new PiTempData()
                    {
                        Temperature_F = dr["Temperature_F"].ToString()
                    ,
                        Temperature_C = dr["Temperature_C"].ToString()
                    ,
                        Humidity = dr["Humidity"].ToString()
                    ,
                        LogDate = dr["LogDate"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

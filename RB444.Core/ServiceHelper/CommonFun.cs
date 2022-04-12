using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using RB444.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RB444.Core.ServiceHelper
{
    public class CommonFun
    {
        private static int defaultPageSize = 5;
        public enum AttachmentsForms
        {
            Country = 1,
            Locations = 2,
            Aircraft = 3,
            Vendor = 4,
            Client = 5,
            Request = 6
        }

        public enum MonthName
        {
            Jan = 1,
            Feb = 2,
            Mar = 3,
            Apr = 4,
            May = 5,
            Jun = 6,
            Jul = 7,
            Aug = 8,
            Sep = 9,
            Oct = 10,
            Nov = 11,
            Dec = 12
        }

        public enum AttachmentsTable
        {
            Countries = 1,
            Locations = 2,
            Aircrafts = 3,
            Vendors = 4,
            Customer = 5,
            Request = 6
        }        

        public static string CalculateFileSize(long bytes)
        {
            string _retrunSize = string.Empty;
            int kb = 0; int mb = 0; int gb = 0;
            int totalBytes = (int)bytes;
            if (totalBytes > 1024) { kb = totalBytes / 1024; _retrunSize = "" + kb + " KB "; } else { return _retrunSize = "" + totalBytes + " Byte "; }
            if (kb > 1024) { mb = kb / 1024; _retrunSize = "" + mb + " MB "; } else { return _retrunSize = "" + kb + " KB "; }
            if (mb > 1024) { gb = mb / 1024; _retrunSize = "" + gb + " GB "; } else { return _retrunSize = "" + mb + " MB "; }
            return _retrunSize;
        }
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public T ConvertToModel<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).FirstOrDefault();
        }
        /// <summary>
        /// Get Datetime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetISTDateTime(DateTime date)
        {
            date = date.AddHours(5);
            date = date.AddMinutes(30);
            return date;
        }

        public static FormDetails GetFormDetails(int formId)
        {
            FormDetails formDetails = null;
            if (formId == (int)AttachmentsForms.Country)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Countries.ToString(),
                    formName = AttachmentsForms.Country.ToString()
                };
            }
            else if (formId == (int)AttachmentsForms.Locations)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Locations.ToString(),
                    formName = AttachmentsForms.Locations.ToString()
                };
            }
            else if (formId == (int)AttachmentsForms.Aircraft)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Aircrafts.ToString(),
                    formName = AttachmentsForms.Aircraft.ToString()
                };
            }
            else if (formId == (int)AttachmentsForms.Vendor)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Vendors.ToString(),
                    formName = AttachmentsForms.Vendor.ToString()
                };
            }
            else if (formId == (int)AttachmentsForms.Client)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Customer.ToString(),
                    formName = AttachmentsForms.Client.ToString()
                };
            }
            else if (formId == (int)AttachmentsForms.Request)
            {
                formDetails = new FormDetails
                {
                    tableName = AttachmentsTable.Request.ToString(),
                    formName = AttachmentsForms.Request.ToString()
                };
            }
            return formDetails;
        }

        public string GetMonthName(int monthId)
        {
            string monthName = string.Empty;
            if (monthId == (int)MonthName.Jan)
                monthName = MonthName.Jan.ToString();
            if (monthId == (int)MonthName.Feb)
                monthName = MonthName.Feb.ToString();
            if (monthId == (int)MonthName.Mar)
                monthName = MonthName.Mar.ToString();
            if (monthId == (int)MonthName.Apr)
                monthName = MonthName.Apr.ToString();
            if (monthId == (int)MonthName.May)
                monthName = MonthName.May.ToString();
            if (monthId == (int)MonthName.Jun)
                monthName = MonthName.Jun.ToString();
            if (monthId == (int)MonthName.Jul)
                monthName = MonthName.Jul.ToString();
            if (monthId == (int)MonthName.Aug)
                monthName = MonthName.Aug.ToString();
            if (monthId == (int)MonthName.Sep)
                monthName = MonthName.Sep.ToString();
            if (monthId == (int)MonthName.Oct)
                monthName = MonthName.Oct.ToString();
            if (monthId == (int)MonthName.Nov)
                monthName = MonthName.Nov.ToString();
            if (monthId == (int)MonthName.Dec)
                monthName = MonthName.Dec.ToString();

            return monthName;
        }

        public static List<ImportanceLevel> GetImportanceLevel()
        {
            var importanceLevelList = new List<ImportanceLevel>();
            var importanceLevel = new ImportanceLevel();

            importanceLevel.id = 1;
            importanceLevel.name = "High";
            importanceLevel.colour_code = "#E64A19";

            importanceLevelList.Add(importanceLevel);

            importanceLevel = new ImportanceLevel();
            importanceLevel.id = 2;
            importanceLevel.name = "Medium";
            importanceLevel.colour_code = "#FFCC01";
            importanceLevelList.Add(importanceLevel);

            importanceLevel = new ImportanceLevel();
            importanceLevel.id = 3;
            importanceLevel.name = "Low";
            importanceLevel.colour_code = "#28a745";
            importanceLevelList.Add(importanceLevel);

            return importanceLevelList;
        }

        public void SetPagination(dynamic model)
        {
            if (object.Equals(model.PageNumber, 0)) model.PageNumber = 1;
            if (object.Equals(model.PageSize, 0)) model.PageSize = defaultPageSize;
            model.Start = ((model.PageNumber - 1) * model.PageSize + 1) - 1;
            model.End = (model.PageNumber * model.PageSize) - model.Start;
        }

        public LocationModel GetIpInfo(string ip)
        {
            // Get IP
            string HostName = Dns.GetHostName();
            var ipaddress = Dns.GetHostAddresses(HostName);
            //var ip = ipaddress.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();

            //var ip = ipaddress.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 && x.IsIPv6LinkLocal == false).FirstOrDefault().ToString();

            //string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(ip))
            //{
            //    ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //}

            //string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(ip))
            //{
            //    //ip = "14.99.119.84";
            //    ip = Request.ServerVariables["REMOTE_ADDR"];
            //}

            // IP API URL
            var Ip_Api_Url = $"http://ip-api.com/json/{ip}";

            LocationModel location = new LocationModel();
            //string url = "https://freegeoip.app/json/{ip}"; // string.Format("https://ipapi.co/{0}/json/", ipAddress);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(Ip_Api_Url);
                //location = new JavaScriptSerializer().Deserialize<LocationModel>(json);
                location = jsonParser.ParsJson<LocationModel>(Convert.ToString(json));
            }

            return location;
        }
    }
}
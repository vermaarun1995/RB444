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

        public enum AttachmentsTable
        {
            Countries = 1,
            Locations = 2,
            Aircrafts = 3,
            Vendors = 4,
            Customer = 5,
            Request = 6
        }

        public async static Task<bool> UploadAtAmazonCDN(Stream FileStream, string FileName, string filepath, string AmazaonCdn_BucketName, IConfiguration configuration)
        {
            try
            {
                if (AmazaonCdn_BucketName == "")
                {
                    AmazaonCdn_BucketName = configuration["AmazonCdnValues:AmazaonCdn_BucketName"];
                    filepath = AmazaonCdn_BucketName + "/" + filepath;
                }
                string accessKeyID = configuration["AmazonCdnValues:AmazaonCdn_KeyID"];
                string secretAccessKeyID = configuration["AmazonCdnValues:AmazaonCdn_AccessKeyID"];
                var credentials = new BasicAWSCredentials(accessKeyID, secretAccessKeyID);
                using (var _S3client = new AmazonS3Client(credentials, RegionEndpoint.USEast1))
                {
                    var request = new PutObjectRequest()
                    {
                        Key = FileName,
                        BucketName = filepath,
                        InputStream = FileStream,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    PutObjectResponse response = await _S3client.PutObjectAsync(request);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async static Task<bool> DownloadObject(int id, int formtype, string fileList, string path, IConfiguration configuration)
        {
            try
            {
                string sDirCreateDate = DateTime.Now.ToString("dd_MM_yy");
                string[] FileNames = fileList.Split(',');
                RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
                IAmazonS3 client = new AmazonS3Client(bucketRegion);

                string accessKey = configuration["AmazonCdnValues:AmazaonCdn_KeyID"];
                string secretKey = configuration["AmazonCdnValues:AmazaonCdn_AccessKeyID"];
                AmazonS3Client s3Client = new AmazonS3Client(new BasicAWSCredentials(accessKey, secretKey), Amazon.RegionEndpoint.USEast1);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                path = path + "/" + sDirCreateDate;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var attachmentFolder = "/Avjet/Attachment/" + GetFormDetails(formtype).formName + "/" + id;
                foreach (var filesName in FileNames)
                {
                    GetObjectRequest request = new GetObjectRequest
                    {
                        BucketName = configuration["AmazonCdnValues:AmazaonCdn_BucketName"] + attachmentFolder,
                        Key = filesName
                    };
                    using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                    {
                        CancellationTokenSource source = new CancellationTokenSource();
                        CancellationToken token = source.Token;
                        string Filedest = path + "/" + filesName;
                        await response.WriteResponseStreamToFileAsync(Filedest, true, token);
                    }
                }
                ////EMR is folder name of the image inside the bucket
                //GetObjectRequest request = new GetObjectRequest();
                //request.BucketName = configuration["AmazonCdnValues:AmazaonCdn_BucketName"] + "/Avjet/Attachment/country/101";
                //request.Key = filename;
                //using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                //{
                //    CancellationTokenSource source = new CancellationTokenSource();
                //    CancellationToken token = source.Token;
                //    string Filedest = path + "/" + filename;
                //    await response.WriteResponseStreamToFileAsync(Filedest, true, token);
                //}
            }
            catch (Exception ex) { return false; }
            return true;
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
    }
}
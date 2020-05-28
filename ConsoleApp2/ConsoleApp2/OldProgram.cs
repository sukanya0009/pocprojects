using CsvHelper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v2.Data.File;

namespace ConsoleApp2
{

    interface ITest
    {
        int Name { get; set; }
        void Show();
        void Hide();
    }

    public abstract class Test : ITest
    {
        public abstract int Name { get; set; }
        public abstract void Show();
        public void Hide() { }
    }
    public class TestChild : Test
    {
        public override int Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public new void Hide() { }
        public override void Show()
        {
            throw new NotImplementedException();
        }
    }
    public interface IFoo
    {
        void Bar();
    }
    public interface IMoo
    {
        void Bar();
    }

    public class Foo : IFoo, IMoo
    {
        public void Bar()
        {
            throw new NotImplementedException();
        }
    }

    class Pet
    {
        Foo fo = new Foo();
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class OldProgram
    {
        static void WriteQuery()
        {

            const string insertQueryTemplate = "insert into VendingPaymentDetail (PaymentMode,TariffType,RechargeAmount,TotalAmount,VendingUserId,VendingToken,DebitReason,CreditReason,CreatedBy,CreatedOn,ModifiedOn,ModifiedBy,IsDeleted,ChequeDate,BankId,ChequeNumber) values ({0} , {1} , {2} , {3} , {4} , {5} , {6} , {7} , {8} , {9} , {10} , {11} , {12}, {13} , {14} , {15}  )",
                            creditReason = "''", debitReason = "''", tariffType = "''";

            const int createdBy = 99, modifiedBy = 99;

            string writePath = @"E:\GridSync\Missing Record CSV\test insert files\insert_hynish" + DateTime.Now.ToString("dd_mm_yyyy_hhmmss") + ".csv",
                    readPath = @"E:\GridSync\Missing Record CSV\test csv\hynish-all.csv";

            string randomTokenNumber = string.Empty;

            using (var reader = new StreamReader(readPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Recharge>().ToList();
                if (records != null && records.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    string str1 = string.Empty;
                    Random rand = new Random();
                    foreach (var row in records)
                    {
                        randomTokenNumber = rand.NextULong(12220005000600070000L, 17888888888888888888L).ToString();
                        str.Append(string.Format(insertQueryTemplate,
                            row.PaymentMode,//"{PaymentMode}", 
                            tariffType,//"{TariffType}",
                            "'" + Encrypt(row.RechargeAmount.ToString()) + "'", //"{RechargeAmount}", 
                            "'" + Encrypt(row.RechargeAmount.ToString()) + "'",//"{TotalAmount}", 
                            row.VendingUserId,//"{VendingUserId}", 
                            "'" + Encrypt(randomTokenNumber.ToString()) + "'",//"{VendingToken}", 
                            debitReason,//"{DebitReason}", 
                            creditReason,//"{CreditReason}", 
                            createdBy,//"{CreatedBy}", 
                            "'" + row.RechargeDate.ToString() + "'",//"{CreatedOn}", 
                            "'" + row.RechargeDate.ToString() + "'",//"{ModifiedOn}", 
                            modifiedBy,//"{ModifiedBy}", 
                            0//"{IsDeleted}"
                            , row.PaymentMode == (int)PaymentType.Cheque ? "'" + row.ChequeDate + "'" : "null"//ChequeDate
                            , row.PaymentMode == (int)PaymentType.Cheque ? row.BankId.ToString() : "null"//BankId
                            , (row.PaymentMode == (int)PaymentType.Cheque ?
                                                   "'" + (string.IsNullOrEmpty(row.ChequeNumber) ?
                                                                rand.Next(333333, 999999).ToString() : row.ChequeNumber) + "'"
                                                    : "null") //ChequeNumber
                            ));

                        str.Append("\n\n");
                    }
                    System.IO.File.WriteAllText(writePath, str.ToString());
                }
            }
            Console.WriteLine("text written");
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private const string templateDocumentId = "10XA__Iu2k9aYUQU3-QLXd7abJWUFJMlutzETy1iUGTw";

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/docs.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = {
                                    //DocsService.Scope.DocumentsReadonly,
                                    DocsService.Scope.Documents,
                                    DocsService.Scope.Drive,
                                    DocsService.Scope.DriveFile
                                 };
        static string ApplicationName = "Google Docs API .NET Quickstart";


        private void test()
        {
            // Create a list of Pet objects.
            List<Pet> pets =
                new List<Pet>{ new Pet { Name="Barley", Age=8 },
                       new Pet { Name="Boots", Age=4 },
                       new Pet { Name="Whiskers", Age=1 } };

            // This query selects only those pets that are 10 or older.
            // In case there are no pets that meet that criteria, call
            // DefaultIfEmpty(). This code passes an (optional) default
            // value to DefaultIfEmpty().
            var oldPets =
                pets.AsQueryable()
                .Where(pet => pet.Age >= 10)
                //.Select(pet => pet.Name)
                .DefaultIfEmpty(new Pet() { Name = "test" })
                .ToArray();

            Console.WriteLine("First query: " + oldPets[0]);
        }
        private static void printAllAnyResults()
        {
            // All
            var list = new List<string>() { "sukanya", "garima", "rachna" }.AsQueryable<string>().All(x => x.Contains("a")); // True
            Console.WriteLine(list);

            list = new List<string>() { "sukanya", "garima", "rachna" }.AsQueryable<string>().All(x => x.Contains("p"));// False
            Console.WriteLine(list);

            list = new List<string>().AsQueryable<string>().All(x => x.Contains("a")); // True
            Console.WriteLine(list);

            // Any
            list = new List<string>() { "sukanya", "garima", "rachna" }.AsQueryable<string>().Any(x => x.Contains("a")); // True
            Console.WriteLine(list);

            list = new List<string>() { "sukanya", "garima", "rachna" }.AsQueryable<string>().Any(x => x.Contains("p"));// False
            Console.WriteLine(list);

            list = new List<string>().AsQueryable<string>().Any(x => x.Contains("a"));// False
            Console.WriteLine(list);
        }
        public void CallDocs()
        {

            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.ReadWrite))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                    ).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Docs API service.
            var service = new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });

            // Define request parameters.
            //String documentId = "195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE";
            //DocumentsResource.GetRequest request = service.Documents.Get(documentId);

            // Prints the title of the requested doc:
            // https://docs.google.com/document/d/195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE/edit
            //Document doc = request.Execute();

            Document newDoc = createDoc(service);



            DriveService driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });

            // Retrieve the existing parents to remove    
            Google.Apis.Drive.v2.FilesResource.GetRequest getRequest = driveService.Files.Get(newDoc.DocumentId);
            //getRequest.Fields = "parents";
            Google.Apis.Drive.v2.Data.File fileNew = getRequest.Execute();

            Google.Apis.Drive.v2.Data.File file = driveService.Files.Get(templateDocumentId).Execute();
            // Fetch file from drive
            //var request = driveService.Files.Get(templateDocumentId);
            //request.Fields = "name,parents";
            //var parent = request.Execute();

            //var newCopiedFile = copyTemplate(driveService, fileNew.Id, file);
            //var item = driveService.Files.Create(file);
            //var newCopiedFile = copyTemplate(driveService, item.F, file);
            //Console.WriteLine("The title of the doc is: {0}", doc.Title);
            //Console.WriteLine("The title of the doc is: {0}", doc22.Title);

            // working
            // copy google doc
            //var newFile = CopyFile(driveService, templateDocumentId, "CopyTitle_" + DateTime.Now.ToString("ddMMyyyy_hhmmss"));

            string replaceText = "1elDcQNW9sZHRsNDz7Ds_TxT8xXgak5JITAPxaSMsVow";
            mergeText(service, replaceText);
        }
        public static File CopyFile(DriveService service, String originFileId, String copyTitle)
        {
            File copiedFile = new File();
            copiedFile.Title = copyTitle;
            try
            {
                return service.Files.Copy(copiedFile, originFileId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            return null;
        }

        private static Document createDoc(DocsService service)
        {
            Document doc = new Document();
            doc.Title = "CopyTitle_" + DateTime.Now.ToLongDateString();
            doc = service.Documents.Create(doc).Execute();
            return doc;
        }
        //private static string UploadFileToDrive(DriveService service, string fileName, string filePath, string conentType)
        //{
        //    var fileMatadata = new Google.Apis.Drive.v3.Data.File();
        //    fileMatadata.Name = fileName;
        //    //fileMatadata.Parents = new List<string> { _folderId };

        //    FilesResource.CreateMediaUpload request;

        //    Google.Apis.Upload.IUploadProgress progress;
        //    using (var stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        request = service.Files.Create(fileMatadata, stream, conentType);
        //        progress = request.Upload();
        //    }

        //    Console.WriteLine(progress.Status);
        //    if (progress.Exception != null)
        //    {
        //        Console.WriteLine(progress.Exception);
        //    }
        //    var file = request.ResponseBody;

        //    return file.Id;
        //}

        private static Google.Apis.Drive.v2.Data.File copyTemplate(DriveService service, string documentId, Google.Apis.Drive.v2.Data.File doc)
        {
            String copyTitle = "CopyTitle_" + DateTime.Now.ToLongDateString();
            Google.Apis.Drive.v2.Data.File copyMetadata = new Google.Apis.Drive.v2.Data.File();
            //copyMetadata.Name = copyTitle;

            //// Retrieve the existing parents to remove    
            //Google.Apis.Drive.v3.FilesResource.GetRequest getRequest = service.Files.Get(documentId);
            //getRequest.Fields = "parents";
            //Google.Apis.Drive.v3.Data.File file = getRequest.Execute();

            Google.Apis.Drive.v2.Data.File documentCopyFile =
                    service.Files.Copy(doc, copyMetadata.Id).Execute();
            String documentCopyId = documentCopyFile.Id;
            return documentCopyFile;
        }

        private static void mergeText(DocsService service, string documentId)
        {
            String customerName = "Alice";
            String date = DateTime.Now.ToString("yyyy/MM/dd");

            List<Request> requests = new List<Request>();
            var customerNameRequest = new Request();
            customerNameRequest.ReplaceAllText = new ReplaceAllTextRequest();
            customerNameRequest.ReplaceAllText.ContainsText = (new SubstringMatchCriteria()
            {
                Text = "{{customer-name}}",
                MatchCase = true
            });
            customerNameRequest.ReplaceAllText.ReplaceText = (customerName);
            requests.Add(customerNameRequest);
            var dateRequest = new Request();
            dateRequest.ReplaceAllText = new ReplaceAllTextRequest();
            dateRequest.ReplaceAllText.ContainsText = (new SubstringMatchCriteria()
            {
                Text = "{{date}}",
                MatchCase = true
            });
            dateRequest.ReplaceAllText.ReplaceText = (date);
            requests.Add(dateRequest);
            BatchUpdateDocumentRequest body = new BatchUpdateDocumentRequest();
            body.Requests = requests;
            service.Documents.BatchUpdate(body, documentId).Execute();

        }
    }
   

    public class Recharge
    {
        public int VendingUserId { get; set; }
        public int RechargeAmount { get; set; }
        public string RechargeDate { get; set; }
        public int PaymentMode { get; set; }
        public string ChequeDate { get; set; }
        public int BankId { get; set; }
        public string ChequeNumber { get; set; }
    }
    public static class Extensions
    {
        //returns a uniformly random ulong between ulong.Min inclusive and ulong.Max inclusive
        public static ulong NextULong(this Random rng)
        {
            byte[] buf = new byte[8];
            rng.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }

        //returns a uniformly random ulong between ulong.Min and Max without modulo bias
        public static ulong NextULong(this Random rng, ulong max, bool inclusiveUpperBound = false)
        {
            return rng.NextULong(ulong.MinValue, max, inclusiveUpperBound);
        }

        //returns a uniformly random ulong between Min and Max without modulo bias
        public static ulong NextULong(this Random rng, ulong min, ulong max, bool inclusiveUpperBound = false)
        {
            ulong range = max - min;

            if (inclusiveUpperBound)
            {
                if (range == ulong.MaxValue)
                {
                    return rng.NextULong();
                }

                range++;
            }

            if (range <= 0)
            {
                throw new ArgumentOutOfRangeException("Max must be greater than min when inclusiveUpperBound is false, and greater than or equal to when true", "max");
            }

            ulong limit = ulong.MaxValue - ulong.MaxValue % range;
            ulong r;
            do
            {
                r = rng.NextULong();
            } while (r > limit);

            return r % range + min;
        }

        //returns a uniformly random long between long.Min inclusive and long.Max inclusive
        public static long NextLong(this Random rng)
        {
            byte[] buf = new byte[8];
            rng.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }

        //returns a uniformly random long between long.Min and Max without modulo bias
        public static long NextLong(this Random rng, long max, bool inclusiveUpperBound = false)
        {
            return rng.NextLong(long.MinValue, max, inclusiveUpperBound);
        }

        //returns a uniformly random long between Min and Max without modulo bias
        public static long NextLong(this Random rng, long min, long max, bool inclusiveUpperBound = false)
        {
            ulong range = (ulong)(max - min);

            if (inclusiveUpperBound)
            {
                if (range == ulong.MaxValue)
                {
                    return rng.NextLong();
                }

                range++;
            }

            if (range <= 0)
            {
                throw new ArgumentOutOfRangeException("Max must be greater than min when inclusiveUpperBound is false, and greater than or equal to when true", "max");
            }

            ulong limit = ulong.MaxValue - ulong.MaxValue % range;
            ulong r;
            do
            {
                r = rng.NextULong();
            } while (r > limit);
            return (long)(r % range + (ulong)min);
        }
    }
}

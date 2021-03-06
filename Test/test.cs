﻿namespace TestSuite
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Data;
    using System.Web.UI;
    using System.Text;
    using System.Xml;
    using Newtonsoft.Json;
using System.Security.Cryptography;
    using Volte.Data.Json;
    using Volte.Utils;

    class TestApp
    {
        static JSONTable oOWXF = new JSONTable();
        static string str;

        static void Test1()
        {
            //load from ToString
            GC.Collect();
            int gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            StreamReader objReader = new StreamReader ("JSONObject1.js");

            str = objReader.ReadToEnd();

            JSONObject _JSONObject = new JSONObject(str);

            StreamWriter swer31 = new StreamWriter ("JSONObject2.js", false);
            swer31.Write(_JSONObject.ToString());
            swer31.Flush();
            swer31.Close();


            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            oOWXF = new JSONTable();
            oOWXF.Parser (str);
            oOWXF.Open();

            JSONObject x1=new JSONObject();
            x1.SetValue("x1",234);
            x1.SetValue("x2",456);

            // oOWXF.Footer.Add(x1);


            Console.WriteLine("Rows  :"+oOWXF.RecordCount);
            Console.WriteLine("Fields:"+oOWXF.Fields.Count);

            StreamWriter swer3 = new StreamWriter ("vdata1.js", false);
            swer3.Write(oOWXF.ToString());
            swer3.Flush();
            swer3.Close();

            Console.WriteLine ("");
            Console.WriteLine ("LoadJson: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

        }

        static void Test2()
        {
            //write to json
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            str = oOWXF.ToString();

            Console.WriteLine ("");
            Console.WriteLine ("write to ToString: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

            StreamWriter swer2 = new StreamWriter ("vdat2.js", false);
            swer2.Write (str);
            swer2.Flush();
            swer2.Close();
        }

        static void Test3()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();


            str = oOWXF.ToString();

            Console.WriteLine ("");
            Console.WriteLine ("write to Xml: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());


        }

        static void Test4()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();


            stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            JSONTable oOWXFxml = new JSONTable();
            //oOWXFxml.LoadXMLString(str);
            //oOWXFxml.Open();

            Console.WriteLine ("");
            Console.WriteLine ("write to Xml: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

        }

        static void Test5()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            JSONTable oOWXF3 = new JSONTable();

            foreach (AttributeMapping _AttributeMapping in oOWXF.Fields) {
                oOWXF3.Declare (_AttributeMapping);
            }

            foreach (string cName in oOWXF.Variable.Names) {

                oOWXF3.Variable[cName] = oOWXF.Variable[cName];
            }

            while (!oOWXF.EOF) {
                oOWXF3.AddNew();

                for (int i = 0; i < oOWXF.Fields.Count; i++) {
                    oOWXF3[i] = oOWXF[i];
                }

                oOWXF3.Update();
                oOWXF.MoveNext();
            }

            oOWXF.Close();

            Console.WriteLine("Rows  :"+oOWXF.RecordCount);
            Console.WriteLine("Fields:"+oOWXF.Fields.Count);

            Console.WriteLine ("");
            Console.WriteLine ("Copy To another: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());


            Console.WriteLine ("Write to vdata3");

            StreamWriter swer3 = new StreamWriter ("vdat3.js", false);
            swer3.Write(oOWXF3.ToString());
            swer3.Flush();
            swer3.Close();

        }

        static void Test6()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            StreamReader _Read = new StreamReader("largedata.js" , Encoding.Default);

            str = _Read.ReadToEnd();

            Console.WriteLine(str.Length);
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            JSONTable oOWXF3 = new JSONTable();
            oOWXF3.Parser(str);
            oOWXF3.Open();

            JSONTable oOWXF4 = new JSONTable();

            foreach (AttributeMapping _AttributeMapping in oOWXF3.Fields) {
                oOWXF4.Declare (_AttributeMapping);
            }

            foreach (string cName in oOWXF3.Variable.Names) {

                oOWXF4.Variable[cName] = oOWXF3.Variable[cName];
            }

            while (!oOWXF3.EOF) {
                oOWXF4.AddNew();

                for (int i = 0; i < oOWXF3.Fields.Count; i++) {
                    oOWXF4[i] = oOWXF3[i];
                }
                oOWXF4.Reference = oOWXF3.Reference;
                oOWXF4.Update();
                oOWXF3.MoveNext();
            }

            Console.WriteLine("Rows  :"+oOWXF3.RecordCount);
            Console.WriteLine("Fields:"+oOWXF3.Fields.Count);

            Console.WriteLine("4Rows  :"+oOWXF4.RecordCount);
            Console.WriteLine("4Fields:"+oOWXF4.Fields.Count);

            Console.WriteLine ("");
            Console.WriteLine ("Copy To another: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

            Console.WriteLine ("Write to vdata3");

            oOWXF3.Flatten = Flatten.Value;

            StreamWriter swer3 = new StreamWriter ("vdat3.js", false);
            swer3.Write(oOWXF3.ToString());
            swer3.Flush();
            swer3.Close();


            swer3 = new StreamWriter ("vdat31.js", false);
            swer3.Write(oOWXF3.ToString());
            swer3.Flush();
            swer3.Close();
           // StreamWriter swer4 = new StreamWriter ("vdat4.js", false);
          //  swer4.Write(oOWXF4.ToString());
          //  swer4.Flush();
          //  swer4.Close();
        }
        static void Test7()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            StreamReader _Read = new StreamReader("largedata.map" , Encoding.Default);

            str = _Read.ReadToEnd();

            Console.WriteLine(str.Length);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            JsonReader reader = new JsonTextReader(new StringReader(str));
            int i=0;
            StreamWriter swer3 = new StreamWriter ("xxvdat3.js", false);
            while (reader.Read())
            {
                i++;
                swer3.Write(reader.Value+"\n");
            //    Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            }
            swer3.Flush();
            swer3.Close();
            Console.WriteLine (i);
            Console.WriteLine ("");
            Console.WriteLine ("Copy To another: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

            Console.WriteLine ("Write to vdata3");

        }
        static void Test8()
        {
            //write to ToString
            GC.Collect();
            int  gc0 = GC.CollectionCount (0);
            int gc1 = GC.CollectionCount (1);
            int gc2 = GC.CollectionCount (2);

            StreamReader _Read = new StreamReader("largedata.map", Encoding.UTF8);

            str = _Read.ReadToEnd();

            JSONObject tt=new JSONObject(str);
            Console.WriteLine(str.Length);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            StreamWriter swer3 = new StreamWriter ("xxvdat3.js", false);
            swer3.Write(tt.ToString());
            swer3.Flush();
            swer3.Close();
            Console.WriteLine ("");
            Console.WriteLine ("Copy To another: " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine ("GC 0:" + (GC.CollectionCount (0) - gc0).ToString());
            Console.WriteLine ("GC 1:" + (GC.CollectionCount (1) - gc1).ToString());
            Console.WriteLine ("GC 2:" + (GC.CollectionCount (2) - gc2).ToString());

            Console.WriteLine ("Write to vdata3");

        }

        static void TestFix()
        {
            JSONTable oOWXF3 = new JSONTable();

            AttributeMapping _ZZFields_CONT_DATA           = new AttributeMapping();
            _ZZFields_CONT_DATA.TableName    = "VARIABLE";
            _ZZFields_CONT_DATA.ColumnName   = "X";
            _ZZFields_CONT_DATA.Name         = "X";
            _ZZFields_CONT_DATA.Caption      = "X";
            _ZZFields_CONT_DATA.Width        = 10;
            _ZZFields_CONT_DATA.Scale        = 2;
            _ZZFields_CONT_DATA.NonPrintable = false;
            _ZZFields_CONT_DATA.TypeChar     = "C";

            oOWXF3.Declare(_ZZFields_CONT_DATA);


            oOWXF3.AddNew();
            oOWXF3.SetValue("X","\"\n");
            oOWXF3.Update();

            oOWXF3.Flatten = Flatten.Value;

            StreamWriter swer3 = new StreamWriter ("TestFix.js", false);
            swer3.Write(oOWXF3.ToString());
            swer3.Flush();
            swer3.Close();

            oOWXF3.Close();

        }

        static void Main()
        {


            //Test1();

            TestFix();

            //Test2();
            //Test3();
            //Test4();
            //Test6();
            //Test7();
            Test8();
            Console.WriteLine(Volte.Utils.Util.ToUnderlineName("ISOCertifiedStaff"));
            Console.WriteLine(Volte.Utils.Util.ToUnderlineName("CertifiedStaff"));
            Console.WriteLine(Volte.Utils.Util.ToUnderlineName("UserID"));
            Console.WriteLine(Volte.Utils.Util.ToCamelCase("iso_certified_staff",1));
            Console.WriteLine(Volte.Utils.Util.ToCamelCase("certified_staff",1));
            Console.WriteLine(Volte.Utils.Util.ToCamelCase("user_id",1));

string password ="1234566wU9HUJyo40400" ;
byte[] hashedDataBytes;
MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password)); 
StringBuilder tmp = new StringBuilder();
foreach (byte i in hashedDataBytes)
{
tmp.Append(i.ToString("x2"));
} 

Console.WriteLine(tmp .ToString());

Console.WriteLine(Volte.Utils.Util.Bytes2Readable(hashedDataBytes));

            Console.ReadLine();
        }
    }
}

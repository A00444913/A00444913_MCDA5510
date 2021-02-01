using System;
using System.IO;
using System.Collections;
using Microsoft.VisualBasic.FileIO;

namespace Assignment1
{
    public class Program
    {


        public static void Main(String[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Console.WriteLine("========================Program Start==="+DateTime.Now+"======================");
            int m = 0;
            Program data_clean = new Program();
            //string path = @"/Users/yangjinting/Projects/Assignment1/Assignment1/Sample Data/2017/11/8/CustomerData1.csv";
            string path2 = @"/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Assignment1/Assignment1/Sample Data/2017/11";
            //data_clean.ReadCsv(path);
            //data_clean.parse(path);
           data_clean.walk(path2);
            foreach(string file in GLobal.file_list_final)
            {
                Console.WriteLine(file);
                data_clean.parse(file);
                //m = m + 1;
                Console.Write("\n");
            }
            //Console.WriteLine("m= " + m);
            Console.WriteLine("=================================summary ======================================");
            Console.WriteLine("all skipped number is "+ GLobal.all_skip);
            Console.WriteLine("all valid number is " + GLobal.all_valid);
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("execution time is " + ts2.TotalMilliseconds+"ms");
        }


        public void parse(String fileName)
        {
            int skipped_row = 0;
            int total_row = 0;
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited; //定义文本分隔符
                    parser.SetDelimiters(",");  //将文本分隔符定义为“，”
                    while (!parser.EndOfData)  //当前光标位置和文件末尾之间没有 非空白 非注释的行
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        foreach (string field in fields)
                        {
                            if (field == "")
                            {
                                skipped_row = skipped_row + 1;
                                break;
                            }
                        }
                        //Console.Write("\n");
                        total_row = total_row + 1;
                    }
                    int valid_row = total_row - skipped_row - 1;
                    //Console.WriteLine("m= " + m);
                    GLobal.all_skip = GLobal.all_skip + skipped_row;
                    GLobal.all_valid = GLobal.all_valid + valid_row;
                    //Console.WriteLine("Total number of skipped row is : " + skipped_row);
                    //Console.WriteLine("Total number of valid row is : " + valid_row);
                }

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
            }

        }


        public void ReadCsv(String fileName)
        {
            try
            {
                // csvファイルを開く
                using (var sr = new System.IO.StreamReader(fileName))
                {
                    // ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        // ファイルから一行読み込む
                        string line = sr.ReadLine();
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        string[] values = line.Split(',');
                        // 出力する
                        for (int i = 0; i < values.Length; i++)
                        {
                            System.Console.Write("{0} ", values[i]);
                        }
                        System.Console.WriteLine();
                    }
                }
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したとき
                System.Console.WriteLine(e.Message);
            }
        }

        public static class GLobal
        {
            public static ArrayList file_list_final=new ArrayList();
            public static int all_skip=0;
            public static int all_valid=0;
        }

        public void walk(String path)
        {
            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach(string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                    //Console.WriteLine("Dir:" + dirpath);
                }
            }
            string[] filelist = Directory.GetFiles(path);
            foreach(string filepath in filelist)
            {
                if(filepath.Contains(".DS_Store"))
                {
                    continue;
                }
                else
                {
                    GLobal.file_list_final.Add(filepath);
                    //Console.WriteLine(filepath);
                }
            }
        }
    }
}


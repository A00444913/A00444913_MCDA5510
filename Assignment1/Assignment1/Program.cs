﻿using System;
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
            Console.WriteLine("========================Program Start===" + DateTime.Now + "======================");
            Program data_clean = new Program();
            //string path = @"/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Assignment1/Assignment1/Sample Data/2017/11/8/CustomerData0.csv";
            string path2 = @"/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Assignment1/Assignment1/Sample Data/";
            //data_clean.parse(path2);
            data_clean.walk(path2);
            foreach(string file in GLobal.file_list_final)
            {
                Console.WriteLine(file);
                data_clean.parse(file);
                Console.Write("\n");
            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("=================================summary ======================================");
            Console.WriteLine("all skipped number is "+ GLobal.all_skip);
            Console.WriteLine("all valid number is " + GLobal.all_valid);
            string a = "execution time is "+ts2.TotalMilliseconds+"ms";
            Console.WriteLine("execution time is " + ts2.TotalMilliseconds+"ms");

            //write logs
            GLobal.WriteLogs("All skipped number is " + GLobal.all_skip);
            GLobal.WriteLogs("All valid number is " + GLobal.all_valid);
            GLobal.WriteLogs("Execution time is " + ts2.TotalMilliseconds + "ms");
        }


        public void parse(String fileName)
        {
            if(fileName is null)
            {
                GLobal.WriteLogs("Open file ERROR: You did not supply a file path");
                return;
            }
            string column = "First Name,Last Name,Street Number,Street,City,Province,Postal code,Country,Phone Number,email Address\r\n";
            string FilePath ="/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Output/test.csv";
            FileStream fs;
            StreamWriter sw;
            if (!File.Exists(FilePath))
            {
                fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(column);
            }
            else
            {
                fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);
            }
            int skipped_row = 0;
            int valid_row = 0;
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
                        if(fields[0]==""||fields[1]==""||fields[2] == ""||fields[3] == ""||fields[4] == ""||fields[5] == ""||fields[6]==""||fields[7]==""||fields[8]==""||fields[9]=="")
                        {
                            skipped_row = skipped_row + 1;
                        }
                        else
                        {
                            if (fields[0] == "First Name")
                            {
                                continue;
                            }
                            else
                            {
                                valid_row = valid_row + 1;
                                string ls_item = "";
                                int cl = fields.Length;
                                for (int i = 0; i < cl; i++)
                                {
                                    if (i == (cl - 1))
                                    {
                                        ls_item += fields[i].ToString() + "\r\n";
                                    }
                                    else
                                    {
                                        ls_item += fields[i].ToString() + ",";
                                    }
                                }
                                sw.Write(ls_item);
                                ls_item = "";
                            }
                        }
                    }
                    sw.Close();
                    fs.Close();
                    GLobal.all_skip = GLobal.all_skip + skipped_row;
                    GLobal.all_valid = GLobal.all_valid + valid_row;
                }

            }
            catch(FileNotFoundException)
            {
                string a = "Exception message: The file or directory cannot be found.";
                GLobal.WriteLogs(a);
            }
            catch (DirectoryNotFoundException)
            {
                GLobal.WriteLogs("Exception message: The file or directory cannot be found.");

            }
            catch (DriveNotFoundException)
            {
                GLobal.WriteLogs("Exception message: The drive specified in 'path' is invalid.");
            }
            catch (PathTooLongException)
            {
                GLobal.WriteLogs("Exception message: 'path' exceeds the maxium supported path length.");
            }
            catch (UnauthorizedAccessException)
            {
                GLobal.WriteLogs("Exception message: You do not have permission to create this file.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                GLobal.WriteLogs("Exception message: There is a sharing violation.");
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                GLobal.WriteLogs("Exception message: The file already exists.");
            }
            catch (IOException e)
            {
                GLobal.WriteLogs($"An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}");
            }


        }


        public static class GLobal
        {
            public static ArrayList file_list_final=new ArrayList();
            public static int all_skip=0;
            public static int all_valid=0;
            public static void WriteLogs(string strLog)
            {
                string sFilePath = "/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Logs";
                string sFileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
                sFileName = sFilePath + "/" + sFileName;
                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }
                FileStream fs;
                StreamWriter sw;
                if (File.Exists(sFileName))
                {
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " --- " + strLog);
                sw.Close();
                fs.Close();
            }
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


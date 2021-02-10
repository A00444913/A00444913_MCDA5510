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
            Console.WriteLine("Program start...");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Program data_clean = new Program();
            string path = @"/Users/yangjinting/Desktop/Sample Data";
            data_clean.Walk(path);
            foreach(string file in GLobal.file_list_final)
            {
                data_clean.Parse(file);
            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;

            //write logs
            GLobal.WriteLogs("All skipped number is " + GLobal.all_skip);
            GLobal.WriteLogs("All valid number is " + GLobal.all_valid);
            GLobal.WriteLogs("Execution time is " + ts2.TotalMilliseconds + "ms");

            Console.WriteLine("Done");
        }


        public void Parse(String fileName)
        {
            if(fileName is null)
            {
                GLobal.WriteLogs("Open file ERROR: You did not supply a file path");
                return;
            }
            string column = "First Name,Last Name,Street Number,Street,City,Province,Postal code,Country,Phone Number,email Address\r\n";
            string FilePath = "/Users/yangjinting/Desktop/git/A00444913_MCDA5510/Assignment1/Output/result.csv";
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
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited; 
                    parser.SetDelimiters(",");  
                    while (!parser.EndOfData) 
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        int l = fields.Length;
                        if(l!=10)
                        {
                            GLobal.all_skip++;
                        }
                        else
                        {
                            if (fields[0] == "" || fields[1] == "" || fields[2] == "" || fields[3] == "" || fields[4] == "" || fields[5] == "" || fields[6] == "" || fields[7] == "" || fields[8] == "" || fields[9] == "")
                            {
                                GLobal.all_skip++;
                            }
                            else
                            {
                                if (fields[0] == "First Name")
                                {
                                    continue;
                                }
                                else
                                {
                                    GLobal.all_valid++;
                                    string ls_item = "";
                                    for (int i = 0; i < l; i++)
                                    {
                                        if (i == (l - 1))
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
                    }
                    sw.Close();
                    fs.Close();
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


        //some global variables and function
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

        public void Walk(String path)
        {
            if (path=="")
            {
                GLobal.WriteLogs("Open file ERROR: You did not supply a file path");
                return;
            }
            try {
                string[] list = Directory.GetDirectories(path);

                if (list == null) return;

                foreach (string dirpath in list)
                {
                    if (Directory.Exists(dirpath))
                    {
                        Walk(dirpath);
                        //Console.WriteLine("Dir:" + dirpath);
                    }
                }
                string[] filelist = Directory.GetFiles(path);
                foreach (string filepath in filelist)
                {
                    if (filepath.Contains(".DS_Store"))
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
            catch (FileNotFoundException)
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
    }
}


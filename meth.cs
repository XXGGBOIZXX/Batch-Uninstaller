using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace METH
{
    public class meth
    {

        public static List<program> plist = new List<program>();
        
        public static string replace(string s)
        {
            if (s == null)
                goto next;
            if (s.Substring(1, 2) == ":\\")
            {
                s = '"' + s.Replace(".exe", ".exe\"");
                goto next;
            }
            s = s.Replace("/I", "/X");
            goto next;

        next:
            return s;
        }
        public static void regkey(RegistryKey k, string root)
        {
            var key = k.OpenSubKey(root);
            int i = 0;
            try
            {
                foreach (var sub in key.GetSubKeyNames())
                {
                    i++;
                    var productKey = key.OpenSubKey(sub);
                    if (productKey != null)
                    {
                        string c = "SystemComponent";
                        var sc = productKey.GetValue(c);
                        if (sc != null)
                        {
                            continue;
                        }

                        string n = "Displayname";
                        String nm;
                        var sn = productKey.GetValue(n);
                        if (sn != null)
                        {
                            nm = Convert.ToString(sn);
                        }
                        else
                            continue;

                        string v = "DisplayVersion";
                        var sv = productKey.GetValue(v);
                        String vr = Convert.ToString(sv);


                        string p = "Publisher";
                        var sp = productKey.GetValue(p);
                        String pb = Convert.ToString(sp);

                        string str;
                        string qu = "QuietUninstallString";
                        var squ = productKey.GetValue(qu);
                        if (squ != null)
                        {
                            String un = Convert.ToString(squ);
                            str = replace(un);
                        }
                        else
                        {

                            String u = "UninstallString";
                            var su = productKey.GetValue(u);
                            if (su == null)
                                continue;
                            else
                            {
                                String un = Convert.ToString(su);
                                str = replace(un);
                            }
                        }
                        program program = new program(nm, vr, pb, str);
                        plist.Add(program);
                    }


                }
                key.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                key.Close();
            }
        }
        public static void listpgr()
        {
            RegistryKey CU = Registry.CurrentUser;
            RegistryKey LM = Registry.LocalMachine;
            regkey(CU, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            regkey(LM, "SOFTWARE\\WOW6432NODE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            regkey(LM, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
        }
        public static void func(String cmd)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/c " + cmd;
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}

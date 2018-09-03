using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RumahSakit.View;
using RumahSakit.Model;

namespace RumahSakit.Controller
{
    class LoginController
    {
        //ADMIN admin = new ADMIN();
        DB_RS_SINGGIHEntities2 et = new DB_RS_SINGGIHEntities2();

        public bool cekLogin(string username, string password)
        {
            bool cek = false;
            var temp = et.ADMINs.FirstOrDefault();
            try
            {
                if (temp.NAME == username && temp.PASSWORD == password)
                {
                    cek = true;
                }
            }
            catch (Exception ex)
            {
                cek = false;
                ex.GetBaseException();
            }
            return cek;
        }

    }
}

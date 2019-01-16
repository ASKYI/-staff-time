using Microsoft.Win32;
using Staff_time.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_time.ViewModel.LoginViewModel
{
    public static class Authorization
    {
        public static RegistryKey stuffTimeSettingsKey;

        public static bool Login()
        {
            var users = Context.usersWork.Read_AllUsers();

            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey softWareKey = currentUserKey.OpenSubKey("SOFTWARE", true);
            RegistryKey stuffTimeKey = softWareKey.CreateSubKey("ChemSoftTimeManager");
            stuffTimeSettingsKey = stuffTimeKey.CreateSubKey("Settings");
            var lastUserID = stuffTimeSettingsKey.GetValue("lastUserID");
            if (lastUserID == null)
                lastUserID = 0;

            var dialog = new View.Dialog.LoginWindow(users, (int)lastUserID);
            bool? isOK = dialog.ShowDialog();
            return isOK == true;
        }

        public static void Logout()
        {
            stuffTimeSettingsKey.SetValue("lastUserID", GlobalInfo.CurrentUser.ID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Staff_time.Model.UserModel
{
    [Serializable]
    public class UserSettings
    {
        public bool IsCollapseTray { get; set; }
        // стандартный конструктор без параметров
        public UserSettings()
        {
        }
        public UserSettings(UserSettings us)
        {
            this.IsCollapseTray = us.IsCollapseTray;
        }
        public UserSettings(bool _isCollapseTray)
        {
            IsCollapseTray = _isCollapseTray;
        }
    }
}

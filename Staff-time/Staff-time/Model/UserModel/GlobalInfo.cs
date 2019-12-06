using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Staff_time.Model.UserModel
{
    public static class GlobalInfo
    {
        private static User _currentUser;
        public static User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                if (_currentUser.Settings == null)
                {
                    UserOptions = new UserSettings();
                    UserOptions.IsCollapseTray = false;
                }
                else
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(UserSettings));
                    using (StringReader textReader = new StringReader(GlobalInfo.CurrentUser.Settings))
                    {
                        UserOptions = (UserSettings)formatter.Deserialize(textReader);
                    }
                }
            }
        }

        private static UserSettings _userOptions;
        public static UserSettings UserOptions
        {
            get
            {
                return _userOptions;
            }
            set
            {
                _userOptions = value;
                XmlSerializer formatter = new XmlSerializer(typeof(UserSettings));
                using (StringWriter textWriter = new StringWriter())
                {
                    formatter.Serialize(textWriter, _userOptions);
                    GlobalInfo.CurrentUser.Settings = textWriter.ToString();
                }
            }
        }
    }
}

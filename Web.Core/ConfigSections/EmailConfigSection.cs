using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Web.Core.ConfigSections
{
    public class EmailSettingRetriever
    {
        public static EmailServiceConfigurationSection config = ConfigurationManager.GetSection("emailSettingRetriever") as EmailServiceConfigurationSection;
        public static MailSettingElementCollection GetMailSettings()
        {
            var config = ConfigurationManager.GetSection("emailSettingRetriever") as EmailServiceConfigurationSection;
            return config.MailSettings;
        }
    }

    public class EmailServiceConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("mailSettings")]
        public MailSettingElementCollection MailSettings
        {
            get { return (MailSettingElementCollection)this["mailSettings"]; }
        }

    }

    [ConfigurationCollection(typeof(MailSettingElement))]
    public class MailSettingElementCollection : ConfigurationElementCollection
    {
        public MailSettingElement this[int index]
        {
            get { return (MailSettingElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }
        public new MailSettingElement this[string key]
        {
            get
            {
                return base.BaseGet(key) as MailSettingElement;
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new MailSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MailSettingElement)element).Name;
        }
    }

    public class MailSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
         [ConfigurationProperty("confirmMailAddress", DefaultValue = "", IsRequired = true)]
        public string ConfirmMailAddress
        {
            get { return (string)this["confirmMailAddress"]; }
            set { this["confirmMailAddress"] = value; }
        }


         [ConfigurationProperty("confirmMailDisplayName", DefaultValue = "", IsRequired = true)]
         public string ConfirmMailDisplayName
         {
             get { return (string)this["confirmMailDisplayName"]; }
             set { this["confirmMailDisplayName"] = value; }
         }

         [ConfigurationProperty("sendAccount", DefaultValue = "", IsRequired = true)]
         public string SendAccount
         {
             get { return (string)this["sendAccount"]; }
             set { this["sendAccount"] = value; }
         }

         [ConfigurationProperty("sendPassword", DefaultValue = "", IsRequired = true)]
         public string SendPassword
         {
             get { return (string)this["sendPassword"]; }
             set { this["sendPassword"] = value; }
         }

    }
}

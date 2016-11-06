using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BSS_WebAPI.Models;
using BSS_DataAccess;

namespace BSS_WebAPI.Utils
{
    public class GlobalSettings
    {
        private static GlobalSettings _instance;

        private GlobalSettings() { }

        public static GlobalSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalSettings();
                }
                return _instance;
            }
        }

        private static BeSmartSportsEntities Context { get; } = new BeSmartSportsEntities();

        public string DefaultCurrency {get;} = Context.GlobalSettings.First(g => g.Name == "Default-Currency").Value;
        public string UserEmailSubject { get; } = Context.GlobalSettings.First(g => g.Name == "User-Email-Subject").Value;
        public string UserEmailBody { get; } = Context.GlobalSettings.First(g => g.Name == "User-Email-Body").Value;
        public string AdminEmailSubject { get; } = Context.GlobalSettings.First(g => g.Name == "Admin-Email-Subject").Value;
        public string AdminEmailBody { get; } = Context.GlobalSettings.First(g => g.Name == "Admin-Email-Body").Value;
        public string SmtpHost        {get;} = Context.GlobalSettings.First(g => g.Name == "Smtp-Host").Value;
        public string SmtpUser        {get;} = Context.GlobalSettings.First(g => g.Name == "Smtp-User").Value;
        public string SmtpPassword    {get;} = Context.GlobalSettings.First(g => g.Name == "Smtp-Password").Value;
        public string SmtpIsSsl       {get;} = Context.GlobalSettings.First(g => g.Name == "Smtp-Is-SSL").Value;
        public string SmtpPort { get; } = Context.GlobalSettings.First(g => g.Name == "Smtp-Port").Value;
        public string Bsb { get; } = Context.GlobalSettings.First(g => g.Name == "BSB").Value;
        public string DbVersion { get; } = Context.GlobalSettings.First(g => g.Name == "DB-Version").Value;
        public string InvalidLoginMessage { get; } = Context.GlobalSettings.First(g => g.Name == "InvalidLogin-Message").Value;

    }
}
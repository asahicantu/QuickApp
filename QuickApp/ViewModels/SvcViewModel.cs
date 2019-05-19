// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================


using DAL.Models;
using System;
using System.Globalization;

namespace QuickApp.ViewModels
{
    public class SvcViewModel : Svc
    {
        private string _stringFormat = "dd/MM/yyyy";
        public string ReadableDate { get { return Date?.ToString(_stringFormat); }  }
        public string NewDate { get; set; }

        public void ProcessModel()
        {
            Date = DateTime.ParseExact(NewDate, _stringFormat, CultureInfo.CurrentCulture);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTC.Models;

namespace ZTC.Controllers
{
    public class BaseController : Controller
    {
        public void Success(string title, string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, title, message, dismissable);
        }

        public void Information(string title, string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, title, message, dismissable);
        }

        public void Warning(string title, string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, title, message, dismissable);
        }

        public void Danger(string title, string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, title, message, dismissable);
        }

        private void AddAlert(string alertStyle, string title, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Title = title,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

    }
}
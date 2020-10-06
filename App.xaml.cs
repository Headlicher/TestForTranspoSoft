using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;

namespace TestForTranspoSoft
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)

        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU"); ;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU"); ;

            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),

              new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

        }
    }
}

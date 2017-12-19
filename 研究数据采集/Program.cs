using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace LSD.Edit.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            IAoInitialize m_AoInitialize = new AoInitializeClass();
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
            if (licenseStatus == esriLicenseStatus.esriLicenseNotInitialized)
            {
                MessageBox.Show("没有esriLicenseProductCodeArcInfo许可！");
                Application.Exit();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
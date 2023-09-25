
using Autodesk.AutoCAD.Interop.Common;
using ConveyorDoc.Business;
using ConveyorDoc.Business.Constants;
using ConveyorDoc.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoCAD = Autodesk.AutoCAD.Interop;

namespace ConveyorDoc.Business.Model
{
    public class DrawingPlotter : IDisposable
    {
        private bool disposedValue = false;

        private Autodesk.AutoCAD.Interop.AcadApplication _application;

        public DrawingPlotter()
        {

            _application = RotManager.GetActiveObject("AutoCAD.Application") as Autodesk.AutoCAD.Interop.AcadApplication;

            if (_application is null)
            {
                _application = new AutoCAD.AcadApplication();
            }
            else
            {
                _application.Documents.Close();
            }

        }


        ~DrawingPlotter()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }


        /// <summary>
        /// Plot's PDF based on DXF
        /// </summary>
        /// <param name="dxf">DXF path</param>
        /// <param name="savePath">Location where pdf will be saved</param>
        public void PlotDXF(string dxf, string savePath)
        {
            
            try
            {
                var acDoc = OpenDXF(_application, dxf);

                if (acDoc != null)
                {
                    SetPlotConfig(acDoc);
                    acDoc.Plot.PlotToFile(savePath);
                    acDoc.Close(false, null);
                }

            }
            catch (Exception)
            {

                _application.Documents.Close();
            }
        }


        /// <summary>
        /// Based on dxf plot PDF for preview
        /// </summary>
        /// <param name="templateTxt">DXF path</param>
        public void PlotPreview(string templateTxt)
        {

            string tempFile = Path.Combine(GeneralConstants.TEMP_FOLDER_DIR, "Previewtemp.dxf");

            File.WriteAllText(tempFile, templateTxt);
            var acDoc = OpenDXF(_application, tempFile);

            if (acDoc != null)
            {
                SetPlotConfig(acDoc);
                acDoc.Plot.PlotToDevice();
                acDoc.Close(false, null);
            }
        }


        /// <summary>
        /// Close all active documents
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);            
            GC.SuppressFinalize(this);
        }

        //private
        private AutoCAD.AcadDocument? OpenDXF(AutoCAD.AcadApplication acApp, string dxf)
        {
            AutoCAD.AcadDocument acDoc = null;
            acApp.Visible = false;
            try
            {
                
                acDoc = acApp.Documents.Open(dxf);


                while (!acApp.GetAcadState().IsQuiescent)
                {
                    Thread.Sleep(25);
                }

                
            }
            catch 
            {
                acApp.Documents.Close();
            }

            return acDoc;

        }

        private void SetPlotConfig(AutoCAD.AcadDocument acDoc)
        {
            acDoc.ActiveLayout.RefreshPlotDeviceInfo();
            acDoc.ActiveLayout.ConfigName = "AutoCAD PDF (High Quality Print).pc3";
            acDoc.ActiveLayout.CanonicalMediaName = "ISO_full_bleed_A4_(297.00_x_210.00_MM)";
            acDoc.ActiveLayout.PlotType = AutoCAD.Common.AcPlotType.acExtents;
            acDoc.ActiveLayout.StandardScale = AutoCAD.Common.AcPlotScale.acScaleToFit;
            acDoc.ActiveLayout.StyleSheet = "monochrome.ctb";
            acDoc.ActiveLayout.PlotWithPlotStyles = true;

            acDoc.SetVariable("BACKGROUNDPLOT", 0);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _application.Documents.Close();                       
                }

                disposedValue = true;
            }
        }

    }
}

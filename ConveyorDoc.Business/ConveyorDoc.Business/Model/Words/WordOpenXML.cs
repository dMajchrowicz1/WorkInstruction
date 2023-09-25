using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConveyorDoc.Business;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml;
using System.IO;
using System.Windows;
using DocumentFormat.OpenXml;
using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Extension;
using ConveyorDoc.Business.Extensions;
using ConveyorDoc.Business.Queries;

namespace ConveyorDoc.Service
{
    public class WordOpenXML : IDisposable
    {


        public WordOpenXML()
        {

            _tempOpenXMLDocPath = Path.Combine(GeneralConstants.TEMP_FOLDER_DIR, "tempWord.docx");

            File.Copy(GeneralConstants.WORD_TEMPLATE_PATH, _tempOpenXMLDocPath, true);

            _wordDoc = WordprocessingDocument.Open(_tempOpenXMLDocPath, true);

            // Change the document type to Document
            _wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document);

            // Get the MainPart of the document
            MainDocumentPart mainPart = _wordDoc.MainDocumentPart;

        }


        ~WordOpenXML()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }


        /// <summary>
        /// Finds all variables in main table
        /// </summary>
        /// <returns>List of found variables</returns>
        public IEnumerable<Variable> FindVariables()
        {
            var result = Enumerable.Empty<Variable>();

            result = GetTaggedRunNodes().ConvertAll(s =>
            {
                return new Variable(s);
            });

            return result;
        }

        /// <summary>
        /// Replace Variables with new values 
        /// </summary>
        /// <param name="vars">List of variables from template</param>
        public void ReplaceVariables(IEnumerable<Variable> vars)
        {
            foreach (var item in GetTaggedRunNodes())
            {
                item.ReplaceChild(new Text { Text = vars.Where(y => y.Default == item.InnerText).First().Value }, item.GetFirstChild<Text>());
            }
        }

        /// <summary>
        /// Add's each fixture to main table as tableRow
        /// </summary>
        /// <param name="fixtures">Fixtures</param>
        public void AddFixtures(IEnumerable<FixtureDto> fixtures)
        {
            //Template main table
            var table = _wordDoc.MainDocumentPart.Document.Body
                .OfType<Table>()
                .FirstOrDefault();
      
            var headerRow = new TableRow(File.ReadAllText(GeneralConstants.FIXTURE_HEADER_TEMPLATE_PATH));
            table.AppendChild(headerRow);


            foreach (var item in fixtures)
            {
                //Row instance
                var row = new TableRow(new TableRowProperties(new TableRowHeight { Val = 350 }));

                //Add number cell to row
                row.Append(CreateCustomCell(AddHyperLink(item.ItemNumber, item.PDF), 3));
                //Add type cell to row
                row.Append(CreateCustomCell(item.ItemType));
                //Add details cell to row
                row.Append(CreateCustomCell(item.Details, 2));

                //Add fixture row to main table
                table.InsertAfter<TableRow>(row, headerRow);

            }

        }


        /// <summary>
        /// Add's each tool to main table as TableRow
        /// </summary>
        /// <param name="tools">Tools</param>
        /// <param name="detailed">Type of Table Row</param>
        public void AddTools(IList<ToolDto> tools, bool detailed)
        {
            //Template main table
            var table = _wordDoc.MainDocumentPart.Document.Body
                .OfType<Table>()
                .FirstOrDefault();

            if (detailed)
            {
                table.AppendChild(new TableRow(File.ReadAllText(GeneralConstants.DETAILED_TOOL_HEADER_PATH)));
                tools.ForEach(x=> table.Append(CreateDetaildToolRow(x)));

            }
            else
            {
                table.AppendChild(new TableRow(File.ReadAllText(GeneralConstants.STANDARD_TOOL_HEADER_PATH)));
                tools.ForEach(x => table.Append(CreateStandardToolRow(x)));
            }

           
        }

        public void AddDescription(string description)
        {
            //Sepecial pargraph in template where rich text will be added
            _wordDoc.MainDocumentPart.Document.Body.RemoveChild<Paragraph>(_wordDoc.MainDocumentPart.Document.Body.OfType<Paragraph>().Last());


            Paragraph paragraph = new Paragraph();
            paragraph.Append(ParseTextForOpenXML(description));

            _wordDoc.MainDocumentPart.Document.Body.AppendChild<Paragraph>(paragraph);

        }

        public void Save(string savePath)
        {
            _wordDoc.Clone(savePath, true).Dispose();           
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _wordDoc.Dispose();
                    if (_tempOpenXMLDocPath is not null)
                    {
                        File.Delete(_tempOpenXMLDocPath);
                    }
                }


                disposedValue = true;
            }
        }


        #region private

        //Fileds
        private bool disposedValue = false;

        private string _tempOpenXMLDocPath;

        private WordprocessingDocument _wordDoc;


        //Methods
        private void ConfigureCellProperties(TableCell cell, int spanCell)
        {
            //Borders
            var border = new Border { Val = BorderValues.Single, Size = 4, Space = 0, Color = "auto" };
            var tableCellBorder = new TableCellBorders
            {
                LeftBorder = new LeftBorder
                {
                    Val = border.Val,
                    Size = border.Size,
                    Space = border.Space,
                    Color = border.Color
                },

                RightBorder = new RightBorder
                {
                    Val = border.Val,
                    Size = border.Size,
                    Space = border.Space,
                    Color = border.Color
                },

                BottomBorder = new BottomBorder
                {
                    Val = border.Val,
                    Size = border.Size,
                    Space = border.Space,
                    Color = border.Color
                },

                TopBorder = new TopBorder
                {
                    Val = border.Val,
                    Size = border.Size,
                    Space = border.Space,
                    Color = border.Color
                }
            };

            cell.TableCellProperties = new TableCellProperties { TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center } };
            cell.TableCellProperties.TableCellBorders = tableCellBorder;
            if (spanCell > 0)
                cell.TableCellProperties.GridSpan = new GridSpan { Val = spanCell };
        }

        private void ConfigureParagraphPropeties(Paragraph paragraph)
        {
            paragraph.ParagraphProperties = new ParagraphProperties
            {
                ParagraphStyleId = new ParagraphStyleId { Val = "Dane" },
                Justification = new Justification { Val = JustificationValues.Center },
            };
        }

        private Paragraph CreateParagraph(string txt)
        {
            var cellParagraph = new Paragraph();
            ConfigureParagraphPropeties(cellParagraph);

            cellParagraph.Append(new Run(new Text(txt.NullToDash())));

            return cellParagraph;
        }

        private  Paragraph CreateParagraph(string[] txtList)
        {
            var cellParagraph = new Paragraph();
            ConfigureParagraphPropeties(cellParagraph);
            foreach (var item in txtList)
            {
                if (item.NotNullOrEmpty())
                {
                    cellParagraph.Append(new Run(new Text(item)));
                }
            }

            return cellParagraph;
        }

        private  Paragraph CreateParagraph(Hyperlink hyperlink)
        {
            var cellParagraph = new Paragraph();
            ConfigureParagraphPropeties(cellParagraph);
            cellParagraph.Append(hyperlink);

            return cellParagraph;

        }

        private TableCell CreateCustomCell(string txt, int spanCell = 0)
        {
            //Init cell and properties
            var cell = new TableCell();
            ConfigureCellProperties(cell, spanCell);


            //Init paragraph and append to cell
            cell.Append(CreateParagraph(txt));

            return cell;
        }

        private TableCell CreateCustomCell(string[] txtList, int spanCell = 0)
        {
            //Init cell and properties
            var cell = new TableCell();
            ConfigureCellProperties(cell, spanCell);


            //Init paragraph and append to cell
            cell.Append(CreateParagraph(txtList));

            return cell;
        }

        private TableCell CreateCustomCell(Hyperlink hyperlink, int spanCell = 0)
        {
            //Init cell and properties
            var cell = new TableCell();
            ConfigureCellProperties(cell, spanCell);


            //Init paragraph and append to cell
            cell.Append(CreateParagraph(hyperlink));

            return cell;
        }

        /// <summary>
        /// Get's all Run nodes from open xml doc where innerText is in tags
        /// </summary>
        /// <returns>List of all found Run nodes</returns>
        private List<Run> GetTaggedRunNodes()
        {
            var runNodes = from table in _wordDoc.MainDocumentPart.Document.Body.Descendants<Table>()
                           from row in table.Descendants<TableRow>()
                           from cell in row.Descendants<TableCell>()
                           from paragraph in cell.Descendants<Paragraph>()
                           where paragraph.InnerText != ""
                           from run in paragraph.Descendants<Run>()
                           where Regex.Match(run.InnerText, RegexPatternsConstants.TAGGED_WORD).Success
                           select run;

            return runNodes.ToList();
        }

        private TableRow CreateStandardToolRow(ToolDto tool)
        {
            //Row instance
            var row = new TableRow(new TableRowProperties(new TableRowHeight { Val = 350 }));
            //Tool Number Cell
            row.Append(CreateCustomCell(AddHyperLink(tool.FullName, tool.PDF),3));
            //Type value Cell
            row.Append(CreateCustomCell(tool.Type));
            //Add details cell to row
            row.Append(CreateCustomCell(tool.Name,2));


            return row;
        }

        private TableRow CreateDetaildToolRow(ToolDto tool)
        {
            //Row instance
            var row = new TableRow(new TableRowProperties(new TableRowHeight { Val = 350 }));


            //Tool Number Cell
            row.Append(CreateCustomCell(AddHyperLink(tool.FullName, tool.PDF)));
            //Z value Cell
            row.Append(CreateCustomCell(tool.Dimensions?.Zvalue));
            //X value Cell
            row.Append(CreateCustomCell(tool.Dimensions?.Xvalue));
            //Type value Cell
            row.Append(CreateCustomCell(tool.Type));
            //Add details cell to row
            row.Append(CreateCustomCell(tool.Parts?.CuttingPart));

            //Tool Item's Cell
            row.Append(CreateCustomCell(new string[]
            {
                tool.Parts.Item2,
                tool.Parts.Item3,
                tool.Parts.Item4,
                tool.Parts.Item5,
                tool.Parts.Item6,
            }));

            return row;
        }

        /// <summary>
        /// Add new Hyperlink relationship to OpenXML word document
        /// </summary>
        /// <param name="txt">Text to be displayed</param>
        /// <param name="url">Url address</param>
        /// <returns>Hyperlink wordDoc element</returns>
        private Hyperlink AddHyperLink(string txt, string url)
        {

            //Url
            if (url != null && url.Length > 2)
            {
                Uri uri = new Uri(url);

                HyperlinkRelationship rel = _wordDoc.MainDocumentPart.AddHyperlinkRelationship(uri, true);


                return new Hyperlink(new Run(new Text(txt)))
                {
                    Id = rel.Id,
                    Tooltip = "Click me"
                };
            }
            else
            {
                return new Hyperlink(new Run(new Text(txt)))
                {
                    Tooltip = "Empty url. Don't click"
                };

            }

        }

        /// <summary>
        /// Parse Ascii text to open xml format
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Open xml Run with all lines of text</returns>
        private Run ParseTextForOpenXML(string text, RunProperties properties = null)
        {
            Run run = new Run();

            run.RunProperties = properties;

            if (text != null)
            {
                string[] newLineArray = { Environment.NewLine, "<br/>", "<br />", "\r\n", "\n", };
                string[] textArray = text.Split(newLineArray, StringSplitOptions.None);

                bool first = true;

                foreach (string line in textArray)
                {
                    if (!first)
                    {
                        run.Append(new Break());
                    }

                    first = false;

                    if (line.Contains("\t"))
                    {
                        run.Append(new TabChar());
                    }

                    run.Append(new Text { Text = line });
                }
            }



            return run;
        }


        /// <summary>
        /// Get's all Run node which contains attribute called default
        /// </summary>
        /// <returns>List od special Run nodes</returns>
        private List<Run> GetSpecialRun()
        {
            var specialRun = from table in _wordDoc.MainDocumentPart.Document.Body.Descendants<Table>()
                             from row in table.Descendants<TableRow>()
                             from cell in row.Descendants<TableCell>()
                             from paragraph in cell.Descendants<Paragraph>()
                             from run in paragraph.Descendants<Run>()
                             from attribute in run.GetAttributes()
                             where attribute.LocalName == "name"
                             select run;

            return specialRun.ToList();
        }





        #endregion
    }





}

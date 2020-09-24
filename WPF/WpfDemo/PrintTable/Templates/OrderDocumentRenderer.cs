using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using WpfDemo.PrintTable.Dtos;

namespace WpfDemo.PrintTable.Templates
{
    class OrderDocumentRenderer : IDocumentRenderer
    {
        public void Render(FlowDocument doc, object data)
        {
            TableRowGroup group = doc.FindName("rowsDetails") as TableRowGroup;
            //引用System.Windows
            System.Windows.Style styleCell = doc.Resources["BorderedCell"] as System.Windows.Style;
            foreach (OrderDetail item in ((OrderMaster)data).OrderDetails)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell(new Paragraph(new Run(item.Sku)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Spec)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Number.ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Unit)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.UnitPrice.ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run((item.Number * item.UnitPrice).ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run((item.Description))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                group.Rows.Add(row);
            }
        }
    }
}

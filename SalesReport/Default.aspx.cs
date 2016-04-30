using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Web.UI.WebControls;

namespace SalesReport
{
    public partial class SalesReportDefault : System.Web.UI.Page
    {
        private List<Transaction> transactions;
        private Table tbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            OleDbConnection cn;
            OleDbCommand cmd;
            OleDbDataReader dr;

            if (IsPostBack == false)
            {
                try
                {
                    this.transactions = new List<Transaction>();
                    //  Get database objects...
                    //  Connect to database and open...
                    cn = new OleDbConnection();

                    if (Request.UserHostAddress.ToString().Equals("::1"))
                    {
                        // Local server...
                        cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vitaliy\Source\Repos\aspnetCS379Site\App_Data\SalesTransactionDB.accdb;Persist Security Info=False;";
                    }
                    else
                    {
                        // Remote server...  (Note difference for older version of Access.)
                        cn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=h:\root\home\valekhnovich-001\www\valekhnovichsite\App_Data\SalesTransactionDB.accdb";
                    }

                    // Create the SQL command...
//                cmd = new OleDbCommand("SELECT * " +
//                                       "FROM ([Customer] c " +
//                                       "    inner join " +
//                                       "[Invoice] i " +
//                                       "    on c.CustNumber = i.CustNumber " +
//                                       " )   inner join " +
//                                       "[LineItemID] ld " +
//                                       "    on i.InvoiceNumber = ld.InvoiceNumber " +
//                                       "where i.Status = 'Open' "
//                                       , cn);
//
//                cn.Open();

                    cmd = new OleDbCommand("SELECT * " +
                                           "FROM " +
                                           "[Customer] c,  [Invoice] i, [LineItemID] ld, [LineItem] li, [Inventory] inv, [Supplier] s " +
                                           "where " +
                                           "c.CustNumber = i.CustNumber " +
                                           "AND i.InvoiceNumber = ld.InvoiceNumber " +
                                           "AND ld.InvoiceNumber = li.LineItemID " +
                                           "AND li.SKU = inv.SKU " +
                                           "AND inv.SupplierID = s.SupplierID " +
                                           "AND i.Status = 'Open' "
                        , cn);

                    cn.Open();

                    // Execute the SQL statement and get the dataset...
                    dr = cmd.ExecuteReader();

                    // Iterate over the dataset, create orders and add to collection...
                    while (dr.Read())
                    {
                        CustomerInfo customerInfo = new CustomerInfo(
                            int.Parse(dr["c.CustNumber"].ToString()),
                            int.Parse(dr["c.Phone"].ToString()),
                            dr["c.Company"].ToString(), 
                            dr["c.Contact"].ToString(),
                            dr["Billing"].ToString(),
                            dr["Shipping"].ToString()
                            );
                        Invoice invoice = new Invoice(
                            int.Parse(dr["i.InvoiceNumber"].ToString()), 
                            dr["Shipped Date"].ToString(), 
                            dr["Order Date"].ToString(),
                            dr["Status"].ToString(),
                            dr["i.CustNumber"].ToString());
                        LineItem lineItem = new LineItem(
                            int.Parse(dr["QuantityOrdered"].ToString()),
                            dr["li.SKU"].ToString(),
                            dr["LineItemID"].ToString());
                        Inventory inventory = new Inventory(
                            int.Parse(dr["QOH"].ToString()),
                            int.Parse(dr["UnitWeight"].ToString()),
                            double.Parse(dr["UnitPrice"].ToString()),
                            dr["inv.SKU"].ToString(),
                            dr["inv.SupplierID"].ToString(),
                            dr["Description"].ToString()
                            );
                        Supplier supplier = new Supplier(
                            dr["s.SupplierID"].ToString(),
                            int.Parse(dr["Phone#"].ToString()),
                            dr["s.Company"].ToString(),
                            dr["s.Contact"].ToString(),
                            dr["Addresses"].ToString()
                            );
                    transactions.Add(new Transaction(customerInfo, invoice, lineItem, inventory, supplier));
                    }

                    dr.Close();
                    cn.Close();
                }
                catch (Exception err)
                {
                    lblStatus.Text = err.Message;
                    return;
                }

            }  //  End !IsPostBack

            try
            {
                //  Restore the orders array from the viewstate...
                if (transactions == null)
                {
                    transactions = (List<Transaction>)ViewState["theOrders"];
                }

                Label lbl;
                int count = 0;
                double total = 0;
                foreach (Transaction transaction in transactions)
                {
                    count++;
                    lbl = new Label();
                    lbl.Text = "ORDER #" + count;
                    lbl.Font.Bold = true;
                    Page.Controls.Add(lbl);

                    tbl = new Table();
                    tbl.BorderStyle = BorderStyle.Solid;
                    tbl.BorderColor = Color.FromArgb(0xBDFCC9);
                    tbl.BackColor = Color.Cornsilk;
                    tbl.EnableViewState = true;
                    transaction.SalesInfo(tbl);
                    Page.Controls.Add(tbl);
                    total += transaction.LineItem.QuantityOrdered*transaction.Inventory.UnitPrice;
                }
                lbl = new Label();
                lbl.Text = "INVOICE SUMMARY";
                lbl.Font.Bold = true;
                Page.Controls.Add(lbl);
                tbl = new Table();
                TableRow row = new TableRow();
                row.Cells.Add(addCell("Order count"));
                row.Cells.Add(addCell("Average Order Amount"));
                row.Cells.Add(addCell("Total Sales"));
                tbl.Rows.Add(row);
                row = new TableRow();
                row.Cells.Add(addCell(count.ToString()));
                row.Cells.Add(addCell("$" + (total/count)));
                row.Cells.Add(addCell("$" + total));
                tbl.Rows.Add(row);
                Page.Controls.Add(tbl);
            }

            catch (Exception err)
            {
                lblStatus.Text = err.Message;
            }
        }

        public List<Transaction> getTransactions()
        {
            return transactions;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                // Serialize the orders array to the viewstate...
                ViewState["theOrders"] = transactions;
            }
            catch (Exception err)
            {
                lblStatus.Text = err.Message;
            }
        }

        protected void Redirect_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CheckOutPage.aspx");
        }

        public static TableCell addCell(String pText)
        {
            TableCell cell = new TableCell();
            cell.BorderStyle = BorderStyle.Groove;
            cell.Text = pText;
            cell.HorizontalAlign = HorizontalAlign.Center;
            return cell;
        }

        [Serializable]
        public class Transaction
        {
            public CustomerInfo CustomerInfo { get; private set; }
            public Invoice Invoice { get; private set; }
            public LineItem LineItem { get; private set; }
            public Inventory Inventory { get; private set; }
            public Supplier Supplier { get; private set; }

            public Transaction(CustomerInfo customerInfo, Invoice invoice, LineItem lineItem, Inventory inventory, Supplier supplier)
            {
                CustomerInfo = customerInfo;
                Invoice = invoice;
                LineItem = lineItem;
                Inventory = inventory;
                Supplier = supplier;
            }

            public void SalesInfo(Table tbl)
            {
                CustomerInfo.Row(tbl);
                Invoice.Row(tbl);
                LineItem.Row(tbl, Inventory);

            }
        }

        [Serializable]
        public class Invoice
        {
            public int InvoiceNum { get; private set; }
            public string ShippingDate { get; private set; }
            public string OrderDate { get; private set; }
            public string Status { get; private set; }
            public string CustNum { get; private set; }

            public Invoice(int invoiceNum, string shippingDate, string orderDate, string status, string custNum)
            {
                InvoiceNum = invoiceNum;
                ShippingDate = shippingDate;
                OrderDate = orderDate;
                Status = status;
                CustNum = custNum;
            }

            public static TableRow RowHeader()
            {
                TableRow row = new TableRow();
                row.Cells.Add(addCell("Invoice Number"));
                row.Cells.Add(addCell("Order Date"));
                row.Cells.Add(addCell("Status"));
                return row;
            }

            public void Row(Table tbl)
            {
                tbl.Rows.Add(RowHeader());
                TableRow row = new TableRow();
                row.Cells.Add(addCell(InvoiceNum.ToString()));
                row.Cells.Add(addCell(OrderDate));
                row.Cells.Add(addCell(Status));
                tbl.Rows.Add(row);
            }
        }

        [Serializable]
        public class LineItem
        {
            public int QuantityOrdered { get; private set; }
            public string SKU { get; private set; }
            public string LineItemID { get; private set; }

            public LineItem(int quantityOrdered, string sku, string lineItemId)
            {
                QuantityOrdered = quantityOrdered;
                SKU = sku;
                LineItemID = lineItemId;
            }

            public static TableRow RowHeader()
            {
                TableRow row = new TableRow();
                row.Cells.Add(addCell("SKU"));
                row.Cells.Add(addCell("Description"));
                row.Cells.Add(addCell("Quantity"));
                row.Cells.Add(addCell("Unit Price"));
                row.Cells.Add(addCell("Extended Price"));
                row.Cells.Add(addCell("Extended Weight"));
                return row;
            }

            public void Row(Table tbl, Inventory inv)
            {
                tbl.Rows.Add(RowHeader());
                TableRow row = new TableRow();
                row.Cells.Add(addCell(SKU));
                row.Cells.Add(addCell(inv.Description));
                row.Cells.Add(addCell(QuantityOrdered.ToString()));
                row.Cells.Add(addCell("$" + inv.UnitPrice.ToString()));
                row.Cells.Add(addCell("$" + (inv.UnitPrice*QuantityOrdered).ToString()));
                row.Cells.Add(addCell((inv.UnitWeight*QuantityOrdered).ToString() + " lbs"));
                tbl.Rows.Add(row);
            }
        }

        [Serializable]
        public class Inventory
        {
            public int QOH { get; private set; }
            public int UnitWeight { get; private set; }
            public double UnitPrice { get; private set; }
            public string SKU { get; private set; }
            public string SupplierID { get; private set; }
            public string Description { get; private set; }

            public Inventory(int qoh, int unitWeight, double unitPrice, string sku, string supplierId, string description)
            {
                QOH = qoh;
                UnitWeight = unitWeight;
                UnitPrice = unitPrice;
                SKU = sku;
                SupplierID = supplierId;
                Description = description;
            }
        }

        [Serializable]
        public class Supplier
        {

            public string SupplierID { get; private set; }
            public int Phone { get; private set; }
            public string Company { get; private set; }
            public string Contact { get; private set; }
            public string Addresses { get; private set; }

            public Supplier(string supplierId, int phone, string company, string contact, string addresses)
            {
                SupplierID = supplierId;
                Phone = phone;
                Company = company;
                Contact = contact;
                Addresses = addresses;
            }


            public TableRow Row()
            {
                TableRow row = new TableRow();
                row.Cells.Add(addCell(SupplierID));
                row.Cells.Add(addCell(Company));
                row.Cells.Add(addCell(Phone.ToString()));
                row.Cells.Add(addCell(Contact));
                row.Cells.Add(addCell(Addresses));
                return row;
            }
        }

        [Serializable]
        public class CustomerInfo
        {
            public int CustNumber { get; private set; }
            public int Phone { get; private set; }
            public string Company { get; private set; }
            public string Contact { get; private set; }
            public string Billing { get; private set; }
            public string Shipping { get; private set; }

            public CustomerInfo(int custNum, int phoneNum, string company, string contact, string billing, string shipping)
            {
                CustNumber = custNum;
                Phone = phoneNum;
                Company = company;
                Contact = contact;
                Billing = billing;
                Shipping = shipping;
            }

            public static TableRow RowHeader()
            {
                TableRow row = new TableRow();
                row.Cells.Add(addCell("Customer Number"));
                row.Cells.Add(addCell("Contact"));
                row.Cells.Add(addCell("Phone #"));
                row.Cells.Add(addCell("Company"));
                row.Cells.Add(addCell("Billing Address"));
                row.Cells.Add(addCell("Shipping Address"));
                return row;
            }

            public TableRow Row(Table tbl)
            {
                tbl.Rows.Add(RowHeader());
                TableRow row = new TableRow();
                row.Cells.Add(addCell(CustNumber.ToString()));
                row.Cells.Add(addCell(Contact));
                row.Cells.Add(addCell(Phone.ToString()));
                row.Cells.Add(addCell(Company));
                row.Cells.Add(addCell(Billing));
                row.Cells.Add(addCell(Shipping));
                tbl.Rows.Add(row);
                return row;
            }


        }
    }
}


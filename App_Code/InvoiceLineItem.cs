using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

[Serializable]
public class InvoiceLineItem
{
    public int CustomerNumber { get; private set; }
    public int OrderNumber { get; private set; }
    public int InvoiceSequenceNum { get; private set; }
    public string SKU { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public double UnitPrice { get; private set; }
    public double Weight { get; private set; }
    public double ShippingCost { get; private set; }
    private const int shippingRate = 2;

    public InvoiceLineItem(int cusNum, int ordNum, int invNum, string sku, string itemDesc, int quantity,
        double unitPrice, double weight)
    {
        CustomerNumber = cusNum;
        OrderNumber = ordNum;
        InvoiceSequenceNum = invNum;
        SKU = sku;
        Description = itemDesc;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Weight = weight;
        ShippingCost = weight*shippingRate;
    }
}
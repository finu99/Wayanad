using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json.Linq;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommFuncs
/// </summary>
public class CommFuncs
{
    Country_DAL cc = new Country_DAL();
    public CommFuncs()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string update_order_job_status(string order_assign_id)
    {


        // Retrieve the total quantity assigned to the order_assign_id
        int totalQuantity = 0;
        string job_type = "";
        string totalQtyQuery = @"SELECT SUM(qty),job_type FROM tbl_order_assigned WHERE id = '" + order_assign_id + "' group by job_type";
        DataSet totalQtyResult = cc.joinselect(totalQtyQuery);
        if (totalQtyResult.Tables.Count > 0 && totalQtyResult.Tables[0].Rows.Count > 0)
        {
            string sumTotal = totalQtyResult.Tables[0].Rows[0][0].ToString();
            job_type = totalQtyResult.Tables[0].Rows[0]["job_type"].ToString();
            int.TryParse(sumTotal, out totalQuantity);

        }
        totalQtyResult.Dispose();

        // Retrieve the total quantity assigned in tbl_order_task
        int assignedQuantity = 0;
        string assignedQtyQuery = @"SELECT SUM(qty) FROM tbl_order_task WHERE order_assign_id = '" + order_assign_id + "'";
        DataSet assignedQtyResult = cc.joinselect(assignedQtyQuery);
        if (assignedQtyResult.Tables.Count > 0 && assignedQtyResult.Tables[0].Rows.Count > 0)
        {
            string sumAssigned = assignedQtyResult.Tables[0].Rows[0][0].ToString();
            int.TryParse(sumAssigned, out assignedQuantity);
        }
        assignedQtyResult.Dispose();

        //order assign  status update starts here
        // Update job status in tbl_order_assigned based on the condition
        string jobUpdateQuery = "";
        if (totalQuantity <= assignedQuantity)
        {
            if (job_type.ToLower() == "packing")
            {
                jobUpdateQuery = @"UPDATE tbl_order_assigned SET packing_status = 'completed' WHERE id = '" + order_assign_id + "'";
            }
            else
            {
                jobUpdateQuery = @"UPDATE tbl_order_assigned SET production_status = 'completed' WHERE id = '" + order_assign_id + "'";
            }
        }
        else
        {
            if (job_type.ToLower() == "packing")
            {
                jobUpdateQuery = @"UPDATE tbl_order_assigned SET packing_status = 'ongoing' WHERE id = '" + order_assign_id + "'";
            }
            else
            {
                jobUpdateQuery = @"UPDATE tbl_order_assigned SET production_status = 'ongoing' WHERE id = '" + order_assign_id + "'";
            }
        }
        int w = cc.Insert(jobUpdateQuery);
        //order assign  status update ends here

        //order items  status update starts here
        // Retrieve the item_id from tbl_order_assigned
        string itemIdQuery = @"SELECT item_id FROM tbl_order_assigned WHERE id = '" + order_assign_id + "'";
        DataSet itemIdResult = cc.joinselect(itemIdQuery);
        if (itemIdResult.Tables.Count > 0 && itemIdResult.Tables[0].Rows.Count > 0)
        {
            string itemId = itemIdResult.Tables[0].Rows[0]["item_id"].ToString();

            if (job_type.ToLower() == "packing")
            {

                itemIdQuery = @"SELECT i.qty total_qty,isnull(sum(oa.qty),0) assigned_qty
FROM tbl_order_items i
left join tbl_order_assigned oa on i.id=oa.item_id
WHERE oa.item_id ='" + itemId + @"' and oa.job_type='packing' 
and (i.production_packing_status='packingonly' or i.production_packing_status='productionandpacking')
and oa.packing_status='completed'
group by i.qty";

               // itemIdQuery = @"SELECT packing_status FROM tbl_order_assigned WHERE item_id = '" + itemId + "' and job_type='packing' and packing_status<>'completed'";
                DataSet itemIdResult1 = cc.joinselect(itemIdQuery);
                if (itemIdResult1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(itemIdResult1.Tables[0].Rows[0]["total_qty"].ToString()) <= Convert.ToInt32(itemIdResult1.Tables[0].Rows[0]["assigned_qty"].ToString()))
                    {
                        //completed
                        jobUpdateQuery = @" UPDATE tbl_order_items SET packing_status = 'completed' WHERE id = '" + itemId + "'";
                    }
                    else
                    {
                        //ongoing
                        jobUpdateQuery = @" UPDATE tbl_order_items SET packing_status = 'ongoing' WHERE id = '" + itemId + "'";
                    }
                   
                }
                else
                {
                    //ongoing
                    jobUpdateQuery = @" UPDATE tbl_order_items SET packing_status = 'ongoing' WHERE id = '" + itemId + "'";
                }
                itemIdResult1.Dispose();
            }
            else
            {
               
                
                
                itemIdQuery = @"SELECT i.qty total_qty,isnull(sum(oa.qty),0) assigned_qty
FROM tbl_order_items i
left join tbl_order_assigned oa on i.id=oa.item_id
WHERE oa.item_id ='" + itemId + @"' and oa.job_type='production' 
and (i.production_packing_status='productiononly' or i.production_packing_status='productionandpacking')
and oa.production_status='completed'
group by i.qty";
                
                //itemIdQuery = @"SELECT production_status FROM tbl_order_assigned WHERE item_id = '" + itemId + "' and job_type='production' and production_status<>'completed'";
                DataSet itemIdResult1 = cc.joinselect(itemIdQuery);
                if (itemIdResult1.Tables[0].Rows.Count>0)
                {
                    if (Convert.ToInt32(itemIdResult1.Tables[0].Rows[0]["total_qty"].ToString()) <= Convert.ToInt32(itemIdResult1.Tables[0].Rows[0]["assigned_qty"].ToString()))
                    {
                        //completed
                        jobUpdateQuery = @" UPDATE tbl_order_items SET production_status = 'completed' WHERE id = '" + itemId + "'";
                    }
                    else
                    {
                        //ongoing
                        jobUpdateQuery = @" UPDATE tbl_order_items SET production_status = 'ongoing' WHERE id = '" + itemId + "'";
                    }
                }
                else
                {
                    //ongoing
                    jobUpdateQuery = @" UPDATE tbl_order_items SET production_status = 'ongoing' WHERE id = '" + itemId + "'";
                    
                }
                itemIdResult1.Dispose();
            }
        }
        int w1 = cc.Insert(jobUpdateQuery);
        //order items  status update ends here

        //order status update starts here
        string qq = @"SELECT order_id FROM tbl_order_items WHERE id in ( SELECT item_id FROM tbl_order_assigned WHERE id = '" + order_assign_id + "')";
        DataSet orderIdResult = cc.joinselect(qq);
        if (orderIdResult.Tables.Count > 0 && orderIdResult.Tables[0].Rows.Count > 0)
        {
            string orderid = orderIdResult.Tables[0].Rows[0]["order_id"].ToString();
            if (job_type.ToLower() == "packing")
            {
                itemIdQuery = @"SELECT packing_status FROM tbl_order_items WHERE order_id = '" + orderid + "' and packing_status<>'completed' and (production_packing_status ='packingonly' or production_packing_status ='productionandpacking' or production_packing_status is null)";
                DataSet orderIdResult1 = cc.joinselect(itemIdQuery);
                if (orderIdResult1.Tables.Count > 0 && orderIdResult1.Tables[0].Rows.Count > 0)
                {
                    //ongoing
                    jobUpdateQuery = @" UPDATE tbl_orders SET packing_status = 'ongoing' WHERE id = '" + orderid + "'";
                }
                else
                {
                    //completed
                    jobUpdateQuery = @" UPDATE tbl_orders SET packing_status = 'completed' WHERE id = '" + orderid + "'";
                }
                orderIdResult1.Dispose();
            }
            else
            {
                itemIdQuery = @"SELECT production_status FROM tbl_order_items WHERE order_id = '" + orderid + "' and production_status<>'completed'  and (production_packing_status ='productiononly' or production_packing_status ='productionandpacking' or production_packing_status is null)";
                DataSet orderIdResult1 = cc.joinselect(itemIdQuery);
                if (orderIdResult1.Tables.Count > 0 && orderIdResult1.Tables[0].Rows.Count > 0)
                {
                    //ongoing
                    jobUpdateQuery = @" UPDATE tbl_orders SET production_status = 'ongoing' WHERE id = '" + orderid + "'";
                }
                else
                {
                    //completed
                    jobUpdateQuery = @" UPDATE tbl_orders SET production_status = 'completed' WHERE id = '" + orderid + "'";
                }
                orderIdResult1.Dispose();
            }
            int jobupdateStatus1 = cc.Insert(jobUpdateQuery);




            //order common status updation code start here
            int pakingonly = 0, productiononly = 0, both = 0;
            string qw = @"select count(production_packing_status) count,production_packing_status from 
tbl_orders o
inner join tbl_order_items i on o.id=i.order_id
where o.id=" + orderid + @"
group by production_packing_status";
            DataSet proDs1 = cc.joinselect(qw);
            if (proDs1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < proDs1.Tables[0].Rows.Count; i++)
                {
                    if (proDs1.Tables[0].Rows[i]["production_packing_status"].ToString() == "productionandpacking")
                    {
                        both = int.Parse(proDs1.Tables[0].Rows[i]["count"].ToString());
                    }
                    else if (proDs1.Tables[0].Rows[i]["production_packing_status"].ToString() == "productiononly")
                    {
                        productiononly = int.Parse(proDs1.Tables[0].Rows[i]["count"].ToString());
                    }
                    else
                    {
                        pakingonly = int.Parse(proDs1.Tables[0].Rows[i]["count"].ToString());
                    }
                }

                string q = "";
                if (both > 0 || (productiononly > 0 && pakingonly > 0))
                {
                    q = @" update tbl_orders set status='completed' where id='" + orderid + @"' and packing_status='completed' and production_status='completed' ";
                }
                else if (productiononly > 0)
                {
                    q = @" update tbl_orders set status='completed' where id='" + orderid + @"' and production_status='completed' ";
                }
                else
                {
                    q = @" update tbl_orders set status='completed' where id='" + orderid + @"' and packing_status='completed' ";
                }

                int e = cc.Insert(q);
            }
            //order common status updation code end here



            if (jobupdateStatus1 > 0)
            {               
                return "success";
            }
            else
            {
                return "failure";
            }
        }
        return "failure";
        //order status update ends here

    }
    //order table status update
    public string update_order_assign_status(string jobType, string item_id, string status)
    {
        string qry = @"SELECT order_id FROM tbl_order_items WHERE id = '" + item_id + "'";
        DataSet qryResult = cc.joinselect(qry);

        string order_id = "";
        if (qryResult.Tables.Count > 0 && qryResult.Tables[0].Rows.Count > 0)
        {
            order_id = qryResult.Tables[0].Rows[0][0].ToString();
        }
        qryResult.Dispose();
        string insertQuery = "";
        if (jobType.ToLower() == "production")
        {
            qry = @"SELECT production_assign_status FROM tbl_order_items WHERE order_id = '" + order_id + "' and production_assign_status<>'completed'";
            DataSet qryResult1 = cc.joinselect(qry);

            string qty = "";
            if (qryResult1.Tables.Count > 0 && qryResult1.Tables[0].Rows.Count > 0)
            {
                //update partial
                insertQuery = @" UPDATE tbl_orders SET production_assign_status = 'partially assigned' WHERE id = '" + order_id + "'";
            }
            else
            {
                //update completed
                insertQuery = @" UPDATE tbl_orders SET production_assign_status = 'completed' WHERE id = '" + order_id + "'";
            }
            qryResult1.Dispose();
        }
        else if (jobType.ToLower() == "packing")
        {
            qry = @"SELECT packing_assign_status FROM tbl_order_items WHERE order_id = '" + order_id + "' and packing_assign_status<>'completed'";
            DataSet qryResult1 = cc.joinselect(qry);
            string qty = "";
            if (qryResult1.Tables.Count > 0 && qryResult1.Tables[0].Rows.Count > 0)
            {
                //update partial
                insertQuery = @" UPDATE tbl_orders SET packing_assign_status = 'partially assigned' WHERE id = '" + order_id + "'";
            }
            else
            {
                //update completed
                insertQuery = @" UPDATE tbl_orders SET packing_assign_status = 'completed' WHERE id = '" + order_id + "'";
            }
            qryResult1.Dispose();
        }
        return insertQuery;
    }

    //  order item table status update
    public string update_order_items_assign_status(string jobType, string item_id, string status)
    {
        //return status;
        string insertQuery = "";
        if (jobType.ToLower() == "production")
        {
            insertQuery = @" UPDATE tbl_order_items SET production_assign_status = '" + status + "' WHERE id = '" + item_id + "'";
        }
        else if (jobType.ToLower() == "packing")
        {
            insertQuery = @" UPDATE tbl_order_items SET packing_assign_status = '" + status + "' WHERE id = '" + item_id + "'";
        }
        return insertQuery;
    }
    public int get_order_item_totalQuantity(string item_id)
    {
        // Query to get the total quantity from tbl_order_items
        string qry = @"SELECT SUM(qty) FROM tbl_order_items WHERE id = '" + item_id + "'";
        DataSet qryResult = cc.joinselect(qry);

        int totalQuantity = 0;
        if (qryResult.Tables.Count > 0 && qryResult.Tables[0].Rows.Count > 0)
        {
            string sum = qryResult.Tables[0].Rows[0][0].ToString();
            int.TryParse(sum, out totalQuantity);
        }
        return totalQuantity;
    }


    public int get_order_assignedQuantity(string item_id, string job_type)
    {
        // Query to get the total assigned quantity from tbl_order_assigned
        string qry2 = @"SELECT SUM(qty) FROM tbl_order_assigned WHERE item_id = '" + item_id + "' and job_type='" + job_type + "'";
        DataSet qryResult2 = cc.joinselect(qry2);

        int assignedQuantity = 0;
        if (qryResult2.Tables.Count > 0 && qryResult2.Tables[0].Rows.Count > 0)
        {
            string sumassigned = qryResult2.Tables[0].Rows[0][0].ToString();
            int.TryParse(sumassigned, out assignedQuantity);
        }
        return assignedQuantity;
    }



    /// get quantity in order task
    public int get_order_assigned_totalQuantity(string order_assign_id)
    {
        // Query to get the total quantity from tbl_order_items
        string qry = @"SELECT SUM(qty),job_type FROM tbl_order_assigned WHERE id = '" + order_assign_id + "' group by job_type";
        DataSet qryResult = cc.joinselect(qry);

        int totalQuantity = 0;
        if (qryResult.Tables.Count > 0 && qryResult.Tables[0].Rows.Count > 0)
        {
            string sum = qryResult.Tables[0].Rows[0][0].ToString();
            int.TryParse(sum, out totalQuantity);
        }
        return totalQuantity;
    }


    public int get_order_taskQuantity(string order_assign_id)
    {
        // Query to get the total assigned quantity from tbl_order_assigned

        string qry2 = @"SELECT SUM(qty) FROM tbl_order_task WHERE order_assign_id = '" + order_assign_id + "'";
        DataSet qryResult2 = cc.joinselect(qry2);

        int assignedQuantity = 0;
        if (qryResult2.Tables.Count > 0 && qryResult2.Tables[0].Rows.Count > 0)
        {
            string sumassigned = qryResult2.Tables[0].Rows[0][0].ToString();
            int.TryParse(sumassigned, out assignedQuantity);
        }
        return assignedQuantity;
    }
    ///


    public int ConvertToInt(Object obj)
        {
            int RetVal = 0;
            try
            {
                if (obj.ToString().Trim() != "")
                    RetVal = int.Parse(obj.ToString());
            }
            catch { }
            return RetVal;
        }
    public Int64 ConvertToInt64(Object obj)
        {
            Int64 RetVal = 0;
            try
            {

                if (obj.ToString().Trim() != "")
                    RetVal = Int64.Parse(obj.ToString());
            }
            catch { }
            return RetVal;
        }


    public double ConvertToNumber_Double(Object obj)
        {
            double RetVal = 0;
            try
            {
                if (obj.ToString().Trim() != "")
                    RetVal = double.Parse(obj.ToString());
            }
            catch { }
            return RetVal;
        }
    public Decimal ConvertToNumber_Dec(Object obj)
        {
            Decimal RetVal = 0;
            try
            {
                if (obj.ToString().Trim() != "")
                    RetVal = Decimal.Parse(obj.ToString());
            }
            catch { }
            return RetVal;
        }
    public String ConvertToString(Object obj)
        {
            String RetVal = "";
            try
            {
                if (String.IsNullOrEmpty(obj.ToString()) == true)
                    RetVal = "";
                else
                    RetVal = obj.ToString();
            }
            catch { RetVal = ""; }
            return RetVal;
        }


    public DateTime FormatDate(String vDate, String vFormat)
    {
        DateTime dt = DateTime.Now;

        try { dt = DateTime.Parse(vDate); }
        catch { }
        try { return DateTime.Parse(dt.ToString(vFormat)); }
        catch { return DateTime.Now; }
    }
    public string generate_tocken()
    {
        // Generate a new GUID
        Guid enc_key = Guid.NewGuid();

        // Format the GUID and add the prefix
        string ordernoString = "tocken" + enc_key.ToString().Replace("-", "").Substring(0, 10);

        // Check if the generated order number exists in the database
        string querry = @"select id from tbl_employees where enc_key='" + ordernoString + "' ";
        DataSet ds = cc.joinselect(querry);

        // If a duplicate is found, generate a new order number
        if (ds.Tables[0].Rows.Count > 0)
        {
            return generate_tocken();
        }

        // Return the unique order number
        return ordernoString;
    }
    public string get_tocken_details(string enc_key)
    {
        string querry = @"select * from tbl_employees where enc_key='" + enc_key + "'";
        DataSet ds = cc.joinselect(querry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToDateTime(ds.Tables[0].Rows[0]["enc_key_date"].ToString()).ToString("dd-MM-yyyy")==DateTime.Now.ToString("dd-MM-yyyy"))
            {
                return ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
                return "Oops! Tocken Expired!";
        }
        else
            return "";
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Attendance_TrainerMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlTrainerList.Items.Insert(0, new ListItem("Trainers Name *", "0"));
        ddltrainingtype.Items.Insert(0, new ListItem("Training Type *", "0"));

    }
}
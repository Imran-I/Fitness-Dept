using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Tools_BMIORBMRORMacroCalc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            btnBMR.Style.Remove("background-color");
            btnBMR.Style.Remove("color");
            btnBMR.Style.Remove("border-top");
            btnBMR.Style.Remove("border-right");
            btnBMR.Style.Remove("border-left");
            btnBMR.Style.Add("border-bottom", "2px solid #c3c4c6");
            btnMacro.Style.Remove("background-color");
            btnMacro.Style.Remove("color");
            btnMacro.Style.Remove("border-top");
            btnMacro.Style.Remove("border-right");
            btnMacro.Style.Remove("border-left");
            btnMacro.Style.Add("border-bottom", "2px solid #c3c4c6");
            btnBMI.Style.Remove("border-bottom");
            btnBMI.Style.Add("border-top-left-radius", "6px");
            btnBMI.Style.Add("border-top-right-radius", "6px");
            btnBMI.Style.Add("border-top", "2px solid #a2a4a796");
            btnBMI.Style.Add("border-right", "2px solid #a2a4a796");
            btnBMI.Style.Add("border-right", "2px solid #a2a4a796");
            btnBMI.Style.Add("background-color", "#37a737");
            btnBMI.Style.Add("color", "#fff");
            btnBMI.Style.Add("border-left", "2px solid #a2a4a796");
            BMIForm.Visible = true;
            BMRForm.Visible = false;
            divMacro.Visible = false;
        }
      
    }

    protected void btnBMI_Click(object sender, EventArgs e)
    {
        divBmi.Visible = true;
        BMIChart.Visible = true;
        btnBMR.Style.Remove("background-color");
        btnBMR.Style.Remove("color");
        btnBMR.Style.Remove("border-top");
        btnBMR.Style.Remove("border-right");
        btnBMR.Style.Remove("border-left");
        btnBMR.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnMacro.Style.Remove("background-color");
        btnMacro.Style.Remove("color");
        btnMacro.Style.Remove("border-top");
        btnMacro.Style.Remove("border-right");
        btnMacro.Style.Remove("border-left");
        btnMacro.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnBMI.Style.Remove("border-bottom");
        btnBMI.Style.Add("border-top-left-radius", "6px");
        btnBMI.Style.Add("border-top-right-radius", "6px");
        btnBMI.Style.Add("border-top", "2px solid #a2a4a796");
        btnBMI.Style.Add("border-right", "2px solid #a2a4a796");
        btnBMI.Style.Add("border-right", "2px solid #a2a4a796");
        btnBMI.Style.Add("background-color", "#37a737");
        btnBMI.Style.Add("color", "#fff");
        btnBMI.Style.Add("border-left", "2px solid #a2a4a796");
        BMIForm.Visible = true;
        BMRForm.Visible = false;
        divMacro.Visible = false;
        clear();
    }

    protected void btnBMR_Click(object sender, EventArgs e)
    {
        divBmi.Visible = false;
        BMIChart.Visible = false;
        btnBMI.Style.Remove("background-color");
        btnBMI.Style.Remove("color");
        btnBMI.Style.Remove("border-top");
        btnBMI.Style.Remove("border-right");
        btnBMI.Style.Remove("border-left");
        btnBMI.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnMacro.Style.Remove("background-color");
        btnMacro.Style.Remove("color");
        btnMacro.Style.Remove("border-top");
        btnMacro.Style.Remove("border-right");
        btnMacro.Style.Remove("border-left");
        btnMacro.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnBMR.Style.Remove("border-bottom");
        btnBMR.Style.Add("border-top-left-radius", "6px");
        btnBMR.Style.Add("border-top-right-radius", "6px");
        btnBMR.Style.Add("border-top", "2px solid #a2a4a796");
        btnBMR.Style.Add("border-right", "2px solid #a2a4a796");
        btnBMR.Style.Add("border-right", "2px solid #a2a4a796");
        btnBMR.Style.Add("background-color", "#37a737");
        btnBMR.Style.Add("color", "#fff");
        btnBMR.Style.Add("border-left", "2px solid #a2a4a796");
        BMIForm.Visible = true;
        BMRForm.Visible = true;
        divMacro.Visible = false;
        clear();
    }

    protected void btnMacro_Click(object sender, EventArgs e)
    {
        btnBMR.Style.Remove("background-color");
        btnBMR.Style.Remove("color");
        btnBMR.Style.Remove("border-top");
        btnBMR.Style.Remove("border-right");
        btnBMR.Style.Remove("border-left");
        btnBMR.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnBMI.Style.Remove("background-color");
        btnBMI.Style.Remove("color");
        btnBMI.Style.Remove("border-top");
        btnBMI.Style.Remove("border-right");
        btnBMI.Style.Remove("border-left");
        btnBMI.Style.Add("border-bottom", "2px solid #c3c4c6");
        btnMacro.Style.Remove("border-bottom");
        btnMacro.Style.Add("border-top-left-radius", "6px");
        btnMacro.Style.Add("border-top-right-radius", "6px");
        btnMacro.Style.Add("border-top", "2px solid #a2a4a796");
        btnMacro.Style.Add("border-right", "2px solid #a2a4a796");
        btnMacro.Style.Add("border-right", "2px solid #a2a4a796");
        btnMacro.Style.Add("background-color", "#37a737");
        btnMacro.Style.Add("color", "#fff");
        btnMacro.Style.Add("border-left", "2px solid #a2a4a796");
        divMacro.Visible = true;
        BMIForm.Visible = false;
        BMRForm.Visible = false;
        clear();
    }
    #region Calculate BMI And BMR and TDEE
    #region Calculate BMI 
    public void CalBMI()
    {
        try
        {
            decimal height = Convert.ToDecimal((txtheight.Text)) / 100;
            decimal BMR = Convert.ToDecimal((txtweight.Text)) / (height * height);
            txtBMI.Text = BMR.ToString("0.00");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Calculate BMR and TDEE
    public void CalBMRandTDEE()
    {
        try
        {
            if (ddlGender.SelectedValue != "0")
            {
                int cal = 0;
                if (ddlGender.SelectedValue == "F")
                {
                    cal = Convert.ToInt32(655 + (Convert.ToDecimal(9.6) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(1.8) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(4.7) * Convert.ToDecimal(txtage.Text)));
                }
                else
                {
                    cal = Convert.ToInt32(66 + (Convert.ToDecimal(13.8) * Convert.ToDecimal(txtweight.Text))
                       + (Convert.ToDecimal(5.0) * Convert.ToDecimal(txtheight.Text))
                       - (Convert.ToDecimal(6.8) * Convert.ToDecimal(txtage.Text)));
                }
                if (ddlWorkOutDetails.SelectedValue != "0")
                {
                    decimal TDEE = Convert.ToInt32(Convert.ToDecimal(cal) * Convert.ToDecimal(ddlWorkOutDetails.SelectedValue));
                    txtBMR.Text = cal.ToString();
                    txtTDEE.Text = TDEE.ToString();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select WorkOut Details');", true);
                    return;
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #region Calculate MAcro 
    public void CalMacro()
    {
        try
        {
            decimal tDEE = Convert.ToDecimal(txtTDEEs.Text);
            int Protein=0;
            int Carbs=0;
            int Fat=0;
            if (ddlDietType.SelectedItem.Text.Trim() == "Muscle Gain")
            {
                Protein = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.35)) / 4);
                Carbs = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.45)) / 4);
                Fat = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.20)) / 9);
            }
            else if (ddlDietType.SelectedItem.Text.Trim() == "Weight Loss")
            {
                Protein = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.50)) / 4);
                Carbs = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.45)) / 4);
                Fat = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.35)) / 9);
            }
            else if (ddlDietType.SelectedItem.Text.Trim() == "Maintenance")
            {
                Protein = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.40)) / 4);
                Carbs = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.55)) / 4);
                Fat = Convert.ToInt32((Convert.ToDecimal(tDEE) * Convert.ToDecimal(0.40)) / 9);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select DietType');", true);
            }
            txtprotein.Text = Protein.ToString();
            txtcarbs.Text = Carbs.ToString();
            txtfats.Text = Fat.ToString();

        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "erroralert('" + ex.ToString().Trim() + "');", true);
        }
    }
    #endregion
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtage.Text != "" && txtheight.Text != "" && ddlGender.SelectedValue != "0" && txtweight.Text != ""
          && ddlWorkOutDetails.SelectedValue != "0")
        {

            CalBMRandTDEE();
            return;
        }
        if (txtweight.Text != "" && txtheight.Text != "")
        {
            CalBMI();
            return;
        }

        if (txtTDEEs.Text != "" && ddlDietType.SelectedValue != "0")
        {
            CalMacro();
            return;
        }
        if (txtheight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Height');", true);
            return;
        }
        if (ddlGender.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Select Gender');", true);
            return;
        }
        if (txtweight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Weight');", true);
            return;
        }
        if (txtage.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "infoalert('Enter Age');", true);
            return;
        }

    }
    public void clear()
    {
        txtprotein.Text = "Your Protein";
        txtcarbs.Text = "Your Carbs";
        txtfats.Text = "Your Fats";
        txtheight.Text = "";
        ddlGender.SelectedValue = "0";
        txtweight.Text = "";
        txtage.Text = "";
        txtBMR.Text = "Your BMR";
        txtBMI.Text = "Your BMI";
        txtTDEE.Text = "Your TDEE";
        txtTDEEs.Text = "";
        ddlWorkOutDetails.SelectedValue = "0";
        ddlDietType.SelectedValue = "0";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();


    }
}
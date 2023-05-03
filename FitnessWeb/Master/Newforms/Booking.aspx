<%@ Page Title="UserEnroll" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Master_Booking_Booking" %>

<asp:Content ID="CntBooking" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .enrollBg {
            background-color: #e9e9e9;
            padding: 1rem;
            border-radius: 2rem;
        }

        .EnrollSummary {
            box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;
            background-color: white;
            border-radius: 1rem;
        }

        .sHead {
            padding: 1rem;
            margin-left: 1rem;
            font-size: 1.7rem;
            font-weight: 900;
            color: black;
            font-style: italic;
        }

        .SummaryHeadSub {
            font-size: 0.8rem;
            font-weight: 700;
            color: #8b8b8b;
            font-style: italic;
        }

        .amountsummary {
            padding: 0.3rem;
            padding-top: 1rem;
            background-color: #e9e9e9;
            margin: 1rem;
            margin-top: 2rem;
            border-radius: 0.7rem;
        }

        .amountsummarysub {
            font-size: 0.8rem;
            font-weight: 900;
        }

        .totalamount {
            font-size: 0.8rem;
            font-weight: 900;
            color: black;
        }

        .SummaryDetails {
            padding: 1rem;
            padding-left: 2rem;
            padding-right: 2rem;
            font-size: 0.6rem;
            margin-top: 3rem;
        }

        .SummaryDetailshead {
            font-size: 0.7rem;
            color: black;
            font-weight: 500;
        }

        .ddlSlotBtn {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 1px solid #b3b3b3;
            padding: 0.4rem;
            border-radius: 1rem;
            color: black;
            background: white;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }
        .ddlSlotBtn:hover {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 1px solid #b3b3b3;
            padding: 0.4rem;
            border-radius: 1rem;
            color: black;
            background: white;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }
        .ddlSlotBtnSelect {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 2px solid #ffffff;
            padding: 0.4rem;
            border-radius: 1rem;
            color: white;
            background: #686565;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }

        .ddlSlotBtnSelect:hover {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 2px solid #ffffff;
            padding: 0.4rem;
            border-radius: 1rem;
            color: white;
            background: #686565;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }

        a {
            color: #000000;
            text-decoration: none;
        }    

        .lblSlotHead {
            margin-left: 0.5rem;
            margin-bottom: 0.5rem;
            font-size: 0.9rem;
            color: black;
            font-weight: 700;
        }

        .ddlSlot{
            overflow: auto;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Enrollment"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="New Enrollment"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div class="PageHeader">
            <h5>New Enrollment</h5>
        </div>
        <div class="row enrollBg">
            <div class="col-12 col-sm-7 col-md-7 col-lg-7 col-xl-7">
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddlCategoryList" AutoPostBack="true" TabIndex="1"
                            OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvClass" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddlCategoryList" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Category">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddltrainingMode"
                            OnSelectedIndexChanged="ddltrainingMode_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"
                            CssClass="form-select" runat="server">
                            <asp:ListItem Value="0">Training Mode *</asp:ListItem>
                            <asp:ListItem Value="D">Direct </asp:ListItem>
                            <asp:ListItem Value="O">Online </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvongym" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddltrainingMode" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Training Mode">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddltraningType" AutoPostBack="true" OnSelectedIndexChanged="ddltraningType_SelectedIndexChanged"
                            TabIndex="3" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvddltraningType" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddltraningType" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Traning Type">
                        </asp:RequiredFieldValidator>

                    </div>
                </div>
                <div id="divSlot" class="row" runat="server" visible="false">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                        <label class="lblSlotHead">Slot List</label>
                        <div class="ddlSlot">
                            <asp:DataList ID="dtlSlot" runat="server"  RepeatDirection="Horizontal" RepeatColumns="6">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFromTime" CssClass="ddlSlotBtn" runat="server" OnClick="lblFromTime_Click" Text='<%#Bind("FromTime") %>'>
                                  
                                </asp:LinkButton>
                                <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="false"> </asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                        </div>
                        


                    </div>
                </div>
                <div class="row">
                    <div id="divtrainer" runat="server" class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddlTrainer"
                            TabIndex="5" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddlTrainer" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Trainer">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtWakeUpTime" AutoComplete="off" CssClass="txtbox timePicker" runat="server" placeholder=" " TabIndex="6"
                                OnTextChanged="txtage_TextChanged" AutoPostBack="true" />
                            <asp:Label CssClass="txtlabel" runat="server">WakeUp Time <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="UserEnroll" ControlToValidate="txtWakeUpTime" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter WakeUp Time">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddlSwapType"
                            TabIndex="5" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddlSwapType" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select SwapType">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtname" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="4" />
                            <asp:Label CssClass="txtlabel" runat="server">Name <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="UserEnroll" ControlToValidate="txtname" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Name">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtmobileNo" AutoComplete="off" MaxLength="10" MinLength="10" TabIndex="7"
                                onKeyPress="return isNumber(event)" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Mobile No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvMno" ValidationGroup="UserEnroll" ControlToValidate="txtmobileNo" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Mobile No.">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <asp:DropDownList ID="ddlGender" CssClass="form-select" runat="server" TabIndex="6"
                            OnSelectedIndexChanged="ddlGender_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">Gender *</asp:ListItem>
                            <asp:ListItem Value="F">Female </asp:ListItem>
                            <asp:ListItem Value="M">Male </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddlGender" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Gender">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtDOB" OnTextChanged="txtDOB_TextChanged" AutoPostBack="true" AutoComplete="off" ClientIDMode="Static" CssClass="txtbox fromDate" runat="server" TabIndex="8" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Date Of Birth <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvDOB" ClientIDMode="Static" ValidationGroup="UserEnroll" ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Date Of Birth">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtage" Enabled="false" AutoComplete="off" ClientIDMode="Static" CssClass="txtbox" runat="server" placeholder=" " TabIndex="5" MaxLength="3"
                                onkeypress="return isNumber(event);"
                                OnTextChanged="txtage_TextChanged" AutoPostBack="true" />
                            <asp:Label CssClass="txtlabel" runat="server">Age <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="Rfvage" ValidationGroup="UserEnroll" ControlToValidate="txtage" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Age">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtJoinDate" AutoComplete="off" CssClass="txtbox fromDate" runat="server" placeholder=" " TabIndex="9" />
                            <asp:Label CssClass="txtlabel" runat="server">Join Date <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvDOJ" ValidationGroup="UserEnroll" ControlToValidate="txtJoinDate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Join Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">

                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtweight" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="10"
                                onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                                OnTextChanged="txtweight_TextChanged" AutoPostBack="true" />
                            <asp:Label CssClass="txtlabel" runat="server">Weight in Kg <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="Rfvweight" ValidationGroup="UserEnroll" ControlToValidate="txtweight" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Weight">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtheight" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="11"
                                onkeypress="return AllowOnlyAmountAndDot(this.id);" MaxLength="4"
                                OnTextChanged="txtheight_TextChanged" AutoPostBack="true" />
                            <asp:Label CssClass="txtlabel" runat="server">Height in cms <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="Rfvheight" ValidationGroup="UserEnroll" ControlToValidate="txtheight" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Height">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtfat" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="12"
                                onkeyup="this.value = minmax(this.value, 0, 100);" onkeypress="return isNumber(event);"
                                MaxLength="3" />
                            <asp:Label CssClass="txtlabel" runat="server">Fat % <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvFat" ValidationGroup="UserEnroll" ControlToValidate="txtfat" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Fat">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                        <asp:DropDownList ID="ddlWorkOutDetails" CssClass="form-select" runat="server" TabIndex="13"
                            OnSelectedIndexChanged="ddlWorkOutDetails_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Value="0">WorkOut Details *</asp:ListItem>
                            <asp:ListItem Value="1.2">I Don't Workout</asp:ListItem>
                            <asp:ListItem Value="1.375">1-5 times/week </asp:ListItem>
                            <asp:ListItem Value="1.55">3-7 times/week  </asp:ListItem>
                            <asp:ListItem Value="1.9">7+ times/week </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="UserEnroll"
                            ControlToValidate="ddlWorkOutDetails" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select ddlWorkOutDetails">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtBMI" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="14" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">BMI <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvBMI" ValidationGroup="UserEnroll" ControlToValidate="txtBMI" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter BMI">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtBMR" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="15" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">BMR <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvBMR" ValidationGroup="UserEnroll" ControlToValidate="txtBMR" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter BMR">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtTDEE" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="16" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">TDEE <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvTDEE" ValidationGroup="UserEnroll" ControlToValidate="txtTDEE" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter TDEE">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlPaymentType" CssClass="form-select" runat="server" TabIndex="17">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvPaymentType" InitialValue="0" ValidationGroup="UserEnroll" ControlToValidate="ddlPaymentType" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Payment  Type">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5">
                <div class="row EnrollSummary">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                        <div class="sHead">
                            <asp:Label ID="SummaryHead" runat="server"></asp:Label>
                            <div class="SummaryHeadSub">
                                <asp:Label ID="SummaryHeadSub" runat="server">Transformation Pack</asp:Label>
                            </div>
                        </div>
                        <div class="row amountsummary">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                                        <div class="float-start">
                                            <asp:Label CssClass="amountsummarysub" runat="server">Total</asp:Label><br />
                                            <br />
                                            <asp:Label CssClass="amountsummarysub" runat="server">Price Amount</asp:Label><br />
                                            <br />
                                            <asp:Label CssClass="amountsummarysub" runat="server">Tax Amount (CGST & SGST)</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                                    <div class="float-end">
                                        ₹
                                        <asp:Label ID="lblTotalAmt" CssClass="amountsummarysub" runat="server"></asp:Label><br />
                                        <br />
                                        ₹ 
                                        <asp:Label ID="lblPriceAmt" CssClass="amountsummarysub" runat="server"></asp:Label><br />
                                        <br />
                                        ₹ 
                                        <asp:Label ID="lblTaxamt" CssClass="amountsummarysub" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row mb-3">
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                    <div class="totalamount float-start">
                                        <asp:Label runat="server">Total Payable</asp:Label>
                                    </div>
                                </div>
                                <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                    <div class="totalamount float-end">
                                        <asp:Label ID="lblFinalamt" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row SummaryDetails">
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <asp:Label CssClass="SummaryDetailshead" runat="server">What else you get</asp:Label>
                                <p id="lblCategoryBenefit" runat="server"></p>
                            </div>
                            <div class="col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                                <asp:Label CssClass="SummaryDetailshead" runat="server">How it Works</asp:Label>
                                <p id="lblCategoryBenefit1" runat="server"></p>
                            </div>
                        </div>
                        <div class="text-center mb-5">
                            <asp:Button runat="server" ID="btnSumbit" ValidationGroup="UserEnroll" OnClick="btnSumbit_Click" TabIndex="18" CssClass="btnSubmit" Text="Enroll Now" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfbranchName" runat="server" />
    <asp:HiddenField ID="hfplanDurationId" runat="server" />
    <asp:HiddenField ID="hftaxId" runat="server" />
    <asp:HiddenField ID="hftaxName" runat="server" />
    <asp:HiddenField ID="hfpriceId" runat="server" />
    <asp:HiddenField ID="hftrainingTypeId" runat="server" />
</asp:Content>


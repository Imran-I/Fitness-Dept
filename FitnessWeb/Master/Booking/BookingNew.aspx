<%@ Page Title="User Enroll" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="BookingNew.aspx.cs" Inherits="Master_Booking_BookingNew" %>

<asp:Content ID="CntBooking1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            font-size: 1.4rem;
            font-weight: 900;
            color: black;
            font-style: italic;
        }

        .SummaryHeadSub {
            font-size: 0.7rem;
            font-weight: 700;
            color: #8b8b8b;
            font-style: normal;
        }

        .amountsummary {
            padding: 0.3rem;
            padding-top: 1rem;
            background-color: #e9e9e9;
            margin: 1rem;
            border-radius: 0.7rem;
        }

        .amountsummarysub {
            font-size: 0.7rem;
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
        }

        .SummaryDetailshead {
            font-size: 0.7rem;
            color: black;
            font-weight: 500;
        }

        .offerclick {
            color: #ffffff;
            font-weight: 900;
            font-size: 0.8rem;
            background-color: #5a0505;
            padding: 0.3rem;
            border-radius: 0.7rem;
        }

            .offerclick:hover {
                color: #ffffff !important;
                background-color: #000000 !important;
                padding: 0.3rem;
                border-radius: 0.7rem;
            }

        .lblofferList {
            color: #000000;
            font-weight: 900;
            font-size: 1rem;
        }

        .lbloffhead {
            font-size: 1.6rem;
            font-weight: 900;
            color: red;
        }

        .lbloff {
            font-size: 1rem;
            font-weight: 900;
            color: #640000;
            text-align-last: center;
        }

        .lbloffsub {
            font-size: 0.8rem;
            font-weight: 900;
            color: #5a5151;
            text-align-last: center;
        }

        .cardoffer {
            box-shadow: rgba(17, 17, 26, 0.1) 0px 8px 24px, rgba(17, 17, 26, 0.1) 0px 16px 56px, rgba(17, 17, 26, 0.1) 0px 24px 80px;
            transition: 0.3s;
            width: 100%;
            height: auto;
            border-radius: 1.25rem;
            background: #fff;
            display: flex;
            flex-direction: column;
            padding: 1rem;
            margin: 10px;
        }

        .lnkremove {
            color: red;
            font-size: 0.7rem;
            margin: 1rem;
            font-weight: 900;
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
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <asp:DropDownList ID="ddlCategoryList" AutoPostBack="true" TabIndex="1"
                            OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvClass" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddlCategoryList" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Category">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
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
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                        <asp:DropDownList ID="ddltraningType" AutoPostBack="true" OnSelectedIndexChanged="ddltraningType_SelectedIndexChanged"
                            TabIndex="3" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RfvddltraningType" InitialValue="0"
                            ValidationGroup="UserEnroll" ControlToValidate="ddltraningType" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Traning Type">
                        </asp:RequiredFieldValidator>

                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtname" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="4" />
                            <asp:Label CssClass="txtlabel" runat="server">Name <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="rfvName" ValidationGroup="UserEnroll" ControlToValidate="txtname" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Name">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtage" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="5" MaxLength="3"
                                onkeypress="return isNumber(event);"
                                OnTextChanged="txtage_TextChanged" AutoPostBack="true" />
                            <asp:Label CssClass="txtlabel" runat="server">Age <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="Rfvage" ValidationGroup="UserEnroll" ControlToValidate="txtage" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Age">
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
                            <asp:TextBox ID="txtmobileNo" AutoComplete="off" MaxLength="10" MinLength="10" TabIndex="7"
                                onKeyPress="return isNumber(event)" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Mobile No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvMno" ValidationGroup="UserEnroll" ControlToValidate="txtmobileNo" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Mobile No.">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtDOB" AutoComplete="off" CssClass="txtbox fromDate" runat="server" TabIndex="8" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Date Of Birth <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvDOB" ValidationGroup="UserEnroll" ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Date Of Birth">
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
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtBMI" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="14" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">BMI <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvBMI" ValidationGroup="UserEnroll" ControlToValidate="txtBMI" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter BMI">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtBMR" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="15" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">BMR <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvBMR" ValidationGroup="UserEnroll" ControlToValidate="txtBMR" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter BMR">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtTDEE" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="16" Enabled="false" />
                            <asp:Label CssClass="txtlabel" runat="server">TDEE <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvTDEE" ValidationGroup="UserEnroll" ControlToValidate="txtTDEE" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter TDEE">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
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
                                            <asp:Label CssClass="amountsummarysub" runat="server">Tax Amount</asp:Label>
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
                                    <div class="totalamount float-end"> ₹ 
                                        <asp:Label ID="lblFinalamt" runat="server"></asp:Label>
                                        <asp:Label ID="lblOffertotal" runat="server"></asp:Label>
                                        <asp:LinkButton ID="lnkRemove" CssClass="lnkremove"
                                            OnClick="lnkRemove_Click" Visible="false"
                                            runat="server">Remove <i class="fa-solid fa-delete-left"></i></asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="row mb-3">
                                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                    <asp:LinkButton ID="lnkOffer" Visible="false" runat="server" CssClass="offerclick float-end"  
                                        OnClick="lnkOffer_Click">Apply Offer</asp:LinkButton>
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
                        <div class="text-center mb-2">
                            <asp:Button runat="server" ID="btnSumbit" OnClick="btnSumbit_Click" TabIndex="18" CssClass="btnSubmit" Text="Enroll Now" />
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

    <div id="divOffer" runat="server" class="DisplyCard" visible="false">
        <div class="CtgryDisplyCardPostion table-responsive">
            <div>
                <div class="PageRoute">
                    <div>
                        <asp:Label ID="lbloffer" CssClass="lblofferList" runat="server" Text="Offer List"></asp:Label>
                    </div>
                </div>
                <asp:DataList ID="dlOfferDetails" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                    Visible="true">
                    <ItemTemplate>
                        <div id="divddlOfferList" runat="server">
                            <div class="cardoffer">
                                <div class="row">
                                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                                        <asp:LinkButton align="left" ID="lblofferHeading" CssClass="lbloff"
                                            OnClick="lblofferHeading_Click" runat="server"
                                            Text='<%# Bind("offerHeading")  %>'></asp:LinkButton>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" CssClass="lbloffsub" Text='<%# Eval("offerType").ToString() == "F" ? "₹":"" %>'></asp:Label>
                                        <asp:Label align="left" ID="lblOfferValue" runat="server"
                                            Text='<%# Bind("offerValue") %>' CssClass="lbloffsub">                                   
                                        </asp:Label><asp:Label ID="lblRs" runat="server" CssClass="lbloffsub" Text='<%# Eval("offerType").ToString() == "P" ? "%":"" %>'></asp:Label>
                                        <asp:Label align="left" ID="lblOfferType" runat="server"
                                            Text='<%# Bind("offerType") %>' Font-Bold="true" Visible="false"></asp:Label>
                                        <asp:Label align="left" ID="lblOfferId" runat="server"
                                            Text='<%# Bind("offerId") %>' Font-Bold="true" Visible="false"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <table style="height: 4px; width: 25px;">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </SeparatorTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>


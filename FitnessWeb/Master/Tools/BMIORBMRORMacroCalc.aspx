<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="BMIORBMRORMacroCalc.aspx.cs" Inherits="Master_Tools_BMIORBMRORMacroCalc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .btnss {
            padding: 5px 50px;
            border: none;
            cursor: pointer;
            flex-grow: 1;
            z-index: 100;
            transition: all 0.5s;
            background-color: #fafafa;
        }
    </style>
    <style>
        table, th, td {
            border: 1px solid black;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Tools"></asp:Label>
            <i class="fafaicon">/</i>

            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="BMI / BMR / Macro Calculator"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">

        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>BMI / BMR / Macro Calculator</h5>

            </div>
            <div class="justify-content-md-start" style="display: flex" id="divAddMasters" runat="server" visible="true">
                <div class="mr-3">
                    <asp:Button
                        ID="btnBMI"
                        runat="server"
                        Text="BMI"
                        CausesValidation="false"
                        TabIndex="1"
                        OnClick="btnBMI_Click"
                        CssClass="btnss" />
                </div>
                <div class="mr-3">
                    <asp:Button
                        ID="btnBMR"
                        runat="server"
                        Text="BMR"
                        TabIndex="2"
                        OnClick="btnBMR_Click"
                        CausesValidation="false"
                        CssClass="btnss" />
                </div>
                <div class="mr-3">
                    <asp:Button
                        ID="btnMacro"
                        runat="server"
                        Text="Macro"
                        TabIndex="3"
                        OnClick="btnMacro_Click"
                        CausesValidation="false"
                        CssClass="btnss" />
                </div>

            </div>

            <div id="divDietPlan" runat="server" style="margin-top: 15px">
                <div class="ddl">
                    <div class="row">
                        <div id="BMIForm" runat="server">

                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtweight" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Weight in Kg <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="Rfvweight" ValidationGroup="Tool"
                                    ControlToValidate="txtweight" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Weight">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtheight" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Height in cms <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="Rfvheight" ValidationGroup="Tool" ControlToValidate="txtheight" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Height">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div id="divBmi" runat="server" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label1" runat="server" Text="BMI" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtBMI" runat="server" Text="Your BMI" Style="color: #1a33e4"></asp:Label>

                            </div>
                            <table id="BMIChart" style="width:50%" runat="server" border="1"  >

                                <tr>
                                    <th>S.No
                                    </th>
                                    <th>BMI
                                    </th>
                                    <th>Weight Range
                                    </th>
                                </tr>
                                <tr>
                                    <td>1
                                    </td>
                                    <td>less than 18.5

                                    </td>
                                    <td>Under Weight
                                    </td>
                                </tr>
                                <tr>
                                    <td>2
                                    </td>
                                    <td>18.5 to 24.9

                                    </td>
                                    <td>Healthy Weight
                                    </td>
                                </tr>
                                <tr>
                                    <td>3
                                    </td>
                                    <td>25.0 to 29.9

                                    </td>
                                    <td>Over Weight 
                                    </td>
                                </tr>
                                <tr>
                                    <td>4
                                    </td>
                                    <td>30.0 or higher

                                    </td>
                                    <td>Obese 
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="BMRForm" runat="server">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtfat" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Fat % <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="RfvFat" ValidationGroup="Tool" ControlToValidate="txtfat" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Fat">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtage" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Age <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="Rfvage" ValidationGroup="Tool" ControlToValidate="txtage" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Age">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <asp:DropDownList ID="ddlGender" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="0">Gender *</asp:ListItem>
                                    <asp:ListItem Value="F">Female </asp:ListItem>
                                    <asp:ListItem Value="M">Male </asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0"
                                    ValidationGroup="Tool" ControlToValidate="ddlGender" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Gender">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <asp:DropDownList ID="ddlWorkOutDetails" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="0">Exercise *</asp:ListItem>
                                    <asp:ListItem Value="1.2">I Don't Workout</asp:ListItem>
                                    <asp:ListItem Value="1.375">1-5 times/week </asp:ListItem>
                                    <asp:ListItem Value="1.55">3-7 times/week  </asp:ListItem>
                                    <asp:ListItem Value="1.9">7+ times/week </asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="Tool"
                                    ControlToValidate="ddlWorkOutDetails" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select ddlWorkOutDetails">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label2" runat="server" Text="BMR" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtBMR" runat="server" Text="Your BMR"></asp:Label>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label4" runat="server" Text="TDEE" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtTDEE" runat="server" Text="Your TDEE"></asp:Label>
                            </div>

                        </div>
                        <div id="divMacro" runat="server">

                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtTDEEs" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">TDEE (kcal) <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Tool
                                    "
                                    ControlToValidate="txtTDEEs" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter TDEE">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                                <asp:DropDownList ID="ddlDietType" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="0">Diet Type *</asp:ListItem>
                                    <asp:ListItem Value="M">Muscle Gain</asp:ListItem>
                                    <asp:ListItem Value="W">Weight Loss </asp:ListItem>
                                    <asp:ListItem Value="MA">Maintenance </asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" ValidationGroup="Tool"
                                    ControlToValidate="ddlDietType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Diet Type">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label3" runat="server" Text="protein" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtprotein" runat="server" Text="Your protein"></asp:Label>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label6" runat="server" Text="carbs" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtcarbs" runat="server" Text="Your carbs"></asp:Label>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="color: #1a33e4; font-weight: 700; font-size: 18px;">
                                <asp:Label ID="label8" runat="server" Text="fat" Style="color: black"></asp:Label>
                                - 
                            <asp:Label ID="txtfats" runat="server" Text="Your fat"></asp:Label>
                            </div>
                        </div>
                        <div class="float-end" id="DietSubmit" runat="server" style="margin-top: 5px">
                            <asp:Button ID="btnSubmit" CssClass="btnSubmit" TabIndex="6" runat="server"
                                Text="Calculate" ValidationGroup="TranDietOutPlan" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" CssClass="btnSubmit" TabIndex="6" runat="server"
                                Text="Reset" ValidationGroup="TranDietOutPlan" OnClick="btnReset_Click" />

                        </div>
                    </div>


                </div>

            </div>

        </div>
    </div>

</asp:Content>


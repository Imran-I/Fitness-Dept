<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerUserAssign.aspx.cs" Inherits="Master_Branch_TrainerUserAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divGv" runat="server">
        <div class="row">

            <div class="PageHeader">
                <h4>Trainer  Reassign</h4>

            </div>

        </div>
        <div class="container-fluid frmcontainer">
            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 ">
                <div class="row">

                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlOfferId"
                            CssClass="form-select" runat="server" TabIndex="5">
                            <asp:ListItem Value="0"> Select Trainer</asp:ListItem>

                            <asp:ListItem Value="0"> Kumar</asp:ListItem>
                           
                        </asp:DropDownList>

                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="DropDownList2"
                            CssClass="form-select" runat="server" TabIndex="5">
                            <asp:ListItem Value="0"> Select Category</asp:ListItem>
                            <asp:ListItem Value="0"> Muscle Building</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="DropDownList3"
                            CssClass="form-select" runat="server" TabIndex="5">
                            <asp:ListItem Value="0"> Select Training Type</asp:ListItem>
                            <asp:ListItem Value="0"> Personal Training</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3" id="divSlotList" runat="server">
                        <div>
                            <asp:Label
                                ID="Label1"
                                runat="server">Slots<span class="reqiredstar">*</span>
                            </asp:Label>
                        </div>
                        <asp:CheckBoxList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" >
                            <asp:ListItem Value="1">9.00 -10.00</asp:ListItem>
                            
                        </asp:CheckBoxList>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="DropDownList1"
                            CssClass="form-select" runat="server" TabIndex="5">
                            <asp:ListItem Value="0"> Select New Trainer</asp:ListItem>
                            <asp:ListItem Value="0"> Mohan</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtFromDate" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server"> From Date  <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                ControlToValidate="txtFromDate" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                                ErrorMessage="Enter Date">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="TextBox1" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server"> To Date  <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ControlToValidate="TextBox1" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                                ErrorMessage="Enter Date">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>

                <%--   <table class="table table-striped table-hover" style="font-size: 0.8rem">
                    <thead>
                        <tr>
                            <th scope="col">S.No</th>
                            <th scope="col">UserName</th>
                            <th scope="col">Assign</th>

                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td>Murali</td>
                            <td>
                                <asp:CheckBox ID="chk" runat="server" />

                            </td>

                        </tr>

                        <tr>
                            <th scope="row">2</th>
                            <td>Santhosh</td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" />

                            </td>

                        </tr>
                        <tr>
                            <th scope="row">3</th>
                            <td>Manoj</td>
                            <td>
                                <asp:CheckBox ID="CheckBox2" runat="server" />

                            </td>

                        </tr>


                    </tbody>
                </table>--%>
            </div>

            <div class="float-end">


                <asp:Button ID="Button1" CssClass="btnSubmit" TabIndex="9" ValidationGroup="FCPrice" runat="server" Text="Submit" />
                <asp:Button ID="Button2" CssClass="btnCancel" runat="server" TabIndex="10" Text="Cancel" />
            </div>


        </div>
    </div>


</asp:Content>


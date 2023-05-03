<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style>
        .ddl {
            padding: 1rem;
            border: 1px dashed;
            margin-bottom: 0.5rem;
            border-radius: 1rem;
        }

        .profile {
            margin-bottom: 0.5rem;
            color: #202124;
            font-size: 1rem;
            margin-left: 1rem;
        }

        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            border: 1px solid;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Admin Login"></asp:Label>
        </div>
    </div>
     <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Admin <span>Login</span></h5>
            </div>
            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                                <asp:DropDownList ID="ddlgymowner" AutoPostBack="true" OnSelectedIndexChanged="ddlgymowner_SelectedIndexChanged" CssClass="form-select" runat="server">                                    
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvTaxType" InitialValue="0" ValidationGroup="AdminLogin" ControlToValidate="ddlgymowner" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Gymowner">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-2">
                               <asp:DropDownList ID="ddlBranch" CssClass="form-select" runat="server">                                    
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="AdminLogin" ControlToValidate="ddlBranch" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Branch">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                      
                    </div>
                  
                </div>
            </div>
            <div class="float-end">
                <asp:Button  ID="btnSubmit" CssClass="btnSubmit" ValidationGroup="AdminLogin" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
      

    </div>
</asp:Content>


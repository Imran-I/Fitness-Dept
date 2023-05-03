<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="OwnerLogin.aspx.cs" Inherits="OwnerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        .inline-label {
            display: inline-block;
            margin-right: 10px;
            font-size: 14px;
            font-family: Helvetica;
        }

        .branchnameheader {
            font-size: 15px;
            color: black;
            font-weight: bold;
            text-align: center;
        }

        .branchUserCount {
            font-size: 14px;
            font-weight: bold;
            display: flex;
            justify-content: start;
            color: black;
            font-family: Helvetica;
        }
           .footerbranchUserCount {
            font-size: 14px;
            font-weight: bold;
            display: flex;
            justify-content: start;
            color: black;
            font-family: Helvetica;
            /*margin-left: 1rem;*/
        }


        .branchUseramount {
            font-size: 14px;
            font-weight: bold;
            display: flex;
            justify-content: start;
            color: black;
            font-family: Helvetica;
            margin-left: -2rem;
        }

        .branchsemicolon {
            font-size: 14px;
            font-weight: bold;
            /*  display: flex;
            justify-content: end;*/
            color: black;
        }

          .branchsemicolonamount {
            font-size: 14px;
            font-weight: bold;
            /*  display: flex;
            justify-content: end;*/
            color: black;
            margin-left: 2.2rem;
        }

        .branchsemicolonAmount {
            font-size: 14px;
            font-weight: bold;
            display: inline-block;
            color: black;
            margin-left: -12px;
        }


        .Count_Container {
            padding: 16px;
            margin: 8px;
            background: #fff;
            border-radius: 0.3rem;
        }


        .frmcontainer {
            padding: 2rem;
            background-color: #f6f7fb !important;
            box-shadow: none;
            border-radius: 1rem;
            padding-left: 2.5rem !important;
            padding-right: 2.5rem !important;
            padding-bottom: 4rem !important;
        }

        .branchUseramountCount {
            font-size: 14px;
            font-weight: bold;
            display: flex;
            justify-content: center;
            color: black;
            font-family: Helvetica;
            margin-left: 3rem;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Branch Login"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Branch <span>Login</span></h5>
            </div>
            <%-- <div class="ddl">--%>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                    <div class="row">
                        <asp:Label ID="branchuserDetails" CssClass="inline-label branchnameheader" runat="server" Text="Branch Wise User Count"></asp:Label>
                    </div>
                    <div class="Count_Container">
                        <asp:DataList ID="UserCount" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8">
                                        <asp:Label ID="Label1" CssClass="inline-label" runat="server" Text='<%# Eval("branchName") %>' />
                                    </div>
                                    <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <asp:Label ID="Label5" CssClass="inline-label branchsemicolon" runat="server" Text=':' />
                                    </div>
                                    <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <asp:Label ID="lblusers" CssClass="inline-label branchUserCount" runat="server" Text='<%# Eval("Users") %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <div style="border-bottom: 1px dotted #000; margin: 5px"></div>
                        <div class="row">
                            <div class="col-12 col-sm-7 col-md-7 col-lg-7 col-xl-7">
                                <asp:Label ID="Label18" CssClass="inline-label fw-bold" runat="server" Text='Total' />
                            </div>
                            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                <asp:Label ID="Label19" CssClass="inline-label branchsemicolon" runat="server" Text=':' />
                            </div>
                            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                <asp:Label ID="lblusercounts" CssClass="inline-label footerbranchUserCount" runat="server" Text='0' />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                    <div class="row">
                        <asp:Label ID="Label17" CssClass="inline-label branchnameheader" runat="server" Text="Branch Wise Amount Collected"></asp:Label>
                    </div>
                    <div class="Count_Container">
                        <asp:DataList ID="divUserCount" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-12 col-sm-7 col-md-7 col-lg-7 col-xl-7">
                                        <asp:Label ID="Label8" CssClass="inline-label" runat="server" Text='<%# Eval("branchName") %>' />
                                    </div>
                                    <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                        <asp:Label ID="Label9" CssClass="branchsemicolonAmount" runat="server" Text=':' />
                                    </div>
                                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                        <asp:Label ID="lbltotalamount" CssClass="inline-label branchUseramount" runat="server" Text='<%# Eval("TotalAmount") %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <div style="border-bottom: 1px dotted #000; margin: 5px"></div>
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                                <asp:Label ID="Label2" CssClass="inline-label fw-bold" runat="server" Text='Total' />
                            </div>
                            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                <asp:Label ID="Label3" CssClass="inline-label branchsemicolonamount" runat="server" Text=':' />
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                                <asp:Label ID="lbluseramount" CssClass="inline-label branchUseramountCount" runat="server" Text='0' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ddl mt-3">
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                        <div class="row justify-content-center">
                            <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-2">
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
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" ValidationGroup="AdminLogin" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>


    </div>
</asp:Content>


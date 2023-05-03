<%@ Page Title="Excel Upload" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="ExcelUploadForEnroll.aspx.cs" Inherits="Master_ExcelUpload_ExcelUploadForEnroll" %>

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
            margin-left: 2rem;
        }

        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            border: 1px solid;
        }

        .divClickExcelContainer {
            display: flex;
            gap: 0.2rem;
            align-items: center;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Enrollment"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Excel Based Enroll"></asp:Label>
        </div>
    </div>

    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Enrollment</h5>
            </div>
            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div class="divClickExcelContainer">
                            <asp:Label
                                ID="lblExcelInfo"
                                CssClass="lblSameTime"
                                runat="server">
                                    *To Download Excel Format
                            </asp:Label>
                            <asp:HyperLink
                                ID="HyperLnkDownLoad"
                                CssClass="lblSameTimeClick"
                                runat="server">
                                    Click Here
                            </asp:HyperLink>
                        </div>
                    </div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 ">
                        <div class="divClickExcelContainer">
                            <asp:Label
                                ID="lblSelectFile"
                                CssClass="lblSameTime"
                                runat="server"> Select File :
                            </asp:Label>
                            <asp:FileUpload
                                ID="FuExcel"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="float-end">
                <asp:Button
                    ID="btnUpload"
                    CssClass="btnSubmit"
                    runat="server"
                    OnClick="btnUpload_Click"
                    Text="Upload" />
            </div>
        </div>
    </div>
</asp:Content>


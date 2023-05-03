<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerMapping.aspx.cs" Inherits="Master_Attendance_TrainerMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Employee Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Trainer Mapping"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Trainer <span>Mapping</span></h5>
            </div>

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlTrainerList" CssClass="form-select" TabIndex="1" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvTrainerList" ValidationGroup="FCPrice"
                        ControlToValidate="ddlTrainerList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Trainer" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddltrainingtype" CssClass="form-select" TabIndex="1" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="FCPrice"
                        ControlToValidate="ddltrainingtype" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Training Type" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
               <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                   <asp:CheckBox ID="Trainingtype1" runat="server" Text="10:00 - 11:00" />
                   <asp:CheckBox ID="Trainingtype2" runat="server" Text="12:00 - 13:00" />
                   <asp:CheckBox ID="Trainingtype3" runat="server" Text="16:00 - 17:00" />
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4"
                    CssClass="btnSubmit" ValidationGroup="TrainerAttendance" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btnCancel" />
            </div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="UserSlotSwapping.aspx.cs" Inherits="Master_Newforms_UserSlotSwapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .userplandetails {
            font-weight: bold;
            color: black;
            font-size: 15px;
        }
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Employee Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="User Slot Swapping"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>User Slot <span>Swapping</span></h5>
            </div>
            <div class="row" id="Userplandetails" runat="server" visible="false">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:Label ID="lbluserswaptype" runat="server"> Swap Type : </asp:Label>
                    <asp:Label ID="userswapType" CssClass="userplandetails" runat="server"></asp:Label>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:Label ID="lblslot" runat="server"> Slot Timing : </asp:Label>
                    <asp:Label ID="userslot" CssClass="userplandetails" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlslotswaptype" CssClass="form-select" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlslotswaptype_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Userslotswap"
                        ControlToValidate="ddlslotswaptype" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Slot SwapType" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlUserName" CssClass="form-select" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Userslotswap"
                        ControlToValidate="ddlUserName" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select User" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                 <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3"  id="divlogdate" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtlogDate" AutoComplete="Off" TabIndex="4" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvlogDate" ValidationGroup="Userslotswap" ControlToValidate="txtlogDate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter From Date">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                    <%--<asp:CheckBox ID="chkSlotList" runat="server"/>--%>
                    <%-- <asp:CheckBoxList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true">                     
                    </asp:CheckBoxList>--%>
                    <asp:RadioButtonList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true"></asp:RadioButtonList>

                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4"
                    CssClass="btnSubmit" ValidationGroup="Userslotswap" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btnCancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>User Slot <span>Swapping</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvUser" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="uniquId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="oldslotId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbloldslotId" runat="server" Text='<%#Bind("slotId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserName" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SlotTime" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblOldSlotTime" runat="server" Text='<%#Bind("OldSlotTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>                           
                         <asp:TemplateField HeaderText="FromDate"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblFromDate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                       <%--  <asp:TemplateField HeaderText="ToDate" Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblToDate" runat="server" Text='<%#Bind("ToDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField HeaderText="SwapType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblconfigName" runat="server" Text='<%#Bind("configName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>--%>
                        
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>
</asp:Content>


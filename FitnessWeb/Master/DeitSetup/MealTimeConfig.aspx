<%@ Page Title="Meal Time Config" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="MealTimeConfig.aspx.cs" Inherits="Master_DeitSetup_MealTimeConfig" %>

<asp:Content ID="CntMealTimeConfig" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
        <i class="fafaicon">/</i>
        <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
        <i class="fafaicon">/</i>
        <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Food Menu Setup"></asp:Label>
        <i class="fafaicon">/</i>
        <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Meal Time Config"></asp:Label>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Meal Time <span>Config</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                    <asp:DropDownList ID="ddlmealType" CssClass="form-select" runat="server" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvmealType" InitialValue="0" ValidationGroup="MealTime" ControlToValidate="ddlmealType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Meal Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txttimeInHrs" CssClass="txtbox" runat="server" placeholder=" " TabIndex="2" />
                        <asp:Label CssClass="txtlabel" runat="server">Time In Hrs <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfdConfigType" ValidationGroup="MealTime" ControlToValidate="txttimeInHrs" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Time In Hrs">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btnSubmit" ValidationGroup="MealTime" TabIndex="3" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Meal Time <span>Config</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvMealTimeConfig" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unique Id" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MealType Id" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmealTypeId" runat="server" Text='<%#Bind("mealTypeId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meal Type Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time In Hrs" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltimeInHrs" runat="server" Text='<%#Bind("timeInHrs") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    OnClick="LnkEdit_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="Config Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="ConfigMstr.aspx.cs" Inherits="Master_Configuration_ConfigMstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Config Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Config <span>Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList  ID="ddlConfigType" CssClass="form-select" runat="server" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvConfigType" InitialValue="0" ValidationGroup="ConfigMstr" ControlToValidate="ddlConfigType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Config Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtConfigTypeName" MaxLength="150" CssClass="txtbox" runat="server" placeholder=" " TabIndex="2" />
                        <asp:Label CssClass="txtlabel" runat="server">Config Name <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvConfigTypeName" ValidationGroup="ConfigMstr" ControlToValidate="txtConfigTypeName" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Config Name">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click"  CssClass="btnSubmit" runat="server" TabIndex="3"
                    ValidationGroup="ConfigMstr" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" TabIndex="4" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Config <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                     <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvConfigMstr" runat="server"  DataKeyNames="configId" AutoGenerateColumns="false" 
                    CssClass ="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type Id" Visible="false"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltypeId" runat="server" Text='<%#Bind("typeId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type Name"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lbltypeName" runat="server" Text='<%#Bind("typeName") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Config Id" Visible="false"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblconfigId" runat="server" Text='<%#Bind("configId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Config Name"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lblconfigName" runat="server" Text='<%#Bind("configName") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25" 
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEdit_Click" />
                            </ItemTemplate>
                            
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
      
</asp:Content>


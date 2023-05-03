<%@ Page Title="YouTube Live" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="LiveConfigMstr.aspx.cs" Inherits="Master_Tools_LiveConfigMstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="YouTube Live"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Youtube <span>Live</span></h5>
            </div>
            <div class="row">
                 <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtPurPoseName" MaxLength="150" CssClass="txtbox"
                            runat="server" placeholder=" " TabIndex="1" ></asp:TextBox>
                        <asp:Label CssClass="txtlabel" runat="server">Purpose Name <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="LiveConfigMstr"
                        ControlToValidate="txtPurPoseName" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Purpose Name">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtLiveDate" MaxLength="150" CssClass="txtbox dateTimepicker" runat="server" placeholder=" " TabIndex="2" ></asp:TextBox>
                        <asp:Label CssClass="txtlabel" runat="server">Live Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        ValidationGroup="LiveConfigMstr" ControlToValidate="txtLiveDate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Live Date">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtLiveUrl" MaxLength="150" CssClass="txtbox" runat="server" placeholder=" " TabIndex="3" ></asp:TextBox>
                        <asp:Label CssClass="txtlabel" runat="server">Live Url<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvConfigTypeName" ValidationGroup="LiveConfigMstr" ControlToValidate="txtLiveUrl" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Live Url">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click"  CssClass="btnSubmit" runat="server" TabIndex="3"
                    ValidationGroup="LiveConfigMstr" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" TabIndex="4" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Youtube <span>Live</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                     <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvLiveConfigMstr" runat="server"  DataKeyNames="uniqueId" AutoGenerateColumns="false" 
                    CssClass ="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Purpose Name"   HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblpurposename" runat="server" Text='<%#Bind("purposename") %>' ></asp:Label>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Live Date"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lbllivedate" runat="server" Text='<%#Bind("livedate") %>'></asp:Label>      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Live Url"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblliveurl" runat="server" Text='<%#Bind("liveurl") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader" Visible="false">
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
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader" Visible="false">
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


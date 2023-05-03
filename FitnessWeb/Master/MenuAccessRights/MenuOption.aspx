<%@ Page Title="Menu Option" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="MenuOption.aspx.cs" Inherits="Master_MenuAccessRights_MenuOption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
          
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Menu Option"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Menu Option</h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtMenuOption" MaxLength="150" Autocomplete="off" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Menu Option<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfdMenuOption" ValidationGroup="MenuOption" ControlToValidate="txtMenuOption" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Config Type Name">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" ValidationGroup="MenuOption" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Menu Option</h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                         <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

             <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvMenuOptionMstr" runat="server" DataKeyNames="optionId" AutoGenerateColumns="false" 
                    CssClass ="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Option Id" Visible="false"  HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbloptionId" runat="server" Text='<%#Bind("optionId") %>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  VerticalAlign="Middle"/>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Option Name"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"> 
                            <ItemTemplate>
                                <asp:Label ID="lbloptionName" runat="server" Text='<%#Bind("optionName") %>'></asp:Label>      
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


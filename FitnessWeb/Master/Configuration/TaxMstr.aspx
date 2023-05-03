<%@ Page Title="Tax Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TaxMstr.aspx.cs"
    Inherits="Master_Configuration_TaxMstr" %>

<asp:Content ID="CntTaxMstr" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
               <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Tax Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Tax <span>Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    
                    <asp:DropDownList ID="ddlServiceName" TabIndex="1" CssClass="form-select" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="TaxMstr"
                        ControlToValidate="ddlServiceName" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Service">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtTaxDesc" MaxLength="50" AutoComplete="Off" TabIndex="2" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Tax Description <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="TaxMstr" ControlToValidate="txtTaxPercentage" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Tax Description">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtTaxPercentage" MaxLength="3"  onkeypress="return isNumber(event);" onkeyup="this.value = minmax(this.value, 0, 100);" AutoComplete="Off" TabIndex="3" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Tax Percentage <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvTaxPercentage" ValidationGroup="TaxMstr" ControlToValidate="txtTaxPercentage" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Tax Percentage">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtEffectiveFrom" AutoComplete="Off" TabIndex="4" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Effective From<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvEffectiveFrom" ValidationGroup="TaxMstr" ControlToValidate="txtEffectiveFrom" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Effective From">
                    </asp:RequiredFieldValidator>
                </div>
                 <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" style="margin-top: 30px;margin-left: -151px;">
                     <div class="float-end">
                        <asp:LinkButton ID="btnAddTax" runat="server" TabIndex="5" OnClick="btnAddTax_Click" Text="Add" ValidationGroup="TaxMstr"
                            CssClass="btnAdd">  </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:GridView ID="gvTax" runat="server" AutoGenerateColumns="false" DataKeyNames="UniqueId"
                    CssClass="table table-striped table-hover border display gvFilter" Style="font-size: 0.8rem">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblserviceId" runat="server" Text='<%#Bind("serviceId") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblServiceName" runat="server" Text='<%#Bind("serviceName") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tax Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxId" runat="server" Text='<%#Bind("taxId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblUniqueId" runat="server" Text='<%#Bind("UniqueId") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lbltaxDescription" runat="server" Text='<%#Bind("taxDescription") %>' Visible="true"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tax Percentage" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxPercentage" runat="server" Text='<%#Bind("taxPercentage") %>'> </asp:Label>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective From" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbleffectiveFrom" runat="server" Text='<%#Bind("effectiveFrom") %>'></asp:Label>
                                <asp:Label ID="lbleffectiveTill" runat="server"  Text='<%#Bind("effectiveTill") %>' Visible="false"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click"> 
                                    <i  class="fa-solid fa-pencil fafaediticon editcolor"></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">
                                  <i class="fa fa-trash editcolor"></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" TabIndex="6" runat="server"  Text="Submit" OnClick="btnSubmit_Click" Enabled="false" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="7" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Tax <span>Master</span></h4>

                   
                     <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvTaxMaster" runat="server" AllowPaging="True" CssClass="table table-striped table-hover border" Style="font-size: 0.8rem"
                    AutoGenerateColumns="False" DataKeyNames="taxId" PageSize="25000" OnDataBound="gvTaxMaster_DataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No." HeaderStyle-CssClass="grdHead">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tax Id" HeaderStyle-CssClass="grdHead" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblTaxId" runat="server" Text='<%# Bind("taxId") %>' Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Service Name | Tax Description  | Tax %" HeaderStyle-CssClass="grdHead">
                            <ItemTemplate>
                                <asp:Label ID="lblServiceName" runat="server" Text='<%# Eval("serviceName")+" | "+Eval("taxDescription")+" | "+Eval("taxPercentage") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Effective From" HeaderStyle-CssClass="grdHead">
                            <ItemTemplate>
                                <asp:Label ID="lblEffectiveFrom" runat="server" Text='<%# Bind("effectiveFrom") %>' ForeColor="Green" Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Effective Till" Visible="false" HeaderStyle-CssClass="grdHead">
                            <ItemTemplate>
                                <asp:Label ID="lblEffectiveTill" runat="server" Text='<%# Bind("effectiveTill") %>' ForeColor="Red" Font-Bold="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active Status" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblActiveStatus" runat="server" Text='<%# Bind("activeStatus") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gvHead" />
                    <AlternatingRowStyle CssClass="gvRow" />
                    <PagerStyle HorizontalAlign="Center" CssClass="gvPage" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


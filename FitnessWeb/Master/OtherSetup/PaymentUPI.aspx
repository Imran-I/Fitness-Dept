<%@ Page Title="Payment UPI" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="PaymentUPI.aspx.cs" Inherits="Master_OtherSetup_PaymentUPI" %>

<asp:Content ID="cndPaymentUPI" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .notes {
            font-size: 0.7rem;
            font-weight: 800;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Other Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Payment UPI Details"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Payment UPI <span>Details</span></h5>
            </div>

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtUPIId" TabIndex="1" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">UPI Id <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvquestion" ValidationGroup="PaymentUPI" ControlToValidate="txtUPIId" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter UPI Id">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtname" TabIndex="1" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Name <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvname" ValidationGroup="PaymentUPI" ControlToValidate="txtname" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Name">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtphoneNumber" MaxLength="10" onKeyPress="return isNumber(event)" TabIndex="3" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Phone Number <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvphoneNumber" ValidationGroup="PaymentUPI" ControlToValidate="txtphoneNumber" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Phone Number">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtmerchantId" TabIndex="4" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Merchant Id <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvmerchantId" ValidationGroup="PaymentUPI" ControlToValidate="txtmerchantId" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Merchant Id">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtmerchantCode" TabIndex="5" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Merchant Code  <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvmerchantCode" ValidationGroup="PaymentUPI" ControlToValidate="txtmerchantCode" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Merchant Code">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtmode" TabIndex="6" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Mode </asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtorgId" TabIndex="7" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Org Id </asp:Label>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtSign" TabIndex="8" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Sign </asp:Label>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txturl" TabIndex="9" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Url </asp:Label>
                    </div>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" TabIndex="10" CssClass="btnSubmit" ValidationGroup="PaymentUPI" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" TabIndex="11" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Payment UPI <span>Details</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row">
                <p class="notes">* Notes :This Form Used For Only Insert And Old Record To Be InActive</p>
            </div>
            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvPaymentUPI" runat="server" DataKeyNames="paymentUPIDetailsId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="paymentUPIDetailsId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblpaymentUPIDetailsId" runat="server" Text='<%#Bind("paymentUPIDetailsId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="gymOwnerId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="branchId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UPI Id" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblUPIId" runat="server" Text='<%#Bind("UPIId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PhoneNumber" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblphoneNumber" runat="server" Text='<%#Bind("phoneNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Merchant Code" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblmerchantCode" runat="server" Text='<%#Bind("merchantCode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Merchant Id" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblmerchantId" runat="server" Text='<%#Bind("merchantId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="orgId" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblorgId" runat="server" Text='<%#Bind("orgId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sign" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblsign" runat="server" Text='<%#Bind("sign") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="url" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblurl" runat="server" Text='<%#Bind("url") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>


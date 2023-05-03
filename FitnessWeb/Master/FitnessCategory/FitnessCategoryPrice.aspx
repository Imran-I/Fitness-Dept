<%@ Page Title="Category Price" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FitnessCategoryPrice.aspx.cs" Inherits="Master_FitnessCategoryPrice" %>

<asp:Content ID="CntFitnessCategoryPrice" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function SelectAllCheckboxes(chk, selector) {
            $('#<%=gvCategoryPrice.ClientID%>').find(selector + " input:checkbox").each(function () {
                ;
                $(this).prop("checked", $(chk).prop("checked"));

            });
        }
    </script>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Fitness Plan Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Category Price"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Category <span>Price</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlCategoryList" CssClass="form-select" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvCategoryList" ValidationGroup="FCPrice"
                        ControlToValidate="ddlCategoryList"  runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Category" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:Label runat="server" CssClass="lblstyle">Price Mode <span class="reqiredstar">*</span></asp:Label>
                    <asp:RadioButtonList ID="RbtnlPriceMode" runat="server" CssClass="frmcheckbox" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="RbtnlPriceMode_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                        <asp:ListItem Value="O">Online</asp:ListItem>
                        <asp:ListItem Value="D" Selected="True">Direct</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RfvpriceMode" ValidationGroup="FCPrice" ControlToValidate="RbtnlPriceMode" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Price Mode">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddltrainingType" CssClass="form-select" runat="server" TabIndex="3">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlPlanDuration" CssClass="form-select" runat="server" TabIndex="4">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvPlanDuration" ValidationGroup="FCPrice"
                        ControlToValidate="ddlPlanDuration" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Plan Duration" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlTax" AutoPostBack="true" OnSelectedIndexChanged="ddlTax_SelectedIndexChanged"
                        CssClass="form-select" runat="server" TabIndex="5">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="Rfvtax" ValidationGroup="FCPrice"
                        ControlToValidate="ddlTax" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Tax" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtactutalAmount" runat="server" AutoComplete="off" TabIndex="6"
                            CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                        <asp:Label CssClass="txtlabel" runat="server">Actutal Amount  <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="FCPrice" ControlToValidate="txtactutalAmount" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Actutal Amount">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlOffer" AutoPostBack="true" OnSelectedIndexChanged="ddlOffer_SelectedIndexChanged"
                        CssClass="form-select" runat="server" TabIndex="5">
                    </asp:DropDownList>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="FCPrice"
                        ControlToValidate="ddlOffer" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer" InitialValue="0">
                    </asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromdate" TabIndex="1" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="FCPrice"
                            ControlToValidate="txtFromdate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">

                        <asp:TextBox ID="txtTodate" TabIndex="2" CssClass="txtbox ConverttoDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="FCPrice"
                            ControlToValidate="txtTodate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter To Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtdisplayAmount" runat="server" TabIndex="6" AutoComplete="off"
                            CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                        <asp:Label CssClass="txtlabel" runat="server">Display Amount  <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="FCPrice" ControlToValidate="txtdisplayAmount" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Display Amount">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtNetAmount" runat="server" TabIndex="6" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtNetAmount_TextChanged"
                            CssClass="txtbox" placeholder=" " MaxLength="7" onkeypress="return AllowOnlyAmountAndDot(this.id);" />
                        <asp:Label CssClass="txtlabel" runat="server">Net Amount  <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvnetAmount" ValidationGroup="FCPrice" ControlToValidate="txtNetAmount" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Net Amount">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtPrice" ReadOnly="true" CssClass="txtbox" runat="server" AutoComplete="off" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Price  <%--<span class="reqiredstar">*</span>--%></asp:Label>
                    </div>
                    <%-- <asp:RequiredFieldValidator ID="RfvShortName" ValidationGroup="FCPrice" ControlToValidate="txtPrice" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Price">
                    </asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txttax" ReadOnly="true" CssClass="txtbox" runat="server" AutoComplete="off" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Tax Amount  <%--<span class="reqiredstar">*</span>--%></asp:Label>
                    </div>
                    <%--  <asp:RequiredFieldValidator ID="rfvtaxamount" ValidationGroup="FCPrice" ControlToValidate="txttax" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Tax Amount">
                    </asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div class="row">

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:Label runat="server" CssClass="lblstyle">Cycle Payments Allowed <span class="reqiredstar">*</span></asp:Label>
                    <asp:RadioButtonList ID="RbtnlcyclePaymentsAllowed" AutoPostBack="true" TabIndex="7"
                        OnSelectedIndexChanged="RbtnlcyclePaymentsAllowed_SelectedIndexChanged"
                        runat="server" CssClass="frmcheckbox" RepeatDirection="Horizontal">
                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RfvCPA" ValidationGroup="FCPrice" ControlToValidate="RbtnlcyclePaymentsAllowed"
                        runat="server" CssClass="rfvStyle" ErrorMessage="Select Cycle Payments Allowed">
                    </asp:RequiredFieldValidator>
                </div>
                <div id="divNoofCycle" runat="server" visible="false" class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">

                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtNoOfCycles" CssClass="txtbox" runat="server" placeholder=" " AutoComplete="off" TabIndex="8" />
                        <asp:Label CssClass="txtlabel" runat="server">Max No. Of Cycles Allowed </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="FCPrice" ControlToValidate="txtNoOfCycles" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Max No. Of Cycles Allowed">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btnSubmit" TabIndex="9" ValidationGroup="FCPrice" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" TabIndex="10" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category  <span>Price</span></h4>
                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd"><i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlOfferGv"
                        CssClass="form-select" runat="server" TabIndex="5">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="FCPriceGV"
                        ControlToValidate="ddlOfferGv" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromdateGV" TabIndex="1" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="FCPriceGV"
                            ControlToValidate="txtFromdateGV" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">

                        <asp:TextBox ID="txtTodateGV" TabIndex="2" CssClass="txtbox ConverttoDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="FCPriceGV"
                            ControlToValidate="txtTodateGV" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter To Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive" style="overflow-x: hidden">

                <asp:GridView ID="gvCategoryPrice" runat="server"
                    DataKeyNames="priceId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="price Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblpriceId" runat="server" Text='<%#Bind("priceId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="gymOwner Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="branch Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="category Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Training TypeId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltrainingTypeId" runat="server" Text='<%#Bind("trainingTypeId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Training Mode" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbltrainingMode" runat="server" Text='<%#Bind("trainingMode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("trainingMode").ToString() == "O"?"Online":"Direct" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan Duration" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblplanDuration" runat="server" Text='<%#Bind("planDuration") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblplanDurationName" runat="server" Text='<%#Bind("planDurationName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblnetAmount" runat="server" Text='<%#Bind("netAmount") %>'></asp:Label>
                                <asp:Label ID="lblactualAmount" runat="server" Text='<%#Bind("actualAmount") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldisplayAmount" runat="server" Text='<%#Bind("displayAmount") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblofferId" runat="server" Text='<%#Bind("offerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblofferName" runat="server" Text='<%#Bind("offerName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblactiveStatusgv" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltoDate" runat="server" Text='<%#Bind("toDate") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lblprice" runat="server" Text='<%#Bind("price") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tax">
                            <ItemTemplate>
                                <asp:Label ID="lbltax" runat="server" Text='<%#Bind("tax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taxId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxId" runat="server" Text='<%#Bind("taxId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cyclePaymentsAllowed" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblcyclePaymentsAllowed" runat="server" Text='<%#Bind("cyclePaymentsAllowed") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="maxNoOfCycles" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblmaxNoOfCycles" runat="server" Text='<%#Bind("maxNoOfCycles") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Offer" HeaderStyle-CssClass="gvHeader">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="javascript:SelectAllCheckboxes(this,'.FormRights');" />
                                <br />
                                Offer
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" CssClass="FormRights" Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubGV" CssClass="btnSubmit" TabIndex="9" ValidationGroup="FCPriceGV" runat="server"
                    Text="Submit" OnClick="btnSubGV_Click" OnClientClick="return confirm('Are sure you want to continue');" />
                <asp:Button ID="btnCancelGV" CssClass="btnCancel" CausesValidation="false" runat="server" TabIndex="10"
                    Text="Cancel" OnClick="btnCancelGV_Click" />
            </div>
        </div>
    </div>
</asp:Content>


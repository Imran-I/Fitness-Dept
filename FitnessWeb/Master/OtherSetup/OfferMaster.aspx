<%@ Page Title="Offer Master" EnableEventValidation="false" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="OfferMaster.aspx.cs" Inherits="Master_OfferMaster" %>

<asp:Content ID="CtOfferMaster" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .empdtls {
            margin-bottom: 1.7rem;
            color: #202124;
            font-size: 1rem;
        }

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
            margin-top: 1rem;
            border: 1px solid;
        }
    </style>


    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Offer Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Offer <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">
                            <a class="empdtls">Offer Details</a>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtOfferHead" AutoComplete="off" MaxLength="50" TabIndex="1" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Offer Heading <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="Rfv" ValidationGroup="Offer" ControlToValidate="txtOfferHead"
                                    runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Offer Heading">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtOfferCode" AutoComplete="off" MaxLength="20" TabIndex="2" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Offer Code <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Offer" ControlToValidate="txtOfferCode"
                                    runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Offer Code">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtOffDesc" TabIndex="3" AutoComplete="off" MaxLength="50" CssClass="txtbox" TextMode="MultiLine" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Offer Description <span class="reqiredstar">*</span></asp:Label>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Offer" ControlToValidate="txtOffDesc"
                                    runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Enter Offer Description">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                                <asp:Label runat="server" CssClass="lblstyle">Offer Period <span class="reqiredstar">*</span></asp:Label>
                                <asp:RadioButtonList ID="rbtnOfferPeriod" TabIndex="5" runat="server" CssClass="frmcheckbox" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="A">After Trans</asp:ListItem>
                                    <asp:ListItem Value="B">Before Trans</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Offer" ControlToValidate="rbtnOfferPeriod" runat="server"
                                    CssClass="rfvStyle" ErrorMessage="Select Offer Period">
                                </asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="profile">Offer Image</div>
                        <asp:Image ID="imgpreview" class="imgpreview" ClientIDMode="Static" runat="server" ImageUrl="../../img/Defaultupload.png" />
                        <asp:FileUpload ID="fuimage" TabIndex="6" CssClass="mx-4" runat="server" onchange="ShowImagePreview(this);" />
                    </div>
                </div>


            </div>
            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Offer Value Details</a>
                    <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                        <asp:Label runat="server" CssClass="lblstyle">Offer Value Type <span class="reqiredstar">*</span></asp:Label>
                        <asp:RadioButtonList ID="rbtnOfferValue" AutoPostBack="true" OnSelectedIndexChanged="rbtnOfferValue_SelectedIndexChanged"
                            TabIndex="6" runat="server" CssClass="frmcheckbox" RepeatDirection="Horizontal">
                            <asp:ListItem Value="P">Percentage (%)</asp:ListItem>
                            <asp:ListItem Value="F">Fixed</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Offer"
                            ControlToValidate="rbtnOfferValue" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Offer Value">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-5 col-md-4 col-lg-5 col-xl-5 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtNooftimes" AutoComplete="off" MaxLength="12" onkeypress="return isNumber(event);" TabIndex="7"
                                CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">No. Of Times Applicable Per User <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Offer"
                                ControlToValidate="txtNooftimes" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter  No Of Times">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div id="divOffPer" runat="server" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtOfferValue" AutoComplete="off" MaxLength="12" TabIndex="8" CssClass="txtbox" onkeypress="return isNumber(event);"
                                onkeyup="this.value = minmax(this.value, 0, 100);" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Offer Value (<i class="fas fa-percentage" style="font-size: 12px;"></i>) 
                                <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Offer"
                                ControlToValidate="txtOfferValue" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Offer Value">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div id="divofffix" visible="false" runat="server" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtofferValueFix" OnTextChanged="txtofferValueFix_TextChanged" AutoPostBack="true" 
                                AutoComplete="off" MaxLength="12" onkeypress="return isNumber(event);"
                                TabIndex="9" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Offer Value (₹) <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="Offer"
                                ControlToValidate="txtofferValueFix" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Offer Value">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMinAmt" MaxLength="12" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtMinAmt_TextChanged"
                                onkeypress="return AllowOnlyAmountAndDot(this.id);"
                                TabIndex="10" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Min Amount (₹) <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="Offer"
                                ControlToValidate="txtMinAmt" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Min Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMaxAmt" OnTextChanged="txtMaxAmt_TextChanged" MaxLength="12" AutoPostBack="true" 
                                AutoComplete="off" TabIndex="11" CssClass="txtbox" onkeypress="return AllowOnlyAmountAndDot(this.id);"
                                runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Max Amount (₹) <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="Offer"
                                ControlToValidate="txtMaxAmt" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Max Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Offer Validity</a>
                    <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtFromdate" AutoComplete="off" TabIndex="12" CssClass="txtbox FromDate" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">From Date <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="Offer"
                                ControlToValidate="txtFromdate" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter From Date">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtTodate" TabIndex="13" AutoComplete="off" CssClass="txtbox ToDate" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">To Date <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="Offer"
                                ControlToValidate="txtTodate" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter To Date">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-5 col-md-5 col-lg-5 col-xl-5 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtTandc" TextMode="MultiLine" AutoComplete="off" TabIndex="14" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Terms And Conditions <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Offer"
                                ControlToValidate="txtTandc" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Terms And Conditions">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" TabIndex="15" ValidationGroup="Offer" CssClass="btnSubmit" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="16" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Offer <span>Master</span>

                    </h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server"
                            CssClass="btnAdd" OnClick="btnAdd_Click">
                         <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">

                <asp:GridView ID="gvOfferMstr" runat="server" DataKeyNames="offerId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Id" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferId" runat="server" Text='<%#Bind("offerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gym Owner Id" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offer Type Period" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferTypePeriod" runat="server" Text='<%#Bind("offerTypePeriod") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Heading" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferHeading" runat="server" Text='<%#Bind("offerHeading") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferDescription" runat="server" Text='<%#Bind("offerDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerCode" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferCode" runat="server" Text='<%#Bind("offerCode") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerImageUrl" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferImageUrl" runat="server" Text='<%#Bind("offerImageUrl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltoDate" runat="server" Text='<%#Bind("toDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerType" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferType" runat="server" Text='<%#Bind("offerType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Value" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferValue" runat="server" Text='<%#Bind("offerValue") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="minAmt" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblminAmt" runat="server" Text='<%#Bind("minAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="maxAmt" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmaxAmt" runat="server" Text='<%#Bind("maxAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="noOfTimesPerUser" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfTimesPerUser" runat="server" Text='<%#Bind("noOfTimesPerUser") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="termsAndConditions" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltermsAndConditions" runat="server" Text='<%#Bind("termsAndConditions") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblexpireStatus" runat="server" Text='<%#Bind("expireStatus") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Add Rules" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkAddDetails"
                                    CssClass="GridAddBtn" Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="linkAddDetails_Click" runat="server">Add</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>


    <%--Offer Rules--%>
    <div id="AddOfferRules" runat="server" class="DisplyCard" visible="false">
        <div class="DisplyCardPostion table-responsive">
            <div class="PageHeader">
                <h5>Offer Rules <span>Master</span>
                    <a onclick="btnClose()" class="float-end btnclose">
                        <i class="fa-solid fa-x"></i></a>
                </h5>
                <div class="text-start">
                    <a class="addlblHead">Offer Name :
                        <asp:Label ID="lblOffName" runat="server"></asp:Label></a>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlRuleType" CssClass="form-select" runat="server">
                        <asp:ListItem Value="0">Rule Type *</asp:ListItem>
                        <asp:ListItem>Festival</asp:ListItem>
                        <asp:ListItem>New Year</asp:ListItem>
                        <asp:ListItem>Payment Type</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvRuleType" InitialValue="0" ValidationGroup="OfferRulesMstr"
                        ControlToValidate="ddlRuleType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Rule Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtOfferRule" TextMode="MultiLine" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Offer Rule <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvConfigTypeName" ValidationGroup="OfferRulesMstr"
                        ControlToValidate="txtOfferRule" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Offer Rule">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="text-end">
                <asp:Button ID="btnSubSubmit" OnClick="btnSubSubmit_Click" CssClass="btnSubmit" runat="server" ValidationGroup="OfferRulesMstr" Text="Submit" />
                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>
            <hr />
            <div id="div" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvOfferRule" Style="font-size: 0.8rem" runat="server" DataKeyNames="offerRuleId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerRuleId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferRuleId" runat="server" Text='<%#Bind("offerRuleId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferId" runat="server" Text='<%#Bind("offerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferHeading" runat="server" Text='<%#Bind("offerHeading") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rule Type Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblruleTypeName" runat="server" Text='<%#Bind("ruleTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Offer Rule" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferRule" runat="server" Text='<%#Bind("offerRule") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ruleType" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblruleType" runat="server" Text='<%#Bind("ruleType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEditRule"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEditRule_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactiveRule_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function ShowImagePreview(input) {

            var fup = document.getElementById("<%=fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreview.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);

                    }
                }
                else {
                    swal("Please, Upload image file less than or equal to 1 MB !!!");
                    fup.focus();
                    return false;
                }
            }
            else {
                swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                fup.focus();
                return false;
            }
        }


        function btnClose() {
            $('#<%= AddOfferRules.ClientID %>').css("display", "none");
        }
    </script>

    <script src="../../Js/CommonDate/CommonFromAndToDate.js"></script>
</asp:Content>


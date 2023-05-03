<%@ Page Title="Subscription Plan" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="SubscriptionPlan.aspx.cs" Inherits="Master_SubscriptionPlan" %>

<asp:Content ID="CtnSubscriptionPlan" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Subscription Plan"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Subscription <span>Plan</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9">
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtPackageName" MaxLength="50" AutoComplete="off" TabIndex="1" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Package Name <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvShortName" ValidationGroup="Subscription" ControlToValidate="txtPackageName" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Package Name">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" AutoComplete="off" TabIndex="2" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Description <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvDescription" ValidationGroup="Subscription" ControlToValidate="txtDescription" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Description">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtNoOfDays" MaxLength="12" TabIndex="3" AutoComplete="off" onkeypress="return isNumber(event);" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">No Of Days <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvNoOfDays" ValidationGroup="Subscription" ControlToValidate="txtNoOfDays" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter No Of Days">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtAmount" MaxLength="12" CssClass="txtbox" AutoComplete="off" runat="server" TabIndex="5"
                                    onkeypress="return AllowOnlyAmountAndDot(this.id);"
                                    AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvAmount" ValidationGroup="Subscription" ControlToValidate="txtAmount" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Amount">
                            </asp:RequiredFieldValidator>
                        </div>
                          <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:DropDownList ID="ddlTax" AutoPostBack="true" OnSelectedIndexChanged="ddlTax_SelectedIndexChanged" TabIndex="4"
                                CssClass="form-select" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="Rfvtax" ValidationGroup="Subscription"
                                ControlToValidate="ddlTax" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Tax" InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtprice" MaxLength="12" CssClass="txtbox" runat="server" AutoComplete="off" TabIndex="6"
                                    onkeypress="return AllowOnlyAmountAndDot(this.id);" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Price  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Subscription" ControlToValidate="txtprice" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Credits">
                            </asp:RequiredFieldValidator>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txttaxAmount" MaxLength="12" CssClass="txtbox" AutoComplete="off" runat="server" TabIndex="7"
                                    onkeypress="return AllowOnlyAmountAndDot(this.id);" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Tax Amount  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Subscription" ControlToValidate="txttaxAmount" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Credits">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtCredits" MaxLength="12" onkeypress="return isNumber(event);" AutoComplete="off" CssClass="txtbox" runat="server" placeholder=" " TabIndex="8" />
                                <asp:Label CssClass="txtlabel" runat="server">Credits  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Subscription" ControlToValidate="txtCredits" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Credits">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:Label runat="server" CssClass="lblstyle">Is Trail Available <span class="reqiredstar">*</span></asp:Label>
                            <asp:RadioButtonList ID="RbtnlTrailAvail" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RbtnlTrailAvail_SelectedIndexChanged" CssClass="frmcheckbox" TabIndex="9" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Subscription" ControlToValidate="RbtnlTrailAvail" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Is Trail Available">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtNoofTrailDays" MaxLength="12" onkeypress="return isNumber(event);" AutoComplete="off" TabIndex="10"
                                    CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" ID="lblNoofTrailDays" runat="server">No Of Trail Days  <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvNoofTailDays" ValidationGroup="Subscription" ControlToValidate="txtNoofTrailDays" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter No of Trail Days">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-3 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-3 mb-12">
                            <div class="profile">Image</div>
                            <img id="imgpreview" class="imgpreview" clientidmode="Static" tabindex="11" runat="server" src="~/img/Defaultupload.png" />
                            <asp:FileUpload ID="fuimage" CssClass="mx-4" runat="server" TabIndex="12" onchange="showpreview(this);" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnPlanSubmit" ValidationGroup="Subscription" TabIndex="13"
                    runat="server" Text="Submit" OnClick="btnPlanSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" TabIndex="14"
                    Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Subscription <span>Plan</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>
            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvSubscriptionPlan" runat="server" DataKeyNames="subscriptionPlanId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="subscriptionPlanId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubscriptionPlanId" runat="server" Text='<%#Bind("subscriptionPlanId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Package Name" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblpackageName" runat="server" Text='<%#Bind("packageName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="imageUrl" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Days" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfDays" runat="server" Text='<%#Bind("noOfDays") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taxId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxId" runat="server" Text='<%#Bind("taxId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnetAmount" runat="server" Text='<%#Bind("netAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="amount" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%#Bind("amount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltax" runat="server" Text='<%#Bind("tax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="taxName" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltaxName" runat="server" Text='<%#Bind("taxName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="credits" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcredits" runat="server" Text='<%#Bind("credits") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cgstTax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcgstTax" runat="server" Text='<%#Bind("cgstTax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="sgstTax" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsgstTax" runat="server" Text='<%#Bind("sgstTax") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="isTrialAvailable" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblisTrialAvailable" runat="server" Text='<%#Bind("isTrialAvailable") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="noOfTrialDays" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfTrialDays" runat="server" Text='<%#Bind("noOfTrialDays") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Add Benefits" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkAddDetails" Text="Add"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>'
                                    OnClick="linkAddDetails_Click" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="AddBenefits" runat="server" class="DisplyCard" visible="false">
        <div class="DisplyCardPostion table-responsive">
            <div class="PageHeader" style="margin-top: -25px">
                <h5>Subscription <span>Benefits</span>
                    <a onclick="btnClose()" class="float-end btnclose">
                        <i class="fa-solid fa-x"></i></a>
                </h5>
                <div class="text-start">
                    <a class="addlblHead" id="BenefitplanName" runat="server"></a>
                </div>
            </div>
            <div class="row" style="margin-top: -20px">
                <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtsubDescription" TextMode="MultiLine" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Description <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="SubBenefits" ControlToValidate="txtsubDescription" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Description">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="profile">Image</div>
                    <img id="imgpreviewSub" class="imgpreview" clientidmode="Static" runat="server" src="~/img/Defaultupload.png" />
                    <asp:FileUpload ID="FileUpload1" CssClass="mx-4" runat="server" onchange="showpreviewsub(this);" />
                </div>
            </div>
            <div class="text-end">
                <asp:Button ID="btnSubSubmit" CssClass="btnSubmit" ValidationGroup="SubBenefits" OnClick="btnSubSubmit_Click" runat="server" Text="Submit" />
                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>

            <hr />
            <div id="divBenefits" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvSubBenefit" Style="font-size: 0.8rem" runat="server" DataKeyNames="SubBenefitId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubBenefitId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblSubBenefitId" runat="server" Text='<%#Bind("SubBenefitId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="subscriptionPlanId" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubscriptionPlanId" runat="server" Text='<%#Bind("subscriptionPlanId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="imageUrl" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEditBenefeits"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEditBenefeits_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactiveBenefeits"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactiveBenefeits_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showpreview(input) {
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

        function showpreviewsub(input) {
            var fup = document.getElementById("<%=FileUpload1.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreviewSub.ClientID%>').prop('src', e.target.result);

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
            $('#<%= AddBenefits.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>


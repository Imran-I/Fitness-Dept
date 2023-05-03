<%@ Page Title="Owner Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true"
    CodeFile="OwnerMstr.aspx.cs" Inherits="Master_OwnerMstr" %>

<asp:Content ID="CtOwnerMaster" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Gym Owner"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Owner Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Owner <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">

                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtGymName"
                                        runat="server"
                                        MaxLength="50"
                                        CssClass="txtbox"
                                        TabIndex="1"
                                        AutoComplete="off"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">
                                        Gym Name<span class="reqiredstar">*</span>
                                    </asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="rfdGymName"
                                        ValidationGroup="OwnerMstr"
                                        ControlToValidate="txtGymName"
                                        runat="server"
                                        CssClass="rfvStyle"
                                        ErrorMessage="Enter Gym Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtShortName"
                                        runat="server"
                                        TabIndex="2"
                                        AutoComplete="off"
                                        CssClass="txtbox"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">
                                        Short Name<span class="reqiredstar">*</span>
                                    </asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="rfdShortName"
                                        ValidationGroup="OwnerMstr"
                                        ControlToValidate="txtShortName"
                                        runat="server"
                                        CssClass="rfvStyle"
                                        ErrorMessage="Enter Short Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtWebSiteUrl"
                                        CssClass="txtbox"
                                        runat="server"
                                        TabIndex="3"
                                        AutoComplete="off"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">
                                        WebSite Url
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtUserName"
                                        CssClass="txtbox"
                                        TabIndex="4"
                                        AutoComplete="off"
                                        runat="server"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">
                                        Gym Owner Name<span class="reqiredstar">*</span>
                                    </asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="rfdUserName"
                                        ValidationGroup="OwnerMstr"
                                        ControlToValidate="txtUserName"
                                        runat="server"
                                        CssClass="rfvStyle"
                                        ErrorMessage="Enter Gym Owner Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div style="display: flex">
                                    <div class="txtboxdiv">
                                        <asp:TextBox
                                            ID="txtPassWord"
                                            CssClass="txtboxPassword passBox1"
                                            TabIndex="5"
                                            AutoComplete="off"
                                            runat="server"
                                            placeholder=" " />
                                        <asp:Label
                                            CssClass="txtlabelPassWord"
                                            runat="server">
                                            Password<span class="reqiredstar">*</span>
                                        </asp:Label>
                                        <asp:RequiredFieldValidator
                                            ID="rfvtxtPassWord"
                                            ValidationGroup="OwnerMstr"
                                            ControlToValidate="txtPassWord"
                                            runat="server"
                                            CssClass="rfvStyle"
                                            Display="Dynamic"
                                            ErrorMessage="Enter Password">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <button
                                        id="show_password"
                                        class="btn btn-light pt-1 btnPassWord"
                                        type="button">
                                        <span class="fa fa-eye icons p-0"></span>
                                    </button>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtMobileNo"
                                        CssClass="txtbox"
                                        onkeypress="return isNumber(event);"
                                        MaxLength="10"
                                        MinLength="10"
                                        TabIndex="6"
                                        AutoComplete="off"
                                        runat="server"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">Mobile No.<span class="reqiredstar">*</span>
                                    </asp:Label>
                                    <asp:RequiredFieldValidator
                                        ID="rfdMobileNo"
                                        ValidationGroup="OwnerMstr"
                                        ControlToValidate="txtMobileNo"
                                        runat="server"
                                        CssClass="rfvStyle"
                                        Display="Dynamic"
                                        ErrorMessage="Enter Mobile No.">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator
                                        ID="revMobileNo" runat="server"
                                        ControlToValidate="txtMobileNo"
                                        ErrorMessage="Invalid Mobile No."
                                        ValidationExpression="[0-9]{10}"
                                        CssClass="rfvStyle"
                                        Display="Dynamic"
                                        ValidationGroup="OwnerMstr">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox
                                        ID="txtEmailId"
                                        CssClass="txtbox"
                                        runat="server"
                                        TabIndex="7"
                                        AutoComplete="off"
                                        placeholder=" " />
                                    <asp:Label
                                        CssClass="txtlabel"
                                        runat="server">Email Id
                                    </asp:Label>
                                    <asp:RegularExpressionValidator
                                        ID="revEmailId"
                                        runat="server"
                                        ControlToValidate="txtEmailId"
                                        ErrorMessage="Invalid Email Id."
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="rfvStyle"
                                        Display="Dynamic"
                                        ValidationGroup="OwnerMstr">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <img id="imgpreview" class="imgpreview" runat="server" clientidmode="Static" src="~/img/User.png" />
                        <asp:FileUpload ID="fuimage" CssClass="mx-4" runat="server" onchange="showpreview(this);" />
                    </div>
                </div>
            </div>

            <div class="float-end">
                <asp:Button
                    ID="btnSubmit"
                    CssClass="btnSubmit"
                    runat="server"
                    Text="Submit" 
                    OnClick="btnSubmit_Click" 
                    TabIndex="8"
                    ValidationGroup="OwnerMstr" />
                <asp:Button
                    ID="btnCancel"
                    CssClass="btnCancel"
                    runat="server" 
                    TabIndex="9"
                    Text="Cancel" 
                    OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Owner <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                          <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvGymOwner"
                    runat="server"
                    DataKeyNames="gymOwnerId,websiteUrl,logoUrl,passWord,mailId,mobileNumber,activeStatus"
                    AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gym Owner name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="gvLblgymOwnerName" runat="server" Text='<%#Bind("gymOwnerName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gym Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="gvLblgymName" runat="server" Text='<%#Bind("gymName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Short Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="gvLblshortName" runat="server" Text='<%#Bind("shortName") %>'></asp:Label>
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
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
    <script type="text/javascript">
        function showpreview(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $(document).ready(function () {
            let showPass = document.querySelector("#show_password");
            let passBox = document.querySelector(".passBox1");
            passBox.type = "password";

            showPass.addEventListener("click", function () {

                if (passBox.type === "password") {
                    passBox.type = "text";
                    $('.icons').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                }
                else {
                    passBox.type = "password";
                    $('.icons').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                }
            })
        });
    </script>
</asp:Content>


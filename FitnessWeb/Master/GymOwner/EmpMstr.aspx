<%@ Page Title="Employee Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="EmpMstr.aspx.cs" Inherits="Master_EmpMstr" %>

<asp:Content ID="CntEmpMstr" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Employee Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Employee Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Employee <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Personal Info</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:Image ID="imgEmpPhotoPrev" class="imgpreview" TabIndex="1" ClientIDMode="Static" runat="server" ImageUrl="~/img/User.png" />
                        <asp:FileUpload ID="Fuimage" CssClass="mx-4" TabIndex="2" runat="server" onchange="ShowImagePreview(this);" />

                    </div>
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">

                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtFirstName" AutoComplete="off" MaxLength="50" TabIndex="3" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">First Name <span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="RfvDietType" ValidationGroup="MstrEmp"
                                        ControlToValidate="txtFirstName" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter First Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtLastName" AutoComplete="off" MaxLength="50" TabIndex="4" CssClass="txtbox" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Last Name <span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="MstrEmp"
                                        ControlToValidate="txtLastName" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Last Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtMobileNo" AutoComplete="off" TabIndex="5" CssClass="txtbox" onkeypress="return isNumber(event);" MaxLength="10" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Mobile No. <span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="MstrEmp"
                                        ControlToValidate="txtMobileNo" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Mobile No.">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtEmailId" AutoComplete="off" MaxLength="50" TabIndex="6" CssClass="txtbox" TextMode="Email" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Email </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtDOB" AutoComplete="off" TabIndex="7" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Date Of Birth <span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="MstrEmp"
                                        ControlToValidate="txtDOB" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Date Of Birth">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtAadharId" AutoComplete="off" TabIndex="8" CssClass="txtbox" onkeypress="return isNumber(event);"
                                        MaxLength="16" runat="server" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Aadhar No. </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        ControlToValidate="txtAadharId" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Aadhar No.">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <asp:DropDownList ID="ddlGender" TabIndex="9" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">Gender *</asp:ListItem>
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    ControlToValidate="ddlGender" runat="server" CssClass="rfvStyle"
                                    ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Gender">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                                <asp:DropDownList ID="ddlMaritalStatus" TabIndex="10" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">Marital Status</asp:ListItem>
                                    <asp:ListItem Value="S">Single</asp:ListItem>
                                    <asp:ListItem Value="M">Married</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    ControlToValidate="ddlMaritalStatus" runat="server" CssClass="rfvStyle"
                                    ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Marital Status">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                </div>


            </div>
            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Address</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtAddress1" AutoComplete="off" TabIndex="11" CssClass="txtbox" TextMode="MultiLine" runat="server"
                                placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Address 1 <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            ControlToValidate="txtAddress1" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" ErrorMessage="Enter Address 1">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtAddress2" AutoComplete="off" TabIndex="12" CssClass="txtbox" TextMode="MultiLine" runat="server"
                                placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Address 2</asp:Label>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtPincode" AutoComplete="off" TabIndex="13" MaxLength="6" onkeypress="return isNumber(event);" CssClass="txtbox"
                                onchange="myFunction()" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Pin Code <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                            ControlToValidate="txtPincode" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" ErrorMessage="Enter Pin Code">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <span runat="server" class="spanstyle">City :</span><asp:Label ID="txtCity" CssClass="lblstyle" runat="server"></asp:Label>
                        <br />
                        <span runat="server" class="spanstyle">District :</span><asp:Label ID="txtDistrict" CssClass="lblstyle" runat="server"></asp:Label>
                        <br />
                        <span runat="server" class="spanstyle">State :</span><asp:Label ID="txtState" CssClass="lblstyle" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="ddl">
                <div class="row">
                    <a class="empdtls">General Info</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlEmployeeType" TabIndex="14" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            ControlToValidate="ddlEmployeeType" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Employee Type">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlDesignation" TabIndex="15" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                            ControlToValidate="ddlDesignation" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Designation">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlDepartment" TabIndex="16" CssClass="form-select" data-style="btn-drak" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                            ControlToValidate="ddlDesignation" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Department">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlShift" TabIndex="17" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                            ControlToValidate="ddlShift" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Shift">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlRole" TabIndex="18" CssClass="form-select" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                            ControlToValidate="ddlRole" runat="server" CssClass="rfvStyle"
                            ValidationGroup="MstrEmp" InitialValue="0" ErrorMessage="Select Role">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtDOJ" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Date Of Joining <span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                ControlToValidate="txtDOJ" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                                ErrorMessage="Enter Date Of Joining">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div id="divPassword" runat="server" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 ">
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
                                            ValidationGroup="MstrEmp"
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
                    </div>
                   

            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" TabIndex="21" CssClass="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="MstrEmp" />
                <asp:Button ID="btnCancel" TabIndex="22" CssClass="btnCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Employee <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                         <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvEmpMstr" runat="server" DataKeyNames="empId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbranchName" runat="server" Text='<%#Bind("branchName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblempId" runat="server" Text='<%#Bind("empId") %>' Visible="false"></asp:Label>

                                <asp:Label ID="lblfirstName" runat="server" Text='<%#Bind("firstName") %>' Visible="true"></asp:Label>-
                                <asp:Label ID="lbllastName" runat="server" Text='<%#Bind("lastName") %>' Visible="true"></asp:Label>


                                <asp:Label ID="lblcity" runat="server" Text='<%#Bind("city") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblstate" runat="server" Text='<%#Bind("state") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgender" runat="server" Text='<%#Bind("gender") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbladdressLine1" runat="server" Text='<%#Bind("addressLine1") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbladdressLine2" runat="server" Text='<%#Bind("addressLine2") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblzipcode" runat="server" Text='<%#Bind("zipcode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldistrict" runat="server" Text='<%#Bind("district") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblmaritalStatus" runat="server" Text='<%#Bind("maritalStatus") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldob" runat="server" Text='<%#Bind("dob") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldoj" runat="server" Text='<%#Bind("doj") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblaadharId" runat="server" Text='<%#Bind("aadharId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblphotoLink" runat="server" Text='<%#Bind("photoLink") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblmobileNo" runat="server" Text='<%#Bind("mobileNo") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblpassWord" runat="server" Text='<%#Bind("passWord") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblmailId" runat="server" Text='<%#Bind("mailId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblshiftId" runat="server" Text='<%#Bind("shiftId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblshiftName" runat="server" Text='<%#Bind("shiftName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblroleId" runat="server" Text='<%#Bind("roleId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblRole" runat="server" Text='<%#Bind("Role") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblactiveStatus" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Role" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                  <asp:Label ID="lblgvroleId" runat="server" Text='<%#Bind("roleId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgvRole" runat="server" Text='<%#Bind("Role") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Type" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblempType" runat="server" Text='<%#Bind("empType") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblempTypeName" runat="server" Text='<%#Bind("empTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                            Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Bind("designation") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldesignationName" runat="server" Text='<%#Bind("designationName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lbldepartment" runat="server" Text='<%#Bind("department") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldepartmentName" runat="server" Text='<%#Bind("departmentName") %>' Visible="true"></asp:Label>
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
    <asp:HiddenField ID="hfState" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfDistrict" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfCity" runat="server" EnableViewState="true" />
    <script type="text/javascript">
        function ShowImagePreview(input) {

            var fup = document.getElementById("<%=Fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgEmpPhotoPrev.ClientID%>').prop('src', e.target.result);

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
    <script>
        // document.getElementById("txtPincode").addEventListener("change", myFunction);
        function myFunction() {
            var NewArea = $('[id*=txtPincode]').val();
            event.preventDefault();

            fetch("https://api.postalpincode.in/pincode/" + $('[id*=txtPincode]').val())
                .then(response => response.json())
                .then(
                    function (data) {
                        if (data[0].Status == 'Success') {
                            document.getElementById('<%=txtCity.ClientID%>').textContent = data[0].PostOffice[0].Block;
                            document.getElementById('<%=txtDistrict.ClientID%>').textContent = data[0].PostOffice[0].District;
                            document.getElementById('<%=txtState.ClientID%>').textContent = data[0].PostOffice[0].State;
                            $('#<%=hfCity.ClientID%>').val(data[0].PostOffice[0].Block);
                            $('#<%=hfDistrict.ClientID%>').val(data[0].PostOffice[0].District);
                            $('#<%=hfState.ClientID%>').val(data[0].PostOffice[0].State);
                            document.getElementById('<%=txtCity.ClientID%>').style.color = 'black';
                        }
                        else {
                            if (data[0].PostOffice == null) {
                                document.getElementById('<%=txtCity.ClientID%>').textContent = 'Invalid Pincode';
                                 document.getElementById('<%=txtDistrict.ClientID%>').textContent = '';
                                 document.getElementById('<%=txtState.ClientID%>').textContent = '';
                                 document.getElementById('<%=txtCity.ClientID%>').style.color = 'red';
                            }

                        }
                    }
                )
                .catch()
        }


    </script>
</asp:Content>


<%@ Page Title="Branch Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="BranchMstr.aspx.cs" Inherits="Master_Branch_BranchMstr" %>

<asp:Content ID="CntBranchMstr" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        .mapstyle {
            height: 300px;
            position: relative;
            overflow: hidden;
            border-radius: 1rem;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Branch Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5><span id="spAddorEdit" runat="server"></span>Branch <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Branch Info</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlOwnerList" TabIndex="1" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="Rfvquestion" InitialValue="0" ValidationGroup="BranchMstr" ControlToValidate="ddlOwnerList" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select  Gym Owner">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtBranchName" AutoComplete="off" TabIndex="2" CssClass="txtbox" runat="server" placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Branch Name <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvBranchName" ValidationGroup="BranchMstr" ControlToValidate="txtBranchName" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Branch Name">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtShortName" AutoComplete="off" TabIndex="3" CssClass="txtbox" runat="server" placeholder=" " MaxLength="10" />
                            <asp:Label CssClass="txtlabel" runat="server">Short Name <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvShortName" ValidationGroup="BranchMstr"
                            ControlToValidate="txtShortName" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Short Name">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMobileNumber" AutoComplete="off" TabIndex="4" onkeypress="return isNumber(event);" MaxLength="10" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Primary Mobile No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvMobileNumber" ValidationGroup="BranchMstr" ControlToValidate="txtMobileNumber" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Mobile Number">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                            ControlToValidate="txtMobileNumber" ErrorMessage="Invalid Phone No."
                            ValidationExpression="[0-9]{10}" CssClass="rfvStyle" ValidationGroup="BranchMstr"></asp:RegularExpressionValidator>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtsecondayMobilenumber" AutoComplete="off" TabIndex="5" onkeypress="return isNumber(event);" MaxLength="10" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Seconday Mobile No.</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtsecondayMobilenumber" ErrorMessage="Invalid Phone No."
                                ValidationExpression="[0-9]{10}" CssClass="rfvStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtemail" AutoComplete="off" TabIndex="6" CssClass="txtbox" TextMode="Email" runat="server" placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Email</asp:Label>
                            <asp:RegularExpressionValidator ID="RefExpVal1" runat="server" ControlToValidate="txtemail"
                                ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                CssClass="rfvStyle" ErrorMessage="Enter Valid EmailId">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtGstNumber" AutoComplete="off" TabIndex="7" CssClass="txtbox" runat="server" placeholder=" " MaxLength="20"/>
                            <asp:Label CssClass="txtlabel" runat="server">GST No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvGstNumber" ValidationGroup="BranchMstr" ControlToValidate="txtGstNumber" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter GST No.">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                      <div class="txtboxdiv">
                        <asp:TextBox ID="txtfromTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Time <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvtxtShiftStartTime" ValidationGroup="ShiftMstr" ControlToValidate="txtfromTime" runat="server"
                        CssClass="rfvStyle" ErrorMessage="Enter From Time">
                    </asp:RequiredFieldValidator>
                    </div>
                </div>
                 <div class="row">                    
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                   <div class="txtboxdiv">
                        <asp:TextBox ID="txttoTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Time <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="ShiftMstr" ControlToValidate="txttoTime" runat="server"
                        CssClass="rfvStyle" ErrorMessage="Enter To Time">
                    </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="ddl">
                <div class="row">
                    <a class="empdtls">Address</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtAddress1" AutoComplete="off" TabIndex="8" CssClass="txtbox" TextMode="MultiLine" runat="server" placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Address 1 <span class="reqiredstar">*</span></asp:Label>
                        </div>
                           <asp:RequiredFieldValidator ID="RfvtxtAddress1" ValidationGroup="BranchMstr" ControlToValidate="txtAddress1" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Address 1">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtAddress2" AutoComplete="off" TabIndex="9" CssClass="txtbox" TextMode="MultiLine" runat="server" placeholder=" " MaxLength="50" />
                            <asp:Label CssClass="txtlabel" runat="server">Address 2</asp:Label>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtPincode" AutoComplete="off" TabIndex="10" MaxLength="6" onkeypress="return isNumber(event);" CssClass="txtbox" 
                                onchange="myFunction()" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Pin Code <span class="reqiredstar">*</span></asp:Label>
                        </div>
                         <asp:RequiredFieldValidator ID="RfvPincode" ValidationGroup="BranchMstr" ControlToValidate="txtPincode" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter Picode">
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
                    <a class="empdtls">Location</a>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtLatitude" TabIndex="11" onkeypress="return isNumber(event);" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Latitude <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvLatitude" ValidationGroup="BranchMstr" ControlToValidate="txtLatitude" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Latitude In Map">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 ">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtLongitude" TabIndex="12" onkeypress="return isNumber(event);" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Longitude <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="Rfvlongitude" ValidationGroup="BranchMstr" ControlToValidate="txtlongitude" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Select Longitude In Map">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div>
                    <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3 mapstyle" id="dvMapget">
                    </div>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" TabIndex="13" ValidationGroup="BranchMstr" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="14" runat="server" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Branch <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                          <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvBranchMstr" runat="server" DataKeyNames="branchId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbranchName" runat="server" Text='<%#Bind("branchName") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblshortName" runat="server" Text='<%#Bind("shortName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbllatitude" runat="server" Text='<%#Bind("latitude") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbllongitude" runat="server" Text='<%#Bind("longitude") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblstate" runat="server" Text='<%#Bind("state") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcity" runat="server" Text='<%#Bind("city") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbladdress1" runat="server" Text='<%#Bind("address1") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbladdress2" runat="server" Text='<%#Bind("address2") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblpincode" runat="server" Text='<%#Bind("pincode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldistrict" runat="server" Text='<%#Bind("district") %>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblfromtime" runat="server" Text='<%#Bind("fromtime") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltotime" runat="server" Text='<%#Bind("totime") %>' Visible="false"></asp:Label>

                                <asp:Label ID="lblapprovalStatus" runat="server" Text='<%#Bind("approvalStatus") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblprimaryMobileNumber" runat="server" Text='<%#Bind("primaryMobileNumber") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblmailId" runat="server" Text='<%#Bind("emailId") %>' Visible="false"></asp:Label>

                                <asp:Label ID="lblsecondayMobilenumber" runat="server" Text='<%#Bind("secondayMobilenumber") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Gst No." HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblgstNumber" runat="server" Text='<%#Bind("gstNumber") %>' Visible="true"></asp:Label>
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
                        <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="gvHeader" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="LnkApprovalStatus"
                                    runat="server"
                                    CssClass='<%#Eval("approvalStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("approvalStatus").ToString() == "A" ? "Approved" : Eval("approvalStatus").ToString() == "C" ? "Cancelled" : "Waiting List"%>'
                                    Enabled='<%#Eval("approvalStatus").ToString() == "A" ? false : Eval("approvalStatus").ToString() == "C" ? false : true %>'
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>'
                                    OnClick="LnkApprovalStatus_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
    <div id="divApprovalPopUp" runat="server" class="DisplyCard" visible="false">
        <div class="DisplyCardPostion table-responsive">
            <div class="PageHeader" style="margin-top: -10px">
                <h5>Approval / Cancellation <span></span>
                    <a onclick="btnClose()" class="float-end btnclose">
                        <i class="fa-solid fa-x"></i></a>
                </h5>

            </div>
            <div id="divApprvCancelBtns" runat="server" class="row" style="margin-left: -1px !important; margin-top: 10px;">
                <div>
                    <asp:Button
                        ID="btnApprove"
                        runat="server"
                        Text="Approve"
                        OnClick="btnApprove_Click"
                        CausesValidation="false"
                        CssClass="btnSubmit" />
                    <asp:Button
                        ID="btnCancelApprove"
                        runat="server"
                        Text="Cancel"
                        OnClick="btnCancelApprove_Click"
                        CausesValidation="false"
                        CssClass="btnCancel" Style="background-color: red; color: white;" />
                </div>


            </div>
            <div class="row" style="margin-left: -1px !important; margin-top: 10px;">
                <div id="divCancellationReason" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12" style="margin-top: 20px;">
                    <div class="row">
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox TextMode="MultiLine" ID="txtReason" CssClass="txtbox" runat="server" MaxLength="200" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Cancellation Reason <span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Check"
                                ControlToValidate="txtReason" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Reason">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:Button ID="btnSubmitPopup" CssClass="btnSubmit"
                                Text="Submit" ValidationGroup="Check" runat="server" OnClick="btnSubmitPopup_Click" />
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:Button ID="btnCancelPopup" CssClass="btnCancel"
                                Text="Cancel" CausesValidation="false" OnClick="btnCancelPopup_Click"
                                runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row justify-content-end" id="divsubmit" runat="server" visible="false">
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hfState" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfDistrict" runat="server" EnableViewState="true" />
    <asp:HiddenField ID="hfCity" runat="server" EnableViewState="true" />

    <asp:HiddenField ID="hflongitude" EnableViewState="true" runat="server" />
    <asp:HiddenField ID="hflatitude" EnableViewState="true" runat="server" />

    <%--    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB56Km4bH3DEKxXLRZBltsTIm3eYgPqt0k"></script>--%>

    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB56Km4bH3DEKxXLRZBltsTIm3eYgPqt0k" type="text/javascript"></script>
    <%-- Script for the Get the Lat and Log--%>
    <script type="text/javascript">
        window.onload = function () {
            const options = {
                enableHighAccuracy: true,
                maximumAge: 30000
            };

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(success, fail, options);
            }
            else {
                alert("Sorry, your browser does not support geolocation services.");
            }
            var latt, longg, map;

            function success(position) {
                document.getElementById('<%=hflongitude.ClientID %>').value = position.coords.longitude;
                document.getElementById('<%=hflatitude.ClientID %>').value = position.coords.latitude;

                var txt = document.getElementById("<%=spAddorEdit.ClientID %>").innerText;
                if (txt == 'Add ') {
                    latt = document.getElementById('<%=hflatitude.ClientID %>').value;
                    longg = document.getElementById('<%=hflongitude.ClientID %>').value;
                    latt = position.coords.latitude;
                    longg = position.coords.longitude;
                }
                else {
                    latt = document.getElementById('<%=txtLatitude.ClientID %>').value;
                    longg = document.getElementById('<%=txtLongitude.ClientID %>').value;
                }
                setlatlong(latt, longg);
            }

            function setlatlong(latt, longg) {
                document.getElementById('<%=txtLatitude.ClientID %>').value = latt;
                document.getElementById('<%=txtLongitude.ClientID %>').value = longg;

                var mapOptions = {
                    center: new google.maps.LatLng(latt, longg),
                    zoom: 10,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById("dvMapget"), mapOptions);

                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(latt, longg),
                });
                marker.setMap(map);

                google.maps.event.addListener(map, "click", function (e) {
                    var latLng = e.latLng;
                    var lat = e.latLng.lat();
                    var long = e.latLng.lng();
                    document.getElementById('<%=hflatitude.ClientID %>').value = lat;
                    document.getElementById('<%=hflongitude.ClientID %>').value = long;

                    document.getElementById('<%=txtLatitude.ClientID %>').value = lat;
                    document.getElementById('<%=txtLongitude.ClientID %>').value = long
                    marker.setMap(null);//remove other markers

                    marker = new google.maps.Marker({
                        position: new google.maps.LatLng(lat, long)
                    });

                    marker.setMap(map);//Set new marker

                    google.maps.event.addDomListener(window, 'load', initialize);
                });

            }

            function fail(error) {
                if (alert(error.message) == "") {
                    alert("Sorry, Failed to get Your Location.");
                }
                else {
                    alert(error.message);
                }
            }
        }

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

        function btnClose() {
            $('#<%= divApprovalPopUp.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>


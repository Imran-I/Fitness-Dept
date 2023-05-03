<%@ Page Title="Menu Option Access" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="MenuOptionAccess.aspx.cs" Inherits="Master_MenuAccessRights_MenuOptionAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <style>
        label {
            font-weight: 500;
            color: black !important;
            font-size: 1.3rem;
        }

        .panel-group .panel + .panel {
            margin-top: 5px;
        }

        .panel-group .panel {
            margin-bottom: 0;
            border-radius: 4px;
        }

        .panel-success {
            border: 1px;
            border-color: #d6e9c6;
        }

        .panel {
            margin-bottom: 5px;
            background-color: #fff;
            border: 1px solid transparent;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
            box-shadow: 0 1px 1px rgba(0,0,0,.05);
        }

        .panel-success > .panel-heading {
            color: #2c2f31;
            background-color: #d2d6d8;
            border-color: #e4e4e4;
            font-size: 18px;
            font-weight: 800;
            text-align: left;
        }


        .panel-group .panel-heading {
            border-bottom: 0;
        }

        .panel-heading {
            padding: 5px 5px;
            border-bottom: 1px solid transparent;
            border-top-left-radius: 3px;
            border-top-right-radius: 3px;
        }

        .panel-body {
            padding: 0px;
            border: 1px solid #c6e1e9;
        }

        .lblChrg {
            padding: 10px 10px;
        }

        .chkChoi {
            color: white !important;
        }

        .chkChoice input {
            margin-right: 5px;
        }

        .holepagenames {
            padding: 8px;
            border-radius: 2px;
            cursor: pointer;
            transition: 0.2s all linear;
            border: 1px solid #fff;
            overflow: scroll;
            max-height: 475px;
        }

        .holepagenamesdef {
            background: #daeefd;
            color: #000000;
        }

        .holepagenamesorg {
            background: #ffffff;
            color: #000000;
        }

        .holepagenamesdarkpurp {
            background: #d5d5fd;
            color: #000000;
        }

        .holepagenamespurp {
            background: #f1dfff;
            color: #000000;
        }

        .holepagenamesgre {
            background: #f5ffdf;
            color: #000000;
        }

        .dividerprpage {
            border-bottom: 1px dashed #000;
            width: 15px;
            /* height: 5px; */
            position: absolute;
            margin-top: -30px;
            margin-left: -15px;
        }

            .dividerprpage:last-child::after {
                background: #eee;
                content: '';
                position: absolute;
                width: 2px;
                height: 20px;
                margin-top: 2px;
                margin-left: -2px;
            }

        table.gvv th {
            text-align: left;
            background: linear-gradient(to bottom, #000000 0%, #000000 100%);
            color: #fff;
        }

        .highlight {
            width: 100%;
            background-color: #eaba93;
        }
    </style>

    <script language="javascript" type="text/javascript">
        
        function CheckAndUncheck(FormChcek) {
            const allcheckBoxInRow = FormChcek.closest('tr').querySelectorAll('input[type=checkbox]');
            const isFormChecked = FormChcek.closest('tr').querySelectorAll('input[type=checkbox]')[0].checked;

            allcheckBoxInRow.forEach((element, index, array) => {
                if (index > 0) {
                    if (isFormChecked) {
                        element.checked = true;
                    }
                    else {
                        element.checked = false;
                    }
                }
            });
        }
        function CheckAndUncheckFormCheckbox(value) {
            const allcheckBoxInRow = value.closest('tr').querySelectorAll('input[type=checkbox]');
            let VAEDarray = [];
            allcheckBoxInRow.forEach((element, index, array) => {
                if (index > 0) {
                    if (element.checked)
                        VAEDarray.push(element);
                }
            });

            if (VAEDarray.length > 0)
                allcheckBoxInRow[0].checked = true;
            else
                allcheckBoxInRow[0].checked = false;
        }
    </script>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
              <asp:Label ID="Label1" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Employee Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Menu Option Access"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">

        <div id="divForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Menu Access</h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-select"
                        TabIndex="1" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlRoles"
                        ValidationGroup="Employee" CssClass="rfvClr" SetFocusOnError="True" InitialValue="0">Select Role</asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-select" TabIndex="1" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserName"
                        ValidationGroup="Employee" CssClass="rfvClr" SetFocusOnError="True" InitialValue="0">Select User Name</asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                    <asp:Button ID="btnSubmit" CssClass="btnSubmit" TabIndex="13" ValidationGroup="BranchMstr" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="14" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>

            <div class="row">
                <div class="row" runat="server" id="divPages" visible="false">

                    <div class="col-sm-12 col-xs-12">
                        <div class="panel panel-success">
                            <div class="panel-heading panelheadchk">
                                Menu Rights  <span class="spanStar">*</span>
                            </div>
                            <div class="panel-body">
                                <div class="row p-0 m-0">
                                    <div class="holepagenames holepagenamesorg col-sm-12 col-xs-12 section" id="divBooking" runat="server">
                                        <asp:GridView ID="gvOption" runat="server"
                                            Width="100%" CssClass="gvv" AutoGenerateColumns="false"
                                            BorderStyle="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" HeaderStyle-CssClass="gvHeader">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option Name" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloptionId" runat="server" Text='<%# Bind("optionId") %>' Font-Bold="true" Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lbloptionName" runat="server" Text='<%# Bind("optionName") %>' Font-Bold="true" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblFormRights" runat="server" Text="Form"></asp:Label>
                                                        <br />
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="FormRights" onclick="CheckAndUncheck(this);" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblAddrights" runat="server" Text="Add"></asp:Label>
                                                        <br />
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="AddCheckBox1" runat="server" CssClass="Addrights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblEditrights" runat="server" Text="Edit"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="EditCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxes(this,'.Editrights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="EditCheckBox1" runat="server" CssClass="Editrights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblViewrights" runat="server" Text="View"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="ViewCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxes(this,'.Viewrights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ViewCheckBox1" runat="server" CssClass="Viewrights" onclick="CheckAndUncheckFormCheckbox(this);"   />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblDeleterights" runat="server" Text="Delete"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="DeleteCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxes(this,'.Deleterights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="DeleteCheckBox1" runat="server" CssClass="Deleterights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>

                                        <asp:GridView ID="GvoptionEdit" runat="server"
                                            Width="100%" CssClass="gvv" AutoGenerateColumns="false"
                                            BorderStyle="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno" HeaderStyle-CssClass="gvHeader">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Option Name" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloptionId" runat="server" Text='<%# Bind("optionId") %>' Font-Bold="true" Width="100px" Visible="false"></asp:Label>
                                                        <asp:Label ID="lbloptionNames" runat="server" Text='<%# Bind("optionName") %>' Font-Bold="true" Width="100px"></asp:Label>
                                                        <asp:Label ID="lblMenuOptionAcessId" runat="server" Text='<%# Bind("MenuOptionAcessId") %>' Font-Bold="true" Width="100px" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblFormRights" runat="server" Text="Form"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="CheckBox2" runat="server" onclick="javascript:SelectAllCheckboxesEdit(this,'.FormRights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("activeStatus").ToString() == "A" ?true:false  %>' CssClass="FormRights" 
                                                            onclick="CheckAndUncheck(this);" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblAddrights" runat="server" Text="Add"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="AddCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxesEdit(this,'.Addrights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="AddCheckBox1" runat="server" Checked='<%# Eval("addRights").ToString() == "Y" ?true:false  %>' CssClass="Addrights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblEditrights" runat="server" Text="Edit"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="EditCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxesEdit(this,'.Editrights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="EditCheckBox1" runat="server" Checked='<%# Eval("editRights").ToString() == "Y" ?true:false  %>' CssClass="Editrights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblViewrights" runat="server" Text="View"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="ViewCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxesEdit(this,'.Viewrights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ViewCheckBox1" runat="server" Checked='<%# Eval("viewRights").ToString() == "Y" ?true:false  %>' CssClass="Viewrights" onclick="CheckAndUncheckFormCheckbox(this);"  />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="grdHead" Visible="true">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblDeleterights" runat="server" Text="Delete"></asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="DeleteCheckBox2" runat="server" onclick="javascript:SelectAllCheckboxesEdit(this,'.Deleterights');" Style="display: none" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="DeleteCheckBox1" runat="server" Checked='<%# Eval("deleteRights").ToString() == "Y" ?true:false  %>' CssClass="Deleterights" onclick="CheckAndUncheckFormCheckbox(this);" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>



        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Menu Access</h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                          <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">

                <div class="table-responsive section">
                    <asp:GridView ID="gvMenuAccess" runat="server" AllowPaging="True"
                        CssClass="table table-striped table-hover border" AutoGenerateColumns="false"
                        BorderStyle="None" PageSize="100">
                        <Columns>
                            <asp:TemplateField HeaderText="Sno" HeaderStyle-CssClass="gvHeader">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvempId" runat="server" Text='<%#Bind("empId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblgvfullName" runat="server" Text='<%#Bind("fullName") %>'></asp:Label>
                                </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Role" HeaderStyle-CssClass="gvHeader">
                                <ItemTemplate>
                                    <asp:Label
                                        ID="lblgvroldId"
                                        runat="server"
                                        Text='<%#Eval("roldId")%>' Visible="false"></asp:Label><asp:Label
                                            ID="lblgvroleName"
                                            runat="server"
                                            Text='<%#Eval("roleName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                                <ItemTemplate>
                                    <asp:ImageButton ID="LnkEdit"
                                        runat="server" OnClick="LnkEdit_Click"
                                        src="../../img/edit-icon.png" alt="image" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                </div>

            </div>
        </div>

    </div>

</asp:Content>


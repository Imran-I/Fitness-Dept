<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerAttendance.aspx.cs" Inherits="Master_Attendance_TrainerAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Employee Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Trainer Attendance"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Trainer <span>Attendance</span></h5>
            </div>

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlTrainerList" CssClass="form-select" TabIndex="1" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvTrainerList" ValidationGroup="TrainerAttendance"
                        ControlToValidate="ddlTrainerList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Trainer" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3"  id="divlogdate" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtlogDate" AutoComplete="Off" TabIndex="4" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Log Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvlogDate" ValidationGroup="TrainerAttendance" ControlToValidate="txtlogDate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Log Date">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3"  id="divintime" runat="server">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtShiftStartTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">InTime <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvtxtShiftStartTime" ValidationGroup="TrainerAttendance" ControlToValidate="txtShiftStartTime" runat="server"
                        CssClass="rfvStyle" ErrorMessage="InTime">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" id="divouttime" runat="server"  visible="false">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtShiftEndTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">OutTime <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvtxtShiftEndTime" ValidationGroup="TrainerAttendance" ControlToValidate="txtShiftEndTime" runat="server"
                        CssClass="rfvStyle" ErrorMessage="Enter OutTime">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" OnClick="btnSubmit_Click"
                    CssClass="btnSubmit" ValidationGroup="TrainerAttendance" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btnCancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Trainer <span>Attendance</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvTrainer" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="uniquId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GymOwnerId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BranchId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblempName" runat="server" Text='<%#Bind("empName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="empId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblempId" runat="server" Text='<%#Bind("empId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbllogDate" runat="server" Text='<%#Bind("logDate") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="InTime"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblinTime" runat="server" Text='<%#Bind("inTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OutTime" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbloutTime" runat="server" Text='<%#Bind("outTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attendance" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblattendance" runat="server" Text='<%#Bind("attendance") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="attStatus" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblattStatus" runat="server" Text='<%#Bind("attStatus") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attendance Type" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblattType" runat="server" Text='<%#Bind("attType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="shiftId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblshiftId"  runat="server" Text='<%#Bind("shiftId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="shiftStartTime" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblshiftStartTime" runat="server" Text='<%#Bind("shiftStartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="shiftEndTime" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblshiftEndTime"  runat="server" Text='<%#Bind("shiftEndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server" 
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit" OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
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
                                <asp:LinkButton ID="linkAddDetails"
                                    CssClass="GridAddBtn" Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="linkAddDetails_Click" runat="server">Add</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>
</asp:Content>


<%@ Page Title="DashBoard" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="AdminDashBoard.aspx.cs" Inherits="AdminDashBoard" %>

<asp:Content ID="cndDashBoard" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Css/DashBoard/DashBoard.css" rel="stylesheet" />
    <link href="Css/DashBoard/AdminDashBoard.css" rel="stylesheet" />
    <style>
        .adminlableheader {
            font-size: 15px;
            display: flex;
        }

        .adminlable {
            font-size: 15px;
        }

        .Nodatafound {
            color: red;
            font-size: 15px;
            text-align: center;
        }
    </style>
    <div class="container-fluid frmcontainer">
        <div class="PageHeader">
            <%--<h5>Dashboard </h5>--%>
        </div>


        <div class="row">
            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                <div class="txtboxdiv">
                    <asp:TextBox ID="txtFromDate" AutoPostBack="true" OnTextChanged="txtFromDate_TextChanged" AutoComplete="off" ClientIDMode="Static" CssClass="txtbox fromDate" runat="server" TabIndex="1" placeholder=" " />
                    <asp:Label CssClass="txtlabel" runat="server">Date<span class="reqiredstar">*</span></asp:Label>
                </div>
            </div>
            <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                <asp:DropDownList ID="ddlTrainerList" CssClass="form-select" TabIndex="1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTrainerList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>



        <%--Trainer Based Start--%>
        <div id="divtrainerbased" runat="server">
            <div id="divSlot" class="row" runat="server">
                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                    <label class="lblSlotHead">Slot List</label>
                    <div class="row ddlSlot">
                        <div class="col-12 col-sm-8 col-md-8 col-lg-8 divSlotList">
                            <asp:DataList ID="dtlSlot" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblFromTime" CssClass="ddlSlotBtn" runat="server" OnClick="lblFromTime_Click" Text='<%#Bind("FromTime") %>'>
                                    </asp:LinkButton>
                                    <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3 chkBokDone">
                            <div class="mb-2">
                                <label class="lblWorkouthead">Muscle Building</label><br />
                                <label class="lblWorkCat">Personal Training 1-1</label>
                            </div>
                            <label class="lblDone">Completed</label><br />
                            <label class="switch">
                                <input type="checkbox" checked>
                                <span class="slider round"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                    <label class="lblSlotHead">Workout Process</label>
                    <div class="row divProgress">
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                            <label class="lblWrkName">Muscle Building</label>
                            <progress id="PrgDay" class="progressBarFull" runat="server" max="100" value="100"></progress>
                            <label class="lblWrkName">Fat Loss</label>
                            <progress id="Progress1" class="progressBar34" runat="server" max="100" value="80"></progress>
                            <label class="lblWrkName">Strength Training</label>
                            <progress id="Progress2" class="progressBarMid" runat="server" max="100" value="50"></progress>
                            <label class="lblWrkName">Yoga</label>
                            <progress id="Progress3" class="progressBar" runat="server" max="100" value="20"></progress>
                        </div>
                        <div class="vl"></div>
                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-center">
                            <label class="lbloverAll">Over All</label><br />
                            <div id="progressbar" runat="server" role="progressbar" visible="false" style="--value: 74"></div>
                            <div class="pie animate" style="--p: 90; --c: #2f2ec5">90%</div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="divUserList" id="divUserattendance" runat="server" visible="false">
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4">
                        <div class="divuserworkOutdtl">
                            <label class="lblUserListHead">User List</label>
                            <br />
                            <asp:DataList ID="dtlUser" CssClass="dtlUser" runat="server" RepeatColumns="1">
                                <ItemTemplate>
                                    <div class="row divuserListSub">
                                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-start">
                                            <asp:LinkButton ID="lblfirstName" CssClass="lnkUser" runat="server" OnClick="lblfirstName_Click" Text='<%#Bind("firstName") %>'>
                                            </asp:LinkButton>
                                            <asp:Label ID="lbluserId" CssClass="lblSetName" runat="server" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                                            <asp:Label ID="lblcategoryId" CssClass="lblSetName" Visible="false" runat="server" Text='<%#Bind("categoryId") %>'></asp:Label>
                                        </div>
                                        <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-end">
                                            <label class="lblPresent">Present  Status</label>
                                            <br />
                                            <label class="switch">
                                                <input type="checkbox">
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                    <div id="divWorkOutDtl" class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4" runat="server" visible="false">
                        <div class="divuserworkOutdtl">
                            <div class="divWorkOut">
                                <asp:DataList ID="dtlWorkOut" runat="server" RepeatDirection="Vertical" RepeatColumns="1" OnItemDataBound="dtlWorkOutType_ItemDataBound" TabIndex="2">
                                    <ItemTemplate>
                                        <div class="dtlWorkList">
                                            <asp:Label ID="lblworkoutCatTypeName" CssClass="lblWaorkCatName" runat="server" Text='<%#Bind("workoutCatTypeName") %>'></asp:Label>
                                            <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"></asp:Label>
                                            <asp:DataList ID="dtlWorkOutList" OnItemDataBound="dtlWorkOutList_ItemDataBound" runat="server" RepeatColumns="3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblworkoutTypeName" CssClass="lblWorkOutName" runat="server" Text='<%#Bind("workoutTypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblworkoutTypeId" runat="server" Visible="false" Text='<%#Bind("workoutTypeId") %>'></asp:Label>
                                                    <asp:Label ID="lblworkoutCatTypeId" runat="server" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                                                    <asp:DataList ID="dtlWorkOutSets" runat="server" RepeatColumns="3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsetTypeName" CssClass="lblSetName" runat="server" Text='<%#Bind("setTypeName") %>'></asp:Label>
                                                            <div class="col-3 col-sm-3 col-md-3 col-lg-3 workOutSetsCheck">
                                                                <asp:CheckBox ID="chkFinished" runat="server"
                                                                    Checked='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? true:false %>'
                                                                    Enabled='<%#Eval("VideoCompletedStatus").ToString() == "Yes"? false:true %>'
                                                                    AutoPostBack="true" OnCheckedChanged="chkFinished_CheckedChanged" />
                                                            </div>
                                                            <asp:Label ID="lblworkoutCatTypeId" runat="server" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                                                            <asp:Label ID="lblworkoutTypeId" runat="server" Visible="false" Text='<%#Bind("workoutTypeId") %>'></asp:Label>
                                                            <asp:Label ID="lblday" runat="server" Visible="false" Text='<%#Bind("day") %>'></asp:Label>
                                                            <asp:Label ID="lblcsetType" runat="server" Visible="false" Text='<%#Bind("csetType") %>'></asp:Label>
                                                            <asp:Label ID="lblReps" runat="server" Visible="false" Text='<%#Bind("cnoOfReps") %>'></asp:Label>
                                                            <asp:Label ID="lblWeight" runat="server" Visible="false" Text='<%#Bind("cweight") %>'></asp:Label>
                                                            <asp:Label ID="lblbookingId" runat="server" Visible="false" Text='<%#Bind("bookingId") %>'></asp:Label>
                                                            <asp:Label ID="lbluserId" runat="server" Visible="false" Text='<%#Bind("userId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 ">
                        <div class="divuserworkOutdtl divAtt text-center">
                            <label class="lbloverAllatt">Over All Attendance</label><br />
                            <div class="wrapper">
                                <div class="circle-out">
                                    <div id="bar" class="circle"></div>
                                    <span class="text">80%</span>
                                </div>
                            </div>
                            <div class="divUserAtt">
                                <label class="lblAtt">Total Students : 100</label><br />
                                <label class="lblAtt">Present Students : 60</label><br />
                                <label class="lblAtt">Absent Students : 40</label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div id="divUserWorkOutList" runat="server" class="row ddlSlot mt-4" visible="false">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <asp:DataList ID="dtlUserList" runat="server" RepeatColumns="3" OnItemDataBound="dtlUserList_ItemDataBound">
                        <ItemTemplate>
                            <div class="dtlUserList">
                                <div class="progress-bar-container">
                                    <asp:Label ID="Label1" CssClass="lbluserName" runat="server" Text='<%#Bind("firstName") %>'></asp:Label>
                                    <div id="progressbar" runat="server" role="progressbar"></div>
                                </div>
                                <asp:DataList ID="dtlUserWorkOutCat" runat="server" RepeatColumns="1">
                                    <ItemTemplate>
                                        <div class="row divuserworkOutdtl">
                                            <div class="col-12 col-sm-10 col-md-10 col-lg-10 col-xl-10 mb-2">
                                                <asp:Label ID="lblworkOutName" CssClass="lblSetName" runat="server" Text='<%#Bind("workOutName") %>'></asp:Label>
                                                &nbsp
                                                <asp:Label ID="lblsets" CssClass="lblSetName" runat="server" Text='<%#Bind("sets") %>'></asp:Label>
                                                &nbsp
                                                <asp:Label ID="lblworkoutType" CssClass="lblSetName" runat="server" Text='<%#Bind("workoutType") %>'></asp:Label>
                                            </div>
                                            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2">
                                                <asp:Label ID="lblcompletedStatus" CssClass="lblSetName" runat="server"
                                                    Text='<%#Eval("completedStatus").ToString() == "Yes"?"✔️":"❌" %>'></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <%--Trainer Based End--%>
        <div class="row mt-3" id="divtrainerattendancedatagrid" runat="server">
            <div class="col-12 col-sm-10 col-md-10 col-lg-10 col-xl-10 mb-3">
                <label class="lblSlotHead">Trainers Attendance</label>
                <div class="PageHeaderadmin">
                    <div class="float-end">
                        <asp:LinkButton ID="lnkaddattendance" runat="server" OnClick="lnkaddattendance_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add Trainer Attendance</asp:LinkButton>
                    </div>
                </div>
                <div class="trainerreassign">
                    <asp:DataGrid ID="divtrainerattendance" runat="server" AutoGenerateColumns="false" GridLines="None">

                        <Columns>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" ItemStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                <HeaderTemplate>
                                    <p>Trainer Name</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("empName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <p>Date</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("logDate") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <p>inTime</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("inTime") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="150px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <p>outTime</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("outTime") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="180px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <p>attendance</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("attendance") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle CssClass="datagridHeadter" VerticalAlign="Middle" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>

        <div class="row mt-3" id="divtrainerreassigndatagrid" runat="server">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                <label class="lblSlotHead">Trainers Re-Assigned</label>
                <div class="PageHeaderadmin">
                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add Re-Assign</asp:LinkButton>
                    </div>
                </div>

                <div class="trainerreassign">
                    <div class="row">
                        <asp:Label ID="trainerreassignnodata" runat="server" CssClass="Nodatafound">No Data Found !!!</asp:Label>
                    </div>
                    <asp:DataGrid ID="DataList1" runat="server" AutoGenerateColumns="false" GridLines="None">

                        <Columns>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" ItemStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                <HeaderTemplate>
                                    <p>OldTrainerName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("oldTrainerName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>NewTrainerName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("newTrainerName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>CategoryName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("categoryName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="150px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>TrainingTYpeName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("trainingTYpeName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="180px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black" HeaderStyle-CssClass="text-center">
                                <HeaderTemplate>
                                    <p>Date</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("Date") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="150px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>SlotTime</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("SlotTime") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle CssClass="datagridHeadter" VerticalAlign="Middle" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>

        <%--Attendance--%>
        <div id="DivtrainerattentanceForm" runat="server" class="DisplyCard" visible="false">
            <div class="CtgryDisplyCardPostion table-responsive">
                <div id="trainerattendanceform" runat="server">
                    <div class="PageHeader">
                        <h5>Trainer <span>Attendance</span>
                            <a onclick="btnClose()" class="float-end btnclose">
                                <i class="fa-solid fa-x"></i></a>
                        </h5>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mt-4">
                            <asp:DropDownList ID="ddlTrainerattendanceList" CssClass="form-select" TabIndex="1" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RfvTrainerList" ValidationGroup="TrainerAttendance"
                                ControlToValidate="ddlTrainerattendanceList" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Trainer" InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" id="divlogdate" runat="server">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtlogDate" AutoComplete="Off" TabIndex="4" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Log Date<span class="reqiredstar">*</span></asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvlogDate" ValidationGroup="TrainerAttendance" ControlToValidate="txtlogDate" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Log Date">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" id="divintime" runat="server">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtShiftStartTime" CssClass="txtbox timePicker" TabIndex="3" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">InTime <span class="reqiredstar">*</span>
                                </asp:Label>
                            </div>
                            <asp:RequiredFieldValidator ID="RfvtxtShiftStartTime" ValidationGroup="TrainerAttendance" ControlToValidate="txtShiftStartTime" runat="server"
                                CssClass="rfvStyle" ErrorMessage="InTime">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" id="divouttime" runat="server" visible="false">
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
                <div id="divtrainerattentanceGv" runat="server">
                    <div class="row">
                        <div class="PageHeader">
                            <h4>Trainer <span>Attendance</span></h4>

                            <a onclick="btnClose()" class="float-end btnclose">
                                <i class="fa-solid fa-x"></i></a>
                        </div>
                    </div>

                    <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive" style="margin-bottom: 0px !important">
                        <asp:GridView ID="gvTrainer" Style="font-size: 0.8rem" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                            CssClass="table table-striped table-hover border">
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
                                <asp:TemplateField HeaderText="Trainer Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
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
                                <asp:TemplateField HeaderText="InTime" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
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
                                        <asp:Label ID="lblshiftId" runat="server" Text='<%#Bind("shiftId") %>'></asp:Label>
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
                                        <asp:Label ID="lblshiftEndTime" runat="server" Text='<%#Bind("shiftEndTime") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                                    <ItemTemplate>
                                        <asp:ImageButton
                                            ID="LnkEdit"
                                            runat="server"
                                            src="img/edit-icon.png" alt="image" Width="25"
                                            Text="Edit" OnClick="LnkEdit_Click" />
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <%--Attendance--%>

        <%--Re-Assign--%>
        <div id="DivtrainerreassignForm" runat="server" class="DisplyCard" visible="false">
            <div class="CtgryDisplyCardPostion table-responsive">
                <div id="trainerReassignform" runat="server" visible="false">
                    <div class="PageHeader">
                        <h5>Trainer  <span>Re-Assign</span>
                            <a onclick="btnCloseReassign()" class="float-end btnclose">
                                <i class="fa-solid fa-x"></i></a>
                        </h5>
                    </div>
                    <div class="row">

                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:DropDownList ID="ddlOldTrainerReassign"
                                CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlOldTrainer_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:DropDownList ID="ddlCategoryReassign"
                                CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:DropDownList ID="ddlTrainingTypeReassign"
                                CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTrainingType_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3" id="divSlotList" runat="server" visible="false">
                            <div>
                                <asp:Label
                                    ID="chkSlotListadmin" CssClass="adminlableheader"
                                    runat="server">Slots<span class="reqiredstar">*</span>
                                </asp:Label>

                            </div>
                            <asp:CheckBoxList ID="chkSlotList" CssClass="adminlable" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                AutoPostBack="true" OnSelectedIndexChanged="chkSlotList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                            <asp:label id="trainernoslots" runat="server" CssClass="Nodatafound"> Slots are Re-Assigned !!!</asp:label>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <asp:DropDownList ID="ddlNewTrainerReassign"
                                CssClass="form-select" runat="server" TabIndex="5">
                            </asp:DropDownList>

                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtFromDateReassign" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server"> From Date  <span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    ControlToValidate="txtFromDateReassign" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                                    ErrorMessage="Enter Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtToDateReassign" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server"> To Date  <span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    ControlToValidate="txtToDateReassign" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                                    ErrorMessage="Enter Date">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                    <div class="float-end">
                        <asp:Button CssClass="btnSubmit" ID="btnSubmitReassign" ValidationGroup="FCSlot" runat="server" Text="Submit" OnClick="btnSubmitReassign_Click" />
                        <asp:Button ID="btnCancelReassign" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancelReassign_Click" />
                    </div>
                </div>


                <div id="divtrainerreassigneGv" runat="server">

                    <div class="row">
                        <div class="PageHeader">
                            <h4>Trainer  <span>Re-Assign</span></h4>
                            <a onclick="btnCloseReassign()" class="float-end btnclose">
                                <i class="fa-solid fa-x"></i></a>

                            <div class="float-end">
                                <asp:LinkButton ID="btnAddReassign" runat="server" OnClick="btnAddReassign_Click"
                                    CssClass="btnAdd">&nbsp;   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div id="divgvreassign" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive" style="margin-bottom: 0px !important">
                        <asp:GridView ID="gvTrainerReassign" Style="font-size: 0.8rem" runat="server" DataKeyNames="oldTrainerId,newTrainerId" AutoGenerateColumns="false"
                            CssClass="table table-striped table-hover border">
                            <Columns>
                                <asp:TemplateField HeaderText="Sno">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Old Tranier Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloldTrainerId" runat="server" Text='<%#Bind("oldTrainerId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbloldTrainerName" runat="server" Text='<%#Bind("oldTrainerName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Tranier Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnewTrainerId" runat="server" Text='<%#Bind("newTrainerId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblnewTrainerName" runat="server" Text='<%#Bind("newTrainerName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TrainingType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltrainingTypeId" runat="server" Text='<%#Bind("trainingTypeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>' Visible="true"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Slot" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="true"></asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date")%>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <%--Re-Assign--%>
    <script>
        const bar = document.getElementById("bar");
        if (bar != null) {
            function setProgress(percent) {
                const p = 180 - (percent / 100) * 180;
                bar.style.transform = `rotate(-${p}deg)`;
            }
            let progress = 0;

            const interval = setInterval(() => {
                progress += 15;
                setProgress(progress);
                if (progress > 80) {
                    clearInterval(interval);
                }
            }, 1200)
        }


        function btnClose() {
            $('#<%= DivtrainerattentanceForm.ClientID %>').css("display", "none");
        }

        function btnCloseReassign() {
            $('#<%= DivtrainerreassignForm.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>


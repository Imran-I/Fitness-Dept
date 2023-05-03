<%@ Page Title="DashBoard" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>

<asp:Content ID="cndDashBoard" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Css/DashBoard/DashBoard.css" rel="stylesheet" />
    <div class="container-fluid frmcontainer">
        <div class="row">
            <div class="col-12 col-sm-2 col-md-2 col-lg-2 col-xl-2 mb-2">
                <div class="txtboxdiv">
                    <asp:TextBox ID="txtFromDate" AutoComplete="off" ClientIDMode="Static" CssClass="txtbox fromDate" runat="server" TabIndex="1" placeholder=" " />
                    <asp:Label CssClass="txtlabel" runat="server">Date<span class="reqiredstar">*</span></asp:Label>
                </div>
            </div>
        </div>
        <%--Trainer--%>

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
                                <asp:Label ID="lbltoTime" runat="server" Text='<%#Bind("toTime") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div id="divTrainerWorkout" class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3 chkBokDone" runat="server" visible="false">
                        <div class="mb-2">
                            <asp:Label ID="lblTcategoryName" runat="server" class="lblWorkouthead"></asp:Label><br />
                            <asp:Label ID="lblTtrainingTypeName" runat="server" class="lblWorkCat"></asp:Label>
                        </div>
                        <label class="lblDone">Completed</label><br />
                        <label class="switch">
                            <asp:CheckBox ID="chkWorkOutDone" OnCheckedChanged="chkWorkOutDone_CheckedChanged" AutoPostBack="true"
                                runat="server" />
                            <span class="slider round"></span>
                        </label>
                    </div>
                </div>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                <label class="lblSlotHead">Workout Process</label>
                <div class="row divProgress">
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6">
                        <div>
                            <asp:DataList ID="dtlTrainerWorkOutList" runat="server" RepeatDirection="Horizontal" RepeatColumns="1">
                                <ItemTemplate>
                                    <asp:Label ID="lblcategoryName" class="lblWrkName" runat="server" Text='<%#Bind("categoryName") %>'></asp:Label>
                                    <progress id="PrgDay" class="progressBar34" runat="server" max="100" value="100"></progress>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <%--  <label class="lblWrkName">Muscle Building</label>
                        <progress id="PrgDay" class="progressBarFull" runat="server" max="100" value="100"></progress>
                        <label class="lblWrkName">Fat Loss</label>
                        <progress id="Progress1" class="progressBar34" runat="server" max="100" value="80"></progress>
                        <label class="lblWrkName">Strength Training</label>
                        <progress id="Progress2" class="progressBarMid" runat="server" max="100" value="50"></progress>
                        <label class="lblWrkName">Yoga</label>
                        <progress id="Progress3" class="progressBar" runat="server" max="100" value="20"></progress>--%>
                    </div>
                    <div class="vl"></div>
                    <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-center">
                        <label class="lbloverAll">Over All</label><br />
                        <div class="progress-bar-container">
                            <div id="divOverallWorkOut" runat="server" role="progressbar"></div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="divUserList">
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
                                            <asp:CheckBox ID="chkPresent" AutoPostBack="true"
                                                Checked='<%#Eval("attendance").ToString() == "P"? true:false %>'
                                                runat="server" OnCheckedChanged="chkPresent_CheckedChanged" />
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
                        <div class="divOverallProgress">
                            <div class="progress3">
                                <div class="barOverflow3">
                                    <div class="bar3"></div>
                                </div>
                                <span id="spoverll" class="spOverallPresentage" runat="server"></span>&nbsp
                                <lable class="spOverallPresentage">%</lable>
                            </div>
                        </div>
                        <div class="divUserAtt">
                            <label class="lblAtt">
                                Total Students :
                                <asp:Label ID="lblOverall" runat="server"></asp:Label>
                            </label>
                            <br />
                            <label class="lblAtt">
                                Present Students :
                                <asp:Label ID="lblPresent" runat="server"></asp:Label></label><br />
                            <label class="lblAtt">
                                Absent Students :
                                <asp:Label ID="lblAbsent" runat="server"></asp:Label></label>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div id="divUserWorkOutList" runat="server" class="row ddlSlot mt-4">
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

        <%--Trainer End--%>


        <div class="row mt-3">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                <label class="lblSlotHead">Trainers Re-Assigned</label>
                <div class="trainerreassign">
                    <asp:DataGrid ID="DataList1" runat="server" AutoGenerateColumns="false" GridLines="None">

                        <Columns>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" ItemStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                <HeaderTemplate>
                                    <p>oldTrainerName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("oldTrainerName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>newTrainerName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("newTrainerName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="130px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>categoryName</p>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <p><%# Eval("categoryName") %></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="150px" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="black">
                                <HeaderTemplate>
                                    <p>trainingTYpeName</p>
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


    </div>
    <script>
        const bar = document.getElementById("bar");
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
    </script>
    <script>
        $(".progress3").each(function () {

            var $bar = $(this).find(".bar3");
            var $val = $(this).find("span");
            var perc = parseInt($val.text(), 10);

            $({ p: 0 }).animate({ p: perc }, {
                duration: 3000,
                easing: "swing",
                step: function (p) {
                    $bar.css({
                        transform: "rotate(" + (45 + (p * 1.8)) + "deg)", // 100%=180° so: ° = % * 1.8
                        // 45 is to add the needed rotation to have the green borders at the bottom
                    });
                    $val.text(p | 0);
                }
            });
        });
    </script>
</asp:Content>


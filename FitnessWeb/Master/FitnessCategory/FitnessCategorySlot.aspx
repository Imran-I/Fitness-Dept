<%@ Page Title="Category Slots" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FitnessCategorySlot.aspx.cs" Inherits="Master_FitnessCategorySlot" %>

<asp:Content ID="CtnFitnessCategorySlots" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.css"
        type="text/css">
    <script src="http://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.js"
        type="text/javascript"></script>
    <style>
        .DisplyCardPostion {
            border-width: 0px;
            position: fixed;
            width: 60%;
            height: auto;
            padding: 0px 50px 0px 50px;
            box-shadow: rgba(0, 0, 0, 0.56) 0px 10px 70px 4px;
            background-color: #ffffff;
            font-size: 40px;
            left: 50%;
            transform: translateX(-50%);
            top: 11%;
            border-radius: 25px;
            padding-top: 2rem;
            padding-bottom: 2rem;
        }

        .AddSlots {
            box-shadow: rgba(0, 0, 0, 0.31) 0px 0px 7px 4px;
            background-color: #ffffff;
        }
    </style>
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Fitness Category"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Category Slot"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Category <span>Slot</span></h5>
            </div>
            <div class="row" runat="server" visible="true">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="text-start">
                        <a class="addlblHead">Category Name :
                       <asp:Label
                           ID="lblCategoryList"
                           runat="server" Text="Fat Loss">
                       </asp:Label>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="text-start">
                        <a class="addlblHead">Training Type :
                       <asp:Label
                           ID="lblTrainingType"
                           runat="server" Text="Personal Training">
                       </asp:Label>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3" runat="server">
                    <div class="text-start">
                        <a class="addlblHead">Slot Mode :
                       <asp:Label
                           ID="lblSlotMode"
                           runat="server" Text="Direct">
                       </asp:Label>
                        </a>
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div>
                        <asp:Label
                            ID="lblWorkingDays"
                            runat="server">Working Days<span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:CheckBoxList ID="chkWorkingDays" runat="server" RepeatDirection="Vertical" AutoPostBack="true"
                        OnSelectedIndexChanged="chkWorkingDays_SelectedIndexChanged">
                        <asp:ListItem Value="1">Sunday</asp:ListItem>
                        <asp:ListItem Value="2">Monday</asp:ListItem>
                        <asp:ListItem Value="3">Tuesday</asp:ListItem>
                        <asp:ListItem Value="4">Wednesday</asp:ListItem>
                        <asp:ListItem Value="5">Thursday</asp:ListItem>
                        <asp:ListItem Value="6">Friday</asp:ListItem>
                        <asp:ListItem Value="7">Saturday</asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3" id="divSlotList" runat="server">
                    <div>
                        <asp:Label
                            ID="Label1"
                            runat="server">Slots<span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:CheckBoxList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" AutoPostBack="true"
                        OnSelectedIndexChanged="chkWorkingDays_SelectedIndexChanged">
                        <asp:ListItem Value="1">9.00 -9.30</asp:ListItem>
                        <asp:ListItem Value="2">10.00 -10.30</asp:ListItem>
                        <asp:ListItem Value="3">11.00 -11.30</asp:ListItem>
                        <asp:ListItem Value="4">12.00 -12.30</asp:ListItem>
                        <asp:ListItem Value="5">1.00 -1.30</asp:ListItem>
                        <asp:ListItem Value="6">2.00 -2.30</asp:ListItem>
                        <asp:ListItem Value="7">3.00 -3.30</asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row" id="DivDate" runat="server">

                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFrmDate" CssClass="txtbox fromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvFrmDate" ValidationGroup="FCSlot" ControlToValidate="txtFrmDate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter From Date">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-2">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtTodate" CssClass="txtbox toDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="FCSlot" ControlToValidate="txtTodate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter To Date">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlEmp" CssClass="form-select" runat="server">
                        <asp:ListItem Value="0">Employee List *</asp:ListItem>
                        <asp:ListItem>Kumar</asp:ListItem>
                        <asp:ListItem>Ajith</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvSlot" ValidationGroup="FCSlot"
                        ControlToValidate="ddlEmp" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Employee" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ValidationGroup="FCSlot" runat="server" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category  <span>Slot</span></h4>

                    <div class="float-end">
                     
                            <asp:LinkButton ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="addlblHead"
                                Style="color: black; margin-left: 10px"><i class="fa-solid fa-arrow-left"></i>  Back To Price</asp:LinkButton>
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">&nbsp;   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <table class="table table-striped table-hover" style="font-size: 0.8rem">
                    <thead>
                        <tr>
                            <th scope="col">S.No</th>
                            <th scope="col">Category</th>
                            <th scope="col">Edit</th>
                            <th scope="col">View</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td>Fat Loss</td>
                            <td>
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                                    Style="color: black">
                              <i class="fa-solid fa-pencil fafaediticon"></i> </asp:LinkButton></td>
                            <td>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"
                                    Style="color: black">
                              <i class="fa-solid fa-eye"></i> </asp:LinkButton></td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>

        <div id="ViewSlotList" runat="server" class="DisplyCard" visible="false">
            <div class="DisplyCardPostion ">
                <div class="row">
                    <div class="PageHeader">
                        <h4>Slot Details
                          <a onclick="btnClose()" class="float-end btnclose">
                              <i class="fa-solid fa-x"></i></a>
                        </h4>

                    </div>
                </div>
                <div id="divEdit" runat="server" class="table-responsive" style="overflow-x: hidden">
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4" style="margin-top: -35px" runat="server">
                            <div class="text-start">
                                <a class="addlblHead">Category Name :
                       <asp:Label ID="lblCategoryNameSlot" runat="server" Text="Fat Loss"></asp:Label>
                                </a>
                            </div>

                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 " style="margin-top: -35px" runat="server">
                            <div class="text-start">
                                <a class="addlblHead">Trainer Name :
                               <asp:Label ID="lblTrainerName" runat="server" Text="Kumar">
                               </asp:Label>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 " style="margin-top: -35px" runat="server">
                            <div class="text-start">
                                <a class="addlblHead">From Date:
                               <asp:Label ID="lblFromDate" runat="server" Text="23-11-22">
                               </asp:Label>
                                </a>
                            </div>
                        </div>
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 " style="margin-top: -35px" runat="server">
                            <div class="text-start">
                                <a class="addlblHead">To Date:
                               <asp:Label ID="lblToDate" runat="server" Text="30-11-22">
                               </asp:Label>
                                </a>
                            </div>
                        </div>

                    </div>
                    <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mt-3" id="div1" runat="server">

                        <table class="table table-striped table-hover" runat="server" style="font-size: 0.8rem">

                            <tr>
                                <th scope="col">S.No</th>
                                <th scope="col">Working Day</th>
                                <th scope="col">Slots</th>
                                <th scope="col">New Slots</th>

                            </tr>

                            <tr>
                                <td>1</td>
                                <td>Sunday</td>
                                <td>
                                    <table class="table table-striped table-hover" style="font-size: 0.8rem">

                                        <tr>
                                            <td>9.00 - 9.30</td>
                                            <td>10.00 - 10.30</td>

                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"
                                        CssClass="btngrid">
                              Add </asp:LinkButton></td>


                            </tr>
                        </table>
                        <table id="divDates" class="table AddSlots" runat="server" style="font-size: 0.8rem" visible="false">
                            <tr class="row" runat="server">
                                <td>
                                    <table>

                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" AutoPostBack="true"
                                                    OnSelectedIndexChanged="chkWorkingDays_SelectedIndexChanged">
                                                    <asp:ListItem Value="3">11.00 -11.30</asp:ListItem>
                                                    <asp:ListItem Value="4">12.00 -12.30</asp:ListItem>
                                                    <asp:ListItem Value="5">1.00 -1.30</asp:ListItem>
                                                    <asp:ListItem Value="6">2.00 -2.30</asp:ListItem>
                                                    <asp:ListItem Value="7">3.00 -3.30</asp:ListItem>
                                                    <asp:ListItem Value="7">4.00 -4.30</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" Style="border: none" runat="server" OnClick="btnCancel_Click">
                                                    <i class="fa-solid fa-check" style="color:black;font-size:25px;padding-left:35px" aria-hidden="true"></i>
                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:LinkButton ID="Button2" Style="border: none" runat="server" OnClick="btnCancel_Click">
                                                    <i class="fa-solid  fa-x" style="color:black;font-size:25px;padding-left:35px"></i>
                                                </asp:LinkButton>

                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>

                        </table>

                    </div>


                </div>
                <div id="divView" runat="server" class="table-responsive" style="overflow-x: hidden">
                    <div id="scheduler_here" class="dhx_cal_container" style='width: 100%; height: 100vh;'>
                        <div class="dhx_cal_navline">
                            <div class="dhx_cal_prev_button">&nbsp;</div>
                            <div class="dhx_cal_next_button">&nbsp;</div>
                            <div class="dhx_cal_today_button"></div>
                            <div class="dhx_cal_date"></div>
                            <div class="dhx_cal_tab" data-tab="day"></div>
                            <div class="dhx_cal_tab" data-tab="week"></div>
                            <div class="dhx_cal_tab" data-tab="month"></div>
                        </div>
                        <div class="dhx_cal_header"></div>
                        <div class="dhx_cal_data"></div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            scheduler.config.first_hour = 6;
            scheduler.config.last_hour = 19;
            scheduler.init('scheduler_here', new Date(2022, 10, 21), "week");
            scheduler.parse([
                 { id: 6, start_date: "2022-11-21 09:00", end_date: "2022-11-21 12:00", text: "Tamil lesson" },
                { id: 1, start_date: "2022-11-22 09:00", end_date: "2022-11-22 12:00", text: "English lesson" },
                { id: 2, start_date: "2022-11-23 10:00", end_date: "2022-11-23 16:00", text: "Math exam" },
                { id: 3, start_date: "2022-11-24 10:00", end_date: "2022-11-24 16:00", text: "Science lesson" },
                { id: 4, start_date: "2022-11-25 16:00", end_date: "2022-11-25 17:00", text: "English lesson" },
                { id: 5, start_date: "2022-11-26 09:00", end_date: "2022-11-26 17:00", text: "Usual event" }
            ]);
        </script>
    </div>
    <script type="text/javascript">

        function btnClose() {
            $('#<%= ViewSlotList.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>


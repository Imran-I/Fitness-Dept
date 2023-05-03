<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerTrackingUser.aspx.cs" Inherits="Master_TrainerTrackingUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ddlSlotBtn {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 1px solid #b3b3b3;
            padding: 0.4rem;
            border-radius: 1rem;
            color: black;
            background: white;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }

            .ddlSlotBtn:hover {
                position: relative;
                display: flex;
                gap: 0.3rem;
                text-align: center;
                border: 1px solid #b3b3b3;
                padding: 0.4rem;
                border-radius: 1rem;
                color: black;
                background: white;
                box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
                width: 4.2rem;
                font-size: 0.7rem;
                font-weight: 800;
                justify-content: center;
            }

        .ddlSlotBtnSelect {
            position: relative;
            display: flex;
            gap: 0.3rem;
            text-align: center;
            border: 2px solid #ffffff;
            padding: 0.4rem;
            border-radius: 1rem;
            color: white;
            background: #686565;
            box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
            width: 4.2rem;
            font-size: 0.7rem;
            font-weight: 800;
            justify-content: center;
        }

            .ddlSlotBtnSelect:hover {
                position: relative;
                display: flex;
                gap: 0.3rem;
                text-align: center;
                border: 2px solid #ffffff;
                padding: 0.4rem;
                border-radius: 1rem;
                color: white;
                background: #686565;
                box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
                width: 4.2rem;
                font-size: 0.7rem;
                font-weight: 800;
                justify-content: center;
            }

        .lblWaorkCatName {
            color: black;
            font-size: 1rem;
            font-weight: 800;
        }

        .lblWorkOutName {
            color: black;
            margin-right: 0.5rem;
            font-size: 0.8rem;
            font-weight: 700;
        }

        .lblSetName {
            color: black;
            margin-right: 0.5rem;
            font-size: 0.6rem;
            font-weight: 500;
        }

        .dtlUserList {
            box-shadow: rgba(60, 64, 67, 0.3) 0px 1px 2px 0px, rgba(60, 64, 67, 0.15) 0px 2px 6px 2px;
            margin-left: 1rem;
            border-radius: 0.5rem;
            padding: 1rem;
        }

        .lbluserName {
            font-size: 1rem;
            color: black;
            font-weight: 800;
        }

        .progress-bar-container {
            position: relative;
            text-align: center;
        }

        @keyframes growProgressBar {
            0%, 33% {
                --pgPercentage: 0;
            }

            100% {
                --pgPercentage: var(--value);
            }
        }

        @property --pgPercentage {
            syntax: '<number>';
            inherits: false;
            initial-value: 0;
        }

        div[role="progressbar"] {
            --size: 5rem;
            --fg: #000;
            --bg: #000;
            --pgPercentage: var(--value);
            animation: growProgressBar 3s 1 forwards;
            width: var(--size);
            height: var(--size);
            border-radius: 50%;
            display: grid;
            place-items: center;
            background: radial-gradient(closest-side, white 80%, transparent 0 99.9%, white 0), conic-gradient(#6a6a6a calc(var(--pgPercentage) * 1%), #cdcdcd 0);
            font-family: Helvetica, Arial, sans-serif;
            font-size: calc(var(--size) / 5);
            color: var(--fg);
            position: relative;
            left: 50%;
            transform: translateX(-50%);
        }

            div[role="progressbar"]::before {
                counter-reset: percentage var(--value);
                content: counter(percentage) '%';
            }

        .divuserworkOutdtl {
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
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Trainer Tracking User"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="true">
            <div class="PageHeader">
                <h5>Trainer <span>Tracking User</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtDate" AutoComplete="off" ClientIDMode="Static" CssClass="txtbox fromDate" runat="server" TabIndex="8" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Date<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvDate" ClientIDMode="Static" ValidationGroup="UserEnroll" ControlToValidate="txtDate" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Date Of Birth">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div id="divSlot" class="row" runat="server">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                    <label class="lblSlotHead">Slot List</label>
                    <div class="ddlSlot">
                        <asp:DataList ID="dtlSlot" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
                            <itemtemplate>
                                <asp:LinkButton ID="lblFromTime" CssClass="ddlSlotBtn" runat="server" OnClick="lblFromTime_Click" Text='<%#Bind("FromTime") %>'>
                                </asp:LinkButton>
                                <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="false"></asp:Label>
                            </itemtemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <asp:DropDownList ID="ddlUserList" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_SelectedIndexChanged" TabIndex="1" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvTrainerList" ValidationGroup="FCPrice"
                        ControlToValidate="ddlUserList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select User" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3" id="divWorkOutDtl" runat="server" visible="false">
                    <asp:DataList ID="dtlWorkOut" runat="server" OnItemDataBound="dtlWorkOutType_ItemDataBound" TabIndex="2">
                        <itemtemplate>
                            <asp:Label ID="lblworkoutCatTypeName" CssClass="lblWaorkCatName" runat="server" Text='<%#Bind("workoutCatTypeName") %>'></asp:Label>
                            <asp:Label ID="lblworkoutCatTypeId" runat="server" Text='<%#Bind("workoutCatTypeId") %>' Visible="false"></asp:Label>
                            <asp:DataList ID="dtlWorkOutList" OnItemDataBound="dtlWorkOutList_ItemDataBound" runat="server" RepeatColumns="3">
                                <itemtemplate>
                                    <asp:Label ID="lblworkoutTypeName" CssClass="lblWorkOutName" runat="server" Text='<%#Bind("workoutTypeName") %>'></asp:Label>
                                    <asp:Label ID="lblworkoutTypeId" runat="server" Visible="false" Text='<%#Bind("workoutTypeId") %>'></asp:Label>
                                    <asp:Label ID="lblworkoutCatTypeId" runat="server" Visible="false" Text='<%#Bind("workoutCatTypeId") %>'></asp:Label>
                                    <asp:DataList ID="dtlWorkOutSets" runat="server" RepeatColumns="3">
                                        <itemtemplate>
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
                                        </itemtemplate>
                                    </asp:DataList>
                                </itemtemplate>
                            </asp:DataList>

                        </itemtemplate>
                    </asp:DataList>
                </div>
            </div>

            <div id="divUserWorkOutList" runat="server" class="row" visible="false">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12">
                    <asp:DataList ID="dtlUserList" runat="server" RepeatColumns="3" OnItemDataBound="dtlUserList_ItemDataBound">
                        <itemtemplate>
                            <div class="dtlUserList">
                                <div class="progress-bar-container">
                                    <asp:Label ID="Label1" CssClass="lbluserName" runat="server" Text='<%#Bind("firstName") %>'></asp:Label>
                                    <div id="progressbar" runat="server" role="progressbar"></div>
                                </div>
                                <asp:DataList ID="dtlUserWorkOutCat" runat="server" RepeatColumns="1">
                                    <itemtemplate>
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
                                    </itemtemplate>
                                </asp:DataList>
                            </div>
                        </itemtemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


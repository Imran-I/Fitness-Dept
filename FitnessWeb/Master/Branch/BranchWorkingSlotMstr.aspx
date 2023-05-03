<%@ Page Title="Working Slot" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true"
    CodeFile="BranchWorkingSlotMstr.aspx.cs" Inherits="Master_WorkingSlotMstr" %>

<asp:Content ID="CtWorkingSlot" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="http://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.css"
        type="text/css">
    <script src="http://cdn.dhtmlx.com/scheduler/edge/dhtmlxscheduler.js"
        type="text/javascript"></script>
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

        .AddSlots {
            box-shadow: rgba(0, 0, 0, 0.31) 0px 0px 7px 4px;
            background-color: #ffffff;
            border-radius:0.6rem;
        }

        .AddSlotstime{
            padding:0.5rem;
        }

        .gvsubHead {
            margin-top: 1rem;
            margin-left: 1rem;
            font-size: 0.8rem !important;
        }

        .gvsub {
            background-color: #e9e9e9;
            padding: 0.4rem;
            border-radius: 1rem;
        }
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Branch Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Branch Schedule"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Branch <span>Schedule</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div>
                            <asp:Label
                                ID="lblWorkingDays"
                                runat="server">Working Days<span class="reqiredstar">*</span>
                            </asp:Label>
                        </div>
                        <asp:CheckBoxList ID="chkWorkingDays" TabIndex="1" runat="server" RepeatDirection="Vertical">
                        </asp:CheckBoxList>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtFromTime" TabIndex="2"  runat="server" CssClass="txtbox timePicker" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">From Time<span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="rfdFromTime" ValidationGroup="WorkingSlotMstr"
                                ControlToValidate="txtFromTime" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter From Time">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtToTime" TabIndex="3"  runat="server" CssClass="txtbox timePicker" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">To Time<span class="reqiredstar">*</span></asp:Label>
                            <asp:RequiredFieldValidator ID="rfdToTime" ValidationGroup="WorkingSlotMstr"
                                ControlToValidate="txtToTime" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter To Time">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="float-end">
                <asp:Button CssClass="btnSubmit" TabIndex="4"  ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="WorkingSlotMstr" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" TabIndex="5"  CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Branch <span>Schedule</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server"   OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive"
                style="overflow-x: hidden">

                <asp:GridView ID="gvWorkingDay" runat="server" DataKeyNames="workingDayId" AutoGenerateColumns="false" 
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Working Day" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblworkingDayId" runat="server" Text='<%#Bind("workingDayId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblworkingDay" runat="server" Text='<%#Bind("workingDay") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>' Visible="false"></asp:Label>
                                <asp:DataList ID="dtlWorkingSlots" runat="server" RepeatColumns="2" BorderStyle="None"
                                    CssClass="AddSlots" RepeatDirection="Vertical" Width="50%" Visible="false">
                                    <HeaderTemplate>
                                         <asp:Label ID="lblslotId"  runat="server" Text="Slots"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblfromTime" CssClass="AddSlotstime" runat="server" Text='<%#Eval("fromTime").ToString().Replace(":00","")+"-"+Eval("toTime").ToString().Replace(":00","") %>' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Add" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkAdd" runat="server" Text="Add" OnClick="LnkAdd_Click" CssClass="GridAddBtn">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Slots" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkView" runat="server" OnClick="LnkView_Click">
                                    <i class="fa fa-eye" style="color:black"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDelete" runat="server" OnClick="LnkDelete_Click">
                                     <i class="fa fa-trash" style="color:black"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </div>
    <div id="AddSlots" runat="server" class="DisplyCard" visible="false">
        <div class="DisplyCardPostion table-responsive">

            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromTimePop" TabIndex="1"   runat="server" CssClass="txtbox timePicker" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Time<span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="workingSlotPop"
                            ControlToValidate="txtFromTimePop" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Time">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtToTimePop" TabIndex="2"  runat="server" CssClass="txtbox timePicker" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Time<span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="workingSlotPop"
                            ControlToValidate="txtToTimePop" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter To Time">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>


            </div>
            <div class="text-end">
                <asp:Button ID="btnSubSubmit" TabIndex="4"  CssClass="btnSubmit" ValidationGroup="workingSlotPop" OnClick="btnSubSubmit_Click" runat="server" Text="Submit" />
                <asp:Button ID="btnSubCancel" TabIndex="5"  CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>

        </div>
    </div>
    <script type="text/javascript">

        function btnClose() {
            $('#<%= AddSlots.ClientID %>').css("display", "none");
        }

    </script>
   
</asp:Content>


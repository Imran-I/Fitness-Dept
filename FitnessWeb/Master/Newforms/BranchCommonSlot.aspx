<%@ Page Title="Working Slot" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true"
    CodeFile="BranchCommonSlot.aspx.cs" Inherits="Master_WorkingSlotMstr" %>

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
            border-radius: 0.6rem;
        }

        .AddSlotstime {
            padding: 0.5rem;
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
            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Branch Slot"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Branch <span>Slot</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    &nbsp;&nbsp;   
                      <h6 id="Branchworkinghours" runat="server"></h6>

                </div>
                <div class="row">

                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtFromTime" TabIndex="2" runat="server" CssClass="txtbox timePicker" placeholder=" " Visible="false" />

                        </div>
                        <asp:DropDownList ID="ddlduration" TabIndex="9" runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Select Slot In Minutes *</asp:ListItem>
                            <asp:ListItem Value="00:30:00">30 Minutes</asp:ListItem>
                            <asp:ListItem Value="01:00:00">60 Minutes</asp:ListItem>
                            <asp:ListItem Value="02:00:00">120 Minutes</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                  <%--  <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtgymstrength" AutoComplete="off" TabIndex="8" CssClass="txtbox" onkeypress="return isNumber(event);"
                                MaxLength="16" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" runat="server">Gym Strength Per Slot</asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                ControlToValidate="txtgymstrength" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Enter Gym Strength Per Slot">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                </div>
            </div>

            <div class="float-end">
                <asp:Button CssClass="btnSubmit" TabIndex="4" ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="WorkingSlotMstr" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" TabIndex="5" CssClass="btnCancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h5>Branch <span>Slot</span></h5>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

               <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvSlots" runat="server" DataKeyNames="slotId,activeStatus"  AutoGenerateColumns="false" 
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="slotId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>'></asp:Label>
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
                          <asp:TemplateField HeaderText="From Time" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblslotFromTime" runat="server" Text='<%#Bind("slotFromTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Time" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblslotToTime" runat="server" Text='<%#Bind("slotToTime") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="slotDuration" Visible="false"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblslotDuration" runat="server" Text='<%#Bind("slotDuration") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="Strength PerSlot"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblstrengthPerSlot" runat="server" Text='<%#Bind("strengthPerSlot") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                     CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    OnClick="lnkActiveOrInactive_Click1"
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>'> </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div> 

</asp:Content>


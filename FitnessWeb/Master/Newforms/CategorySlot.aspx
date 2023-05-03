<%@ Page Title="Category Slots" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="CategorySlot.aspx.cs" Inherits="Master_FitnessCategorySlot" %>

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
      

            <div class="row"> 

                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlTrainer" CssClass="form-select" runat="server" TabIndex="1"
                         OnSelectedIndexChanged="ddlTrainer_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvSlot" ValidationGroup="FCSlot"
                        ControlToValidate="ddlTrainer" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Trainer" InitialValue="0">
                    </asp:RequiredFieldValidator>
                </div>
                 <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddlSpecialist" TabIndex="2" 
                            runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                        </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="FCSlot"
                        ControlToValidate="ddlSpecialist" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Specialist" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    </div>
                 <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:DropDownList ID="ddltrainingType" TabIndex="3" 
                            runat="server" CssClass="form-select" RepeatDirection="Horizontal">
                        </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="FCSlot"
                        ControlToValidate="ddltrainingType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Training Type" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    </div>
                  <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtgymStrength" runat="server" AutoComplete="off" TabIndex="4"
                            CssClass="txtbox" placeholder=" " MaxLength="7"  />
                        <asp:Label CssClass="txtlabel" runat="server">Gym Strength<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="FCSlot"
                        ControlToValidate="txtgymStrength" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Gym Strength">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3" id="divSlotList" runat="server">
                    <div>
                        <asp:Label
                            ID="Label1"
                            runat="server">Slots<span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:CheckBoxList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" TabIndex="5" >
                    </asp:CheckBoxList>
                </div>
            </div>
         
            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" ValidationGroup="FCSlot" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category  <span>Slot</span></h4>

                    <div class="float-end">
                     
                          
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">&nbsp;   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                  <asp:GridView ID="gvWorkingDay" runat="server" DataKeyNames="trainerId" AutoGenerateColumns="false" 
                    CssClass="table table-striped table-hover border display gvFilter" OnRowDataBound="gvWorkingDay_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tranier Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltrainerId" runat="server" Text='<%#Bind("trainerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainerName" runat="server" Text='<%#Bind("trainerName") %>' ></asp:Label>                            
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
                        <asp:TemplateField HeaderText="Gym Strength" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                 <asp:Label ID="lblgymStrength" runat="server" Text='<%#Bind("gymStrength") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>


                        <asp:TemplateField  Visible="false" >
                            <ItemTemplate>
                                <asp:DataList ID="dtlSlotDetails" runat="server" >
                                    <ItemTemplate>
                                         <asp:Label ID="lbltrainerId" runat="server" Text='<%#Bind("trainerId") %>' Visible="false"></asp:Label>
                                         <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbltrainingTypeIds" runat="server" Text='<%#Bind("trainingTypeId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblcategorySlotId" runat="server" Text='<%#Bind("categorySlotId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblactiveStatus" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit" OnClick="LnkEdit_Click"/>
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>
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
   
</asp:Content>

